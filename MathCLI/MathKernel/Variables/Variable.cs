using MathCLI.MathKernel.TermVisitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Variables
{
    public class Variable : ITerm
    {
        public char Name;
        public int Precedence { get => int.MaxValue; }

        public Variable(char name)
        {
            Name = name;
        }

        public T Accept<T>(ITermVisitor<T> visitor) => visitor.Visit(this);

        public override string ToString() => $"{Name}";
    }
}
