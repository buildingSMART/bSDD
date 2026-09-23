using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace ConsoleDemo;

/// <summary>
/// Authentication of an application (machine to machine): the app registration authenticates itself with its client id and secret
/// (client credentials flow). No user is involved, so the token contains no e-mail address.
/// The API checks if the client id of your app registration is registered as user of the organization.
/// </summary>
/// <remarks>
/// Azure AD B2C needs a custom policy for the client credentials flow, therefore the policy is part of the token endpoint.
/// MSAL does not support the B2C client credentials flow, so the token is requested with a plain HTTP call.
/// Your app registration needs the application permissions of the bSDD API, e.g. 'read.all' to upload files
/// and 'manage.dictionaries.all' to change the status of a dictionary. Azure AD B2C returns them in the 'scp' claim of the token.
/// </remarks>
public class ApplicationAuthentication(string clientId, string clientSecret, string policy, string? hostname = null, string? scope = null)
{
    private readonly HttpClient httpClient = new();

    public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        var tokenEndpoint = BsddSettings.GetTokenEndpoint(policy, hostname);
        var form = new Dictionary<string, string>
        {
            ["grant_type"] = "client_credentials",
            ["client_id"] = clientId,
            ["client_secret"] = clientSecret,
            ["scope"] = scope ?? BsddSettings.ApplicationScope
        };

        using var response = await httpClient.PostAsync(tokenEndpoint, new FormUrlEncodedContent(form), cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new InvalidOperationException($"Could not get an access token from '{tokenEndpoint}': {response.StatusCode}. {error}");
        }

        var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken);
        if (string.IsNullOrEmpty(tokenResponse?.AccessToken))
        {
            throw new InvalidOperationException($"The response of '{tokenEndpoint}' does not contain an access token");
        }

        return tokenResponse.AccessToken;
    }

    private class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }
    }
}
