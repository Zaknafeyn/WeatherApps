using System;
using System.Linq;

namespace Services
{
    public static class Extensions
    {
        public static Uri Append(this Uri uri, params string[] paths)
        {
            return new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => string.Format("{0}/{1}", current.TrimEnd('/'), path.TrimStart('/'))));
        }

        public static Uri AppendQuery(this Uri uri, params string[] query)
        {
            var uriBuilder = new UriBuilder(uri) { Query = string.Join("&", query) };
            return uriBuilder.Uri;
        }

        public static Uri AppendQuery(this Uri uri, string query)
        {
            var uriBuilder = new UriBuilder(uri) { Query = query };
            return uriBuilder.Uri;
        }

    }
}