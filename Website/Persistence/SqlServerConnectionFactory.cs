using Microsoft.Data.SqlClient;
using System.Data;

namespace Website.Persistence;

/// <summary>
/// Creates MSSQL database connections.
/// </summary>
public class SqlServerConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    /// <summary>
    /// Initialises a new instance of the <see cref="SqlServerConnectionFactory"/> class.
    /// </summary>
    /// <param name="connectionString">The connection string to connect to.</param>
    public SqlServerConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    /// <summary>
    /// Creates a new database connection.
    /// </summary>
    /// <param name="cancellationToken">A token which can be used to cancel the operation.</param>
    /// <returns>An awaitable task that results in the created connection.</returns>
    public async Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default)
    {
        var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        return connection;
    }

    /// <summary>
    /// Creates a new closed database connection.
    /// </summary>
    public IDbConnection CreateClosedConnection() => new SqlConnection(_connectionString);
}