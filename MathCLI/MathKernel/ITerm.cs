using MathCLI.MathKernel.Variables;
using MathCLI.MathKernel.TermVisitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel
{
    public interface ITerm
    {
        int Precedence { get; }

        ITerm Accept(ITermVisitor visitor);
        string ToString();
    }
}
