using MathCLI.MathKernel.Terms;
using MathCLI.MathKernel.TermVisitors;
using MathCLI.MathKernel.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MathCLI.MathKernel
{
    public class NegateTerm : ITerm
    {
        public ITerm Term { get; private set; }
        public int Precedence => 2;

        public NegateTerm(ITerm term)
        {
            Term = term;
        }

        public ITerm Accept(ITermVisitor visitor) => visitor.Visit(this);

        public override string ToString() => $"(-{Term})";
    }
}
