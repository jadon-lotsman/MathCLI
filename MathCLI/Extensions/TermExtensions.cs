using MathCLI.MathKernel;
using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Rules;
using MathCLI.MathKernel.Rules.Add;
using MathCLI.MathKernel.Rules.Div;
using MathCLI.MathKernel.Rules.Mul;
using MathCLI.MathKernel.Rules.Neg;
using MathCLI.MathKernel.Rules.Pow;
using MathCLI.MathKernel.Rules.Sub;
using MathCLI.MathKernel.Terms;
using MathCLI.MathKernel.Variables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.Extensions
{
    public static class TermExtensions
    {
        public static Term Simplify(this Term term)
        {
            var _negateRules = new RuleGroup<NegateTerm>(new()
            {
                new NegNegateRule(),
                new NegNumberRule(),
                new NegDivisionRule(),
                new NegMultiplicationRule()
            });

            var _addRules = new RuleGroup<Addition>(new()
            {
                new AddZeroRule(),
                new AddNegateRule(),
                new AddNumbersRule(),
                new AddEqualsRule()
            });

            var _subRules = new RuleGroup<Subtraction>(new()
            {
                new SubNegateRule(),
                new SubZeroRule(),
                new SubEqualsRule(),
                new SubNumbersRule()
            });

            var _mulRules = new RuleGroup<Multiplication>(new()
            {
                new MulZeroRule(),
                new MulOneRule(),
                new MulNegativeOneRule(),
                new MulNumbersRule(),
                new MulEqualsRule()
            });

            var _divRules = new RuleGroup<Division>(new()
            {
                new DivByZeroRule(),
                new DivOneRule(),
                new DivZeroRule(),
                new DivEqualsRule(),
                new DivNumbersRule()
            });

            var _powRules = new RuleGroup<Power>(new()
            {
                new PowZeroRule(),
                new PowOneRule(),
                new PowOneBaseRule(),
                new PowZeroBaseRule(),
                new PowNumbersRule()
            });


            var rules = new RuleContext(_negateRules, _addRules, _subRules, _mulRules, _divRules, _powRules);
            TermTraversal.TryBottomUpAll(term, t => rules.Apply(t, out _), out var result);
            return result;
        }

        public static bool TrySimplifyStep(this Term term, out Term? result)
        {
            var _negateRules = new RuleGroup<NegateTerm>(new()
            {
                new NegNegateRule(),
                new NegNumberRule(),
                new NegDivisionRule(),
                new NegMultiplicationRule()
            });

            var _addRules = new RuleGroup<Addition>(new()
            {
                new AddZeroRule(),
                new AddNegateRule(),
                new AddNumbersRule(),
                new AddEqualsRule()
            });

            var _subRules = new RuleGroup<Subtraction>(new()
            {
                new SubNegateRule(),
                new SubZeroRule(),
                new SubEqualsRule(),
                new SubNumbersRule()
            });

            var _mulRules = new RuleGroup<Multiplication>(new()
            {
                new MulZeroRule(),
                new MulOneRule(),
                new MulNegativeOneRule(),
                new MulNumbersRule(),
                new MulEqualsRule()
            });

            var _divRules = new RuleGroup<Division>(new()
            {
                new DivByZeroRule(),
                new DivOneRule(),
                new DivZeroRule(),
                new DivEqualsRule(),
                new DivNumbersRule()
            });

            var _powRules = new RuleGroup<Power>(new()
            {
                new PowZeroRule(),
                new PowOneRule(),
                new PowOneBaseRule(),
                new PowZeroBaseRule(),
                new PowNumbersRule()
            });

            var rules = new RuleContext(_negateRules, _addRules, _subRules, _mulRules, _divRules, _powRules);
            var b = TermTraversal.TryBottomUpStep(term, t => rules.Apply(t, out _), out result);
            return b;
        }

        public static Term Substitute(this Term term, VariableContext variables)
        {
            TermTraversal.TryBottomUpAll(term, t => t is Variable v && variables.TryGetVariable(v.Name, out var value) ? new Number(value!) : null, out var result);
            return result;
        }

        public static string Render(this Term term)
            => TermRender.Render(term);
    }
}
