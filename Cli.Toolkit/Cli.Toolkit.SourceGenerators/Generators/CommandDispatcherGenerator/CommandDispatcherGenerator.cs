using System.Text;
using Microsoft.CodeAnalysis.Text;

namespace TomRR.Cli.Toolkit.Generators.CommandDispatcherGenerator;

public static class CommandDispatcherGenerator
{
    public static void Generate(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx 
            => ctx.AddSource(
                $"CommandDispatcher.g.cs",
                SourceText.From(CommandDispatcher.SourceCode, Encoding.UTF8)));
    }
}