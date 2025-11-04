using TomRR.Cli.Toolkit.Generators.CommandBinders.CommandBinder;
using TomRR.Cli.Toolkit.Generators.CommandBinders.CommandBinderRegistryGenerator;

namespace TomRR.Cli.Toolkit.SourceGeneration;

[Generator]
public class CommandBinders : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        GenerateCommandBinderRegistry(context);
        CommandBinderGenerator.Generate(context);

    }
    
    
    private static void GenerateCommandBinderRegistry(IncrementalGeneratorInitializationContext context)
    {
        var commandClasses = context.SyntaxProvider
            .CreateSyntaxProvider(
                static (s, _) => s is ClassDeclarationSyntax,
                static (ctx, _) => AttributeHelper.GetCommandAttribute(ctx))
            .Where(static m => m is not null)
            .Select(static (m, _) => m!.Value)
            .Collect();
    
        context.RegisterSourceOutput(commandClasses,
            static (spc, arr) => CommandBinderRegistryGenerator.Generate(spc, arr));
    }

}