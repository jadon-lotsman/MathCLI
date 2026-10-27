using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules.Mul
{
    public class MulOneRule : TermRule<Multiplication>
    {
        public override string Name => "x * 1 -> x, 1 * x -> x";
        protected override Term? Apply(Multiplication mul)
        {
            if (mul.Args[1] is Number rOne && rOne.Value.IsOne) return mul.Args[0];
            if (mul.Args[0] is Number lOne && lOne.Value.IsOne) return mul.Args[1];
            return null;
        }
    }
}
