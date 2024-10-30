using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Identity.Client;

var config = new PublicClientApplicationOptions
{
    // 'Directory (tenant) ID' of the app registration in the Microsoft Entra admin center
    TenantId = "14762077-e0ef-404b-b843-5c32a95a0d43",

    // 'Application (client) ID' of the app registration in the Microsoft Entra admin center
    ClientId = "4aba821f-d4ff-498b-a462-c2837dbbba70"
};


const string tenantName = "buildingsmartservices";
const string tenant = $"{tenantName}.onmicrosoft.com";
const string scope = $"https://{tenantName}.onmicrosoft.com/api/read";
const string policySignUpSignIn = "b2c_1a_signupsignin_c";
const string azureAdB2CHostname = "authentication.buildingsmart.org";
const string authorityBase = $"https://{azureAdB2CHostname}/tfp/{tenant}/";
const string authoritySignUpSignIn = $"{authorityBase}{policySignUpSignIn}";
const string redirectUri = "http://localhost";

const string apiBaseUrl = "https://test.bsdd.buildingsmart.org";
string searchListUrl = $"{apiBaseUrl}/api/SearchInDictionary/v1?DictionaryUri=" + WebUtility.UrlEncode("https://identifier.buildingsmart.org/uri/bs-agri/testpriv/1.0");

// In order to take advantage of token caching, your MSAL client singleton must
// have a lifecycle that at least matches the lifecycle of the user's session in
// the console application.
var publicMsalClient = PublicClientApplicationBuilder.CreateWithApplicationOptions(config)
    .WithB2CAuthority(authoritySignUpSignIn)
    .WithRedirectUri(redirectUri)
    .WithLogging(Log, LogLevel.Info, false)
    .Build();

AuthenticationResult? msalAuthenticationResult = null;

// Attempt to use a cached access token if one is available. This will renew existing, but
// expired access tokens if possible. In this specific sample, this will always result in
// a cache miss, but this pattern would be what you'd use on subsequent calls that require
// the usage of the same access token.
IEnumerable<IAccount> accounts = (await publicMsalClient.GetAccountsAsync(policySignUpSignIn)).ToList();

if (accounts.Any())
{
    try
    {
        msalAuthenticationResult = await publicMsalClient.AcquireTokenSilent(
            [scope],
            accounts.First()).ExecuteAsync();
    }
    catch (MsalUiRequiredException)
    {
        // No usable cached token was found for this scope + account or Entra ID insists in
        // an interactive user flow.
    }
}

if (msalAuthenticationResult == null)
{
    // Initiate the device code flow.
    msalAuthenticationResult = await publicMsalClient.AcquireTokenInteractive([scope])
        .ExecuteAsync();
}

// At this point we now have a valid access token for Microsoft Graph, with only the specific scopes
// necessary to complete the following call. Build the Microsoft Graph HTTP request, using the obtained
// access token.
using var searchRequest = new HttpRequestMessage(HttpMethod.Get, searchListUrl);
searchRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", msalAuthenticationResult.AccessToken);
//searchRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJSUzI1NiIsImtpZCI6InZ6bTcxb1o0WjZlQVRYdC0zUmF2SUEybUdnVG8xeDJpZmgyX0FGZk5oMEEiLCJ0eXAiOiJKV1QifQ.eyJhdWQiOiIxNDRlMDM3Zi02YTcxLTQxYmItOGNlMS00ZTg1NTQ1MjA3NTYiLCJpc3MiOiJodHRwczovL2F1dGhlbnRpY2F0aW9uLmJ1aWxkaW5nc21hcnQub3JnLzE0NzYyMDc3LWUwZWYtNDA0Yi1iODQzLTVjMzJhOTVhMGQ0My92Mi4wLyIsImV4cCI6MTczMDI4NDgyMywibmJmIjoxNzMwMjgxMjIzLCJzdWIiOiI4N2FmNmMxOC03ZTYyLTQzZWEtYTgzMi1iZWY0OWIwMzBhZWMiLCJlbWFpbCI6ImVyaWtfYmFhcnNAaG90bWFpbC5jb20iLCJuYW1lIjoiRXJpayBCLiIsImdpdmVuX25hbWUiOiJFcmlrIiwiZmFtaWx5X25hbWUiOiJCYWFycyIsImlzRm9yZ290UGFzc3dvcmQiOmZhbHNlLCJub25jZSI6IjFjYWM4NWNlLTAzNDItNGJkNi1iYjE4LTUzMGM1MTAzNTA5MiIsInNjcCI6Im1hbmFnZSByZWFkIiwiYXpwIjoiZjYwY2MzZGUtYzBhNC00NjI3LTlmZjktNWUxNGE1OWQxYWNlIiwidmVyIjoiMS4wIiwiaWF0IjoxNzMwMjgxMjIzfQ.r-ox5LV66iiAz5GeGwL7cm5OGMhV1rB_4_Ai2HIKni3i8TAOknx6yqDQFJMT7hHhZJSrtOU8_CDTbcKFL56Fq68NbcnHVoxqIOhM_I-IY0ulxN2SnrMgOJ7GlGFQWPonbclTzL_RspAIwyMHv38Df4CWlbE4dsoeqFXq4ait8yCwBGpma3-awuum2-CpoVEZjhTXnhPMh8z2nZhiiUYobT9Qru_RObo53Rzo4nmqRaN8VyiLysVJIrwu2habmX5YjJGGtUUDZcrjTg8skrCBWwLP3WLB5sT6KW4LnvFauX6I3X42EL-eD3BKoF2AWaDsWAUECGxJgl8_HKdzT76ODg");

// Make the API call
var httpClient = new HttpClient();
var searchResponse = await httpClient.SendAsync(searchRequest);
searchResponse.EnsureSuccessStatusCode();

// Present the results to the user (formatting the JSON for readability)
var responseBody = JsonDocument.Parse(await searchResponse.Content.ReadAsStringAsync());
Console.WriteLine(JsonSerializer.Serialize(responseBody,
    new JsonSerializerOptions()
    {
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    }));


static void Log(LogLevel level, string message, bool containsPii)
{
    var logs = ($"{level} {message}");
    var sb = new StringBuilder();
    sb.Append(logs);
    File.AppendAllText(System.Reflection.Assembly.GetExecutingAssembly().Location + ".msalLogs.txt", sb.ToString());
    sb.Clear();
}
