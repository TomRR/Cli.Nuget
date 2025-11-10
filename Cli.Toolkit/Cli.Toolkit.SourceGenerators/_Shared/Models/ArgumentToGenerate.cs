namespace TomRR.Cli.Toolkit._Shared.Models;

public class ArgumentToGenerate
{
    public string PropertyName { get; }
    public string PropertyType { get; }
    public int Position { get; }
    public string OfOption { get; } 

    public ArgumentToGenerate(string propName, string propType, int pos, string ofOption)
    {
        PropertyName = propName;
        PropertyType = propType;
        Position = pos;
        OfOption = ofOption;
    }
}