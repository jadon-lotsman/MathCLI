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
        public bool IsValue { get => false; }

        public Variable(char name)
        {
            Name = name;
        }

        public ITerm Accept(ITermVisitor visitor) => visitor.Visit(this);

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
