using MathCLI.MathKernel.Variables;
using MathCLI.MathKernel.TermVisitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Terms
{
    public class Subtraction : ArgumentTerm
    {
        public override bool IsInfix => true;
        public override bool IsRightAssoc => false;
        public override int Precedence => 1;
        public Subtraction(params ITerm[] args) : base("-", args, 2) { }

        public override T Accept<T>(ITermVisitor<T> visitor) => visitor.Visit(this);
    }
}
