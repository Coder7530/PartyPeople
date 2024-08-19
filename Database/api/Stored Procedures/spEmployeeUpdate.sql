
CREATE   PROC [api].[spEmployeeUpdate]
(
    @Id [int],
    @FirstName [nvarchar](255),
    @LastName [nvarchar](255),
    @DateOfBirth [date]
)
AS
BEGIN
    UPDATE  [E]
    SET     [E].[FirstName] = @FirstName,
            [E].[LastName] = @LastName,
            [E].[DateOfBirth] = @DateOfBirth
    OUTPUT [Inserted].[Id],
           [Inserted].[FirstName],
           [Inserted].[LastName],
           [Inserted].[DateOfBirth]
    FROM    [dbo].[Employee] AS [E]
    WHERE   [E].[Id] = @Id;
END;
