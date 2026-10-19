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
    public interface ITermVisitor<T>
    {
        T Visit(Fraction fraction);
        T Visit(Variable variable);
        T Visit(NegateTerm negate);
        T Visit(Addition addition);
        T Visit(Subtraction subtraction);
        T Visit(Multiplication multiplication);
        T Visit(Division division);
        T Visit(Power power);
    }
}
