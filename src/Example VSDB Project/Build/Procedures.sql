SET NOCOUNT ON

EXEC sp_addmessage @msgnum = 100000, @severity =  1, @msgtext = 'Number of objects == |5|', @lang = 'us_english', @replace = 'REPLACE'
EXEC sp_addmessage @msgnum = 100010, @severity =  1, @msgtext = 'Processed object |%d| of 5', @lang = 'us_english', @replace = 'REPLACE'
GO
RAISERROR (100000, 1, 1)
GO

IF OBJECT_ID(N'[dbo].[sprOne]', 'P') IS NOT NULL
  DROP PROCEDURE [dbo].[sprOne]
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.sprOne
AS
BEGIN

	SET NOCOUNT ON;
   
	SELECT
    [Col1],
    [Col2],
    [Col3],
    [Col4]
  FROM
    [dbo].[TableOne]

	exec [dbo].sprThree
END
GO
SET QUOTED_IDENTIFIER OFF
GO

RAISERROR (100010, 1, 1, 1)
GO

IF OBJECT_ID(N'[dbo].[sprTwo]', 'P') IS NOT NULL
  DROP PROCEDURE [dbo].[sprTwo]
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.sprTwo
AS
BEGIN

	SET NOCOUNT ON;
   
	SELECT
    [Col1],
    [Col2]
  FROM
    [dbo].[TableOne]
END
GO
SET QUOTED_IDENTIFIER OFF
GO

RAISERROR (100010, 1, 1, 2)
GO

IF OBJECT_ID(N'[MySchema].[sprOne]', 'P') IS NOT NULL
  DROP PROCEDURE [MySchema].[sprOne]
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [MySchema].[sprOne]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM dbo.fncTwo();
END;
GO
SET QUOTED_IDENTIFIER OFF
GO

RAISERROR (100010, 1, 1, 3)
GO

IF OBJECT_ID(N'[dbo].[sprThree]', 'P') IS NOT NULL
  DROP PROCEDURE [dbo].[sprThree]
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sprThree]
AS
	exec [dbo].sprOne
RETURN 0
GO
SET QUOTED_IDENTIFIER OFF
GO

RAISERROR (100010, 1, 1, 4)
GO
RAISERROR (100010, 1, 1, 5)
GO
