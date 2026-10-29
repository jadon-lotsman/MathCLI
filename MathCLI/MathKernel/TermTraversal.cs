using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel
{
    public static class TermTraversal
    {
        public static bool TryBottomUpAll(Term source, Func<Term, Term?> func, out Term result)
        {
            bool changed = false;

            if (source is ArgumentTerm term)
            {
                var args = term.Args;
                Term[]? newArgs = null;
                for (int i = 0; i < args.Length; i++)
                {
                    if (TryBottomUpAll(args[i], func, out var newChild))
                    {
                        if (newArgs == null)
                        {
                            newArgs = new Term[args.Length];
                            for (int j = 0; j < i; j++)
                                newArgs[j] = args[j];
                        }

                        newArgs[i] = newChild;
                    }
                    else if (newArgs != null)
                    {
                        newArgs[i] = args[i];
                    }
                }

                if (newArgs != null)
                {
                    source = term.WithArgs(newArgs);
                    changed = true;
                }
            }

            if (func(source) is Term hit)
            {
                result = hit;
                return true;
            }

            result = source;
            return changed;
        }

        public static bool TryBottomUpStep(Term source, Func<Term, Term?> func, out Term result)
        {
            if (source is ArgumentTerm term)
            {
                var args = term.Args;
                for (int i = 0; i < args.Length; i++)
                {
                    if (TryBottomUpStep(args[i], func, out var newChild))
                    {
                        var newArgs = new Term[args.Length];
                        for (int j = 0; j < args.Length; j++)
                            newArgs[j] = j == i ? newChild : args[j];

                        result = term.WithArgs(newArgs);
                        return true;
                    }
                }
            }

            if (func(source) is Term hit)
            {
                result = hit;
                return true;
            }

            result = source;
            return false;
        }
    }
}
