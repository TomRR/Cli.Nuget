// using Roo.Cli.Core.CommandBinders;
//
// namespace Roo.Cli.Core;
//
// // Auto-generated at compile time
// internal static class CommandBinderRegistry
// {
//     public static async Task BindAndRun(ICommand cmd, string[] args)
//     {
//         switch (cmd)
//         {
//             case InitCommand command: 
//                 InitCommand_Binder.Bind(command, args);
//                 await command.RunAsync();
//                 break;
//
//             case StatusCommand command:
//                 StatusCommand_Binder.Bind(command, args);
//                 await command.RunAsync();
//                 break;
//             
//             case VersionCommand command:
//                 VersionCommand_Binder.Bind(command, args);
//                 await command.RunAsync();
//                 break;
//             
//             case HelpCommand command:
//                 HelpCommand_Binder.Bind(command, args);
//                 await command.RunAsync();
//                 break;
//             
//             case CloneCommand command:
//                 CloneCommand_Binder.Bind(command, args);
//                 await command.RunAsync();
//                 break;
//             
//             case PullCommand command:
//                 PullCommand_Binder.Bind(command, args);
//                 await command.RunAsync();
//                 break;
//             case PushCommand command:
//                 PushCommand_Binder.Bind(command, args);
//                 await command.RunAsync();
//                 break;
//             case FetchCommand command:
//                 FetchCommand_Binder.Bind(command, args);
//                 await command.RunAsync();
//                 break;
//             case CommitCommand command:
//                 CommitCommand_Binder.Bind(command, args);
//                 await command.RunAsync();
//                 break;
//             case AddCommand command:
//                 AddCommand_Binder.Bind(command, args);
//                 await command.RunAsync();
//                 break;
//
//             default:
//                 throw new InvalidOperationException(
//                     $"No binder generated for command type {cmd.GetType().FullName}");
//         }
//     }
// }
