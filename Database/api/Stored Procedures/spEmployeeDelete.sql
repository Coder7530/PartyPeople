
CREATE   PROC [api].[spEmployeeDelete]
(
    @Id [int]
)
AS
BEGIN
    DELETE [E]
    FROM    [dbo].[Employee] AS [E]
    WHERE   [E].[Id] = @Id;
END;
