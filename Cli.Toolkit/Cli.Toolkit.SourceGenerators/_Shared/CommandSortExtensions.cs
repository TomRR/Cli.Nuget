namespace TomRR.Cli.Toolkit._Shared;

public static class CommandSortExtensions
{
    public static ImmutableArray<CommandToGenerate> SortByName(this ImmutableArray<CommandToGenerate> commands)
    {
        if (commands.IsDefault) return ImmutableArray<CommandToGenerate>.Empty;

        // Use LINQ OrderBy with a StringComparer to guarantee ordinal casing rules
        return commands.OrderBy(c => c.CommandName, StringComparer.OrdinalIgnoreCase)
            .ToImmutableArray();
    }
}