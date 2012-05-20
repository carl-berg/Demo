using System.Linq;
using Demo.AnonymousTypes.Domain.Entities;
using Demo.AnonymousTypes.Domain.Persistence.Commands;
using Demo.AnonymousTypes.Domain.Persistence.Queries;
using NUnit.Framework;

namespace Demo.AnonymousTypes
{
	public class When_persisting_a_user : TestBase
	{
		private readonly User _user;

		public When_persisting_a_user()
		{
			_user = new User
			{
				FirstName = "John",
				LastName = "Doe",
				Email = "jdoe@company.com"
			};
		}

		public override void Setup()
		{
			base.Setup();
			new CreateUserCommand(_user).Execute();
		}

		[Test]
		public void User_was_persisted()
		{
			var user = new GetAllUsers().Execute().Single();
			Assert.AreEqual(_user.Email, user.Email);
			Assert.AreEqual(_user.FirstName, user.FirstName);
			Assert.AreEqual(_user.LastName, user.LastName);
		}
	}
}

