CREATE PROCEDURE [api].[spDrinkDelete]
    @Id INT
AS
BEGIN
    DELETE FROM [dbo].[Drink]
    WHERE [Id] = @Id
END