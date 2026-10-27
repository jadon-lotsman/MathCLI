using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules
{
    public interface IRuleGroup
    {
        Type TargetType { get; }
        Term? Apply(Term term, out string? rul);
    }
}
