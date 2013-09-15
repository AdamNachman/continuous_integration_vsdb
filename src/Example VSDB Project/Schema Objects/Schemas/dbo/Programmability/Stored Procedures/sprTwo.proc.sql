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