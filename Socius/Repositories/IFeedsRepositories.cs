using Socius.Dto.Commands;
using Socius.Models.Repositories;

namespace Socius.Repositories
{
	public interface IFacebookCredentialsRepository : ISociusRepository<FacebookCredentialsSchema, UpdateFacebookCredentialsCommand>
	{
	}

	public interface IInstagramCredentialsRepository : ISociusRepository<InstagramCredentialsSchema>
	{
	}

	public interface ITwitterCredentialsRepository : ISociusRepository<TwitterCredentialsSchema>
	{
	}
}
