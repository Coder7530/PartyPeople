

CREATE   PROC [api].[spEventCreate]
(
    @Description [nvarchar](255),
    @StartDateTime [datetime2],
    @EndDateTime [datetime2],
    @MaximumCapacity [int]
)
AS
BEGIN
    INSERT INTO [dbo].[Event]
    (
        [Description],
        [StartDateTime],
        [EndDateTime],
        [MaximumCapacity]
    )
    OUTPUT [Inserted].[Id],
           [Inserted].[Description],
           [Inserted].[StartDateTime],
           [Inserted].[EndDateTime],
           [Inserted].[MaximumCapacity]
    VALUES
    (
        @Description,
        @StartDateTime,
        @EndDateTime,
        @MaximumCapacity
    );
END;
