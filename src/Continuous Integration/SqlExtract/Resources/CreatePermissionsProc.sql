-- Copyright (c) 2009 - 2010 All Right Reserved Adam Nachman
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
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spGeneratePermissions]') AND type in (N'P', N'PC'))
	BEGIN
		DROP PROCEDURE [dbo].[spGeneratePermissions]
	END
GO
CREATE PROCEDURE [dbo].[spGeneratePermissions]
(
	@Principal		NVARCHAR(200),
	@Permissions    NVARCHAR(1000),
	@ObjectTypes    VARCHAR(200),
	@SchemaName     NVARCHAR(200) = NULL
) AS

	SET NOCOUNT ON ;

	DECLARE @SQL NVARCHAR(max)
	DECLARE @Params NVARCHAR(125)
	SELECT @Params = N'@Principal NVARCHAR(200),@Permissions NVARCHAR(1000),@ObjectTypes VARCHAR(200),@SchemaName NVARCHAR(200)'

	SELECT @SQL = N'SELECT ' + CHAR(13) + 
					N'''GRANT '' + @Permissions + '' ON ['' + s.[Name] + ''].['' + o.[Name]+ ''] TO ['' + @Principal + '']'' + Char(13)+Char(10)+''GO'' ' + CHAR(13) +
					N'FROM' + CHAR(13) +
						N'dbo.sysobjects o' + CHAR(13) +
						N'INNER JOIN sys.schemas s ON o.uid = s.schema_id' + CHAR(13) +
					N'WHERE' + CHAR(13) +
					N'o.Type IN ('+ @ObjectTypes +') AND s.[Name] <> ''sys'' AND o.[Name] <> ''spGeneratePermissions''' + CHAR(13) +
					N'AND (s.[Name] = @SchemaName OR ISNULL(@SchemaName,'''') = '''')'
					
	EXEC sp_executeSQL @SQL,@Params,@Principal ,@Permissions ,@ObjectTypes, @SchemaName
GO