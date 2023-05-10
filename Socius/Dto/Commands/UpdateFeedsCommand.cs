namespace Socius.Dto.Commands
{

	public interface ISociusUpdateCommand
	{
	}

	public class UpdateFeedsCommand
	{
		public UpdateFacebookCredentialsCommand? Facebook { get; set; }
		public UpdateInstagramCredentialsCommand? Instagram { get; set; }
		public UpdateTwitterCredentialsCommand? Twitter { get; set; }
	}

	public class UpdateFacebookCredentialsCommand : ISociusUpdateCommand
	{
		public long AppId { get; set; }
		public string ClientSecret { get; set; }
		public long PageID { get; set; }
		public string Token { get; set; }
	}

	public class UpdateInstagramCredentialsCommand : ISociusUpdateCommand
	{
		public long ClientId { get; set; }
		public string ClientSecret { get; set; }
		public string RedirectUri { get; set; }
		public string Token { get; set; }
		public DateTime TokenExpiry { get; set; }
	}

	public class UpdateTwitterCredentialsCommand : ISociusUpdateCommand
	{
		public long UserID { get; set; }
		public string Token { get; set; }
	}
}
