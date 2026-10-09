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


        public string[] Solve(string expression, VariableContext context, SolverMode mode=SolverMode.Value)
        {
            ReadResult read = _reader.Read(expression);
            ITerm term = _compiler.CompileDescent(read.Tokens);

            if (mode == SolverMode.Value)
                return [term.Execute(context).GetReduced().ToString()];

            var steps = new List<string>() { term.ToString() };
            while(!term.IsValue)
            {
                term = term.ReduceStep(context);
                steps.Add(term.ToString());
            }

            if (term.Execute(context).TryGetReduced(out var reduced))
                steps.Add(reduced.ToString());

            return steps.ToArray();
        }
    }
}
