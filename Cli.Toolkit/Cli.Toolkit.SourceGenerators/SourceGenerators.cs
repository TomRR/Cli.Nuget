using System.Text;
using Microsoft.CodeAnalysis.Text;
using TomRR.Cli.Toolkit.Generators.CommandDispatcherGenerator;
using TomRR.Cli.Toolkit.Generators.CoreGeneratorCodes;
using TomRR.Cli.Toolkit.Generators.Interfaces;

namespace TomRR.Cli.Toolkit;

[Generator]
public class SourceGenerators : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Attributes.Attributes.Generate(context);
        InterfaceGenerator.Generate(context);
        CliAppGeneratorGenerate(context);
        CommandDispatcherGenerator.Generate(context);
        DependencyInjectionGenerator.Generate(context);
    }

    private void CliAppGeneratorGenerate(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx 
            => ctx.AddSource(
                $"CliApp.g.cs",
                SourceText.From(CliApp.SourceCode, Encoding.UTF8)));
        
        context.RegisterPostInitializationOutput(ctx 
            => ctx.AddSource(
                $"CliAppBuilder.g.cs",
                SourceText.From(CliAppBuilder.SourceCode, Encoding.UTF8)));
        
        context.RegisterPostInitializationOutput(ctx 
            => ctx.AddSource(
                $"CliAppBuilderExtensions.g.cs",
                SourceText.From(CliAppBuilderExtensions.SourceCode, Encoding.UTF8)));
    }
}