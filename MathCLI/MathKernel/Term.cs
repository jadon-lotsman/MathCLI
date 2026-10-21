using MathCLI.Extensions;
using MathCLI.MathKernel.TermVisitors;
using MathCLI.MathKernel.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel
{
    public abstract class Term : IEquatable<Term>
    {
        public abstract int Precedence { get; }

        public abstract T Accept<T>(ITermVisitor<T> visitor);
        public override string ToString() => this.Render();

        public abstract bool Equals(Term? other);
        public override bool Equals(object? obj) => Equals(obj as Term);
        public abstract override int GetHashCode();

        public static bool operator ==(Term? left, Term? right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }
        public static bool operator !=(Term? left, Term? right) => !(left == right);
    }
}
