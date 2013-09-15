SET NOCOUNT ON

EXEC sp_addmessage @msgnum = 100000, @severity =  1, @msgtext = 'Number of objects == |4|', @lang = 'us_english', @replace = 'REPLACE'
EXEC sp_addmessage @msgnum = 100010, @severity =  1, @msgtext = 'Processed object |%d| of 4', @lang = 'us_english', @replace = 'REPLACE'
GO
RAISERROR (100000, 1, 1)
GO
IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ViewOne]'))
  DROP VIEW [dbo].[ViewOne]
GO
RAISERROR (100010, 1, 1, 1)
GO
IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ViewTwo]'))
  DROP VIEW [dbo].[ViewTwo]
GO
RAISERROR (100010, 1, 1, 2)
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW dbo.ViewOne
AS
SELECT     t1.Col1, t1.Col2, t1.Col3, t1.Col4, t2.Col1 AS Expr1, t2.Col2 AS Expr2, t2.Col3 AS Expr3
FROM         dbo.TableOne AS t1 INNER JOIN
                      dbo.TableTwo AS t2 ON t1.Col1 = t2.Col2
GO
RAISERROR (100010, 1, 1, 3)
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW dbo.ViewTwo
AS
SELECT     Col1, Col2, Col3, Col4, Expr1, Expr2, Expr3
FROM         dbo.ViewOne
GO
RAISERROR (100010, 1, 1, 4)
GO
