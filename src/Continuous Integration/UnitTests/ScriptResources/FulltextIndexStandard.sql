CREATE 
		FULLTEXT 
    INDEX 
	ON [dbo].[TableSix]
		([Col4],[asda])
	KEY INDEX [FulltextIndexStandard]
	ON [FulltextCatalogStandard]
	WITH CHANGE_TRACKING AUTO;