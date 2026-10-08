using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Variables
{
    public class Variable : ITerm
    {
        private char Name;
        public int Precedence { get => int.MaxValue; }
        public bool IsValue { get => false; }

        public Variable(char name)
        {
            Name = name;
        }

        public Fraction Execute(VariableContext context) => context.GetVariable(Name);
        public ITerm ReduceStep(VariableContext context) => Execute(context);
        public ITerm Substitute(VariableContext context) => Execute(context);

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
