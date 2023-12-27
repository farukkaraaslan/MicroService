namespace CustomerAPI.Interfaces;

public interface IServiceCallHelper
{
    Task<string> Post(Uri uri, HttpMethod method, StringContent stringContent);
    Task<object> Get(string uri);
}
