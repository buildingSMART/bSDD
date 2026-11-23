using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ConsoleDemo;

/// <summary>
/// Calls the (secured) bSDD API with the given access token
/// </summary>
public class BsddApiClient(string accessToken)
{
    private readonly HttpClient httpClient = new();
    private readonly string baseUrl = BsddSettings.ApiBaseUrl;

    /// <summary>
    /// Searches in a dictionary, see https://api.bsdd.buildingsmart.org/swagger
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
    /// Gets list of dictionaries. If logged in, it includes private dictionaries of the organization, see https://api.bsdd.buildingsmart.org/swagger
    /// </summary>
    public async Task GetDictionariesAsync(CancellationToken cancellationToken = default)
    {
        var url = $"{baseUrl}/api/Dictionary/v1";

        using var searchRequest = new HttpRequestMessage(HttpMethod.Get, url);
        searchRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var searchResponse = await httpClient.SendAsync(searchRequest, cancellationToken);
        searchResponse.EnsureSuccessStatusCode();

        // Present the results to the user (formatting the JSON for readability)
        var responseBody = JsonDocument.Parse(await searchResponse.Content.ReadAsStringAsync(cancellationToken));
        Console.WriteLine(Format(responseBody));
    }

    /// <summary>
    /// Uploads a bSDD import file, see https://api.bsdd.buildingsmart.org/swagger
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

    /// <summary>
    /// Downloads a dictionary as a bSDD import file, see https://api.bsdd.buildingsmart.org/swagger
    /// </summary>
    public async Task<bool> DownloadDictionaryAsync(string dictionaryUri, string outputDirectory, CancellationToken cancellationToken = default)
    {
        var downloadUrl = $"{baseUrl}/api/DictionaryDownload/bsdd/v1?DictionaryUri=" + WebUtility.UrlEncode(dictionaryUri);

        using var downloadRequest = new HttpRequestMessage(HttpMethod.Get, downloadUrl);
        downloadRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var downloadResponse = await httpClient.SendAsync(downloadRequest, cancellationToken);

        if (!downloadResponse.IsSuccessStatusCode)
        {
            Console.WriteLine($"Download of '{dictionaryUri}' failed: {(int)downloadResponse.StatusCode} {downloadResponse.ReasonPhrase}");
            if (downloadResponse.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
            {
                Console.WriteLine("Check if your application is allowed to download this (private) dictionary.");
            }

            Console.WriteLine(await downloadResponse.Content.ReadAsStringAsync(cancellationToken));
            return false;
        }

        var fileName = downloadResponse.Content.Headers.ContentDisposition?.FileNameStar
                        ?? downloadResponse.Content.Headers.ContentDisposition?.FileName?.Trim('"')
                        ?? $"{dictionaryUri.Split('/').Last()}.json";

        var filePath = Path.Combine(outputDirectory, fileName);
        await using var fileStream = File.Create(filePath);
        await downloadResponse.Content.CopyToAsync(fileStream, cancellationToken);

        Console.WriteLine($"Dictionary '{dictionaryUri}' downloaded to '{filePath}'.");
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
