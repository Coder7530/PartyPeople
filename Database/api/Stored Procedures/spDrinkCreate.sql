CREATE PROCEDURE [api].[spDrinkCreate]
    @Name NVARCHAR(100),
    @Description NVARCHAR(255) = NULL
AS
BEGIN
    INSERT INTO [dbo].[Drink] ([Name], [Description])
    OUTPUT Inserted.Id, Inserted.Name, Inserted.Description
    VALUES (@Name, @Description)
END