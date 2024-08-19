
CREATE   PROC [api].[spEventList]
(
    @IncludeHistoricEvents bit = 0
)
AS
BEGIN
    SELECT  [E].[Id],
            [E].[Description],
            [E].[StartDateTime],
            [E].[EndDateTime],
            [E].[MaximumCapacity]
    FROM    [dbo].[Event] AS [E]
    WHERE   (
                @IncludeHistoricEvents = 1
                OR  [E].[EndDateTime] > GETDATE()
            );
END;
