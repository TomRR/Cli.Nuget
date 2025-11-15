namespace TomRR.Cli.Toolkit._Shared;

public static class AttributeParser
{
    private const string CommandAttributeName = $"{Constance.NamespaceBase}.Attributes.CommandAttribute";
    private const string OptionAttributeName = $"{Constance.NamespaceBase}.Attributes.OptionAttribute";
    private const string ArgumentAttributeName = $"{Constance.NamespaceBase}.Attributes.ArgumentAttribute";
    
    // This is the core semantic logic
    public static CommandToGenerate? GetCommandToGenerate(SemanticModel semanticModel, ClassDeclarationSyntax classSyntax)
    {
        // Get the symbol for the class
        ISymbol? symbol = semanticModel.GetDeclaredSymbol(classSyntax);

        // Safely cast it to a "named type" (like a class or struct)
        if (symbol is not INamedTypeSymbol classSymbol)
            return null;

        // --- CORRECTED: Find by full name ---
        AttributeData? commandAttribute = classSymbol.GetAttributes()
            .FirstOrDefault(a => GetFullName(a.AttributeClass) == CommandAttributeName);
        
        if (commandAttribute is null) // Attribute must exist
            return null;

        // --- Extracted Command Data ---
        // Argument [0]: Name (string), optional, falls back to class name
        string commandName = GetAttributeValue(commandAttribute.ConstructorArguments.ElementAtOrDefault(0)) ?? classSymbol.Name;

        // ADDED: Argument [1]: Description (string), optional, falls back to empty string
        string description = GetAttributeValue(commandAttribute.ConstructorArguments.ElementAtOrDefault(1)) ?? string.Empty;

        // ADDED: Argument [2]: ShortNames (string[] params array), optional
        List<string> shortNames = new();
        if (commandAttribute.ConstructorArguments.Length > 2)
        {
            TypedConstant shortNamesConstant = commandAttribute.ConstructorArguments[2];
            if (shortNamesConstant.Kind == TypedConstantKind.Array)
            {
                shortNames.AddRange(
                    shortNamesConstant.Values
                        .Select(GetAttributeValue)
                        .Where(s => s is not null)
                        .Cast<string>()
                );
            }
        }
        
        // --- Create the Command Model ---
        string ns = classSymbol.ContainingNamespace.ToDisplayString();
        string className = classSymbol.Name;
        
        // UPDATED: New constructor signature used
        var commandToGenerate = new CommandToGenerate(ns, className, commandName, description, shortNames);

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
}