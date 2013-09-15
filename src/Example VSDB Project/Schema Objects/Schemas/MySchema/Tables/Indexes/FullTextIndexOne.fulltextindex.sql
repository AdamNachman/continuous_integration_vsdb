CREATE 
		FULLTEXT 
    INDEX 
	ON [MySchema].[TableSix]
		(Col4)
	KEY INDEX [MyIndexName]
	ON [FulltextCatalogStandard]
	WITH CHANGE_TRACKING AUTO;
