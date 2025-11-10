namespace TomRR.Cli.Toolkit.Abstractions;

public partial interface ICliApp
{
    public Task RunAsync(string[] args);
}