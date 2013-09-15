SET NOCOUNT ON

EXEC sp_addmessage @msgnum = 100000, @severity =  1, @msgtext = 'Number of objects == |8|', @lang = 'us_english', @replace = 'REPLACE'
EXEC sp_addmessage @msgnum = 100010, @severity =  1, @msgtext = 'Processed object |%d| of 8', @lang = 'us_english', @replace = 'REPLACE'
GO
RAISERROR (100000, 1, 1)
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fncOne]') AND type IN (N'FN', N'FS', N'FT', N'IF', N'TF'))
  DROP FUNCTION [dbo].[fncOne]
GO
RAISERROR (100010, 1, 1, 1)
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fncTwo]') AND type IN (N'FN', N'FS', N'FT', N'IF', N'TF'))
  DROP FUNCTION [dbo].[fncTwo]
GO
RAISERROR (100010, 1, 1, 2)
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[MySchema].[fncThree]') AND type IN (N'FN', N'FS', N'FT', N'IF', N'TF'))
  DROP FUNCTION [MySchema].[fncThree]
GO
RAISERROR (100010, 1, 1, 3)
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetIds]') AND type IN (N'FN', N'FS', N'FT', N'IF', N'TF'))
  DROP FUNCTION [dbo].[GetIds]
GO
RAISERROR (100010, 1, 1, 4)
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION dbo.fncOne
(
	@Param1 int
)
RETURNS int
AS
BEGIN

	RETURN @Param1

END
GO

SET QUOTED_IDENTIFIER OFF
GO

RAISERROR (100010, 1, 1, 5)
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION dbo.fncTwo()
RETURNS @Temp TABLE 
(
	Col1 int PRIMARY KEY NOT NULL
)
AS
BEGIN
	INSERT INTO @Temp (Col1)
  SELECT 1
    UNION ALL
  SELECT 2
    UNION ALL
  SELECT 3
    UNION ALL
  SELECT 4
	
	RETURN;
END
GO

SET QUOTED_IDENTIFIER OFF
GO

RAISERROR (100010, 1, 1, 6)
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION MySchema.fncThree
(
	@Param1 int
)
RETURNS int
AS
BEGIN
	RETURN @Param1
END
GO

SET QUOTED_IDENTIFIER OFF
GO

RAISERROR (100010, 1, 1, 7)
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetIds]
(
	@list NVARCHAR (MAX),
	@delim NVARCHAR (1)
)
RETURNS 
     TABLE ([Id] INT NULL)
AS
 EXTERNAL NAME [AdamNachman.SqlClr].[AdamNachman.SqlClr.Selection].[GetIds];
GO

SET QUOTED_IDENTIFIER OFF
GO

RAISERROR (100010, 1, 1, 8)
GO
