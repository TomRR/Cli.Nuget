namespace TomRR.Cli.Tooling.Sample.Commands;

[Command(name: nameof(sn), description: "pull description for testing")]
public sealed partial class PushCommand : ICommand
{
    private const string sn = "test";
    public Task RunAsync()
    {
        throw new System.NotImplementedException();
    }
    [Option("-zed", hasValue: false)]
    public bool Zed { get; set; }
    [Option("--interactive", "-i", hasValue: false)]
    public bool Interactive { get; set; }
    [Option("--added", hasValue: false)]
    public bool Added { get; set; }
    
    [Option("--select", "-s", hasValue: false)]
    public bool Select { get; set; }
    
    [Argument(ofOption: nameof(Select), 0)]
    public string? SelectedRepositoryName { get; set; }
    
    [Option("--force", "-f", hasValue: false, description: "Force push")]
    public bool Force { get; set; }
    
    [Option("--set-upstream", hasValue: false)]
    public bool SetUpstream { get; set; }
    
    [Argument(ofOption: nameof(SetUpstream), 0)]
    public string? UpstreamRemote { get; set; }
    
    [Argument(ofOption: nameof(SetUpstream), 1)]
    public string? UpstreamBranch { get; set; }
}