using MathCLI.MathKernel.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Terms
{
    public class Power : FunctionTerm
    {
        public Power(ITerm[] args) : base("pow", args, 2) { }

        public override Fraction ExecuteFunction(VariableContext context)
        {
            return Args[0].Execute(context).Pow((int)Args[1].Execute(context));
        }
    }
}
