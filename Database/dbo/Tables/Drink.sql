CREATE TABLE [dbo].[Drink]
(
	[Id]            INT IDENTITY(1,1)   NOT NULL,
    [Name]          NVARCHAR(100)       NOT NULL,
    [Description]   NVARCHAR(255)       NULL

    PRIMARY KEY CLUSTERED ([Id] ASC)
)
