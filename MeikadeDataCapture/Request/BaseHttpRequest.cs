using Flurl.Http;

namespace MeikadeDataCapture.Request;

public class BaseHttpRequest
{
    public Task<T> Post<T>(string url, object body)
    {
        var data = ConfigureHeader(url).AllowAnyHttpStatus().PostJsonAsync(body);

        return Execute<T>(data);
    }

    public Task<T> Post<T>(string url, object body, Dictionary<string, string> headers)
    {
        var data = ConfigureHeader(url);

        foreach (var header in headers)
        {
            data.WithHeader(header.Key, header.Value);
        }

        var result = data.AllowAnyHttpStatus().PostJsonAsync(body);

        return Execute<T>(result);
    }

    public Task<T> Put<T>(string url, object body)
    {
        var data = ConfigureHeader(url).AllowAnyHttpStatus().PutJsonAsync(body);

        return Execute<T>(data);
    }

    public Task<T> Delete<T>(string url)
    {
        var data = ConfigureHeader(url).AllowAnyHttpStatus().DeleteAsync();

        return Execute<T>(data);
    }

    public Task<T> Get<T>(string url, object? body = null)
    {
        var data = ConfigureHeader(url).SetQueryParams(body).AllowAnyHttpStatus().GetAsync();

        return Execute<T>(data);
    }

    public Task<T> Get<T>(string url, Dictionary<string, string> headers, object? body = null)
    {
        var data = ConfigureHeader(url);

        foreach (var header in headers)
        {
            data.WithHeader(header.Key, header.Value);
        }

        var result = data.SetQueryParams(body).AllowAnyHttpStatus().GetAsync();

        return Execute<T>(result);
    }

    private IFlurlRequest ConfigureHeader(string url)
    {
        var data = url.WithHeader("Unique-ID", "af09752a-df74-4981-90d1-dc8b805a29ca")
            .WithHeader("App-Name", "Meikade")
            .WithHeader("App-ID", "0e3103ed-dfb2-49df-95d2-3bcbec76fa34")
            .WithHeader("App-Version", "4.5.0")
            .WithHeader("App-Build-OS", "android")
            .WithHeader("Theme-Dark", "true")
            .WithHeader("Premium-Days", "100")
            .WithHeader("Accept-Language", "fa-IR");

        return data;
    }

    private async Task<T> Execute<T>(Task<IFlurlResponse> input)
    {
        var result = await input;

        var response = await result.GetJsonAsync<T>();

        return response;
    }
}