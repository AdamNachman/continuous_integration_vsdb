-- Copyright (c) 2008 - 2010 All Right Reserved Anton Koekemoer
-- Licensed under the Apache License, Version 2.0 (the "License");
-- you may not use this file except in compliance with the License.
-- You may obtain a copy of the License at
--
-- http:www.apache.org/licenses/LICENSE-2.0
--
-- Unless required by applicable law or agreed to in writing, software
-- distributed under the License is distributed on an "AS IS" BASIS,
-- WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
-- See the License for the specific language governing permissions and
-- limitations under the License.
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spGenerateMergeData]') AND type in (N'P', N'PC'))
	BEGIN
		DROP PROCEDURE [dbo].[spGenerateMergeData]
	END
GO
-- ========================================================================
--
--	Example 1:
--  ---------
--
--	Description:
--		Export all data from table_1 and generate safe insert statements (no primary key violations).
--
--		Statements generated are in the form
--
--		IF NOT EXISTS (SELECT * FROM [dbo].[Table_1] WHERE ....
--	   	INSERT INTO [dbo].[Table_1](...) VALUES(...)
--
--  Statement:
--  	EXEC spGenerateMergeData @tablename='table_1'
--
--
--	Example 2:
--  ---------
--
--	Description:
--		Export all data from table_1 and generate merge statements .
--
--		Statements generated are in the form
--
--		UPDATE [dbo].[Table_1] SET ... WHERE ...
--			IF @@ROWCOUNT = 0 INSERT INTO [dbo].[Table_1](...) VALUES(...)
--
--  Statement:
--  	EXEC spGenerateMergeData @tablename='table_1', @merge=1
--
--	Example 3:
--  ---------
--
--	Description:
--		Export all data from table_1 where the field Id is greater that 100.
--
--		Statements generated are in the form
--
--		IF NOT EXISTS (SELECT * FROM [dbo].[Table_1] WHERE ....
--	   	INSERT INTO [dbo].[Table_1](...) VALUES(...)
--
--  Statement:
--  	EXEC spGenerateMergeData @tablename='table_1', @from = 'from table_1 where id > 100'
CREATE PROCEDURE [dbo].[spGenerateMergeData]
(
	 @tablename				varchar(1024),
	 @owner					varchar(1024) = NULL,
	 @into					VARCHAR(1024) = NULL,
	 @include_columns		VARCHAR(1024) = NULL,
	 @exclude_columns		VARCHAR(1024) = NULL,
	 @from					varchar(1024) = NULL,
	 @include_timestamp		bit = 0,
	 @merge					bit = 0,
	 @debug					bit = 0,
	 @disable_constraints	bit = 0,		-- When 1, disables foreign key constraints and enables them after the INSERT statements
	 @updateonly			bit = 0
) AS
BEGIN
		SET NOCOUNT ON -- We don't care about the rows affected
		----------------------------------------------------------------------------------------------------------
		--	Parameter Checking
		----------------------------------------------------------------------------------------------------------
		-- Check the owner,  try to get the owner if not specified
		IF @owner IS NULL
			BEGIN
				SET @owner = PARSENAME(@tablename, 2)
				IF  @owner IS NULL
					BEGIN
						SELECT @owner = TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @tablename
					END
				ELSE
					SET @tablename = PARSENAME(@tablename, 1)
			END
		ELSE IF PARSENAME(@tablename, 2) IS NOT NULL
			BEGIN
				RAISERROR('Do not specify both the @owner parameter and the schema', 16, 1);
				--RETURN -1;
		END

		IF (@into IS NULL)
			BEGIN
				SET @into = QUOTENAME(@owner) + '.' + QUOTENAME(@tablename)
			END

		----------------------------------------------------------------------------------------------------------
		--	Load columns
		----------------------------------------------------------------------------------------------------------
		-- Declare a temporary table that will hold a list of columns in
		-- the table and their types. This query also builds fragments that will
		-- be used in the select statement to get the literal value of columns
		DECLARE @columns TABLE(
			ORDINAL int,
			COLUMN_NAME VARCHAR(1024),
			DATE_TYPE VARCHAR(1024),
			SEL VARCHAR(1024),
			INCL BIT,
			IsIdentity BIT,
			PK int
		);

		INSERT INTO @columns
		SELECT distinct
			cols.ORDINAL_POSITION AS ORDINAL,
			QUOTENAME(cols.COLUMN_NAME) AS COLUMN_NAME,
			cols.DATA_TYPE,
			(
				CASE
					WHEN cols.DATA_TYPE  IN ('char','varchar','nchar','nvarchar')  THEN
						'COALESCE('''''''' + REPLACE(RTRIM(' + QUOTENAME(cols.COLUMN_NAME) + '),'''''''','''''''''''')+'''''''',''NULL'')'
					WHEN cols.DATA_TYPE  IN ('datetime','smalldatetime') THEN
						'COALESCE('''''''' + RTRIM(CONVERT(char,' + QUOTENAME(cols.COLUMN_NAME) + ',109))+'''''''',''NULL'')'
					WHEN cols.DATA_TYPE  IN ('uniqueidentifier') THEN
						'COALESCE('''''''' + REPLACE(CONVERT(char(255),RTRIM(' + QUOTENAME(cols.COLUMN_NAME) + ')),'''''''','''''''''''')+'''''''',''NULL'')'
					WHEN cols.DATA_TYPE  IN ('text','ntext')  THEN
						'COALESCE('''''''' + REPLACE(CONVERT(char(8000),' + QUOTENAME(cols.COLUMN_NAME) + '),'''''''','''''''''''')+'''''''',''NULL'')'
					WHEN cols.DATA_TYPE  IN ('binary','varbinary')   THEN
						--'COALESCE(RTRIM(CONVERT(char,' + 'CONVERT(int,' + cols.COLUMN_NAME + '))),''NULL'')'
						'COALESCE(((' + '(master.dbo.fn_hexadecimal(' + QUOTENAME(cols.COLUMN_NAME) + ')))),''NULL'')'
					WHEN cols.DATA_TYPE  IN ('timestamp','rowversion')   THEN
						CASE
							WHEN @include_timestamp = 0
								THEN
									'''DEFAULT'''
								ELSE
									'COALESCE(RTRIM(CONVERT(char,' + 'CONVERT(int,' + QUOTENAME(cols.COLUMN_NAME) + '))),''NULL'')'
						END
					WHEN cols.DATA_TYPE  IN ('float','real','money','smallmoney')  THEN
						'COALESCE(LTRIM(RTRIM(' + 'CONVERT(char, ' +  QUOTENAME(cols.COLUMN_NAME)  + ',2)' + ')),''NULL'')'
					WHEN cols.DATA_TYPE  IN ('image') THEN
						NULL
					ELSE
						'COALESCE(LTRIM(RTRIM(' + 'CONVERT(char, ' +  QUOTENAME(cols.COLUMN_NAME)  + ')' + ')),''NULL'')'
				END
			),
			(
				CASE
					WHEN @include_columns IS NOT NULL AND CHARINDEX(QUOTENAME(cols.COLUMN_NAME),@include_columns)<> 0 THEN
						1
					WHEN @exclude_columns IS NOT NULL AND CHARINDEX(QUOTENAME(cols.COLUMN_NAME),@exclude_columns)<> 0 THEN
						0
	                ELSE
						1
				END
			) AS INCL,
			COLUMNPROPERTY(OBJECT_ID(QUOTENAME(@owner) + '.' + QUOTENAME(@tablename)),cols.COLUMN_NAME,'IsIdentity') AS IsIdentity,
			(
				SELECT 
					kcu.ORDINAL_POSITION 
				FROM 
					INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tc 
				LEFT OUTER JOIN
					INFORMATION_SCHEMA.KEY_COLUMN_USAGE as kcu on
					kcu.CONSTRAINT_SCHEMA = tc.CONSTRAINT_SCHEMA
					AND kcu.CONSTRAINT_NAME = tc.CONSTRAINT_NAME
					AND kcu.TABLE_SCHEMA = tc.TABLE_SCHEMA
					AND kcu.TABLE_NAME = tc.TABLE_NAME
					AND kcu.COLUMN_NAME = cols.COLUMN_NAME
				WHERE
					tc.CONSTRAINT_TYPE in ( 'PRIMARY KEY' ) AND
					tc.CONSTRAINT_SCHEMA = cols.TABLE_SCHEMA AND
					tc.TABLE_NAME = cols.TABLE_NAME
			) PK
		FROM
			INFORMATION_SCHEMA.COLUMNS cols
		WHERE
			cols.table_schema = @owner AND
			cols.table_name = @tablename
		order by
			1

	-- First we generate an update statement
	-- We get the primary keys for the table, and then generate the update for each record
	-- in the table
	-- Note: if your table does not have a pk, a merge cannot occur. Also, you should probably revisit your design ...
	DECLARE @where NVARCHAR(MAX)
	DECLARE @update NVARCHAR(MAX)
	DECLARE @insert NVARCHAR(MAX)
	DECLARE @values NVARCHAR(MAX)
	DECLARE @order NVARCHAR(MAX)

	DECLARE curPrimary CURSOR LOCAL FOR SELECT COLUMN_NAME, SEL FROM @columns WHERE PK IS NOT NULL AND INCL <> 0
	DECLARE @columnName NVARCHAR(MAX)
	DECLARE @sel NVARCHAR(MAX)
	OPEN curPrimary
	FETCH NEXT FROM curPrimary INTO @columnName, @sel
	WHILE @@FETCH_STATUS = 0
		BEGIN
			IF @where IS NULL SET @where = @ColumnName + ' = '' + ' + @sel
			ELSE SET @where = @where + ' + '' AND ' + @ColumnName + ' = '' + ' + @sel
			IF @order IS NULL SET @order = 'ORDER BY ' + @ColumnName
			ELSE SET @order = @order + ', ' + @ColumnName
			FETCH NEXT FROM curPrimary INTO @columnName, @sel
		END
	CLOSE curPrimary
	DEALLOCATE  curPrimary

	DECLARE curColumns CURSOR LOCAL FOR SELECT COLUMN_NAME, SEL FROM @columns WHERE PK IS NULL AND SEL IS NOT NULL AND INCL <> 0
	OPEN curColumns
	FETCH NEXT FROM curColumns INTO @columnName, @sel
	WHILE @@FETCH_STATUS = 0
		BEGIN
			IF @update IS NULL SET @update = 'UPDATE ' + @into + ' SET ' + @ColumnName + ' = '' + ' + @sel
			ELSE SET @update = @update + ' + '' ,' + @ColumnName + ' = '' + ' + @sel
			FETCH NEXT FROM curColumns INTO @columnName, @sel
		END
	CLOSE curColumns
	DEALLOCATE  curColumns

	DECLARE curInsert CURSOR LOCAL FOR SELECT COLUMN_NAME, SEL FROM @columns WHERE SEL IS NOT NULL AND INCL <> 0
	OPEN curInsert
	FETCH NEXT FROM curInsert INTO @columnName, @sel
	WHILE @@FETCH_STATUS = 0
		BEGIN
			IF @insert IS NULL
				BEGIN
					SET @insert = 'INSERT INTO ' + @into + '(' + @ColumnName
					SET @values = 'VALUES('' + ' + @sel
				END
			ELSE
				BEGIN
					SET @insert = @insert + ', ' + @ColumnName
					SET @values = @values + ' + '', '' + ' + @sel
				END

			FETCH NEXT FROM curInsert INTO @columnName, @sel
		END
	CLOSE curInsert
	DEALLOCATE  curInsert

	DECLARE @statements TABLE
	(
		ROW		INT,
		STMT	INT,
		[SQL]	NVARCHAR(MAX),
		PRIMARY KEY (ROW, STMT)
	);

	-- Next the Merge of data
	INSERT INTO @statements(ROW, STMT, SQL) VALUES(0, 1, 'PRINT ''Merging data into ' + @into + '''');
	INSERT INTO @statements(ROW, STMT, SQL) VALUES(0, 2, '');

	IF EXISTS (SELECT * FROM @columns where IsIdentity <> 0)
		BEGIN
			-- Over here we should add the Identity insert enable
			INSERT INTO @statements(ROW, STMT, SQL) VALUES(0, 3, 'PRINT ''Enable identity insert''');
			INSERT INTO @statements(ROW, STMT, SQL) VALUES(0, 4, 'SET IDENTITY_INSERT ' + @into + ' ON');
			INSERT INTO @statements(ROW, STMT, SQL) VALUES(0, 5, 'GO');
		END

	IF @disable_constraints <> 0
		BEGIN
			-- Over here we should add the disable constraints
			INSERT INTO @statements(ROW, STMT, SQL) VALUES(0, 6, 'PRINT ''Disable constraint checking''');
			INSERT INTO @statements(ROW, STMT, SQL) VALUES(0, 7, 'ALTER TABLE ' + @into + ' NOCHECK CONSTRAINT ALL');
			INSERT INTO @statements(ROW, STMT, SQL) VALUES(0, 8, 'GO');
		END

	INSERT INTO @statements(ROW, STMT, SQL) VALUES(0, 9, '');
	INSERT INTO @statements(ROW, STMT, SQL) VALUES(0, 10, 'PRINT ''Merge data...''');
	INSERT INTO @statements(ROW, STMT, SQL) VALUES(0, 11, '');



	DECLARE @sql VARCHAR(max)
	IF @merge = 1
		BEGIN
			SET @sql = 'SELECT  ROW_NUMBER() OVER(' + @order + '), 1, ''' + @update + ' + '' WHERE ' + @where + ' ' + COALESCE(@from, 'FROM ' + QUOTENAME(@owner) + '.' + QUOTENAME(@tablename ))

			IF @debug = 1 PRINT @sql
			INSERT INTO @statements exec(@sql)
			
			IF @updateonly = 0
				BEGIN
					SET @sql = 'SELECT ROW_NUMBER() OVER(' + @order + '), 2, ''   IF @@ROWCOUNT = 0 ' + @insert + ') ' + @values + '+ '')'' ' + COALESCE(@from, 'FROM ' + QUOTENAME(@owner) + '.' + QUOTENAME(@tablename))

					IF @debug = 1 PRINT @sql
					INSERT INTO @statements exec(@sql)
				END
		END
	ELSE
		BEGIN
			SET @sql = 'SELECT ROW_NUMBER() OVER(' + @order + '), 1, ''IF NOT EXISTS (SELECT * FROM ' + @into + ' WHERE ' + @where + ' + '')'' ' + COALESCE(@from, 'FROM ' + QUOTENAME(@owner) + '.' + QUOTENAME(@tablename))
			IF @debug = 1 PRINT @sql
			INSERT INTO @statements exec(@sql)

			SET @sql = 'SELECT ROW_NUMBER() OVER(' + @order + '), 2, ''   ' + @insert + ') ' + @values + '+ '')'' ' + COALESCE(@from, 'FROM ' + QUOTENAME(@owner) + '.' + QUOTENAME(@tablename))
			IF @debug = 1 PRINT @sql
			INSERT INTO @statements exec(@sql)

		END


		INSERT INTO @statements(ROW, STMT, SQL) VALUES(999999999, 0, 'GO');
		INSERT INTO @statements(ROW, STMT, SQL) VALUES(999999999, 1, '');
		IF @disable_constraints <> 0
			BEGIN
				-- Over here we should add the enable constraints
				INSERT INTO @statements(ROW, STMT, SQL) VALUES(999999999, 2, 'PRINT ''Enable constraint checking''');
				INSERT INTO @statements(ROW, STMT, SQL) VALUES(999999999, 3, 'ALTER TABLE ' + @into + ' CHECK CONSTRAINT ALL');
				INSERT INTO @statements(ROW, STMT, SQL) VALUES(999999999, 4, 'GO');
			END

		IF EXISTS (SELECT * FROM @columns where IsIdentity <> 0)
			BEGIN
				-- Over here we should add the Identity insert disable
				INSERT INTO @statements(ROW, STMT, SQL) VALUES(999999999, 5, 'PRINT ''Disable identity insert''');
				INSERT INTO @statements(ROW, STMT, SQL) VALUES(999999999, 6, 'SET IDENTITY_INSERT ' + @into + ' OFF');
				INSERT INTO @statements(ROW, STMT, SQL) VALUES(999999999, 7, 'GO');
			END

		-- Next the Merge of data
		INSERT INTO @statements(ROW, STMT, SQL) VALUES(999999999, 8, 'PRINT ''Done''');

		-- Now we can output the data
		-- We set the as because we want the column name to be outputted as a comment
		-- if the script is executed with variable width lines set, the column name is
		-- underlined by one character, and that then causes a problem. We synthetically
		-- create a column name that is wider than 1 chracter as a comment,  so the script
		-- works
		SELECT [SQL] AS '-- Version 1.0                                                   ' FROM @statements ORDER BY ROW, STMT

END