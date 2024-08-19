using Dapper;
using System.Data;
using Website.Models;
using Website.Persistence;

namespace Website.Repositories;

/// <summary>
/// Repository for accessing employees from a database.
/// </summary>
public class EmployeeRepository : RepositoryBase
{
    /// <summary>
    /// Initialises a new instance of the <see cref="EmployeeRepository"/> class.
    /// </summary>
    /// <param name="connectionProvider">The connection provider to use.</param>
    public EmployeeRepository(IDbConnectionProvider connectionProvider) : base(connectionProvider)
    {
    }

    /// <summary>
    /// Gets all employees.
    /// </summary>
    /// <param name="cancellationToken">A token which can be used to cancel asynchronous operations.</param>
    /// <returns>An awaitable task whose result is the employees found.</returns>
    public async Task<IReadOnlyCollection<Employee>> GetAllAsync(CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(
            "[api].[spEmployeeList]",
            parameters: null,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken);

        var employees = await Connection.QueryAsync<Employee>(command);

        return employees
            .OrderBy(x => x.LastName)
            .ToArray();
    }

    /// <summary>
    /// Gets an employee by ID.
    /// </summary>
    /// <param name="id">The ID of the employee to get.</param>
    /// <param name="cancellationToken">A token which can be used to cancel asynchronous operations.</param>
    /// <returns>An awaitable task whose result is the employee if found, otherwise <see langword="null"/>.</returns>
    public async ValueTask<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var parameters = new
        {
            Id = id
        };

        var command = new CommandDefinition(
            "[api].[spEmployeeGet]",
            parameters: parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken);

        return await Connection.QuerySingleOrDefaultAsync<Employee>(command);
    }

    /// <summary>
    /// Determines whether an employee with the given ID exists.
    /// </summary>
    /// <param name="id">The ID of the employee to check.</param>
    /// <param name="cancellationToken">A token which can be used to cancel asynchronous operations.</param>
    /// <returns>An awaitable task whose result indicates whether the employee exists.</returns>
    public async ValueTask<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        var parameters = new
        {
            Id = id
        };

        var command = new CommandDefinition(
            "[api].[spEmployeeExists]",
            parameters: parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken);

        return await Connection.ExecuteScalarAsync<bool>(command);
    }

    /// <summary>
    /// Creates a new employee.
    /// </summary>
    /// <param name="employee">The employee to create. The <see cref="Employee.Id"/> is ignored.</param>
    /// <returns>An awaitable task that results in the created employee, with its Id.</returns>
    public async Task<Employee> CreateAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        var parameters = new
        {
            employee.FirstName,
            employee.LastName,
            employee.DateOfBirth
        };

        var command = new CommandDefinition(
            "[api].[spEmployeeCreate]",
            parameters: parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken);

        var createdEmployee = await Connection.QuerySingleAsync<Employee>(command);

        return createdEmployee;
    }

    /// <summary>
    /// Updates an existing employee.
    /// </summary>
    /// <param name="employee">The updated employee details. The <see cref="Employee.Id"/> should be the Id of the employee to update.</param>
    /// <returns>An awaitable task that results in the updated employee.</returns>
    public async Task<Employee> UpdateAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        var parameters = new
        {
            employee.Id,
            employee.FirstName,
            employee.LastName,
            employee.DateOfBirth
        };

        var command = new CommandDefinition(
            "[api].[spEmployeeUpdate]",
            parameters: parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken);

        var updatedEmployee = await Connection.QuerySingleAsync<Employee>(command);

        return updatedEmployee;
    }

    /// <summary>
    /// Deletes an existing employee.
    /// </summary>
    /// <param name="employeeId">The ID of the employee to delete.</param>
    /// <returns>An awaitable task.</returns>
    public async Task DeleteAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        var parameters = new
        {
            Id = employeeId
        };

        var command = new CommandDefinition(
            "[api].[spEmployeeDelete]",
            parameters: parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken);

        await Connection.ExecuteAsync(command);
    }
}