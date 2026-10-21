using MathCLI.Extensions;
using MathCLI.MathKernel.TermVisitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Variables
{
    public class Variable : Term
    {
        public char Name { get; }
        public override int Precedence => int.MaxValue;

        public Variable(char name)
        {
            Name = name;
        }

        public override T Accept<T>(ITermVisitor<T> visitor) => visitor.Visit(this);
        public override bool Equals(Term? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return other is Variable v && Name == v.Name;
        }
        public override int GetHashCode() => HashCode.Combine(typeof(Variable), Name);
    }
}
