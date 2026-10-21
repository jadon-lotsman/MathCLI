using MathCLI.Extensions;
using MathCLI.MathKernel.TermVisitors;
using MathCLI.MathKernel.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MathCLI.MathKernel.Terms
{
    public class NegateTerm : Term
    {
        public Term InnerTerm { get; }
        public override int Precedence => 2;

        public NegateTerm(Term inner)
        {
            InnerTerm = inner;
        }

        public override T Accept<T>(ITermVisitor<T> visitor) => visitor.Visit(this);
        public override bool Equals(Term? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return other is NegateTerm n && InnerTerm.Equals(n.InnerTerm);
        }
        public override int GetHashCode() => HashCode.Combine(typeof(NegateTerm), InnerTerm);
    }
}
