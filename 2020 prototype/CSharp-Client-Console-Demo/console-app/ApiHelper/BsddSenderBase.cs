using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace bSDD.DemoClientConsole.ApiHelper
{
    public abstract class BsddSenderBase
    {
        private readonly HttpClient _httpClient;
        private readonly IApiSettings apiSettings;

        protected BsddSenderBase(HttpClient httpClient, IApiSettings apiSettings)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = new TimeSpan(0, 0, 100);

            this.apiSettings = apiSettings;
        }

        protected HttpClient HttpClient => _httpClient;

        protected async Task PostAsync(string relativeUri, HttpContent httpContent, CancellationToken cancellationToken)
        {
            var uri = CreateUri(relativeUri);
            await CatchTransientExceptions(async () =>
            {
                var response = await HttpClient.PostAsync(uri, httpContent, cancellationToken);
                await EvaluateResponse(response, uri);
            },
                uri);
        }

        // protected async Task PostAsJsonAsync<T>(string relativeUri, T dataToPost, CancellationToken cancellationToken) where T : class
        // {
        //     var uri = CreateUri(relativeUri);
        //     await CatchTransientExceptions(async () =>
        //     {
        //         // var response = await HttpClient.PostAsJsonAsync(uri, dataToPost, cancellationToken);
        //         await EvaluateResponse(response, uri);
        //     },
        //         uri);
        // }

        private async Task CatchTransientExceptions(Func<Task> sender, Uri uri)
        {
            try
            {
                await sender();
            }
            catch (TaskCanceledException ex) when (ex.Message.Contains("HttpClient.Timeout"))
            {
                // Identify it as transient exception so the message processor can decide to retry later on
                throw new Exception($"Timeout while posting to '{uri}' (timeout is {HttpClient.Timeout} seconds)");
            }

        }

        private Uri CreateUri(string relativeUri)
        {
            var uri = new Uri($"{apiSettings.ApiBaseUrl.TrimEnd('/')}{relativeUri}");
            return uri;
        }

        private async Task EvaluateResponse(HttpResponseMessage response, Uri uri)
        {
            if (response.IsSuccessStatusCode)
            {
                // For demo purposes writing to console. Use a separate logger for real use
                Console.WriteLine($"Response status is {response.StatusCode}");
            }
            else
            {
                // Note: maybe log all requests and responses? (use middleware for that)
                var stringContent = await response.Content.ReadAsStringAsync();
                var builder = new StringBuilder();
                builder.AppendLine("Default Request Headers:");
                foreach (var header in HttpClient.DefaultRequestHeaders)
                {
                    builder.AppendLine($"{header.Key}:{string.Join(";", header.Value)}");
                }

                // For demo purposes writing to console. Use a separate logger for real use
                Console.WriteLine(
                    $"bSDD API response is: {response.StatusCode} - {stringContent} - {response.ReasonPhrase}",
                    null,
                    new { Headers = builder.ToString() });
            }
        }
    }
}
