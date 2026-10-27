using MathCLI.MathKernel.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules.Neg
{
    public class NegMultiplicationRule : TermRule<NegateTerm>
    {
        public override string Name => "-(a * b) -> (-a) * b";
        protected override Term? Apply(NegateTerm negate)
        {
            if (negate.Args[0] is Multiplication mul)
                return new Multiplication(new NegateTerm(mul.Args[0]), mul.Args[1]);
            return null;
        }
    }
}
