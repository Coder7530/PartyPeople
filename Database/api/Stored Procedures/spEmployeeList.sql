
CREATE   PROC [api].[spEmployeeList]
AS
BEGIN
    SELECT  [E].[Id],
            [E].[FirstName],
            [E].[LastName],
            [E].[DateOfBirth]
    FROM    [dbo].[Employee] AS [E];
END;
