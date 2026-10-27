using MathCLI.Extensions;
using MathCLI.MathKernel;
using MathCLI.MathKernel.Terms;
using MathCLI.MathKernel.Variables;
using MathCLI.MathSyntax.ExpressionCompiler;
using MathCLI.MathSyntax.ExpressionReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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


        public string[] Solve(string expression, VariableContext variables, SolverMode mode = SolverMode.Value)
        {
            ReadResult read = _reader.Read(expression);
            Term term = _compiler.CompileDescent(read.Tokens);

            var result = term.Substitute(variables).Simplify();

            return [TermRender.Render(result)];
        }
    }
}
