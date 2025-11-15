namespace TomRR.Cli.Tooling.Sample.Commands;

[Command(name: nameof(Name), description: "pull description for testing")]
public sealed partial class WithBaseCommand : BaseCommand
{
    private const string Name = "WithBaseClassCommand";
    
    [Option("--added", hasValue: false)]
    public bool Added { get; set; }
    
    [Option("-zed", hasValue: false)]
    public bool Zed { get; set; }

}