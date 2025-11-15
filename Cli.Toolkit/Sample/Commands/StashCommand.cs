namespace TomRR.Cli.Tooling.Sample.Commands;

[Command("stash", "stash Command", "st", "staa")]
public sealed partial class StashCommand : ICommand
{
    [Option("--interactive", "-i", hasValue: false)]
    public bool Interactive { get; set; }
    public Task RunAsync()
    {
        throw new System.NotImplementedException();
    }
}