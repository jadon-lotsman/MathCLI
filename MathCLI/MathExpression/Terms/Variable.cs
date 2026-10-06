using MathCLI.MathKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathExpression.Terms
{
    public class Variable : ITerm
    {
        char ch;

        public Variable(char ch)
        {
            this.ch = ch;
        }

        public Fraction Execute(VariableContext context)
        {
            return context.GetVariable(ch);
        }

        public override string ToString()
        {
            return $"{ch}";
        }
    }
}
