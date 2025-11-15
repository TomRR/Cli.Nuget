namespace TomRR.Cli.Toolkit._Shared.Models;

public class CommandToGenerate
{
    public string Namespace { get; }
    public string ClassName { get; }
    public string CommandName { get; }
    // ADDED: Property for the command's description
    public string Description { get; }
    // ADDED: Property for the array of short names
    public List<string>? ShortNames { get; }
    public List<OptionToGenerate> Options { get; } = new List<OptionToGenerate>();

    // UPDATED: Constructor now accepts Description and ShortNames
    public CommandToGenerate(string ns, string className, string commandName, string description, IEnumerable<string> shortNames)
    {
        Namespace = ns;
        ClassName = className;
        CommandName = commandName;
        Description = description;
        ShortNames = new List<string>(shortNames);
    }
}