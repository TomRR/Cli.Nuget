namespace TomRR.Cli.Toolkit.Abstractions;

public interface ICliAppBuilder
{
    public CliAppBuilder AddCommand<TCommand>(string name, params string[] shortNames)
        where TCommand : class, ICommand;
    public CliAppBuilder AddDependencies(Action<IServiceCollection> configureServices);
    public CliAppBuilder AddPostBuildAction(Action<IHost> action);
    public CliApp Build();
    public Task RunAsync(string[] args);
}