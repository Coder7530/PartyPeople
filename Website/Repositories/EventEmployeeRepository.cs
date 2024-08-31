using Dapper;
using System.Data;
using System.Threading;
using Website.Models;
using Website.Persistence;

namespace Website.Repositories
{
    public class EventEmployeeRepository : RepositoryBase
    {
        public EventEmployeeRepository(IDbConnectionProvider connectionProvider) : base(connectionProvider)
        {
        }

        public async Task<IEnumerable<Employee>> GetEmployeesForEventAsync(int eventId, CancellationToken cancellationToken = default) 
        {
            var parameters = new { EventId = eventId };

            var command = new CommandDefinition("[api].[spEventEmployeeList]",
                parameters: parameters,
                commandType: System.Data.CommandType.StoredProcedure,
                cancellationToken: cancellationToken);

            var list = await Connection.QueryAsync<Employee>(command);

            return list;
        }

        public async Task AddEmployeeToEventAsync(int eventId, int employeeId, CancellationToken cancellationToken = default) 
        {
            var parameters = new { EventId = eventId, EmployeeId = employeeId };

            var command = new CommandDefinition(
                "[api].[spEventEmployeeAdd]",
                parameters: parameters,
            commandType: CommandType.StoredProcedure,
                cancellationToken: cancellationToken);

            await Connection.ExecuteAsync(command);
        }

        public async Task RemoveEmployeeFromEventAsync(int eventId, int employeeId, CancellationToken cancellationToken = default) 
        {
            var parameters = new { EventId = eventId, EmployeeId = employeeId };

            var command = new CommandDefinition(
            "[api].[spEventEmployeeRemove]",
            parameters: parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken);

            await Connection.ExecuteAsync(command);
        }
    }
}
