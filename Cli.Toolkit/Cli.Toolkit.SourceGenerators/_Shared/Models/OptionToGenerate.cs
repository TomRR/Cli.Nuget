namespace TomRR.Cli.Toolkit._Shared.Models;

public class OptionToGenerate
{
    public string PropertyName { get; }
    public string PropertyType { get; } 
    public string LongName { get; }
    public string? ShortName { get; }
    public List<ArgumentToGenerate> Arguments { get; } = new List<ArgumentToGenerate>();

    public OptionToGenerate(string propName, string propType, string longName, string? shortName)
    {
        PropertyName = propName;
        PropertyType = propType;
        LongName = longName;
        ShortName = shortName;
    }
}