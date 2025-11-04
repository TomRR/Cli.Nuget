// namespace Roo.Cli.Core.CommandBinders;
//
// public static class PushCommand_Binder
// {
//     public static void Bind(PushCommand cmd, string[] args)
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
//                 case "--force":
//                 case "-f":
//                     cmd.Force = true;
//                     break;
//                 
//                 // --- Option with 1 argument ---
//                 case "--select":
//                 case "-s":
//                     cmd.Select = true;
//                     if (TryGetNext(args, ref i, out var repo))
//                         cmd.SelectedRepositoryName = repo;
//                     break;
//
//                 // --- Option with 2 arguments ---
//                 case "--set-upstream":
//                     cmd.SetUpstream = true;
//                     if (TryGetNext(args, ref i, out var remote))
//                     {
//                         cmd.UpstreamRemote = remote;
//                     }
//                     if (TryGetNext(args, ref i, out var branch))
//                     {
//                         cmd.UpstreamBranch = branch;
//
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
//         
//         var next = args[index + 1];
//         if (next.StartsWith("-"))
//         {
//             return false;
//         }
//         
//         value = next;
//         index++; // advance since we consumed it
//         return true;
//     }
// }
