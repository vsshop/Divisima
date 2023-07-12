namespace WebApplication48.Models
{
	public class AuthorizationModel
	{
		public Guid Id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
	}

	public class RegistrationModel : AuthorizationModel
	{
		public string Email { get; set; }
	}
}
