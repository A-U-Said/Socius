namespace Socius.Dto.Commands
{
	public class FacebookUserTokenExchangeCommand
	{
		public string AccessToken { get; set; }
		public string UserID { get; set; }
		public int ExpiresIn { get; set; }
		public string SignedRequest { get; set; }
		public string GraphDomain { get; set; }
		public int DataAccessExpirationTime { get; set; }
	}
}
