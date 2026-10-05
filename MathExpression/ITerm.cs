using MathCLI.MathExpression;
using MathCLI.MathKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathTerm
{
    public interface ITerm
    {
        Fraction Execute(VariableContext context);
        string ToString();
    }
}
