using MathCLI.MathExpression.ExpressionCompiler;
using MathCLI.MathExpression.ExpressionReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel
{
    public class Kernel
    {
        private VariableContext _variableContext;

        public Kernel(VariableContext variableContext)
        {
            _variableContext = variableContext;
        }


        public string Calc(string str)
        {
            ReadResult readResult = ExpressionReader.Read(str);

            if (readResult.UnknownTokens.Any())
                Console.WriteLine(Format(str, readResult.UnknownTokens));

            var buider = new ExpressionCompiler();
            var tree = buider.CompileDescent(readResult.Tokens);

            return tree.Execute(_variableContext).ToString();
        }


        public static string Format(string input, IReadOnlyList<Token> unknown)
        {
            string snippet = input.Replace('\t', ' ');
            string caretLine = string.Empty;

            foreach (var unk in unknown)
            {
                int caretOffset = unk.Position - unk.Length - caretLine.Length + 1;
                caretLine += new string(' ', caretOffset) + new string('~', unk.Length);
            }

            return $"""
            Error: Unknown tokens
              {snippet}
              {caretLine}
            """;
        }
    }
}
