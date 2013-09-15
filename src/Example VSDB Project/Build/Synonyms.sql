SET NOCOUNT ON

EXEC sp_addmessage @msgnum = 100000, @severity =  1, @msgtext = 'Number of objects == |3|', @lang = 'us_english', @replace = 'REPLACE'
EXEC sp_addmessage @msgnum = 100010, @severity =  1, @msgtext = 'Processed object |%d| of 3', @lang = 'us_english', @replace = 'REPLACE'
GO
RAISERROR (100000, 1, 1)
GO
IF EXISTS (SELECT * FROM sys.synonyms WHERE object_id = OBJECT_ID(N'[dbo].[Synonym1]'))
  DROP SYNONYM [dbo].[Synonym1]
GO
RAISERROR (100010, 1, 1, 1)
GO

IF NOT EXISTS (SELECT * FROM sys.synonyms WHERE [name]='Synonym1')
BEGIN
CREATE SYNONYM [dbo].[Synonym1] 
    FOR [MySchema].[sprOne]
END
GO
RAISERROR (100010, 1, 1, 2)
GO
RAISERROR (100010, 1, 1, 3)
GO
