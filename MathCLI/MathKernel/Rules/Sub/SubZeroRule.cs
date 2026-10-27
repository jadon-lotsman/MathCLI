using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules.Sub
{
    public class SubZeroRule : TermRule<Subtraction>
    {
        public override string Name => "x - 0 -> x, 0 - x -> -x";
        protected override Term? Apply(Subtraction sub)
        {
            if (sub.Args[1] is Number rZero && rZero.Value.IsZero) return sub.Args[0];
            if (sub.Args[0] is Number lZero && lZero.Value.IsZero) return new NegateTerm(sub.Args[1]);
            return null;
        }
    }
}
