ALTER TABLE [MySchema].[TableOne]
    ADD CONSTRAINT [ForeignKeyDifferentSchema] FOREIGN KEY ([Col2]) REFERENCES [dbo].[TableOne] ([Col1]) ON DELETE CASCADE ON UPDATE CASCADE;