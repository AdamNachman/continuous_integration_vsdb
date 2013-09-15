CREATE FUNCTION [dbo].[Function With Spaces]
(
	@Param1 int
)
RETURNS int
AS
BEGIN
	RETURN @Param1
END