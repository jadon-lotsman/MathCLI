using MathCLI.MathKernel.Variables;
using MathCLI.MathKernel.TermVisitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Terms
{
    public class Division : BinaryTerm
    {
        public override int Precedence { get => 3; }
        public Division(ITerm left, ITerm right) : base(left, ':', right) { }

        public override ITerm Accept(ITermVisitor visitor) => visitor.Visit(this);
    }
}
