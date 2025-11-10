namespace TomRR.Cli.Toolkit.CommandBinders.Models;

public class CommandToGenerate
{
    public string Namespace { get; }
    public string ClassName { get; }
    public string CommandName { get; }
    public List<OptionToGenerate> Options { get; } = new List<OptionToGenerate>();

    public CommandToGenerate(string ns, string className, string commandName)
    {
        Namespace = ns;
        ClassName = className;
        CommandName = commandName;
    }
}