using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MathCLI.Extensions
{
    public static class StringExtension
    {
        public static string RemoveMultispaces(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return str;

            str = str.Trim();
            str = Regex.Replace(str, @"\s+", " ");

            return new string(str);
        }

    }
}
