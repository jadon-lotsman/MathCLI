using MathCLI.MathExpression.ExpressionReader;
using MathCLI.MathExpression.ExpressionReader.Enums;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.Extensions
{
    public static class TokenExtensions
    {
        public static bool IsMatch(this Token tok, string symbol) =>
            (tok.Kind == TokenType.Operator || tok.Kind == TokenType.Function) && tok.Value == symbol;

        public static bool IsAnyMatch(this Token tok, params string[] symbols) =>
            (tok.Kind == TokenType.Operator || tok.Kind == TokenType.Function) && symbols.Contains(tok.Value);
    }
}
