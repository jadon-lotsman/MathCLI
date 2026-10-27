using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules.Div
{
    public class DivByZeroRule : TermRule<Division>
    {
        public override string Name => "a / 0 -> ex";
        protected override Term? Apply(Division div)
        {
            if (div.Args[1] is Number rZero && rZero.Value.IsZero)
                throw new DivideByZeroException();
            return null;
        }
    }
}
