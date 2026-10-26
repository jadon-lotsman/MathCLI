using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel
{
    public class RuleGroup<T> where T : Term
    {
        private readonly List<TermRule<T>> _rules;

        public RuleGroup(List<TermRule<T>> rules)
        {
            _rules = rules;
        }

        public Term? Apply(T term, out string? ruleName)
        {
            ruleName = default;

            if (!_rules.Any())
                return null;

            foreach (var rule in _rules)
            {
                var result = rule.Apply(term);
                if (result != null)
                {
                    ruleName = rule.Name;
                    return result;
                }
            }

            return null;
        }
    }
}
