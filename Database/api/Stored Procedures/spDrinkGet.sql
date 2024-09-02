CREATE PROCEDURE [api].[spDrinkGet]
    @Id INT
AS
BEGIN
    SELECT [Id], [Name], [Description]
    FROM [dbo].[Drink]
    WHERE [Id] = @Id
END
