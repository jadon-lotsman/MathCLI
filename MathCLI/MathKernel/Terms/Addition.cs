using MathCLI.MathKernel.Variables;
using MathCLI.MathKernel.TermVisitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Terms
{
    public class Addition : ArgumentTerm
    {
        public override bool IsInfix => true;
        public override bool IsRightAssoc => false;
        public override int Precedence => 1;
        public Addition(params ITerm[] args) : base("+", args, 2) { }

        public override ITerm Accept(ITermVisitor visitor) => visitor.Visit(this);
    }
}
