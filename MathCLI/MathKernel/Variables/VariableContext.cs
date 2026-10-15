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

        public bool TryGetVariable(char name, out Fraction? fraction)
        {
            fraction = default;

            if (variables.ContainsKey(name))
                fraction = variables[name];
            else
                return false;

            return true;
        }

        public Fraction GetVariable(char name) => TryGetVariable(name, out var fraction) ? fraction! : throw new Exception("Variable '" + name + "' is not exist");

        public void SetVariable(char name, double value)
        {
            if (variables.ContainsKey(name))
                variables[name] = new Fraction(value);
            else
                variables.Add(name, new Fraction(value));
        }
    }
}
