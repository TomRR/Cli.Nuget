namespace TomRR.Cli.Toolkit;

public sealed partial class CliAppBuilder : ICliAppBuilder
{
    private readonly IHostBuilder _builder;
    private readonly List<Action<IHost>> _postBuildActions = new();

    // Internal collection to allow direct service registration
    public IServiceCollection Services { get; } = new ServiceCollection();

    private CliAppBuilder(IHostBuilder builder)
    {
        _builder = builder;
    }

    public static CliAppBuilder CreateDefaultBuilder(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddDebug();
            });

        return new CliAppBuilder(builder);
    }
    public static CliAppBuilder CreateSlimBuilder(string[] args)
    {
        var builder = new HostBuilder();
        return new CliAppBuilder(builder);
    }

    public CliAppBuilder AddCommand<TCommand>(string name, params string[] shortNames)
        where TCommand : class, ICommand
    {
        AddDependencies(services =>
        {
            services.AddSingleton<TCommand>();
            services.AddSingleton<ICommand, TCommand>();
        });

        // Post-build action to register with dispatcher
        AddPostBuildAction(host =>
        {
            var dispatcher = host.Services.GetRequiredService<ICommandDispatcher>();
            dispatcher.Register<TCommand>(name, shortNames);
        });

        return this;
    }
    
    public CliAppBuilder AddDependencies(Action<IServiceCollection> configureServices)
    {
        configureServices(Services);
        return this;
    }

    public CliAppBuilder AddPostBuildAction(Action<IHost> action)
    {
        _postBuildActions.Add(action);
        return this;
    }

    public CliApp Build()
    {
        // Merge the collected services into the HostBuilder
        _builder.ConfigureServices(s =>
        {
            foreach (var service in Services)
            {
                s.Add(service);
            }
        });

        var host = _builder.Build();

        foreach (var action in _postBuildActions)
        {
            action(host);
        }

        return new CliApp(host);
    }

    public async Task RunAsync(string[] args)
    {
        var cliApp = Build();
        await cliApp.RunAsync(args);
    }
}