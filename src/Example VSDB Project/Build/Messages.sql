SET NOCOUNT ON

EXEC sp_addmessage @msgnum = 100000, @severity =  1, @msgtext = 'Number of objects == |2|', @lang = 'us_english', @replace = 'REPLACE'
EXEC sp_addmessage @msgnum = 100010, @severity =  1, @msgtext = 'Processed object |%d| of 2', @lang = 'us_english', @replace = 'REPLACE'
GO
RAISERROR (100000, 1, 1)
GO

IF NOT EXISTS (SELECT * FROM sys.service_message_types WHERE name = N'MessageType1')
BEGIN
CREATE MESSAGE TYPE [MessageType1]
	VALIDATION = NONE;
END
GO
RAISERROR (100010, 1, 1, 1)
GO

IF NOT EXISTS (SELECT * FROM sys.service_message_types WHERE name = N'MessageType2')
BEGIN
CREATE MESSAGE TYPE [MessageType2]
	VALIDATION = NONE;
END
GO
RAISERROR (100010, 1, 1, 2)
GO
