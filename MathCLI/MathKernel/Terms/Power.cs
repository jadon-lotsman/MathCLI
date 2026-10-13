using MathCLI.MathKernel.Variables;
using MathCLI.MathKernel.TermVisitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Terms
{
    public class Power : FunctionTerm
    {
        public Power(params ITerm[] args) : base("pow", args, 2) { }

        public override ITerm AcceptFunction(ITermVisitor visitor) => visitor.Visit(this);
    }
}
