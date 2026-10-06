using MathCLI.MathKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathExpression.Terms
{
    public class Power : ITerm
    {
        ITerm left;
        ITerm right;

        public Power(ITerm left, ITerm right)
        {
            this.left = left;
            this.right = right;
        }

        public Fraction Execute(VariableContext context)
        {
            return new Fraction(Math.Pow(left.Execute(context), right.Execute(context)));
        }

        public override string ToString()
        {
            return $"pow({left}, {right})";
        }
    }
}
