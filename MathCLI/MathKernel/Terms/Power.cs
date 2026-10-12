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


        public override ITerm Simplify()
        {
            Args[0] = Args[0].Simplify();
            Args[1] = Args[1].Simplify();

            if (Args[1] is Fraction e0 && e0.IsZero) return Fraction.One;
            if (Args[1] is Fraction e1 && e1.IsOne) return Args[0];
            if (Args[0] is Fraction b1 && b1.IsOne) return Fraction.One;
            if (Args[0] is Fraction b0 && b0.IsZero) return Fraction.Zero;

            if (Args[0] is Fraction bf && Args[1] is Fraction ef && ef.IsInteger)
            {
                if (ef.Numerator > int.MaxValue || ef.Numerator < int.MinValue)
                    return this;
                return bf.Pow((int)ef.Numerator);
            }

            return this;
        }
    }
}
