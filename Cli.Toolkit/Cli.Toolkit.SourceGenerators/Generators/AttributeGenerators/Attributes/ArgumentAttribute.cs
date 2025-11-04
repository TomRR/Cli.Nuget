namespace TomRR.Cli.Toolkit.Generators.AttributeGenerators.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
internal sealed class ArgumentAttribute : Attribute
{
    /// <summary>
    /// The name of the option property this argument belongs to (if any).
    /// Use nameof(OptionProperty) to keep it type-safe.
    /// </summary>
    public string OfOption { get; }
    /// <summary>
    /// Position of this argument relative to the command or its corresponding option.
    /// </summary>
    public int Position { get; }
    public ArgumentAttribute(string ofOption, int position = 0)
    {
        OfOption = ofOption;
        Position = position;
    }
}