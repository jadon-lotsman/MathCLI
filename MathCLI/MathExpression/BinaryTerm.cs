using MathCLI.MathExpression.Terms;
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
        public bool IsValue { get => false; }

        public BinaryTerm(ITerm left, ITerm right)
        {
            Left = left;
            Right = right;
        }

        public abstract Fraction Execute(VariableContext context);


        public ITerm ReduceStep(VariableContext context)
        {
            if (Left.IsValue && Right.IsValue)
                return Execute(context);

            if (!Left.IsValue)
            {
                Left = Left.ReduceStep(context);
                return this;
            }

            Right = Right.ReduceStep(context);
            return this;
        }

        public ITerm Substitute(VariableContext context)
        {
            Left = Left.Substitute(context);
            Right = Right.Substitute(context);

            return this;
        }
    }
}
