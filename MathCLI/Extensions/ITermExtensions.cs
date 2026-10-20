using MathCLI.MathKernel;
using MathCLI.MathKernel.TermVisitors;
using MathCLI.MathKernel.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.Extensions
{
    public static class ITermExtensions
    {
        public static ITerm Simplify(this ITerm term)
            => term.Accept(new SimplifyVisitor());

        public static ITerm Substitute(this ITerm term, VariableContext variables)
            => term.Accept(new SubstituteVisitor(variables));

        public static string Render(this ITerm term)
            => term.Accept(new RenderVisitor());
    }
}
