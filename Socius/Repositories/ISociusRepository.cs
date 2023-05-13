using Socius.Models.Repositories;

namespace Socius.Repositories
{
    public interface ISociusRepository<T> where T : ISociusSchema
	{
		string GetDbName();
		Task<T> Create(T record);
		Task<ICollection<T>> GetAll();
		Task<T?> GetSingle(int recordId);
		Task<IEnumerable<T>?> GetBy(Func<T, bool> condition);
		Task Update(T record);
		Task Delete(T record);
	}

}
