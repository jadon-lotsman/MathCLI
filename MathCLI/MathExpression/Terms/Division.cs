using MathCLI.MathKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathExpression.Terms
{
    public class Division : BinaryTerm
    {
        public Division(ITerm left, ITerm right) : base(left, right) { }

        public override Fraction Execute(VariableContext context)
        {
            return Left.Execute(context) / Right.Execute(context);
        }

        public override string ToString()
        {
            return $"{Left}/{Right}";
        }
    }
}
