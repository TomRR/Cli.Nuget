namespace TomRR.Cli.Toolkit.Abstractions;

public partial interface ICommandDispatcher
{
    public void Register<TCommand>(string name, params string[] shortNames)
        where TCommand : class, ICommand;

    public Task DispatchAsync(string[] args);
}