using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel
{
    public static class TermTraversal
    {
        public static Term BottomUp(Term source, Func<Term, Term> func)
        {
            if (source is not ArgumentTerm term) return func(source);

            var length = term.Args.Length;
            var newArgs = new Term[length];
            var changed = false;
            for (int i = 0; i < length; i++)
            {
                newArgs[i] = BottomUp(term.Args[i], func);

                if (!ReferenceEquals(newArgs[i], term.Args[i]))
                    changed = true;
            }

            if (changed)
                return func(term.WithArgs(newArgs));
            
            return func(source);
        }
    }
}
