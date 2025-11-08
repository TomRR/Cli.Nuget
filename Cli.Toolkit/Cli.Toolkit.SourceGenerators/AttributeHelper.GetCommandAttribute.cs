
using TomRR.Cli.Toolkit.Generators.AttributeGenerators.Attributes;

namespace TomRR.Cli.Toolkit;

public static partial class AttributeHelper
{
    public static (string Namespace, string ClassName, string CommandName,string[]? CommandShortNames, string? CommandDescription)? GetCommandAttribute(
        GeneratorSyntaxContext context)
    {
        if (context.Node is not ClassDeclarationSyntax classSyntax)
            return null;

        foreach (var attrList in classSyntax.AttributeLists)
        {
            foreach (var attr in attrList.Attributes)
            {
                var symbol = context.SemanticModel.GetSymbolInfo(attr).Symbol;
                if (symbol is not IMethodSymbol methodSymbol)
                    continue;

                var attrType = methodSymbol.ContainingType;
                var attrName = attrType.ToDisplayString();

                if (!attrName.EndsWith(".Command") && !attrName.EndsWith(".CommandAttribute"))
                    continue;

                if (context.SemanticModel.GetDeclaredSymbol(classSyntax) is not INamedTypeSymbol classSymbol)
                    return null;
  
                // Default values
                string commandName = classSymbol.Name;
                if (commandName.EndsWith("Command", StringComparison.Ordinal))
                {
                    commandName = commandName.Substring(0, commandName.Length - "Command".Length);
                }

                commandName = commandName.ToLowerInvariant();

                string? description = null;
                List<string> shortNames = new();

                var ctorSymbol = context.SemanticModel.GetSymbolInfo(attr).Symbol as IMethodSymbol;
                if (ctorSymbol == null) continue;

                var args = attr.ArgumentList?.Arguments ?? default;
                for (int i = 0; i < args.Count; i++)
                {
                    var arg = args[i];
                    var constValue = GetConst(arg.Expression, context.SemanticModel);
                    if (constValue is null) continue;

                    // Get the parameter name from the constructor symbol
                    var paramName = i < ctorSymbol.Parameters.Length
                        ? ctorSymbol.Parameters[i].Name
                        : null;

                    if (paramName == null) continue;

                    switch (paramName)
                    {
                        case nameof(CommandAttribute.Name):
                            if (constValue is string s && !string.IsNullOrWhiteSpace(s))
                                commandName = s;
                            break;
                        case nameof(CommandAttribute.Description):
                            if (constValue is string d && !string.IsNullOrWhiteSpace(d))
                                description = d;
                            break;
                        case nameof(CommandAttribute.ShortNames):
                            if (constValue is string sn && !string.IsNullOrWhiteSpace(sn))
                                shortNames.Add(sn);
                            break;
                    }
                }

// --- Read arguments ---
if (attr.ArgumentList != null)
{
    foreach (var arg in attr.ArgumentList.Arguments)
    {
        var constValue = context.SemanticModel.GetConstantValue(arg.Expression);
        if (!constValue.HasValue) continue;

        if (arg.NameEquals != null)
        {
            // Named argument
            switch (arg.NameEquals.Name.Identifier.Text)
            {
                case nameof(CommandAttribute.Name):
                    if (constValue.Value is string s && !string.IsNullOrWhiteSpace(s))
                        commandName = s;
                    break;
                case nameof(CommandAttribute.Description):
                    if (constValue.Value is string d && !string.IsNullOrWhiteSpace(d))
                        description = d;
                    break;
                case nameof(CommandAttribute.ShortNames):
                    if (constValue.Value is string sn && !string.IsNullOrWhiteSpace(sn))
                        shortNames.Add(sn);
                    break;
            }
        }
        else
        {
            // Positional argument (order: Name, Description, ShortNames...)
            int index = attr.ArgumentList.Arguments.IndexOf(arg);
            switch (index)
            {
                case 0:
                    if (constValue.Value is string s && !string.IsNullOrWhiteSpace(s))
                        commandName = s;
                    break;
                case 1:
                    if (constValue.Value is string d && !string.IsNullOrWhiteSpace(d))
                        description = d;
                    break;
                default:
                    if (constValue.Value is string sn && !string.IsNullOrWhiteSpace(sn))
                        shortNames.Add(sn);
                    break;
            }
        }
    }
}




                return (
                    Namespace: classSymbol.ContainingNamespace.ToDisplayString(),
                    ClassName: classSymbol.Name,
                    CommandName: commandName,
                    CommandShortNames: shortNames.Count > 0 ? shortNames.ToArray() : null,
                    CommandDescription: description
                    
                );
            }
        }

        return null;
    }
    
    
    static object? GetConst(ExpressionSyntax expr, SemanticModel model)
    {
        // 1) literal
        var cv = model.GetConstantValue(expr);
        if (cv.HasValue) return cv.Value;

        // 2) nameof(x) → "x"
        if (expr is InvocationExpressionSyntax inv
            && inv.Expression is IdentifierNameSyntax id
            && id.Identifier.Text == "nameof"
            && inv.ArgumentList.Arguments.Count == 1)
        {
            var arg = inv.ArgumentList.Arguments[0].Expression as IdentifierNameSyntax;
            if (arg != null) return arg.Identifier.Text;
        }

        // 3) x where x is const field / const local
        if (expr is IdentifierNameSyntax ident)
        {
            var symbol = model.GetSymbolInfo(ident).Symbol;

            if (symbol is IFieldSymbol fs && fs.HasConstantValue)
                return fs.ConstantValue;

            if (symbol is ILocalSymbol ls && ls.HasConstantValue)
                return ls.ConstantValue;
        }

        return null;
    }



}