SET NOCOUNT ON

EXEC sp_addmessage @msgnum = 100000, @severity =  1, @msgtext = 'Number of objects == |1|', @lang = 'us_english', @replace = 'REPLACE'
EXEC sp_addmessage @msgnum = 100010, @severity =  1, @msgtext = 'Processed object |%d| of 1', @lang = 'us_english', @replace = 'REPLACE'
GO
RAISERROR (100000, 1, 1)
GO

IF NOT EXISTS (SELECT * FROM sys.service_queues WHERE name = N'Queue1')
BEGIN
CREATE QUEUE [dbo].[Queue1]
END
GO
RAISERROR (100010, 1, 1, 1)
GO
