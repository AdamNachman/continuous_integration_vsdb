﻿ALTER TABLE [TableOne]
    ADD CONSTRAINT [ForeignKeyNoSchema] FOREIGN KEY ([Col2]) REFERENCES [dbo].[TableOne] ([Col1]) ON DELETE CASCADE ON UPDATE CASCADE;