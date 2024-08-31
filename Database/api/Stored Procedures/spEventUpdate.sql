
CREATE   PROC [api].[spEventUpdate]
(
    @Id INT,
    @Description [nvarchar](255),
    @StartDateTime [datetime2],
    @EndDateTime [datetime2],
    @MaximumCapacity [int]
)
AS
BEGIN
    UPDATE [E]
    SET [E].[Description] = @Description,
        [E].[StartDateTime] = @StartDateTime,
        [E].[EndDateTime] = @EndDateTime,
        [E].[MaximumCapacity] = @MaximumCapacity
    OUTPUT 
        [Inserted].[Id],
        [Inserted].[Description],
        [Inserted].[StartDateTime],
        [Inserted].[EndDateTime],
        [Inserted].[MaximumCapacity]
    FROM [dbo].[Event] AS [E]
    WHERE [E].[Id] = @Id;
END;
