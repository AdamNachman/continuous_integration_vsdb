SET NOCOUNT ON

EXEC sp_addmessage @msgnum = 100000, @severity =  1, @msgtext = 'Number of objects == |1|', @lang = 'us_english', @replace = 'REPLACE'
EXEC sp_addmessage @msgnum = 100010, @severity =  1, @msgtext = 'Processed object |%d| of 1', @lang = 'us_english', @replace = 'REPLACE'
GO
RAISERROR (100000, 1, 1)
GO

IF OBJECT_ID(N'[MySchema].[TriggerStandard]', 'TR') IS NOT NULL
  DROP TRIGGER [MySchema].[TriggerStandard]
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [TriggerStandard]
    ON [MySchema].[TriggerTable]
    FOR DELETE, INSERT, UPDATE 
    AS 
    BEGIN
    	SET NOCOUNT ON;
    END
GO
SET QUOTED_IDENTIFIER OFF
GO

RAISERROR (100010, 1, 1, 1)
GO
