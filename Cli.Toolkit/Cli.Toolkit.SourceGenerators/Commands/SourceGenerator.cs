namespace TomRR.Cli.Toolkit.Commands;

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

        var modelsProvider = commandClasses.Select((tuples, ct) =>
        {
            return tuples.Select(t => new CommandClassModel(
                t.Namespace,
                t.ClassName,
                t.CommandName,
                t.CommandShortNames,
                t.CommandDescription
            )).ToImmutableArray();
        });
        
        CommandEmitter.Emit(context, modelsProvider);
    }
}