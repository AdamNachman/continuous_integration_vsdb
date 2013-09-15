SET NOCOUNT ON

EXEC sp_addmessage @msgnum = 100000, @severity =  1, @msgtext = 'Number of objects == |13|', @lang = 'us_english', @replace = 'REPLACE'
EXEC sp_addmessage @msgnum = 100010, @severity =  1, @msgtext = 'Processed object |%d| of 13', @lang = 'us_english', @replace = 'REPLACE'
GO
RAISERROR (100000, 1, 1)
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS(SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[TableThree]') AND name = N'IX_TableThree_Col3_Col4_b')
BEGIN
CREATE INDEX [IX_TableThree_Col3_Col4_b]
    ON [dbo].[TableThree]([Col3] ASC, [Col4] ASC)
    INCLUDE([Col2]) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);
END
GO
RAISERROR (100010, 1, 1, 1)
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS(SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[MySchema].[TableFour]') AND name = N'PK_TableFour')
BEGIN
ALTER TABLE [MySchema].[TableFour]
	ADD CONSTRAINT [PK_TableFour]
	PRIMARY KEY ([Col1])
END
GO
RAISERROR (100010, 1, 1, 2)
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS(SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[MySchema].[TableFour]') AND name = N'IX_TableFour_Col2_a')
BEGIN
CREATE NONCLUSTERED INDEX [IX_TableFour_Col2_a]
    ON [MySchema].[TableFour]
	(Col2);
END
GO
RAISERROR (100010, 1, 1, 3)
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS(SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[MySchema].[TableFour]') AND name = N'IX_TableFour_Col2_b')
BEGIN
CREATE UNIQUE NONCLUSTERED INDEX [IX_TableFour_Col2_b]
    ON [MySchema].[TableFour]
	(Col2);
END
GO
RAISERROR (100010, 1, 1, 4)
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS(SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[MySchema].[TableFour]') AND name = N'IX_TableFour_Col2_c')
BEGIN
CREATE UNIQUE INDEX [IX_TableFour_Col2_c]
    ON [MySchema].[TableFour]
	(Col2);
END
GO
RAISERROR (100010, 1, 1, 5)
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS(SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[MySchema].[TableFour]') AND name = N'UQ_TableFour_a')
BEGIN
ALTER TABLE [MySchema].[TableFour]
    ADD CONSTRAINT [UQ_TableFour_a]
    UNIQUE (Col1)
END
GO
RAISERROR (100010, 1, 1, 6)
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS(SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[MySchema].[TableFive]') AND name = N'PK_TableFive')
BEGIN
ALTER TABLE [MySchema].[TableFive]
	ADD CONSTRAINT [PK_TableFive]
	PRIMARY KEY ([Col1])
END
GO
RAISERROR (100010, 1, 1, 7)
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS(SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[MySchema].[TableSix]') AND name = N'MyIndexName')
BEGIN
CREATE UNIQUE INDEX [MyIndexName]
    ON [MySchema].[TableSix]
	(Col4);
END
GO
RAISERROR (100010, 1, 1, 8)
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[MySchema].[DF_TableSix_Col2]',N'D') AND parent_object_id = object_id('[MySchema].[TableSix]'))
BEGIN
ALTER TABLE [MySchema].[TableSix]
    ADD CONSTRAINT [DF_TableSix_Col2] DEFAULT ((2)) FOR [Col2];
END
GO
RAISERROR (100010, 1, 1, 9)
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_TableOne_Col2]',N'D') AND parent_object_id = object_id('[dbo].[TableOne]'))
BEGIN
ALTER TABLE [dbo].[TableOne]
    ADD CONSTRAINT [DF_TableOne_Col2] DEFAULT ((2)) FOR [Col2];
END
GO
RAISERROR (100010, 1, 1, 10)
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_TableThree_Col2]',N'D') AND parent_object_id = object_id('[dbo].[TableThree]'))
BEGIN
ALTER TABLE [dbo].[TableThree]
    ADD CONSTRAINT [DF_TableThree_Col2] DEFAULT ((3)) FOR [Col2];
END
GO
RAISERROR (100010, 1, 1, 11)
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS(SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[TableThree]') AND name = N'IX_TableThree_Col3_Col4_a')
BEGIN
CREATE NONCLUSTERED INDEX [IX_TableThree_Col3_Col4_a]
    ON [dbo].[TableThree]([Col3] ASC, [Col4] ASC)
    INCLUDE([Col2]) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);
END
GO
RAISERROR (100010, 1, 1, 12)
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TableTwo_TableOne]') AND parent_object_id = OBJECT_ID(N'[dbo].[TableTwo]'))
BEGIN
ALTER TABLE [dbo].[TableTwo]
    ADD CONSTRAINT [FK_TableTwo_TableOne] FOREIGN KEY ([Col2]) REFERENCES [dbo].[TableOne] ([Col1]) ON DELETE CASCADE ON UPDATE CASCADE;
END
GO
RAISERROR (100010, 1, 1, 13)
GO
