PRINT 'Merging data into [dbo].[TableOne]'

PRINT 'Enable identity insert'
SET IDENTITY_INSERT [dbo].[TableOne] ON
GO

PRINT 'Merge data...'

UPDATE [dbo].[TableOne] SET [Col2] = 1 ,[Col3] = 'One' ,[Col4] = 'wunn!' WHERE [Col1] = 1
   IF @@ROWCOUNT = 0 INSERT INTO [dbo].[TableOne]([Col1], [Col2], [Col3], [Col4]) VALUES(1, 1, 'One', 'wunn!')
UPDATE [dbo].[TableOne] SET [Col2] = 2 ,[Col3] = 'Two' ,[Col4] = 'too!' WHERE [Col1] = 2
   IF @@ROWCOUNT = 0 INSERT INTO [dbo].[TableOne]([Col1], [Col2], [Col3], [Col4]) VALUES(2, 2, 'Two', 'too!')
UPDATE [dbo].[TableOne] SET [Col2] = 3 ,[Col3] = 'Three' ,[Col4] = 'free!' WHERE [Col1] = 3
   IF @@ROWCOUNT = 0 INSERT INTO [dbo].[TableOne]([Col1], [Col2], [Col3], [Col4]) VALUES(3, 3, 'Three', 'free!')
UPDATE [dbo].[TableOne] SET [Col2] = 4 ,[Col3] = 'Four' ,[Col4] = 'for!' WHERE [Col1] = 4
   IF @@ROWCOUNT = 0 INSERT INTO [dbo].[TableOne]([Col1], [Col2], [Col3], [Col4]) VALUES(4, 4, 'Four', 'for!')
UPDATE [dbo].[TableOne] SET [Col2] = 5 ,[Col3] = 'Five' ,[Col4] = 'fave!' WHERE [Col1] = 5
   IF @@ROWCOUNT = 0 INSERT INTO [dbo].[TableOne]([Col1], [Col2], [Col3], [Col4]) VALUES(5, 5, 'Five', 'fave!')
GO

PRINT 'Disable identity insert'
SET IDENTITY_INSERT [dbo].[TableOne] OFF
GO
PRINT 'Done'
