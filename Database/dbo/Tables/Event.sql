CREATE TABLE [dbo].[Event] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [Description]     NVARCHAR (255) NOT NULL,
    [StartDateTime]   DATETIME2 (7)  NOT NULL,
    [EndDateTime]     DATETIME2 (7)  NOT NULL,
    [MaximumCapacity] INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

