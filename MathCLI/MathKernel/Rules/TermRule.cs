using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules
{
    public abstract class TermRule<T> where T : Term
    {
        public abstract string Name { get; }

        protected abstract Term? Apply(T term);
        public Term? Apply(Term term) => term is T typedTerm ? Apply(typedTerm) : null;
    }
}
