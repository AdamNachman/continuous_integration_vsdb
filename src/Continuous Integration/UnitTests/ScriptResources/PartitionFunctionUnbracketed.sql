﻿CREATE PARTITION FUNCTION PartitionUnbracketed
	(
		int
	)
	AS RANGE LEFT 
	FOR VALUES (1,100,1000);