namespace ConsoleDemo;

/// <summary>
/// Settings of the bSDD test and production environments and their Azure AD B2C tenant.
/// </summary>
public static class BsddSettings
{
    public const string TenantName = "buildingSMARTservices";
    public const string Tenant = $"{TenantName}.onmicrosoft.com";
    public const string AzureAdB2CHostname = "authentication.buildingsmart.org";

    /// <summary>
    /// Client id ('Application (client) ID' of the app registration) used for the interactive user demo.
    /// For the machine to machine demo you use the client id of your own app registration.
    /// </summary>
    public const string DemoUserClientId = "e093db4d-9bf4-47a0-bc6e-93d3d1ebf45d";

    /// <summary>
    /// Policy used to sign in a user
    /// </summary>
    public const string PolicySignUpSignIn = "b2c_1a_signupsignin_c";

    /// <summary>
    /// Policy used for the client credentials flow (machine to machine).
    /// Azure AD B2C needs a custom policy for this flow, use the option --policy if your tenant uses another name.
    /// </summary>
    public const string PolicyClientCredentials = "B2C_1A_CLIENTCREDENTIALSFLOW";

    /// <summary>
    /// 'Application ID URI' of the app registration of the bSDD API. It is also the audience of the access token,
    /// so it must match the API you call.
    /// </summary>
    public const string ApplicationIdUri = $"https://{Tenant}/bsddapi";

    public const string ApiBaseUrl = "https://api.bsdd.buildingsmart.org";

    /// <summary>
    /// Scope for a user: the user signs in and the API checks the e-mail address in the token
    /// </summary>
    public static string GetUserReadScope()
    {
        return $"{ApplicationIdUri}/read";
    }

    /// <summary>
    /// Scope for an application: all application permissions (roles) granted to the app registration, e.g. read.all and manage.dictionaries.all.
    /// The API checks if the client id of the application is registered for the organization.
    /// </summary>
    public static string GetApplicationScope()
    {
        return $"{ApplicationIdUri}/.default";
    }

    public const string RedirectUri = "http://localhost";

    public static string GetUserAuthority()
    {
        return $"https://{AzureAdB2CHostname}/tfp/{Tenant}/{PolicySignUpSignIn}";
    }

    public static string GetTokenEndpoint(string policy)
    {
        return $"https://{AzureAdB2CHostname}/{Tenant}/{policy}/oauth2/v2.0/token";
    }
}
