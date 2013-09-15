CREATE PARTITION FUNCTION [Partition Function With Spaces]
	(
		int
	)
	AS RANGE LEFT 
	FOR VALUES (1,100,1000);