using System.Net.Http;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions;

public static partial class HttpClientPostExtensionMethods
{

	public static async Task<HttpResponseMessage> PostAndEnsureNotFound(this HttpClient client, string requestUri, HttpContent content, ITestOutputHelper output = null)
	{
		output?.WriteLine($"Requesting with POST {requestUri}");
		var response = await client.PostAsync(requestUri, content);
		response.EnsureNotFound();
		return response;
	}
}
