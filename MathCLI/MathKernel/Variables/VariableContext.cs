using MathCLI.MathKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Variables
{
    public class VariableContext
    {
        Dictionary<char, Fraction> variables;

        public VariableContext()
        {
            variables = new Dictionary<char, Fraction>();
            SetVariable('p', Math.PI);
            SetVariable('e', Math.E);
        }

        public Fraction GetVariable(char name)
        {
            if (variables.ContainsKey(name))
                return variables[name];
            else
                throw new Exception("Variable '" + name + "' is not exist");
        }

        public void SetVariable(char name, double value)
        {
            if (variables.ContainsKey(name))
                variables[name] = new Fraction(value);
            else
                variables.Add(name, new Fraction(value));
        }
    }
}
