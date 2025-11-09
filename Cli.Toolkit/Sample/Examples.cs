
// using Entities;

namespace TomRR.Cli.Tooling.Sample;


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

public class Examples
{
    // Create generated entities, based on DDD.UbiquitousLanguageRegistry.txt
    public object[] CreateEntities()
    {
        return new object[]
        {
            // new Customer(),
            // new Employee(),
            // new Product(),
            // new Shop(),
            // new Stock()
        };
    }

    // Execute generated method Report
    // public IEnumerable<string> CreateEntityReport(SampleEntity entity)
    // {
    //     return entity.Report();
    // }
}