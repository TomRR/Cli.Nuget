namespace TomRR.Cli.Toolkit.Attributes;

[Generator]
public class SourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        AttributesEmitter.Emit(context);
    }
}