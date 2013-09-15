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