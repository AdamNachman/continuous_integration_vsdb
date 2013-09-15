ALTER TABLE [MySchema].[TableTwo]
    ADD CONSTRAINT [DefaultConstrantDifferentSchema] DEFAULT ((2)) FOR [Col2];