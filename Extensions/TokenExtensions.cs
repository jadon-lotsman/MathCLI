using MathCLI.MathExpression.ExpressionReader;
using MathCLI.MathExpression.ExpressionReader.Enums;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.Extensions
{
    public static class TokenExtensions
    {
        public static bool IsOperator(this Token tok, string symbol) =>
            tok.Kind == TokenType.Operator && tok.Value == symbol;

        public static bool IsAnyOperator(this Token tok, params string[] symbols) =>
            tok.Kind == TokenType.Operator && symbols.Contains(tok.Value);
    }
}
