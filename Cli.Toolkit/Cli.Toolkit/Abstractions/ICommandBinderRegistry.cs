namespace TomRR.Cli.Toolkit.Abstractions;

public partial interface ICommandBinderRegistry
{
    public Task BindAndRun(ICommand cmd, string[] args);
}