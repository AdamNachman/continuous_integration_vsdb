ALTER TABLE [MySchema].TableOne
	ADD CONSTRAINT CheckConstraintUnbracketedName
	CHECK (Col1 = 'Value');