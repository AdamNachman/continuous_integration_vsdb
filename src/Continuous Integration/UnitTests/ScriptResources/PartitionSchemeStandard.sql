CREATE PARTITION SCHEME [PartitionSchemeStandard]
	AS PARTITION [PartitionFunctionStandard]
	TO (MyFilegroup);