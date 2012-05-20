using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Demo.AnonymousTypes.Domain.Entities;

namespace Demo.AnonymousTypes.Domain.Persistence.Queries
{
	public class GetAllUsers : CommandQueryBase, IQuery<IEnumerable<User>>
	{
		public GetAllUsers()
			: base(ConfigurationManager.ConnectionStrings["ActiveConnection"].ConnectionString)
		{
		}

		public IEnumerable<User> Execute()
		{
			return Query("SELECT * FROM [User]", ParseUser);
		}

		protected virtual User ParseUser(IDataRecord dataRecord)
		{
			return new User
			{
			    Email = Parse<User, string>(dataRecord, x => x.Email),
			    FirstName = Parse<User, string>(dataRecord, x => x.FirstName),
			    LastName = Parse<User, string>(dataRecord, x => x.LastName),
			    Id = Parse<User, int>(dataRecord, x => x.Id)
			};
		}
	}
}