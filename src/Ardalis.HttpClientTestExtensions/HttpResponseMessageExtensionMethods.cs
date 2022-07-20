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
				ThrowHelper(HttpStatusCode.NotFound, response.StatusCode);
			}
		}

		public static void EnsureNoContent(this HttpResponseMessage response)
		{
			if (response.StatusCode != HttpStatusCode.NoContent)
			{
				ThrowHelper(HttpStatusCode.NoContent, response.StatusCode);
			}
		}

		public static void EnsureUnauthorized(this HttpResponseMessage response)
		{
			if (response.StatusCode != HttpStatusCode.Unauthorized)
			{
				ThrowHelper(HttpStatusCode.Forbidden, response.StatusCode);
			}
		}

		public static void EnsureForbidden(this HttpResponseMessage response)
		{
			if (response.StatusCode != HttpStatusCode.Forbidden)
			{
				ThrowHelper(HttpStatusCode.Forbidden, response.StatusCode);
			}
		}

		private static HttpRequestException ThrowHelper(HttpStatusCode expectedStatusCode, HttpStatusCode actualStatusCode)
		{
			throw new HttpRequestException($"Expected {expectedStatusCode.ToString("D")} {expectedStatusCode.ToString("G")} but was {actualStatusCode.ToString("D")} {actualStatusCode.ToString("G")}");
		}
	}
}
