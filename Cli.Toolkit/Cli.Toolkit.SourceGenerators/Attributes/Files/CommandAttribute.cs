namespace TomRR.Cli.Toolkit.Attributes.Files;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
internal sealed partial class CommandAttribute : Attribute
{
    public string? Name { get; }
    public string?[] ShortNames { get; }
    public string? Description { get; }
    

    // public CommandAttribute() { }

    // public CommandAttribute(string? name = null, string? shortName = null, string? description = null,)
    // {
    //     Name = name;
    //     ShortNames = new[] { shortName };
    //     Description = description;
    // }
    public CommandAttribute(string? name = null, string? description = null, params string?[] shortNames)
    {
        Name = name;
        ShortNames = shortNames;
        Description = description;
    }
    
    
    // public ServiceLifetime? DependencyLifetime { get; }

    // public CommandAttribute(string? name = null, string? description = null, string? shortName = null, ServiceLifetime? dependencyLifetime = null)
    // {
    //     Name = name;
    //     ShortNames = new[] { shortName };
    //     Description = description;
    //     DependencyLifetime = dependencyLifetime;
    // }
}