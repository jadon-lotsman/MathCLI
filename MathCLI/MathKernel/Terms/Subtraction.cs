using MathCLI.MathKernel.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Terms
{
    public class Subtraction : BinaryTerm
    {
        public Subtraction(ITerm left, ITerm right) : base(left, right) { }

        public override Fraction Execute(VariableContext context)
        {
            return Left.Execute(context) - Right.Execute(context);
        }

        public override string ToString()
        {
            return $"{Left}-{Right}";
        }
    }
}
