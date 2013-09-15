SET NOCOUNT ON

EXEC sp_addmessage @msgnum = 100000, @severity =  1, @msgtext = 'Number of objects == |1|', @lang = 'us_english', @replace = 'REPLACE'
EXEC sp_addmessage @msgnum = 100010, @severity =  1, @msgtext = 'Processed object |%d| of 1', @lang = 'us_english', @replace = 'REPLACE'
GO
RAISERROR (100000, 1, 1)
GO

IF NOT EXISTS (SELECT * FROM sys.services WHERE name = N'Service1')
BEGIN
CREATE SERVICE [Service1]
	ON QUEUE [dbo].[Queue1]
	( 
	    [Contract1]
	)
END
GO
RAISERROR (100010, 1, 1, 1)
GO
