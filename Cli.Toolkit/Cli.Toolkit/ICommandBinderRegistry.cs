namespace TomRR.Cli.Toolkit;

public partial interface ICommandBinderRegistry
{
    public Task BindAndRun(ICommand cmd, string[] args);
}