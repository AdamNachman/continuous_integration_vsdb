SET NOCOUNT ON

EXEC sp_addmessage @msgnum = 100000, @severity =  1, @msgtext = 'Number of objects == |1|', @lang = 'us_english', @replace = 'REPLACE'
EXEC sp_addmessage @msgnum = 100010, @severity =  1, @msgtext = 'Processed object |%d| of 1', @lang = 'us_english', @replace = 'REPLACE'
GO
RAISERROR (100000, 1, 1)
GO

IF NOT EXISTS (SELECT * FROM sys.filegroups WHERE [name]='MyFilegroup')
BEGIN
/*
Do not change the database name.
It will be properly coded for build and deployment
This is using sqlcmd variable substitution
*/
ALTER DATABASE [$(DatabaseName)]
    ADD FILEGROUP [MyFilegroup]
END
GO
RAISERROR (100010, 1, 1, 1)
GO
