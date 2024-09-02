CREATE TABLE [dbo].[EventEmployees]
(
    [Id]            INT         IDENTITY (1, 1) NOT NULL,
    [EventId]       INT         NOT NULL,
    [EmployeeId]    INT         NOT NULL,

    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT FK_EventEmployees_Event FOREIGN KEY([EventId]) REFERENCES [dbo].[Event] ([Id]) ON DELETE CASCADE,
    CONSTRAINT FK_EventEmployees_Employee FOREIGN KEY([EmployeeId]) REFERENCES [dbo].[Employee] ([Id]) ON DELETE CASCADE,
    CONSTRAINT UQ_EventEmployee UNIQUE ([EventId], [EmployeeId])
)
