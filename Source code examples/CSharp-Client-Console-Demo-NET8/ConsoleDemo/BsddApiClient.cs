using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ConsoleDemo;

/// <summary>
/// Calls the (secured) bSDD API with the given access token
/// </summary>
public class BsddApiClient(string accessToken, string? apiBaseUrl = null)
{
    private readonly HttpClient httpClient = new();
    private readonly string baseUrl = apiBaseUrl ?? BsddSettings.ApiBaseUrl;

    /// <summary>
    /// Searches in a dictionary, see https://test.bsdd.buildingsmart.org/swagger
    /// </summary>
    public async Task SearchInDictionaryAsync(string dictionaryUri, CancellationToken cancellationToken = default)
    {
        var searchListUrl = $"{baseUrl}/api/SearchInDictionary/v1?DictionaryUri=" + WebUtility.UrlEncode(dictionaryUri);

        using var searchRequest = new HttpRequestMessage(HttpMethod.Get, searchListUrl);
        searchRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var searchResponse = await httpClient.SendAsync(searchRequest, cancellationToken);
        searchResponse.EnsureSuccessStatusCode();

        // Present the results to the user (formatting the JSON for readability)
        var responseBody = JsonDocument.Parse(await searchResponse.Content.ReadAsStringAsync(cancellationToken));
        Console.WriteLine(Format(responseBody));
    }

    /// <summary>
    /// Uploads a bSDD import file, see https://test.bsdd.buildingsmart.org/swagger
    /// </summary>
    public async Task<bool> UploadImportFileAsync(string filePath, string organizationCode, bool validateOnly, bool isTest, CancellationToken cancellationToken = default)
    {
        var uploadUrl = $"{baseUrl}/api/UploadImportFile/v2";

        await using var fileStream = File.OpenRead(filePath);
        using var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        using var form = new MultipartFormDataContent
        {
            { fileContent, "FormFile", Path.GetFileName(filePath) },
            { new StringContent(organizationCode), "OrganizationCode" },
            { new StringContent(validateOnly.ToString()), "ValidateOnly" },
            { new StringContent(isTest.ToString()), "IsTest" }
        };

        using var uploadRequest = new HttpRequestMessage(HttpMethod.Post, uploadUrl) { Content = form };
        uploadRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var uploadResponse = await httpClient.SendAsync(uploadRequest, cancellationToken);
        var responseText = await uploadResponse.Content.ReadAsStringAsync(cancellationToken);

        if (!uploadResponse.IsSuccessStatusCode)
        {
            Console.WriteLine($"Upload of '{filePath}' failed: {(int)uploadResponse.StatusCode} {uploadResponse.ReasonPhrase}");
            if (uploadResponse.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
            {
                Console.WriteLine($"Check if your application is registered as user of organization '{organizationCode}' and is allowed to upload.");
            }

            Console.WriteLine(responseText);
            return false;
        }

        Console.WriteLine(Format(JsonDocument.Parse(responseText)));
        return true;
    }

    private static string Format(JsonDocument json)
    {
        return JsonSerializer.Serialize(json,
            new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
    }
}
