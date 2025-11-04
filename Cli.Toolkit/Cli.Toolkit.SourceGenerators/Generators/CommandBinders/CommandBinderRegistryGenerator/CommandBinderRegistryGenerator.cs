using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis.Text;

namespace TomRR.Cli.Toolkit.Generators.CommandBinders.CommandBinderRegistryGenerator;

public static class CommandBinderRegistryGenerator
{
    public static void Generate(
        SourceProductionContext context,
        ImmutableArray<(string Namespace, string ClassName, string CommandName, string[]? CommandShortNames, string? CommandDescription)> commands)
    {
        context.AddSource(
            $"_CommandBinderRegistry.g.cs",
            SourceText.From(CommandBinderRegistryCode.SourceCode(commands), Encoding.UTF8));
    }
    
}