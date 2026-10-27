using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel.Rules
{
    public class RuleContext
    {
        private readonly Dictionary<Type, IRuleGroup> _groupByTypes;

        public RuleContext(params IRuleGroup[] groups)
        {
            _groupByTypes = groups.ToDictionary(g => g.TargetType);
        }

        public Term? Apply(Term term, out string? rul)
        {
            rul = default;
            if (_groupByTypes.TryGetValue(term.GetType(), out var rules))
                return rules.Apply(term, out rul);

            return null;
        }
    }
}
