using MathCLI.MathKernel.Terms;
using MathCLI.MathKernel.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.TermVisitors
{
    public class SubstituteVisitor : ITermVisitor
    {
        private VariableContext _variables;

        public SubstituteVisitor(VariableContext variables)
        {
            _variables = variables;
        }


        public ITerm Visit(Fraction fraction) => fraction;
        public ITerm Visit(Variable variable) => _variables.TryGetVariable(variable.Name, out var fraction) ? fraction! : variable;

        public ITerm Visit(NegateTerm negate)
        {
            var term = negate.Term.Accept(this);

            return new NegateTerm(term);
        }

        public ITerm Visit(Addition addition)
        {
            var left = addition.Args[0].Accept(this);
            var right = addition.Args[1].Accept(this);

            return new Addition(left, right);
        }

        public ITerm Visit(Subtraction subtraction)
        {
            var left = subtraction.Args[0].Accept(this);
            var right = subtraction.Args[1].Accept(this);

            return new Subtraction(left, right);
        }

        public ITerm Visit(Multiplication multiplication)
        {
            var left = multiplication.Args[0].Accept(this);
            var right = multiplication.Args[1].Accept(this);

            return new Multiplication(left, right);
        }

        public ITerm Visit(Division division)
        {
            var left = division.Args[0].Accept(this);
            var right = division.Args[1].Accept(this);

            return new Division(left, right);
        }

        public ITerm Visit(Power power)
        {
            var left = power.Args[0].Accept(this);
            var right = power.Args[1].Accept(this);

            return new Power(left, right);
        }
    }
}
