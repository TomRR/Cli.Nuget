namespace TomRR.Cli.Toolkit;

public static partial class CliAppBuilderExtensions
{
    public static CliAppBuilder AddCommand<TCommand>(
        this CliAppBuilder builder, string name, params string[] shortNames)
        where TCommand : class, ICommand
    {
        builder.AddDependencies(services =>
        {
            services.AddSingleton<TCommand>();
            services.AddSingleton<ICommand, TCommand>();
            
            services.AddSingleton<CommandDispatcher>(); // idempotent
        });

        // Post-build action to register with dispatcher
        builder.AddPostBuildAction(host =>
        {
            var dispatcher = host.Services.GetRequiredService<CommandDispatcher>();
            dispatcher.Register<TCommand>(name, shortNames);
        });

        return builder;
    }
}
