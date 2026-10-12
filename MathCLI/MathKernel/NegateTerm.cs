using MathCLI.MathKernel.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MathCLI.MathKernel
{
    public class NegateTerm : ITerm
    {
        public ITerm Term { get; private set; }
        public int Precedence => 2;
        public bool IsValue => false;

        public NegateTerm(ITerm term)
        {
            Term = term;
        }


        public Fraction Execute(VariableContext context) => -Term.Execute(context);
        public ITerm Simplify() => this;


        public ITerm ReduceStep(VariableContext context)
        {
            if (Term.IsValue)
                return Execute(context);

            Term = Term.ReduceStep(context);
            return this;
        }

        public ITerm Substitute(VariableContext context)
        {
            Term = Term.Substitute(context);
            return this;
        }

        public override string ToString()
        {
            if (Term.Precedence >= Precedence + 1)
                return $"-{Term}";

            return $"-({Term})";
        }
    }
}
