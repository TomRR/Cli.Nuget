namespace TomRR.Cli.Toolkit.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed partial class CommandAttribute : Attribute
{
    public string? Name { get; }
    public string?[] ShortNames { get; }
    public string? Description { get; }
    
    public CommandAttribute(string? name = null, string? description = null, params string?[] shortNames)
    {
        Name = name;
        ShortNames = shortNames;
        Description = description;
    }
}