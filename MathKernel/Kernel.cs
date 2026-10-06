using MathCLI.MathExpression.ExpressionReader;
using MathCLI.MathExpression.ExpressionTokenReader;
using MathCLI.Term.ExpressionBuilder;
using MathCLI.Term.ExpressionStringReader;
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

            var buider = new ExpressionCompiler();
            var tree = buider.CompileDescent(readResult.Tokens);

            return tree.Execute(_variableContext).ToString();
        }
    }
}
