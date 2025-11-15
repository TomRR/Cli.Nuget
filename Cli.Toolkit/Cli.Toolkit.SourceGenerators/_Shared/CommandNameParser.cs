namespace TomRR.Cli.Toolkit._Shared;

public static class CommandNameParser
{
    public static string NormalizeCommandName(string typeName)
    {
        const string suffix = "Command";

        if (typeName.EndsWith(suffix, StringComparison.Ordinal))
        {
            typeName = typeName.Substring(0, typeName.Length - suffix.Length);
        }

        return typeName.ToLowerInvariant();
    }
}