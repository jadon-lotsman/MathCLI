using MathCLI.MathKernel.Terms;
using MathCLI.MathKernel.Variables;
using MathCLI.MathNumerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.TermVisitors
{
    public interface ITermVisitor
    {
        ITerm Visit(Fraction fraction);
        ITerm Visit(Variable variable);
        ITerm Visit(NegateTerm negate);
        ITerm Visit(Addition addition);
        ITerm Visit(Subtraction subtraction);
        ITerm Visit(Multiplication multiplication);
        ITerm Visit(Division division);
        ITerm Visit(Power power);
    }
}
