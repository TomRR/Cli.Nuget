namespace TomRR.Cli.Tooling.Sample.Commands;

[Command(description: "pull description for testing")]
public sealed partial class PullCommand : ICommand
{
    [Option("--interactive", "-i", hasValue: false)]
    public bool Interactive { get; set; }
    public Task RunAsync()
    {
        throw new System.NotImplementedException();
    }
    [Argument(ofOption: nameof(Interactive), 0)]
    public string? UpstreamRemote { get; set; }
}