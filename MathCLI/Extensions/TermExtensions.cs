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
    public static class TermExtensions
    {
        public static Term Simplify(this Term term, VariableContext variables)
            => term.Accept(new SimplifyVisitor(variables));

        public static Term Substitute(this Term term, VariableContext variables)
            => term.Accept(new SubstituteVisitor(variables));

        public static string Render(this Term term)
            => term.Accept(new RenderVisitor());
    }
}
