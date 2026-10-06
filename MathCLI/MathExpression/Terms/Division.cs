using MathCLI.MathKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathExpression.Terms
{
    public class Division : ITerm
    {
        ITerm left;
        ITerm right;

        public Division(ITerm left, ITerm right)
        {
            this.left = left;
            this.right = right;
        }

        public Fraction Execute(VariableContext context)
        {
            return left.Execute(context) / right.Execute(context);
        }

        public override string ToString()
        {
            return $"{left}/{right}";
        }
    }
}
