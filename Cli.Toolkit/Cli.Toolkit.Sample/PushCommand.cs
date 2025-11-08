using System.Threading.Tasks;
using Cli.Toolkit.SourceGenerators;
using Cli.Toolkit.SourceGenerators.Attributes;

namespace TomRR.Cli.Tooling.Sample;

[Command(name: nameof(sn), description: "pull description for testing")]
public class PushCommand : ICommand
{
    private const string sn = "test";
    public Task RunAsync()
    {
        throw new System.NotImplementedException();
    }
    [Option("--interactive", "-i", hasValue: false)]
    public bool Interactive { get; set; }
    //
    // [Option("--select", "-s", hasValue: false)]
    // public bool Select { get; set; }
    //
    // [Argument(0, ofOption: nameof(Select))]
    // public string? SelectedRepositoryName { get; set; }
    //
    // [Option("--force", "-f", hasValue: false, description: "Force push")]
    // public bool Force { get; set; }
    //
    // [Option("--set-upstream", hasValue: false)]
    // public bool SetUpstream { get; set; }
    //
    // [Argument(0, ofOption: nameof(SetUpstream))]
    // public string? UpstreamRemote { get; set; }
    //
    // [Argument(1, ofOption: nameof(SetUpstream))]
    // public string? UpstreamBranch { get; set; }
}