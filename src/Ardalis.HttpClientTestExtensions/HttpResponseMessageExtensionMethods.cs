using System.Net;
using System.Net.Http;

namespace Ardalis.HttpClientTestExtensions
{
    public static class HttpResponseMessageExtensionMethods
    {
        public static void EnsureNotFound(this HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.NotFound)
            {
                throw new HttpRequestException($"Expected 404 Not Found but was {response.StatusCode}.");
            }
        }
    }
}
