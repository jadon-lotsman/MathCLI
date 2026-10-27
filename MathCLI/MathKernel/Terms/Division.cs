using MathCLI.MathKernel.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Terms
{
    public class Division : ArgumentTerm
    {
        public override Notation Notation => Notation.Infix;
        public override bool IsRightAssoc => false;
        public override int Precedence => 3;
        public Division(params Term[] args) : base("/", args, 2) { }

        public override ArgumentTerm WithArgs(Term[] args) => new Division(args);
    }
}
