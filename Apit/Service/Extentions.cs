using System.Text.RegularExpressions;

namespace Apit.Service
{
    public static class Extentions
    {
        public static string NormalizeAddress(this string str)
        {
            // "  hel*lo   wor;'l/\d  -123 "

            // "hel*lo wor;'l/\d -123"
            var result = Regex.Replace(str.Trim(), @"\s+", " ");
            // "hel*lo-wor;'l/\d--123"
            result = result.Replace(' ', '-');
            // "hello-world--123"
            return Regex.Replace(result, "[^a-zA-Z0-9-.]+", "", RegexOptions.Compiled);
        }
    }
}