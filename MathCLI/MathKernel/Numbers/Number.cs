using MathCLI.Extensions;
using MathCLI.MathKernel.TermVisitors;
using MathCLI.MathNumerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Numbers
{
    public class Number : Term
    {
        public Fraction Value { get; }
        public override int Precedence => int.MaxValue;

        public Number(Fraction fraction)
        {
            Value = fraction;
        }

        public override T Accept<T>(ITermVisitor<T> visitor) => visitor.Visit(this);
        public override bool Equals(Term? term)
        {
            if (term is null) return false;
            if (ReferenceEquals(this, term)) return true;
            return term is Number num && Value.Equals(num.Value);
        }
        public override int GetHashCode() => HashCode.Combine(typeof(Number), Value);
    }
}
