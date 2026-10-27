using MathCLI.MathKernel.Variables;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Terms
{
    public class NegateTerm : ArgumentTerm
    {
        public override Notation Notation => Notation.Prefix;
        public override bool IsRightAssoc => false;
        public override int Precedence => 2;
        public NegateTerm(params Term[] args) : base("~", args, exArgs: 1) { }

        public override ArgumentTerm WithArgs(Term[] args) => new NegateTerm(args);
    }
}
