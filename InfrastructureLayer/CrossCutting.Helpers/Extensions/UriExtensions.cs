using System;

namespace CrossCutting.Helpers.Extensions
{
    public static class UriExtensions
    {
        public static Uri Combine(this Uri uri, params string[] segments)
        {
            return new Uri(CombineAsString(uri, segments));
        }

        public static string CombineAsString(this Uri uri, string[] segments)
        {
            return Strings.CombineAsURL(uri.AbsoluteUri, segments);
        }
    }
}
