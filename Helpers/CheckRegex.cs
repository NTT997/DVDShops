using System.Text.RegularExpressions;

namespace DVDShops.Helpers
{
    public class CheckRegex
    {
        public static bool CheckLink(string url)
        {
            string urlRegex = @"\bhttps?://(?:www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b(?:/[-a-zA-Z0-9@:%._\+~#=]*)?";
            return Regex.IsMatch(url, urlRegex);
        }
    }
}
