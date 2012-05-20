namespace Demo.AnonymousTypes.Domain.Persistence
{
	public interface IQuery<out TResult>
	{
		TResult Execute();
	}
}