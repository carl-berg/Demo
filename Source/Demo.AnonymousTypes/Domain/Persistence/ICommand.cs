namespace Demo.AnonymousTypes.Domain.Persistence
{
	public interface ICommand<out TResult> : ICommand
	{
		TResult Result { get; }
	}

	public interface ICommand
	{
		void Execute();
	}
}