using TomRR.Cli.Toolkit.CommandBinders.CommandBinder;

namespace TomRR.Cli.Toolkit.CommandBinders;

[Generator]
public class SourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var commandClasses = context.SyntaxProvider
            .CreateSyntaxProvider(
                static (s, _) => s is ClassDeclarationSyntax,
                static (ctx, _) => AttributeHelper.GetCommandAttribute(ctx))
            .Where(static m => m is not null)
            .Select(static (m, _) => m!.Value)
            .Collect();
    
        context.RegisterSourceOutput(commandClasses,
            static (spc, arr) => CommandBinderRegistryGenerator.CommandBinderRegistryEmitter.Emit(spc, arr));
        
        CommandBinderEmitter.Emit(context);
    }

}