// namespace Roo.Cli.Core.CommandBinders;
//
// public static class CommitCommand_Binder
// {
//     public static void Bind(CommitCommand cmd, string[] args)
//     {
//         for (var i = 0; i < args.Length; i++)
//         {
//             var token = args[i];
//
//             switch (token)
//             {
//                 case "--interactive":
//                 case "-i":
//                     cmd.Interactive = true;
//                     break;
//                 
//                 case "--select":
//                 case "-s":
//                     cmd.Select = true;
//                     if (TryGetNext(args, ref i, out var repo))
//                     {
//                         cmd.SelectedRepositoryName = repo;
//                     }                    
//                     break;
//
//                 case "--amend":
//                     cmd.Amend = true;
//                     break;
//
//                 case "--message":
//                 case "-m":
//                     cmd.Message = true;
//                     if (TryGetNext(args, ref i, out var message))
//                     {
//                         cmd.MessageArg = message;
//                     }
//                     break;
//
//                 // --- Unknown tokens ---
//                 default:
//                     // Optional: log unknown flag for debugging
//                     // Console.WriteLine($"Unknown option: {token}");
//                     break;
//             }
//         }
//     }
//     
//     private static bool TryGetNext(string[] args, ref int index, out string value)
//     {
//         value = string.Empty;
//         if (index + 1 >= args.Length)
//         {
//             return false;
//         }
//         var next = args[index + 1];
//         if (next.StartsWith("-"))
//         {
//             return false;
//         }
//         value = next;
//         index++; // advance since we consumed it
//         return true;
//     }
// }
