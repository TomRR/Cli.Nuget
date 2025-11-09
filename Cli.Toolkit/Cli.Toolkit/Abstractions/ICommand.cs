namespace TomRR.Cli.Toolkit.Abstractions;

public partial interface ICommand
{
    Task RunAsync();
}