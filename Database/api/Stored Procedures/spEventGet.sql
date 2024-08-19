
CREATE   PROC [api].[spEventGet]
(
    @Id [int]
)
AS
BEGIN
    SELECT  [E].[Id],
            [E].[Description],
            [E].[StartDateTime],
            [E].[EndDateTime],
            [E].[MaximumCapacity]
    FROM    [dbo].[Event] AS [E]
    WHERE   [E].[Id] = @Id;
END;
