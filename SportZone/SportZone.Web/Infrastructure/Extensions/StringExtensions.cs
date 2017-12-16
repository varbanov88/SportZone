using System.Text.RegularExpressions;

using static SportZone.Common.GlobalConstants;

namespace SportZone.Web.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string ToFriendlyUrl(this string text)
           => Regex.Replace(text, @"[^A-Za-z0-9_\.~]+", "-").ToLower();

        public static string ValidateTag(this string text)
        {
            text = text.ToLower();

            var pattern = "^[a-z]{3,10}$";
            var match = Regex.Match(text, pattern);
            if (!match.Success)
            {
                return string.Empty;
            }

            return text;
        }
    }
}