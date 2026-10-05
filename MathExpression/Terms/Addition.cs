using MathCLI.MathKernel;
using MathCLI.MathTerm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MathCLI.MathExpression.Terms
{
    public class Addition : ITerm
    {
        ITerm left;
        ITerm right;

        public Addition(ITerm left, ITerm right)
        {
            this.left = left;
            this.right = right;
        }

        public Fraction Execute(VariableContext context)
        {
            return left.Execute(context) + right.Execute(context);
        }

        public override string ToString()
        {
            return $"{left}+{right}";
        }
    }
}
