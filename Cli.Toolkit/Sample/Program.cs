namespace TomRR.Cli.Tooling.Sample;

public static class ProgramExtensions
{
    public static async Task Build()
    {
        var args = new[] {"as", "as"};
        var builder = CliAppBuilder.CreateDefaultBuilder(args);
        // builder.Services.AddDependencies();
        builder.AddCommands().AddCommandBinderRegistry();

        var app = builder.Build();
        await app.RunAsync(args);
    }
    

}

// public static partial class CommandBinderRegistryDependencyInjectionExtensions
// {
//     public static global::TomRR.Cli.Toolkit.CliAppBuilder AddCommandBinderRegistry(
//         this global::TomRR.Cli.Toolkit.CliAppBuilder builder)
//     {
//         return builder.AddDependencies(services =>
//         {
//             services.AddSingleton<global::TomRR.Cli.Toolkit.Abstractions.ICommandBinderRegistry, global::Cli.Toolkit.SourceGenerators.CommandBinders.CommandBinderRegistry>();
//         });
//     }
// }
