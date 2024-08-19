
CREATE   PROC [api].[spEmployeeExists]
(
    @Id [int]
)
AS
BEGIN
    SELECT  CAST(CASE
                     WHEN EXISTS (
                                     SELECT 1
                                     FROM   [dbo].[Employee] AS [E]
                                     WHERE  [E].[Id] = @Id
                                 ) THEN 1
                     ELSE 0
                 END AS bit);
END;
