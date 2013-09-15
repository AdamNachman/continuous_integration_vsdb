CREATE FUNCTION [dbo].[GetIds]
(
	@list NVARCHAR (MAX),
	@delim NVARCHAR (1)
)
RETURNS 
     TABLE ([Id] INT NULL)
AS
 EXTERNAL NAME [AdamNachman.SqlClr].[AdamNachman.SqlClr.Selection].[GetIds];