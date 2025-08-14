using System.Text.RegularExpressions;

namespace QsysSharp.Communications
{
    public static class StringExtensions
    {
        /// <summary>
        /// Replaces hex with ascii representation ex. \x0d = [0D]
        /// </summary>
        /// <param name="source">String to replace hex</param>
        /// <returns></returns>
        public static string ReplaceHex(this string source)
        {
            return Regex.Replace(source,
              @"\p{Cc}",
              a => string.Format("[{0:X2}]", (byte)a.Value[0])
            );
        }
    }
}