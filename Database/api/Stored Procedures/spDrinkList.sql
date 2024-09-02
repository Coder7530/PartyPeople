CREATE PROCEDURE [api].[spDrinkList]
AS
BEGIN
    SELECT [Id], [Name], [Description]
    FROM [dbo].[Drink]
END