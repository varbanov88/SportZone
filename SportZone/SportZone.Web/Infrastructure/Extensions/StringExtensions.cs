using System.Linq;
using System.Text.RegularExpressions;

namespace SportZone.Web.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string ToFriendlyUrl(this string text)
           => Regex.Replace(text, @"[^A-Za-z0-9_\.~]+", "-").ToLower();

        public static string ValidateTag(this string text)
        {
            text = text.ToLower();
            if (text.Length < 3 || text.Length > 30)
            {
                return string.Empty;
            }

            if (!text.All(s => char.IsLetter(s)))
            {
                return string.Empty;
            }

            return text;
        }
    }
}