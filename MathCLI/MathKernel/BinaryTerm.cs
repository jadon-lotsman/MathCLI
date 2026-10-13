using MathCLI.MathKernel.Variables;
using MathCLI.MathKernel.TermVisitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel
{
    public abstract class BinaryTerm : ITerm
    {
        public ITerm Left;
        public ITerm Right;
        protected char Symbol { get; }
        public abstract int Precedence { get; }
        public bool IsValue { get => false; }

        public BinaryTerm(ITerm left, char symbol, ITerm right)
        {
            Left = left;
            Right = right;
            Symbol = symbol;
        }


        public abstract ITerm Accept(ITermVisitor visitor);

        public override string ToString()
        {
            string left = WrapLeft(Left);
            string right = WrapRight(Right);
            return $"{left}{Symbol}{right}";
        }

        private string WrapLeft(ITerm term)
        {
            return term.Precedence < Precedence ? $"({term})" : term.ToString();
        }

        private string WrapRight(ITerm term)
        {
            if (term is NegateTerm) return $"({term})";

            if (term.Precedence < Precedence) return $"({term})";
            if (term.Precedence > Precedence) return term.ToString();
            return $"({term})";
        }
    }
}
