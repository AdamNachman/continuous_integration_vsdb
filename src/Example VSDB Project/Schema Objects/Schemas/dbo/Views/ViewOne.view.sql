CREATE VIEW dbo.ViewOne
AS
SELECT     t1.Col1, t1.Col2, t1.Col3, t1.Col4, t2.Col1 AS Expr1, t2.Col2 AS Expr2, t2.Col3 AS Expr3
FROM         dbo.TableOne AS t1 INNER JOIN
                      dbo.TableTwo AS t2 ON t1.Col1 = t2.Col2