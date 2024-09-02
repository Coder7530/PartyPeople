CREATE PROCEDURE [api].[spEventEmployeeRemove]
    @EventId INT,
    @EmployeeId INT
AS
BEGIN
    DELETE FROM [dbo].[EventEmployees]
    WHERE EventId = @EventId AND EmployeeId = @EmployeeId
END