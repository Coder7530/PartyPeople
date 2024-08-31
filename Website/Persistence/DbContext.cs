using Website.Repositories;

namespace Website.Persistence;


public class DbContext
{
    /// <summary>
    /// Gets a repository for accessing employees.
    /// </summary>
    public EmployeeRepository Employees { get; }

    /// <summary>
    /// Gets a repository for accessing events.
    /// </summary>
    public EventRepository Events { get; }

    /// <summary>
    /// Gets a repository for accessing event employees.
    /// </summary>
    public EventEmployeeRepository EventEmployee { get; }

    /// <summary>
    /// Initialises a new instance of the <see cref="DbContext"/> class.
    /// </summary>
    /// <param name="connectionFactory">The database connection factory to use for database repositories.</param>
    public DbContext(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        _connectionProvider = new DbConnectionProvider(connectionFactory);

        Employees = new EmployeeRepository(_connectionProvider);
        Events = new EventRepository(_connectionProvider);
        EventEmployee = new EventEmployeeRepository(_connectionProvider);
    }


    private readonly IDbConnectionFactory _connectionFactory;
    private readonly DbConnectionProvider _connectionProvider;
}
