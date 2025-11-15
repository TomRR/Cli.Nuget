
namespace TomRR.Cli.Toolkit.DependencyInjections;

[Generator]
public class SourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // 1. FIND classes and TRANSFORM them into our semantic model
        IncrementalValuesProvider<CommandToGenerate> commandModels = context.SyntaxProvider
            .CreateSyntaxProvider(
                // Predicate: Fast syntax filter for classes with attributes
                predicate: static (s, _) => s is ClassDeclarationSyntax c && c.AttributeLists.Count > 0,
                
                // Transform: Get the SemanticModel and build our CommandToGenerate
                transform: static (ctx, cancellationToken) => 
                {
                    var semanticModel = ctx.SemanticModel; 
                    var classSyntax = (ClassDeclarationSyntax)ctx.Node;
                    
                    // Call our helper directly from inside the transform
                    return AttributeParser.GetCommandToGenerate(semanticModel, classSyntax); 
                })
            .Where(static m => m is not null) // Filter out classes that weren't valid commands
            .Select(static (m, _) => m!)     // Get the non-nullable version
            .WithTrackingName("CommandModels");

        // NEW STEP: Collect all individual commands into a single ImmutableArray provider.
        // This is what we will use for the CommandBinderRegistryEmitter.
        IncrementalValueProvider<ImmutableArray<CommandToGenerate>> allCommandModels = 
            commandModels.Collect()
                .WithTrackingName("AllCommandModels");
        
        context.RegisterSourceOutput(allCommandModels, DependencyInjectionEmitter.Emit);

        // DependencyInjectionEmitter.Emit(context, commandClasses);
    }
}