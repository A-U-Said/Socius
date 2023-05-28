using Umbraco.Cms.Infrastructure.Scoping;
using Socius.Models.Repositories;

namespace Socius.Repositories
{
	public class SociusRepository<T> : ISociusRepository<T>
		where T : ISociusSchema
	{
		private readonly IScopeProvider _scopeProvider;
		private readonly string _dbName;
		private readonly string _primaryKey;

		public SociusRepository(IScopeProvider scopeProvider, string dbName, string primaryKey = "Id")
		{
			_scopeProvider = scopeProvider;
			_dbName = dbName;
			_primaryKey = primaryKey;
		}

		public string GetDbName()
		{
			return _dbName;
		}

		public virtual async Task<T> Create(T record)
		{
			using var scope = _scopeProvider.CreateScope();
			await scope.Database.InsertAsync(record);
			scope.Complete();

			return record;
		}

		public virtual async Task<ICollection<T>> GetAll()
		{
			using var scope = _scopeProvider.CreateScope();
			var queryResults =  await scope.Database.FetchAsync<T>($"SELECT * FROM {_dbName}");
			scope.Complete();

			return queryResults;
		}

		public virtual async Task<IEnumerable<T>?> GetBy(Func<T, bool> condition)
		{
			using var scope = _scopeProvider.CreateScope();
			var queryResults = await scope.Database.FetchAsync<T>($"SELECT * FROM {_dbName}");
			scope.Complete();

			return queryResults.Where(condition);
		}

		public virtual async Task<T?> GetSingle(int recordId)
		{
			using var scope = _scopeProvider.CreateScope();
			var queryResult = await scope.Database.SingleByIdAsync<T>(recordId);
			scope.Complete();

			if (queryResult == null )
			{
				return default;
			}

			return queryResult;
		}

		public virtual async Task Update(T record)
		{
			using var scope = _scopeProvider.CreateScope();
			var queryResults = await scope.Database.UpdateAsync(record);
			scope.Complete();
		}

		public virtual async Task Delete(T record)
		{
			using var scope = _scopeProvider.CreateScope();
			var queryResults = await scope.Database.DeleteAsync(record);
			scope.Complete();
		}
	}
}
