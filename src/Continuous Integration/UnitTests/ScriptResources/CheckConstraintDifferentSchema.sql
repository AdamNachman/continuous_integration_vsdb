ALTER TABLE [MySchema].[TableOne]
	ADD CONSTRAINT [CheckConstraintDifferentSchema]
	CHECK (Col1 = 'Value');