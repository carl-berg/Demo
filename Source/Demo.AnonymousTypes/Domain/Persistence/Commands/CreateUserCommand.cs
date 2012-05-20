using System.Configuration;
using Demo.AnonymousTypes.Domain.Entities;

namespace Demo.AnonymousTypes.Domain.Persistence.Commands
{
	public class CreateUserCommand : CommandQueryBase, ICommand
	{
		private readonly User _user;

		public CreateUserCommand(User user)
			: base(ConfigurationManager.ConnectionStrings["ActiveConnection"].ConnectionString)
		{
			_user = user;
		}

		public void Execute()
		{
			const string sql = @"INSERT INTO [User](FirstName, LastName, Email) 
				VALUES(@FirstName, @LastName, @Email)";
			Execute(sql, new { _user.FirstName, _user.LastName, _user.Email });
		}
	}
}
