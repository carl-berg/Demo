using Demo.AnonymousTypes.Domain.Persistence.Commands;
using NUnit.Framework;

namespace Demo.AnonymousTypes
{
	[TestFixture]
	public abstract class TestBase
	{
		[TestFixtureSetUp]
		public virtual void Setup()
		{
			new DeleteAllUsersCommand().Execute();
		}
	}
}