using MathCLI.MathKernel.Variables;
using MathCLI.MathKernel.TermVisitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Terms
{
    public class Subtraction : BinaryTerm
    {
        public override int Precedence { get => 1; }
        public Subtraction(ITerm left, ITerm right) : base(left, '-', right) { }

        public override ITerm Accept(ITermVisitor visitor) => visitor.Visit(this);
    }
}
