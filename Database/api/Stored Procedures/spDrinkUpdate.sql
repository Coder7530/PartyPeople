CREATE PROCEDURE [api].[spDrinkUpdate]
    @Id INT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(255) = NULL
AS
BEGIN
    UPDATE [dbo].[Drink]
    SET [Name] = @Name, [Description] = @Description
    WHERE [Id] = @Id

    SELECT [Id], [Name], [Description]
    FROM [dbo].[Drink]
    WHERE [Id] = @Id
END