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
        protected ITerm Term;
        public int Precedence => int.MaxValue;
        public bool IsValue => false;

        public NegateTerm(ITerm term)
        {
            Term = term;
        }


        public Fraction Execute(VariableContext context) => -Term.Execute(context);

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
            return $"-({Term})";
        }
    }
}
