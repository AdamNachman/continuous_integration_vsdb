SET NOCOUNT ON

EXEC sp_addmessage @msgnum = 100000, @severity =  1, @msgtext = 'Number of objects == |2|', @lang = 'us_english', @replace = 'REPLACE'
EXEC sp_addmessage @msgnum = 100010, @severity =  1, @msgtext = 'Processed object |%d| of 2', @lang = 'us_english', @replace = 'REPLACE'
GO
RAISERROR (100000, 1, 1)
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS(SELECT * FROM sys.database_principals WHERE name = N'MyDatabaseRole' AND type = 'R')
BEGIN
CREATE ROLE [MyDatabaseRole]
    AUTHORIZATION [dbo];
END
GO
RAISERROR (100010, 1, 1, 1)
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS(SELECT * FROM sys.database_principals WHERE name = N'MyOtherDatabaseRole' AND type = 'R')
BEGIN
CREATE ROLE [MyOtherDatabaseRole]
    AUTHORIZATION [dbo];
END
GO
RAISERROR (100010, 1, 1, 2)
GO
