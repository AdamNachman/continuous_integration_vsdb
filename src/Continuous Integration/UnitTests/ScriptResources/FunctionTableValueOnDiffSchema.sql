﻿CREATE FUNCTION [MySchema].FunctionTableValueOnDiffSchema()
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