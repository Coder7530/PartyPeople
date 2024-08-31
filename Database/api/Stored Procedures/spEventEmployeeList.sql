CREATE PROCEDURE [api].[spEventEmployeeList]
    @EventId INT
AS
BEGIN
    SELECT e.*
    FROM [dbo].[Employee] e
    INNER JOIN [dbo].[EventEmployees] ee ON e.Id = ee.EmployeeId
    WHERE ee.EventId = @EventId
END
