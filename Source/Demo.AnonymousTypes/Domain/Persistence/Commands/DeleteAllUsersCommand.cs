using System.Configuration;

namespace Demo.AnonymousTypes.Domain.Persistence.Commands
{
	public class DeleteAllUsersCommand : CommandQueryBase, ICommand
	{
		public DeleteAllUsersCommand()
			: base(ConfigurationManager.ConnectionStrings["ActiveConnection"].ConnectionString)
		{

		}

		public void Execute()
		{
			Execute("DELETE FROM [User]");
		}
	}
}