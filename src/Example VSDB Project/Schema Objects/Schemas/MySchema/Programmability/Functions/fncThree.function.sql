﻿CREATE FUNCTION MySchema.fncThree
(
	@Param1 int
)
RETURNS int
AS
BEGIN
	RETURN @Param1
END