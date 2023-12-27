using CustomerAPI.Interfaces;
using System.Net;

namespace CustomerAPI.Helper;

public class ServiceHelper : IServiceCallHelper
{
    public async  Task<object> Get(string uri)
    {
      using(WebClient webClient = new WebClient())
        {
            var response = webClient.DownloadString(uri);
            return response;
        }
    }

    public async Task<string> Post(Uri uri, HttpMethod method, StringContent stringContent)
    {
        using (var client = new HttpClient())
        {
            var request = new HttpRequestMessage
            {
                Method = method,
                Content = stringContent,
                RequestUri = uri
            };
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        }
    }
}
