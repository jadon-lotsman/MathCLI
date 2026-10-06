using MathCLI.MathKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathExpression
{
    public interface ITerm
    {
        bool IsValue { get; }

        Fraction Execute(VariableContext context);
        ITerm ReduceStep(VariableContext context);
        ITerm Substitute(VariableContext context);

        string ToString();
    }
}
