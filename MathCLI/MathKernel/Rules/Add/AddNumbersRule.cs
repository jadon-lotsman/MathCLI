using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules.Add
{
    public class AddNumbersRule : TermRule<Addition>
    {
        public override string Name => "a + b -> c";
        protected override Term? Apply(Addition add)
        {
            if (add.Args[0] is Number a && add.Args[1] is Number b) return new Number(a.Value + b.Value);
            return null;
        }
    }
}
