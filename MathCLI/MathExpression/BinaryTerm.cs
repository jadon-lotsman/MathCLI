using MathCLI.MathKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathExpression
{
    public abstract class BinaryTerm : ITerm
    {
        protected ITerm Left;
        protected ITerm Right;

        public BinaryTerm(ITerm left, ITerm right)
        {
            Left = left;
            Right = right;
        }

        public abstract Fraction Execute(VariableContext context);

        public ITerm Substitute(VariableContext context)
        {
            Left = Left.Substitute(context);
            Right = Right.Substitute(context);

            return this;
        }
    }
}
