CREATE TABLE [dbo].[Employee] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]   NVARCHAR (255) NOT NULL,
    [LastName]    NVARCHAR (255) NOT NULL,
    [DateOfBirth] DATE           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

