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
        char Name;

        public Variable(char name)
        {
            Name = name;
        }

        public Fraction Execute(VariableContext context) => context.GetVariable(Name);
        public ITerm Substitute(VariableContext context) => Execute(context);

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
