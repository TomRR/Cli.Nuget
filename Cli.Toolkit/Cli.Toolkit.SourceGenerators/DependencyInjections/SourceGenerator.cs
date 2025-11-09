
namespace TomRR.Cli.Toolkit.DependencyInjections;

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

        DependencyInjectionEmitter.Emit(context, commandClasses);
    }
}