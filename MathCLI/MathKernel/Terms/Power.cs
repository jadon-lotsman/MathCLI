using MathCLI.MathKernel.Variables;
using MathCLI.MathKernel.TermVisitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Terms
{
    public class Power : ArgumentTerm
    {
        public override bool IsInfix => true;
        public override bool IsRightAssoc => true;
        public override int Precedence => 4;
        public Power(params Term[] args) : base("^", args, 2) { }

        public override T Accept<T>(ITermVisitor<T> visitor) => visitor.Visit(this);
    }
}
