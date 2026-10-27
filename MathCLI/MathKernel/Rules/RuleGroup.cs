using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules
{
    public class RuleGroup<T> : IRuleGroup where T : Term
    {
        private readonly TermRule<T>[] _rules;
        public Type TargetType => typeof(T);

        public RuleGroup(List<TermRule<T>> rules)
        {
            _rules = rules.ToArray();
        }

        public Term? Apply(Term term, out string? rul)
        {
            rul = default;

            foreach (var rule in _rules)
            {
                var result = rule.Apply(term);
                if (result != null)
                {
                    rul = rule.Name;
                    return result;
                }
            }

            return null;
        }
    }
}
