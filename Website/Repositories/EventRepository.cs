using Dapper;
using System.Data;
using Website.Models;
using Website.Persistence;

namespace Website.Repositories;

/// <summary>
/// Repository for accessing events from a database.
/// </summary>
public class EventRepository : RepositoryBase
{
    /// <summary>
    /// Initialises a new instance of the <see cref="EventRepository"/> class.
    /// </summary>
    /// <param name="connectionProvider">The connection provider to use.</param>
    public EventRepository(IDbConnectionProvider connectionProvider) : base(connectionProvider)
    {
    }

    /// <summary>
    /// Gets all events.
    /// </summary>
    /// <param name="cancellationToken">A token which can be used to cancel asynchronous operations.</param>
    /// <returns>An awaitable task whose result is the events found.</returns>
    public async Task<IReadOnlyCollection<Event>> GetAllAsync(bool includeHistoricEvents = false, CancellationToken cancellationToken = default)
    {
        var parameters = new
        {
            IncludeHistoricEvents = includeHistoricEvents
        };

        var command = new CommandDefinition(
            "[api].[spEventList]",
            parameters: parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken);

        var events = await Connection.QueryAsync<Event>(command);

        return events
            .OrderBy(x => x.StartDateTime)
            .ToArray();
    }

    /// <summary>
    /// Gets an event by ID.
    /// </summary>
    /// <param name="id">The ID of the event to get.</param>
    /// <param name="cancellationToken">A token which can be used to cancel asynchronous operations.</param>
    /// <returns>An awaitable task whose result is the event if found, otherwise <see langword="null"/>.</returns>
    public async ValueTask<Event?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var parameters = new
        {
            Id = id
        };

        var command = new CommandDefinition(
            "[api].[spEventGet]",
            parameters: parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken);

        return await Connection.QuerySingleOrDefaultAsync<Event>(command);
    }

    /// <summary>
    /// Determines whether an event with the given ID exists.
    /// </summary>
    /// <param name="id">The ID of the event to check.</param>
    /// <param name="cancellationToken">A token which can be used to cancel asynchronous operations.</param>
    /// <returns>An awaitable task whose result indicates whether the event exists.</returns>
    public async ValueTask<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        var parameters = new
        {
            Id = id
        };

        var command = new CommandDefinition(
            "[api].[spEventExists]",
            parameters: parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken);

        return await Connection.ExecuteScalarAsync<bool>(command);
    }

    /// <summary>
    /// Creates a new event.
    /// </summary>
    /// <param name="event">The event to create. The <see cref="Event.Id"/> is ignored.</param>
    /// <returns>An awaitable task that results in the created event, with its Id.</returns>
    public async Task<Event> CreateAsync(Event @event, CancellationToken cancellationToken = default)
    {
        var parameters = new
        {
            @event.Description,
            @event.StartDateTime,
            @event.EndDateTime,
            @event.MaximumCapacity
        };

        var command = new CommandDefinition(
            "[api].[spEventCreate]",
            parameters: parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken);

        var createdEvent = await Connection.QuerySingleAsync<Event>(command);

        return createdEvent;
    }

    /// <summary>
    /// Updates an existing event.
    /// </summary>
    /// <param name="event">The updated event details. The <see cref="Event.Id"/> should be the Id of the event to update.</param>
    /// <returns>An awaitable task that results in the updated event.</returns>
    public async Task<Event> UpdateAsync(Event @event, CancellationToken cancellationToken = default)
    {
        var parameters = new
        {
            @event.Description,
            @event.StartDateTime,
            @event.EndDateTime,
            @event.MaximumCapacity
        };

        var command = new CommandDefinition(
            "[api].[spEventUpdate]",
            parameters: parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken);

        var updatedEvent = await Connection.QuerySingleAsync<Event>(command);

        return updatedEvent;
    }

    /// <summary>
    /// Deletes an existing event.
    /// </summary>
    /// <param name="eventId">The ID of the event to delete.</param>
    /// <returns>An awaitable task.</returns>
    public async Task DeleteAsync(int eventId, CancellationToken cancellationToken = default)
    {
        var parameters = new
        {
            Id = eventId
        };

        var command = new CommandDefinition(
            "[api].[spEventDelete]",
            parameters: parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken);

        await Connection.ExecuteAsync(command);
    }
}