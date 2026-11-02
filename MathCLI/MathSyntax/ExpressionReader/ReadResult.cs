using MathCLI.MathSyntax.ExpressionReader.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathSyntax.ExpressionReader
{
    public class ReadResult
    {
        public bool HasUnknown { get;  }
        public string Source { get; }
        public IReadOnlyList<Token> Tokens { get; }
        public IReadOnlyList<Token> UnknownTokens { get; }


        public ReadResult(string source, IReadOnlyList<Token> tokens)
        {
            Source = source;
            Tokens = tokens;
            UnknownTokens = tokens.Where(t => t.Type == TokenType.Unknown).ToList();
            HasUnknown = UnknownTokens.Any();
        }

        public ReadOnlySpan<char> TextOf(int index)
            => Source.AsSpan(Tokens[index].Start, Tokens[index].Length);
    }
}
