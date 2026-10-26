using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules
{
    public class AddZeroRule : TermRule<Addition>
    {
        public override string Name => "x + 0 -> x, 0 + x -> x";
        protected override Term? Apply(Addition add)
        {
            if (add.Args[1] is Number rZero && rZero.Value.IsZero) return add.Args[0];
            if (add.Args[0] is Number lZero && lZero.Value.IsZero) return add.Args[1];
            return null;
        }
    }
}
