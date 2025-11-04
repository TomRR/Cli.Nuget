// namespace Roo.Cli.Core.CommandBinders;
//
// public static class InitCommand_Binder
// {
//     public static void Bind(InitCommand cmd, string[] args)
//     {
//         var position = 0;
//         for (var i = 0; i < args.Length; i++)
//         {
//             var token = args[i];
//
//             switch (token)
//             {
//                 case "--path":
//                 case "-p":
//                     if (i + 1 < args.Length)
//                     {
//                         cmd.FilePath = args[++i];
//                     }
//                     else
//                     {
//                         throw new ArgumentException("Option --path requires a value");
//                     }
//                     break;
//
//                 case "--force":
//                 case "-f":
//                     cmd.Force = true;
//                     break;
//
//                 default:
//                     if (position == 0) cmd.Template = token;
//                     else throw new ArgumentException($"Unexpected argument: {token}");
//                     position++;
//                     break;
//             }
//         }
//     }
// }
