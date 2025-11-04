namespace TomRR.Cli.Toolkit;

public partial class  CommandDispatcher
{
    private readonly IServiceProvider _provider;
    private readonly ICommandBinderRegistry _commandBinderRegistry;
    private readonly Dictionary<string, Type> _commands = new();

    public CommandDispatcher(IServiceProvider provider, ICommandBinderRegistry commandBinderRegistry)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        _commandBinderRegistry = commandBinderRegistry ?? throw new ArgumentNullException(nameof(commandBinderRegistry));
    }

    public void Register<TCommand>(string name, params string[] shortNames)
        where TCommand : class, ICommand
    {
        _commands[name] = typeof(TCommand);

        foreach (var shortName in shortNames.Where(s => !string.IsNullOrWhiteSpace(s)))
        {
            _commands[shortName!] = typeof(TCommand);
        }
    }

    public async Task DispatchAsync(string[] args)
    {
        await Task.CompletedTask;

        if (args.Length == 0)
        {
            Console.WriteLine("No command specified.");
            Console.WriteLine("Available commands: " + string.Join(", ", _commands.Keys));
            return;
        }
        

        var commandName = args[0];
        if (_commands.TryGetValue(commandName, out var type))
        {
            var cmd = (ICommand)_provider.GetRequiredService(type);
 
            var remaining = args.Skip(1).ToArray();
            await _commandBinderRegistry.BindAndRun(cmd, remaining);
            
        }
        else
        {
            Console.WriteLine($"Unknown command: {commandName}");
        }
        await Task.CompletedTask;

    }
}
