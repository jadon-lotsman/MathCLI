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

namespace MathCLI.MathKernel
{
    public static class TermRender
    {
        public static string Render(Term term)
        {
            if (term is null) throw new ArgumentNullException(nameof(term));

            return term switch
            {
                Number number       => number.Value.ToString(),
                Variable variable   => variable.Name.ToString(),
                ArgumentTerm arg    => RenderArgument(arg),
                _ => throw new NotSupportedException($"Cannot render term of type {term.GetType().Name}")
            };
        }

        private static string RenderArgument(ArgumentTerm term) => term.Notation switch
        {
            Notation.Function   => RenderFunction(term),
            Notation.Prefix     => RenderPrefix(term),
            Notation.Infix      => RenderInfix(term),
            Notation.Postfix    => RenderPostfix(term),
            _ => throw new NotSupportedException($"Unknown notation: {term.Notation}")
        };

        private static string RenderFunction(ArgumentTerm term)
        {
            var sb = new StringBuilder();
            sb.Append(term.Name).Append('(');

            for (int i = 0; i < term.Args.Length; i++)
            {
                if (i > 0) sb.Append(", ");
                sb.Append(Render(term.Args[i]));
            }

            sb.Append(')');
            return sb.ToString();
        }

        private static string RenderPrefix(ArgumentTerm term)
        {
            var inner = Render(term.Args[0]);
            bool needBrackets = term.Args[0].Precedence <= term.Precedence
                                || inner.StartsWith("-");

            return needBrackets ? $"{term.Name}({inner})" : $"{term.Name}{inner}";
        }

        private static string RenderInfix(ArgumentTerm term)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < term.Args.Length; i++)
            {
                if (i > 0)
                    sb.Append(term.Name);

                int minPrec = i == 0
                    ? term.Precedence + (term.IsRightAssoc ? 1 : 0)
                    : term.Precedence + (term.IsRightAssoc ? 0 : 1);

                var inner = Render(term.Args[i]);

                bool needParens = term.Args[i].Precedence < minPrec
                    || (i > 0 && inner.StartsWith("-"));

                sb.Append(needParens ? $"({inner})" : inner);
            }

            return sb.ToString();
        }

        private static string RenderPostfix(ArgumentTerm term)
        {
            var inner = Render(term.Args[0]);
            bool needBrackets = term.Args[0].Precedence < term.Precedence;

            return needBrackets ? $"({inner}){term.Name}" : $"{inner}{term.Name}";
        }
    }
}
