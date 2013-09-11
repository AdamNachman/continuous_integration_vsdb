ALTER TABLE [dbo].[TableOne]
    ADD CONSTRAINT [CheckConstraintStandard]
	CHECK (Col1 = 'Value');