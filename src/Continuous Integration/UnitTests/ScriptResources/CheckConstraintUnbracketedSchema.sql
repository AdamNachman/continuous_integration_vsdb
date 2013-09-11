ALTER TABLE dbo.[TableOne]
    ADD CONSTRAINT [CheckConstraintUnbracketedSchema]
	CHECK (Col1 = 'Value');