namespace TomRR.Cli.Toolkit.Abstractions;

public interface ICommandDispatcher
{
    public void Register<TCommand>(string name, params string[] shortNames)
        where TCommand : class, ICommand;

    public Task DispatchAsync(string[] args);
}