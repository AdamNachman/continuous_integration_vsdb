﻿CREATE PARTITION FUNCTION [PartitionFunctionStandard]
	(
		int
	)
	AS RANGE LEFT 
	FOR VALUES (1,100,1000);