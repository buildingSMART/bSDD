using System.Text;
using Microsoft.Identity.Client;

namespace ConsoleDemo;

/// <summary>
/// Authentication of a user: the user signs in interactively (authorization code flow with PKCE).
/// The token contains the e-mail address of the user, the API uses it to determine what the user is allowed to do.
/// </summary>
public class UserAuthentication
{
    private readonly IPublicClientApplication publicMsalClient;

    public UserAuthentication()
    {
        var config = new PublicClientApplicationOptions
        {
            // 'Application (client) ID' of the app registration in the Microsoft Entra admin center
            ClientId = BsddSettings.DemoUserClientId
        };

        // In order to take advantage of token caching, your MSAL client singleton must
        // have a lifecycle that at least matches the lifecycle of the user's session in
        // the console application.
        publicMsalClient = PublicClientApplicationBuilder.CreateWithApplicationOptions(config)
            .WithB2CAuthority(BsddSettings.GetUserAuthority())
            .WithRedirectUri(BsddSettings.RedirectUri)
            .WithLogging(Log, LogLevel.Info, false)
            .Build();
    }

    public async Task<string> GetAccessTokenAsync()
    {
        string[] scopes = [BsddSettings.GetUserReadScope()];
        AuthenticationResult? msalAuthenticationResult = null;

        // Attempt to use a cached access token if one is available. This will renew existing, but
        // expired access tokens if possible. In this specific sample, this will always result in
        // a cache miss, but this pattern would be what you'd use on subsequent calls that require
        // the usage of the same access token.
        IEnumerable<IAccount> accounts = (await publicMsalClient.GetAccountsAsync(BsddSettings.PolicySignUpSignIn)).ToList();

        if (accounts.Any())
        {
            try
            {
                msalAuthenticationResult = await publicMsalClient.AcquireTokenSilent(scopes, accounts.First()).ExecuteAsync();
            }
            catch (MsalUiRequiredException)
            {
                // No usable cached token was found for this scope + account or Entra ID insists in
                // an interactive user flow.
            }
        }

        // Sign in the user
        msalAuthenticationResult ??= await publicMsalClient.AcquireTokenInteractive(scopes).ExecuteAsync();

        LogScopes(scopes, msalAuthenticationResult);

        return msalAuthenticationResult.AccessToken;
    }

    /// <summary>
    /// Azure AD B2C silently drops a requested scope it does not grant and then only returns an id token.
    /// Note: when the token response has no scope, MSAL fills <see cref="AuthenticationResult.Scopes"/> with the requested scopes,
    /// so only an empty access token shows that nothing was granted.
    /// </summary>
    private static void LogScopes(string[] requestedScopes, AuthenticationResult result)
    {
        Console.WriteLine($"Requested scopes: {string.Join(" ", requestedScopes)}");
        if (string.IsNullOrEmpty(result.AccessToken))
        {
            Console.WriteLine("No access token returned, only an id token: the scope is not granted.");
            Console.WriteLine("Check that the API permission is added to the app registration, admin consent is granted and the scope is exposed by the API.");
            return;
        }

        Console.WriteLine($"Granted scopes:   {string.Join(" ", result.Scopes)}");
    }

    private static void Log(LogLevel level, string message, bool containsPii)
    {
        var logs = ($"{level} {message}");
        var sb = new StringBuilder();
        sb.Append(logs);
        File.AppendAllText(System.Reflection.Assembly.GetExecutingAssembly().Location + ".msalLogs.txt", sb.ToString());
        sb.Clear();
    }
}
