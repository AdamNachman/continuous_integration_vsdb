ALTER TABLE dbo.[TableOne]
    ADD CONSTRAINT [ForeignKeyUnbracketedSchema] FOREIGN KEY ([Col2]) REFERENCES [dbo].[TableOne] ([Col1]) ON DELETE CASCADE ON UPDATE CASCADE;