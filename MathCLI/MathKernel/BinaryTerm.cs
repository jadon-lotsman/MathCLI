using MathCLI.MathKernel.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel
{
    public abstract class BinaryTerm : ITerm
    {
        protected ITerm Left;
        protected ITerm Right;
        protected char Symbol { get; }
        public bool IsValue { get => false; }

        public BinaryTerm(ITerm left, char symbol, ITerm right)
        {
            Left = left;
            Right = right;
            Symbol = symbol;
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

        public override string ToString()
        {
            return $"{Left}{Symbol}{Right}";
        }
    }
}
