
CREATE   PROC [api].[spEmployeeCreate]
(
    @FirstName [nvarchar](255),
    @LastName [nvarchar](255),
    @DateOfBirth [date]
)
AS
BEGIN
    INSERT INTO [dbo].[Employee]
    (
        [FirstName],
        [LastName],
        [DateOfBirth]
    )
    OUTPUT [Inserted].[Id],
           [Inserted].[FirstName],
           [Inserted].[LastName],
           [Inserted].[DateOfBirth]
    VALUES
    (
        @FirstName,
        @LastName,
        @DateOfBirth
    );
END;
