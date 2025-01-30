using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Identity.Client;

var config = new PublicClientApplicationOptions
{
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

using var searchRequest = new HttpRequestMessage(HttpMethod.Get, searchListUrl);
searchRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", msalAuthenticationResult.AccessToken);

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
