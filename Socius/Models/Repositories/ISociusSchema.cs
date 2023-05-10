using Socius.Dto.Commands;

namespace Socius.Models.Repositories
{
    public interface ISociusSchema
	{
	}

	public interface ISociusSchema<TUpdateCommand> 
		where TUpdateCommand : ISociusUpdateCommand
	{
		void Update(TUpdateCommand newDetails);
	}
}
