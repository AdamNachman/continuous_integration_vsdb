SET NOCOUNT ON

EXEC sp_addmessage @msgnum = 100000, @severity =  1, @msgtext = 'Number of objects == |1|', @lang = 'us_english', @replace = 'REPLACE'
EXEC sp_addmessage @msgnum = 100010, @severity =  1, @msgtext = 'Processed object |%d| of 1', @lang = 'us_english', @replace = 'REPLACE'
GO
RAISERROR (100000, 1, 1)
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS(SELECT * FROM sys.schemas WHERE name = N'MySchema')
BEGIN
EXEC sp_executesql N'CREATE SCHEMA [MySchema]
    AUTHORIZATION [dbo];'
END
GO
RAISERROR (100010, 1, 1, 1)
GO
