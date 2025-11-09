namespace TomRR.Cli.Toolkit.Commands;

public sealed class CommandClassModel
{
    public string Namespace { get; set; } = "";
    public string ClassName { get; set; } = "";
    public string CommandName { get; set; } = "";
    public string[]? CommandShortNames { get; set; }
    public string? CommandDescription { get; set; }

    public CommandClassModel()
    { }
    public CommandClassModel(
        string @namespace,
        string className,
        string commandName,
        string[]? commandShortNames = null,
        string? commandDescription = null)
    {
        Namespace = @namespace;
        ClassName = className;
        CommandName = commandName;
        CommandShortNames = commandShortNames;
        CommandDescription = commandDescription;
    }
}