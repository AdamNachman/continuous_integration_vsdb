--Remove random named constraint so that we can replace it with a known, named object. This makes it easier to access programatically in the future. This makes it easier to access programatically in the future
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DF__TableOne__Col1__17F790F9]') AND type = 'D')
	BEGIN
		ALTER TABLE [dbo].[TableOne] DROP CONSTRAINT [DF__TableOne__Col1__17F790F9]
	END
GO

