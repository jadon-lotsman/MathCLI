using MathCLI.MathKernel.TermVisitors;
using MathCLI.MathNumerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Numbers
{
    public class Number : ITerm
    {
        public Fraction Value { get; }
        public int Precedence => int.MaxValue;

        public Number(Fraction fraction)
        {
            Value = fraction;
        }

        public T Accept<T>(ITermVisitor<T> visitor) => visitor.Visit(this);
        public override string ToString() => $"{Value}";
    }
}
