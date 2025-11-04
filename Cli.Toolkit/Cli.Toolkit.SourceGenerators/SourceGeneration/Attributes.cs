using TomRR.Cli.Toolkit.Generators.AttributeGenerators;

namespace TomRR.Cli.Toolkit.SourceGeneration;

[Generator]
public class Attributes : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        ArgumentAttributeGenerator.Generate(context);
        CommandAttributeGenerator.Generate(context);
        OptionAttributeGenerator.Generate(context);
    }
}