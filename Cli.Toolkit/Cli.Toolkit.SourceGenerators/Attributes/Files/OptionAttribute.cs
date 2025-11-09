namespace TomRR.Cli.Toolkit.Attributes.Files;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
internal sealed partial class OptionAttribute : Attribute
{
    public string LongName { get; }
    public string? ShortName { get; }
    public bool HasValue { get; }
    public string? Description { get; }

    public OptionAttribute(string longName, string? shortName = null, bool hasValue = true, string? description = null)
    {
        LongName = longName;
        ShortName = shortName;
        HasValue = hasValue;
        Description = description;
    }
}