using Dapper;
using System.Data;
using Website.Models;
using Website.Persistence;

namespace Website.Repositories
{
    public class DrinkRepository : RepositoryBase
    {
        public DrinkRepository(IDbConnectionProvider connectionProvider) : base(connectionProvider)
        {
        }

        public async Task<IReadOnlyCollection<Drink>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var command = new CommandDefinition(
                "[api].[spDrinkList]",
                commandType: CommandType.StoredProcedure,
                cancellationToken: cancellationToken);

            var drinks = await Connection.QueryAsync<Drink>(command);
            return drinks.ToList();
        }

        public async Task<Drink> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var parameters = new { Id = id };
            var command = new CommandDefinition(
                "[api].[spDrinkGet]",
                parameters: parameters,
                commandType: CommandType.StoredProcedure,
                cancellationToken: cancellationToken);

            return await Connection.QuerySingleOrDefaultAsync<Drink>(command);
        }

        public async Task<Drink> CreateAsync(Drink drink, CancellationToken cancellationToken = default)
        {
            var parameters = new { drink.Name, drink.Description };
            var command = new CommandDefinition(
                "[api].[spDrinkCreate]",
                parameters: parameters,
                commandType: CommandType.StoredProcedure,
                cancellationToken: cancellationToken);

            return await Connection.QuerySingleAsync<Drink>(command);
        }

        public async Task<Drink> UpdateAsync(Drink drink, CancellationToken cancellationToken = default)
        {
            var parameters = new { drink.Id, drink.Name, drink.Description };
            var command = new CommandDefinition(
                "[api].[spDrinkUpdate]",
                parameters: parameters,
                commandType: CommandType.StoredProcedure,
                cancellationToken: cancellationToken);

            return await Connection.QuerySingleAsync<Drink>(command);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var parameters = new { Id = id };
            var command = new CommandDefinition(
                "[api].[spDrinkDelete]",
                parameters: parameters,
                commandType: CommandType.StoredProcedure,
                cancellationToken: cancellationToken);

            await Connection.ExecuteAsync(command);
        }
    }
}
