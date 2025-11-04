namespace TomRR.Cli.Toolkit;

public partial interface ICommand
{
    Task RunAsync();
}