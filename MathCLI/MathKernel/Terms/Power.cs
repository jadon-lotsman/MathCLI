using MathCLI.MathKernel.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Terms
{
    public class Power : BinaryTerm
    {
        public Power(ITerm left, ITerm right) : base(left, right) { }

        public override Fraction Execute(VariableContext context)
        {
            return new Fraction(Math.Pow(Left.Execute(context), Right.Execute(context)));
        }

        public override string ToString()
        {
            return $"pow({Left}, {Right})";
        }
    }
}
