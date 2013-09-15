--I'm inserting this comment so that I can test the CREATE of a new FUNCTION with this comments
CREATE FUNCTION [MySchema].[FunctionCommentPrefix]
(
	@Param1 int
)
RETURNS int
AS
BEGIN
	RETURN @Param1
END