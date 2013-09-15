IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MyOldUnusedTable]') AND type in (N'U'))
	BEGIN
		DROP TABLE [dbo].[MyOldUnusedTable];
	END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MyOldUnusedProcedure]') AND type in (N'P', N'PC'))
	BEGIN
		DROP PROCEDURE [dbo].[MyOldUnusedProcedure];
	END
GO