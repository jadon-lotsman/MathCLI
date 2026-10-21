using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using MathCLI.MathKernel.Variables;
using MathCLI.MathNumerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.TermVisitors
{
    public class SubstituteVisitor : ITermVisitor<Term>
    {
        private readonly VariableContext _variables;

        public SubstituteVisitor(VariableContext variables)
        {
            _variables = variables;
        }


        public Term Visit(Number number) => number;
        public Term Visit(Variable variable) => _variables.TryGetVariable(variable.Name, out var fraction) ? new Number(fraction!) : variable;

        public Term Visit(NegateTerm negate)
        {
            var term = negate.InnerTerm.Accept(this);

            return new NegateTerm(term);
        }

        public Term Visit(Addition addition)
        {
            var left = addition.Args[0].Accept(this);
            var right = addition.Args[1].Accept(this);

            return new Addition(left, right);
        }

        public Term Visit(Subtraction subtraction)
        {
            var left = subtraction.Args[0].Accept(this);
            var right = subtraction.Args[1].Accept(this);

            return new Subtraction(left, right);
        }

        public Term Visit(Multiplication multiplication)
        {
            var left = multiplication.Args[0].Accept(this);
            var right = multiplication.Args[1].Accept(this);

            return new Multiplication(left, right);
        }

        public Term Visit(Division division)
        {
            var left = division.Args[0].Accept(this);
            var right = division.Args[1].Accept(this);

            return new Division(left, right);
        }

        public Term Visit(Power power)
        {
            var left = power.Args[0].Accept(this);
            var right = power.Args[1].Accept(this);

            return new Power(left, right);
        }
    }
}
