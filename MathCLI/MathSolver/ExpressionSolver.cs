using MathCLI.MathSyntax.ExpressionCompiler;
using MathCLI.MathSyntax.ExpressionReader;
using MathCLI.MathKernel;
using MathCLI.MathKernel.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathCLI.MathKernel.TermVisitors;
using MathCLI.Extensions;

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


        public string[] Solve(string expression, VariableContext context, SolverMode mode = SolverMode.Value)
        {
            ReadResult read = _reader.Read(expression);
            ITerm term = _compiler.CompileDescent(read.Tokens);

            return [term.Simplify().ToString()];
        }
    }
}
