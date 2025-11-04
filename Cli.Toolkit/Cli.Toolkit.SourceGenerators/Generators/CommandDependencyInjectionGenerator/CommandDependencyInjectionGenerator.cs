using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis.Text;
using TomRR.Cli.Toolkit.Generators.CoreGeneratorCodes;

namespace TomRR.Cli.Toolkit.Generators.CommandDependencyInjectionGenerator;

public static class CommandDependencyInjectionGenerator
{
    public static void Generate(
        SourceProductionContext context,
        ImmutableArray<(string Namespace, string ClassName, string CommandName, string[]? CommandShortNames, string? CommandDescription)> commands)
    {
        context.AddSource(
            $"DependencyInjection.g.cs",
            SourceText.From(CommandDependencyInjection.SourceCode(commands), Encoding.UTF8));
    }
    
}