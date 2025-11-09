namespace TomRR.Cli.Toolkit.Abstractions;

public interface ICliApp
{
    public Task RunAsync(string[] args);
}