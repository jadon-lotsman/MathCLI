using MathCLI.MathKernel.Variables;
using MathCLI.MathKernel.TermVisitors;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathCLI.Extensions;

namespace MathCLI.MathKernel
{
    public abstract class ArgumentTerm : Term
    {
        public string Name { get; }
        public Term[] Args { get; }
        public int MinArgs { get; }
        public int? MaxArgs { get; }
        public abstract bool IsInfix { get; }
        public abstract bool IsRightAssoc { get; }


        public ArgumentTerm(string name, Term[] args, int minArgs) : this(name, args, minArgs, minArgs) { }

        public ArgumentTerm(string name, Term[] args, int minArgs, int? maxArgs)
        {
            if (IsInfix && args.Length < 2)
                throw new InvalidOperationException($"Infix term '{name}' requires at least 2 args, got {args.Length}");

            if (args.Length < minArgs || (maxArgs.HasValue && args.Length > maxArgs.Value))
                throw new ArgumentException($"Term '{name}' expects {DescribeArity(minArgs, maxArgs)}, got {args.Length}");

            Name = name;
            Args = args;
            MinArgs = minArgs;
            MaxArgs = maxArgs;
        }


        public override bool Equals(Term? term)
        {
            if (term is null) return false;
            if (ReferenceEquals(this, term)) return true;

            if (GetType() != term.GetType()) return false;
            if (term is not ArgumentTerm otherTerm) return false;

            if (Args.Length != otherTerm.Args.Length) return false;

            for (int i = 0; i < Args.Length; i++)
            {
                if (Args[i] != otherTerm.Args[i]) return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(GetType());
            foreach (var arg in Args)
            {
                hash.Add(arg);
            }
            return hash.ToHashCode();
        }

        private string DescribeArity(int min, int? max)
        {
            if (max is null)
                return min == 1
                    ? "at least 1 argument"
                    : $"at least {min} arguments";

            if (min == max)
                return min switch
                {
                    0 => "no arguments",
                    1 => "exactly 1 argument",
                    _ => $"exactly {min} arguments"
                };

            return $"between {min} and {max} arguments";
        }
    }
}
