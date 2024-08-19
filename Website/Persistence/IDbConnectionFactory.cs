using System.Data;

namespace Website.Persistence;

/// <summary>
/// Interface to an object that creates database connections.
/// </summary>
public interface IDbConnectionFactory
{
    /// <summary>
    /// Creates a new database connection.
    /// </summary>
    /// <param name="cancellationToken">A token which can be used to cancel the operation.</param>
    /// <returns>An awaitable task that results in the created connection.</returns>
    Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new closed database connection.
    /// </summary>
    IDbConnection CreateClosedConnection();
}
