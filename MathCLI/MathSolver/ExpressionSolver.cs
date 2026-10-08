using MathCLI.MathSyntax.ExpressionCompiler;
using MathCLI.MathSyntax.ExpressionReader;
using MathCLI.MathKernel;
using MathCLI.MathKernel.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathSolver
{
    public class ExpressionSolver
    {
        private ExpressionCompiler _compiler;
        private ExpressionReader _reader;


        public ExpressionSolver(ExpressionCompiler compiler, ExpressionReader reader)
        {
            _compiler = compiler;
            _reader = reader;
        }


        public Fraction Solve(string expression, VariableContext context)
        {
            ReadResult read = _reader.Read(expression);
            ITerm term = _compiler.CompileDescent(read.Tokens);

            return term.Execute(context);
        }
    }
}
