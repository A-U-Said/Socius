using Socius.Dto.Commands;
using Socius.Models.Repositories;

namespace Socius.Repositories
{
    public interface ISociusRepository<T,U>
		where U : ISociusUpdateCommand
		where T : ISociusSchema<U>
	{
		string GetDbName();
		Task Create(T record);
		Task<ICollection<T>> GetAll();
		Task<T?> GetSingle(int recordId);
		Task<IEnumerable<T>?> GetBy(Func<T, bool> condition);
		Task Update(T record);
		Task Delete(T record);
	}

}
