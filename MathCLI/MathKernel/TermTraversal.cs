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

        public static Term BottomUpStep(Term source, Func<Term, Term> func)
        {
            if (source is not ArgumentTerm term) return func(source);

            var length = term.Args.Length;
            var newArgs = new Term[length];
            var changed = false;
            for (int i = 0; i < length; i++)
            {
                newArgs[i] = BottomUpStep(term.Args[i], func);

                if (!ReferenceEquals(newArgs[i], term.Args[i]))
                {
                    changed = true;
                    for (int j = 0; j < length; j++)
                        newArgs[j] = i == j ? newArgs[i] : term.Args[j];

                    break;
                }
            }

            if (changed)
                return term.WithArgs(newArgs);

            return func(source);
        }
    }
}
