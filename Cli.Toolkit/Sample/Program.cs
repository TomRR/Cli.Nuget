namespace TomRR.Cli.Tooling.Sample;

public static class ProgramExtensions
{
    public static async Task Build()
    {
        var args = new[] {"as", "as"};
        var builder = CliAppBuilder.CreateDefaultBuilder(args);
        builder.AddCommands().AddCommandBinderRegistry();

        var app = builder.Build();
        await app.RunAsync(args);
    }
}
