using TomRR.Cli.Toolkit.CommandBinders.CommandBinder.Models;

namespace TomRR.Cli.Toolkit.CommandBinders.CommandBinder;

public static class CommandBinderEmitter
{
    // --- CORRECTED: Use fully qualified names ---
    // (I'm assuming CommandAttribute is in the same namespace)
    private const string CommandAttributeName = $"{Constance.NamespaceBase}.Attributes.CommandAttribute";
    private const string OptionAttributeName = $"{Constance.NamespaceBase}.Attributes.OptionAttribute";
    private const string ArgumentAttributeName = $"{Constance.NamespaceBase}.Attributes.ArgumentAttribute";

    public static void Emit(IncrementalGeneratorInitializationContext context)
    {
        // 1. FIND classes and TRANSFORM them into our semantic model
        IncrementalValuesProvider<CommandToGenerate> commandModels = context.SyntaxProvider
            .CreateSyntaxProvider(
                // Predicate: Fast syntax filter for classes with attributes
                predicate: static (s, _) => s is ClassDeclarationSyntax c && c.AttributeLists.Count > 0,
                
                // Transform: Get the SemanticModel and build our CommandToGenerate
                transform: static (ctx, cancellationToken) => 
                {
                    var semanticModel = ctx.SemanticModel; 
                    var classSyntax = (ClassDeclarationSyntax)ctx.Node;
                    
                    // Call our helper directly from inside the transform
                    return GetCommandToGenerate(semanticModel, classSyntax); 
                })
            .Where(static m => m is not null) // Filter out classes that weren't valid commands
            .Select(static (m, _) => m!)     // Get the non-nullable version
            .WithTrackingName("CommandModels");

        // 2. REGISTER the source output
        context.RegisterSourceOutput(commandModels, static (spc, command) =>
        {
            GenerateBinder(spc, command);
        });
    }

    // This is the core semantic logic
    private static CommandToGenerate? GetCommandToGenerate(SemanticModel semanticModel, ClassDeclarationSyntax classSyntax)
    {
        // Get the symbol for the class
        ISymbol? symbol = semanticModel.GetDeclaredSymbol(classSyntax);

        // Safely cast it to a "named type" (like a class or struct)
        if (symbol is not INamedTypeSymbol classSymbol)
            return null;

        // --- CORRECTED: Find by full name ---
        AttributeData? commandAttribute = classSymbol.GetAttributes()
            .FirstOrDefault(a => GetFullName(a.AttributeClass) == CommandAttributeName);
        
        if (commandAttribute is null || commandAttribute.ConstructorArguments.Length == 0)
            return null;

        // --- Create the Command Model ---
        string commandName = GetAttributeValue(commandAttribute.ConstructorArguments[0]) ?? "UnknownCommand";
        string ns = classSymbol.ContainingNamespace.ToDisplayString();
        string className = classSymbol.Name;
        var commandToGenerate = new CommandToGenerate(ns, className, commandName);

        var allArguments = new List<ArgumentToGenerate>();

        // --- Find All Options and Arguments ---
        foreach (var member in EnumerateSelfAndBaseProperties(classSymbol))
        {
            foreach (var attr in member.GetAttributes())
            {
                // --- CORRECTED: Get full name for comparison ---
                string attrFullName = GetFullName(attr.AttributeClass);

                // Is it an [Option]?
                if (attrFullName == OptionAttributeName && attr.ConstructorArguments.Length > 0)
                {
                    string longName = GetAttributeValue(attr.ConstructorArguments[0])!;
                    string? shortName = (attr.ConstructorArguments.Length > 1) ? GetAttributeValue(attr.ConstructorArguments[1]) : null;
                    
                    var option = new OptionToGenerate(
                        propName: member.Name,
                        propType: member.Type.ToDisplayString(),
                        longName: longName,
                        shortName: shortName
                    );
                    commandToGenerate.Options.Add(option);
                    break; // Move to the next property
                }

                // ...
                // Is it an [Argument]?
                if (attrFullName == ArgumentAttributeName && attr.ConstructorArguments.Length > 0)
                {
                    // The first argument is now 'ofOption' (string)
                    string ofOption = GetAttributeValue(attr.ConstructorArguments[0]) ?? "Unknown";

                    // The second argument is now 'position' (int), check for its presence
                    int position = 0;
                    if (attr.ConstructorArguments.Length > 1)
                    {
                        object? posValue = attr.ConstructorArguments[1].Value;
                        if (posValue is int i)
                        {
                            position = i;
                        }
                    }

                    var argument = new ArgumentToGenerate(
                        propName: member.Name,
                        propType: member.Type.ToDisplayString(),
                        pos: position,
                        ofOption: ofOption
                    );
                    allArguments.Add(argument);
                    break; // Move to the next property
                }
            }
        }

        // --- Link Arguments to their Options ---
        foreach (var arg in allArguments)
        {
            var parentOption = commandToGenerate.Options.FirstOrDefault(o => o.PropertyName == arg.OfOption);
            parentOption?.Arguments.Add(arg);
        }

        // Sort the arguments for each option by position
        foreach (var option in commandToGenerate.Options)
        {
            option.Arguments.Sort((a, b) => a.Position.CompareTo(b.Position));
        }

        return commandToGenerate;
    }
    
    // --- NEW: Helper to get full metadata name ---
    private static string GetFullName(INamedTypeSymbol? symbol)
    {
        return symbol?.ToDisplayString() ?? string.Empty;
    }

    // Helper to safely get string values from attribute arguments
    private static string? GetAttributeValue(TypedConstant constant)
    {
        if (constant.Kind == TypedConstantKind.Error) return null;
        if (constant.Kind == TypedConstantKind.Primitive && constant.Value is string s)
        {
            return s;
        }
        return constant.Value?.ToString();
    }

    // --- (This part is unchanged from before) ---
    private static void GenerateBinder(SourceProductionContext spc, CommandToGenerate command)
    {
        var builder = new StringBuilder();

        builder.AppendLine("//<auto-generated/>");
        builder.AppendLine("#nullable enable");
        builder.AppendLine();
        // Use the command's namespace, or a hardcoded one if you prefer
        builder.AppendLine($"namespace {Constance.NamespaceBaseSourceGenerator}.CommandBinders;"); 
        builder.AppendLine();
        builder.AppendLine($"public static class {command.ClassName}_Binder");
        builder.AppendLine("{");
        builder.AppendLine($"    public static void Bind(global::{command.Namespace}.{command.ClassName} cmd, string[] args)");
        builder.AppendLine("    {");
        builder.AppendLine("        for (var i = 0; i < args.Length; i++)");
        builder.AppendLine("        {");
        builder.AppendLine("            var token = args[i];");
        builder.AppendLine("            switch (token)");
        builder.AppendLine("            {");

        // Generate cases for each option
        foreach (var option in command.Options)
        {
            builder.AppendLine($"                case \"{option.LongName}\":");
            if (!string.IsNullOrEmpty(option.ShortName))
            {
                builder.AppendLine($"                case \"{option.ShortName}\":");
            }
            
            // Set the option's flag (e.g., cmd.Interactive = true;)
            builder.AppendLine($"                    cmd.{option.PropertyName} = true;");

            // Generate argument parsing
            if (option.Arguments.Count > 0)
            {
                builder.AppendLine(); 
                builder.AppendLine($"                    // --- Parse {option.Arguments.Count} arg(s) for {option.LongName} ---");
                foreach (var arg in option.Arguments) // Already sorted
                {
                    builder.AppendLine($"                    if (TryGetNext(args, ref i, out var arg{arg.Position}_{arg.PropertyName}))");
                    builder.AppendLine("                    {");
                    // TODO: Add type conversion if arg.PropertyType isn't string
                    builder.AppendLine($"                        cmd.{arg.PropertyName} = arg{arg.Position}_{arg.PropertyName};");
                    builder.AppendLine("                    }");
                    // builder.AppendLine("                    else");
                    // builder.AppendLine("                    {");
                    // builder.AppendLine($"                        // Optional: Handle missing argument for {arg.PropertyName}");
                    // builder.AppendLine("                    }");
                }
            }
            
            builder.AppendLine("                    break;");
            builder.AppendLine(); 
        }

        builder.AppendLine("                default:");
        builder.AppendLine("                    // Optional: log unknown flag");
        builder.AppendLine("                    break;");
        builder.AppendLine("            }");
        builder.AppendLine("        }");
        builder.AppendLine("    }");
        builder.AppendLine();
        
        // Add the helper method
        builder.AppendLine($"{TryGetNextCode}");
        
        builder.AppendLine("}"); // End class

        spc.AddSource($"{command.ClassName}_Binder.g.cs", builder.ToString());
    }
    
    private static IEnumerable<IPropertySymbol> EnumerateSelfAndBaseProperties(INamedTypeSymbol type)
    {
        for (INamedTypeSymbol? t = type; 
             t != null && t.SpecialType != SpecialType.System_Object; 
             t = t.BaseType)
        {
            foreach (var p in t.GetMembers().OfType<IPropertySymbol>())
                yield return p;
        }
    }
    
    public static string TryGetNextCode = $@"

private static bool TryGetNext(string[] args, ref int index, out string value)
{{
    value = string.Empty;
    if (index + 1 >= args.Length)
    {{
        return false;
    }}
        
    var next = args[index + 1];
    if (next.StartsWith(""-""))
    {{
        return false;
    }}
        
    value = next;
    index++; // advance since we consumed it
    return true;
}}
";
}

// --- (Your models: CommandToGenerate, OptionToGenerate, ArgumentToGenerate) ---
// (These should be defined either in the same file or another file in the generator project)