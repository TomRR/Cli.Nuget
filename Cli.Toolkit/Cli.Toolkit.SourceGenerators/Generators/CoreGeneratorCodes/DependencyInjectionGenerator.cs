using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis.Text;

namespace TomRR.Cli.Toolkit.Generators.CoreGeneratorCodes;

public static class DependencyInjectionGenerator
{
    public static void Generate(IncrementalGeneratorInitializationContext context)
    {
        var commandClasses = context.SyntaxProvider
            .CreateSyntaxProvider(
                static (s, _) => s is ClassDeclarationSyntax,
                static (ctx, _) => AttributeHelper.GetCommandAttribute(ctx))
            .Where(static m => m is not null)
            .Select(static (m, _) => m!.Value)
            .Collect();

        context.RegisterSourceOutput(commandClasses,
            static (spc, arr) => GenerateDependencyInection(spc, arr));
    }

    public static void GenerateDependencyInection(
        SourceProductionContext context,
        ImmutableArray<(string Namespace, string ClassName, string CommandName, string[]? CommandShortNames, string? CommandDescription)> commands)
    {
        context.AddSource(
            $"DependencyInjection.Commands.g.cs",
            SourceText.From(CommandDependencyInjection.SourceCode(commands), Encoding.UTF8));
        context.AddSource(
            $"DependencyInjection.CommandBinderRegistry.g.cs",
            SourceText.From(CommandBinderRegistryCode.SourceCode, Encoding.UTF8));
    }
}