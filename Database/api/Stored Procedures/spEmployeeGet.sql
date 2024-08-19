
CREATE   PROC [api].[spEmployeeGet]
(
    @Id [int]
)
AS
BEGIN
    SELECT  [E].[Id],
            [E].[FirstName],
            [E].[LastName],
            [E].[DateOfBirth]
    FROM    [dbo].[Employee] AS [E]
    WHERE   [E].[Id] = @Id;
END;
