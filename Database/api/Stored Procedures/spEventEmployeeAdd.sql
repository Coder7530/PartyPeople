CREATE PROCEDURE [api].[spEventEmployeeAdd]
	@EventId INT,
	@EmployeeId INT
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM [dbo].[EventEmployees] WHERE EventId = @EventId AND EmployeeId = @EmployeeId)
	BEGIN
		INSERT INTO [dbo].[EventEmployees] (EventId, EmployeeId)
		VALUES (@EventId, @EmployeeId)
	END
END	
