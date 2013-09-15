CREATE FUNCTION dbo.[FunctionUnBracketedSchema]
(
	@Param1 int
)
RETURNS int
AS
BEGIN
	RETURN @Param1
END