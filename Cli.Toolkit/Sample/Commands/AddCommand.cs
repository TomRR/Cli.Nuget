namespace TomRR.Cli.Tooling.Sample.Commands;

[Command]
public sealed partial class AddCommand : ICommand
{
    [Option("--interactive", "-i", hasValue: false)]
    public bool Interactive { get; set; }
    
    [Argument(ofOption: nameof(Interactive),5)]
    public string? InteractiveArg { get; set; }
    public Task RunAsync()
    {
        throw new System.NotImplementedException();
    }
    
    [Option("--set-upstream", hasValue: false)]
    public bool SetUpstream { get; set; }

    [Argument(ofOption: nameof(SetUpstream),0)]
    public string? UpstreamRemote { get; set; }

    [Argument(ofOption: nameof(SetUpstream), 1)]
    public string? UpstreamBranch { get; set; }
}