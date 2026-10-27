using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules.Mul
{
    public class MulNumbersRule : TermRule<Multiplication>
    {
        public override string Name => "a * b -> c";
        protected override Term? Apply(Multiplication mul)
        {
            if (mul.Args[0] is Number a && mul.Args[1] is Number b)
                return new Number(a.Value * b.Value);
            return null;
        }
    }
}
