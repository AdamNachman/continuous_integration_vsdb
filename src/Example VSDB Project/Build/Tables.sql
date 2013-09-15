SET NOCOUNT ON

EXEC sp_addmessage @msgnum = 100000, @severity =  1, @msgtext = 'Number of objects == |7|', @lang = 'us_english', @replace = 'REPLACE'
EXEC sp_addmessage @msgnum = 100010, @severity =  1, @msgtext = 'Processed object |%d| of 7', @lang = 'us_english', @replace = 'REPLACE'
GO
RAISERROR (100000, 1, 1)
GO

SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[dbo].[TableOne]', 'U') IS NULL
BEGIN
CREATE TABLE [dbo].[TableOne]
(
    [Col1] INT           IDENTITY (1, 1) NOT NULL,
    [Col2] INT           NOT NULL,
    [Col3] NVARCHAR (50) NOT NULL,
    [Col4] VARCHAR (50)  NOT NULL
);
END
GO

RAISERROR (100010, 1, 1, 1)
GO

SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[dbo].[TableThree]', 'U') IS NULL
BEGIN
CREATE TABLE [dbo].[TableThree]
(
    [Col1] INT           IDENTITY (1, 1) NOT NULL,
    [Col2] INT           NOT NULL,
    [Col3] NVARCHAR (50) NOT NULL,
    [Col4] VARCHAR (50)  NOT NULL
);
END
GO

RAISERROR (100010, 1, 1, 2)
GO

SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[dbo].[TableTwo]', 'U') IS NULL
BEGIN
CREATE TABLE [dbo].[TableTwo]
(
    [Col1] INT          IDENTITY (1, 1) NOT NULL,
    [Col2] INT          NOT NULL,
    [Col3] NVARCHAR (50) NOT NULL
);
END
GO

RAISERROR (100010, 1, 1, 3)
GO

SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[MySchema].[TableFour]', 'U') IS NULL
BEGIN
CREATE TABLE [MySchema].[TableFour]
(
	[Col1] INT NOT NULL, 
	[Col2] INT NULL
);
END
GO

RAISERROR (100010, 1, 1, 4)
GO

SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[MySchema].[TableFive]', 'U') IS NULL
BEGIN
CREATE TABLE [MySchema].[TableFive]
(
	[Col1] INT NOT NULL, 
	[Col2] INT NULL
);
END
GO

RAISERROR (100010, 1, 1, 5)
GO

SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[MySchema].[TableSix]', 'U') IS NULL
BEGIN
CREATE TABLE [MySchema].[TableSix]
(
	[Col1] INT NOT NULL, 
	[Col2] INT NULL, 
	[Col3] INT NOT NULL,
	[Col4] NVARCHAR(50) NOT NULL
);
END
GO

RAISERROR (100010, 1, 1, 6)
GO

SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[MySchema].[TriggerTable]', 'U') IS NULL
BEGIN
CREATE TABLE [MySchema].[TriggerTable]
(
	Col1 int NOT NULL, 
	Col2 int NULL
);
END
GO

RAISERROR (100010, 1, 1, 7)
GO
