using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using MathCLI.MathKernel.Variables;
using MathCLI.MathNumerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.TermVisitors
{
    public class RenderVisitor : ITermVisitor<string>
    {
        public RenderVisitor() { }

        public string Visit(Number number)  => number.Value.ToString();
        public string Visit(Variable variable) => variable.Name.ToString();


        public string Visit(NegateTerm negate)
            => RenderArgument(negate);
        public string Visit(Addition addition)
            => RenderArgument(addition);
        public string Visit(Subtraction subtraction)
            => RenderArgument(subtraction);
        public string Visit(Multiplication multiplication)
            => RenderArgument(multiplication);
        public string Visit(Division division)
            => RenderArgument(division);
        public string Visit(Power power)
            => RenderArgument(power);


        private string RenderArgument(ArgumentTerm term) => term.Notation switch
        {
            Notation.Function   => RenderFunction(term),
            Notation.Prefix     => RenderPrefix(term),
            Notation.Infix      => RenderInfix(term),
            Notation.Postfix    => RenderPostfix(term),
        };

        private string RenderFunction(ArgumentTerm term)
        {
            var sb = new StringBuilder();
            sb.Append(term.Name).Append('(');

            for (int i = 0; i < term.Args.Length; i++)
            {
                if (i > 0) sb.Append(", ");
                sb.Append(term.Args[i].Accept(this));
            }

            sb.Append(')');
            return sb.ToString();
        }

        private string RenderPrefix(ArgumentTerm term)
        {
            var inner = term.Args[0].Accept(this);
            bool needBrackets = term.Args[0].Precedence <= term.Precedence || inner.StartsWith("-");

            return needBrackets ? $"{term.Name}({inner})" : $"{term.Name}{inner}";
        }

        private string RenderInfix(ArgumentTerm term)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < term.Args.Length; i++)
            {
                if (i > 0)
                    sb.Append(term.Name);

                int minPrec = i == 0
                    ? term.Precedence + (term.IsRightAssoc ? 1 : 0)
                    : term.Precedence + (term.IsRightAssoc ? 0 : 1);

                var inner = term.Args[i].Accept(this);

                bool needParens = term.Args[i].Precedence < minPrec
                    || (i > 0 && inner.StartsWith("-"));

                sb.Append(needParens ? $"({inner})" : inner);
            }

            return sb.ToString();
        }

        private string RenderPostfix(ArgumentTerm term)
        {
            var inner = term.Args[0].Accept(this);
            bool needBrackets = term.Args[0].Precedence < term.Precedence;

            return needBrackets ? $"({inner}){term.Name}" : $"{inner}{term.Name}";
        }
    }
}
