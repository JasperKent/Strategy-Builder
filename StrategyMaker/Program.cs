using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace StrategyMaker
{
    class Program
    {
        static void BuildAssembly(SyntaxTree syntaxTree, string assemblyName)
        {
            List<MetadataReference> references = new List<MetadataReference>()
            {
               MetadataReference.CreateFromFile(Assembly.Load("System.Private.CoreLib").Location),
               MetadataReference.CreateFromFile(Assembly.Load("System.Runtime").Location),
               MetadataReference.CreateFromFile(Assembly.Load("RockPaperScissors.Basics").Location),
               MetadataReference.CreateFromFile(Assembly.Load("netstandard").Location),
               MetadataReference.CreateFromFile(Assembly.Load("System.Runtime.Extensions").Location)
            };

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                new[] { syntaxTree },
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                );

            using FileStream stream = new FileStream(Path.Combine("../../../../RockPaperScissors/bin/debug/netcoreapp3.1/Strategies", assemblyName), FileMode.Create);

            EmitResult result = compilation.Emit(stream);

            foreach (var diagnostic in result.Diagnostics)
                Console.WriteLine($"{diagnostic.Id}: {diagnostic.GetMessage()}");
        }

        static void Main()
        {
            Console.Write("Percentage Rock? ");
            int pcRock = int.Parse(Console.ReadLine());

            Console.Write("Percentage Paper? ");
            int pcPaper = int.Parse(Console.ReadLine());

            int pcScissors = 100 - pcRock - pcPaper;

            Console.WriteLine($"{pcRock}% Rock. {pcPaper}% Paper. {pcScissors}% Scissors.");

            Console.Write("What do you want to call your strategy? ");
            string name = Console.ReadLine();

            var builder = new StringBuilder();

            builder.AppendLine("using RockPaperScissors.Basics;");

            builder.AppendLine("namespace RockPaperScissors.Strategies");
            builder.AppendLine("{");
            builder.AppendLine($"    public class {name} : IRPSStrategy");
            builder.AppendLine("    {");
            builder.AppendLine("        public Sign Throw()");
            builder.AppendLine("        {");
            builder.AppendLine("            int val = IRPSStrategy._random.Next(100);");
            builder.AppendLine($"            if (val < {pcRock})");
            builder.AppendLine($"                return Sign.Rock;");
            builder.AppendLine($"            else if (val < {pcRock + pcPaper})");
            builder.AppendLine($"                return Sign.Paper;");
            builder.AppendLine($"            else ");
            builder.AppendLine($"                return Sign.Scissors;");
            builder.AppendLine("        }");
            builder.AppendLine("    }");
            builder.AppendLine("}");

            SyntaxTree tree = CSharpSyntaxTree.ParseText(builder.ToString());

            BuildAssembly(tree, $"{name}.dll");
        }
    }
}
