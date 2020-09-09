using System;
using System.IO;
using System.Linq;
using System.Text;
using bSDD.DemoClientConsole.Contract;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace bSDD.DemoClientConsole
{
    public static class Program
    {
        // For authentication & authorization (those items should be in a config file)
        public static readonly string TenantName = "bsddprototype1";
        private static readonly string Tenant = $"{TenantName}.onmicrosoft.com";
        private static readonly string AzureAdB2CHostname = $"{TenantName}.b2clogin.com";
        public static readonly string ClientId = "e2d11588-bf15-47eb-bdf8-2c61541fb474";
        public static readonly string RedirectUri = "com.onmicrosoft.bsddprototypeb2c.democonsoleapp://oauth/redirect";
        public static string PolicySignUpSignIn = "b2c_1_signupsignin1";
        public static string PolicyEditProfile = "b2c_1_profileediting1";
        public static string PolicyResetPassword = "b2c_1_passwordreset1";

        public static string[] ApiScopes = { "https://bsddprototype1.onmicrosoft.com/api/read" };

        private static string AuthorityBase = $"https://{AzureAdB2CHostname}/tfp/{Tenant}/";
        public static string AuthoritySignUpSignIn = $"{AuthorityBase}{PolicySignUpSignIn}";
        public static string AuthorityEditProfile = $"{AuthorityBase}{PolicyEditProfile}";
        public static string AuthorityResetPassword = $"{AuthorityBase}{PolicyResetPassword}";

        // For accessing API endpoint
        public static string ApiEndpoint = "https://bs-dd-api-prototype.azurewebsites.net/api/SearchList?DomainGuid=ed05efb8-bb61-4170-ab6e-92387bf77764";

        public static int Main(string[] args)
        {
            var publicClientApp = PublicClientApplicationBuilder.Create(ClientId)
                .WithB2CAuthority(AuthoritySignUpSignIn)
                .WithRedirectUri(RedirectUri)
                .WithLogging(Log, LogLevel.Info, false) // don't log PII details on a regular basis
                .Build();

            TokenCacheHelper.Bind(publicClientApp.UserTokenCache);
            AuthenticationResult authResult = null;
            try
            {
                var accounts = publicClientApp.GetAccountsAsync(PolicySignUpSignIn).GetAwaiter().GetResult();

                authResult = publicClientApp.AcquireTokenSilent(ApiScopes, accounts.FirstOrDefault())
                    .ExecuteAsync().GetAwaiter().GetResult();

                DisplayUserInfo(authResult);
            }
            catch (MsalUiRequiredException)
            {
                Console.WriteLine("You need to sign-in first");
                try
                {
                    authResult = Helpers.SignIn(publicClientApp, ApiScopes, AuthorityResetPassword, null).GetAwaiter().GetResult();
                    DisplayUserInfo(authResult);
                }
                catch (Exception e)
                {
                    return ExitWithError(e.ToString());
                }
            }
            catch (Exception ex)
            {
                return ExitWithError($"Error Acquiring Token Silently:{Environment.NewLine}{ex}");
            }

            Console.WriteLine($"Calling {ApiEndpoint}...");
            var resultText = Helpers.GetHttpContentWithToken(ApiEndpoint, authResult.AccessToken).GetAwaiter().GetResult();
            var searchResult = JsonConvert.DeserializeObject<SearchResultContract>(resultText);
            Console.WriteLine("Result received");
            Console.WriteLine($"Number of classifications found: {searchResult.NumberOfClassificationsFound}");
            if (searchResult.NumberOfClassificationsFound > 0)
            {
                // Just printing first item from result list for demo purposes
                var firstDomain = searchResult.Domains[0];
                var firstClassification = firstDomain.Classifications[0];
                Console.WriteLine($"First item in result list, domain: {firstDomain.Name}, classification: {firstClassification.Name}");
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to close");
            Console.ReadLine();
            return 0;
        }

        private static int ExitWithError(string error)
        {
            Console.WriteLine(error);
            Console.WriteLine();
            Console.WriteLine("Press Enter to close");
            Console.ReadLine();

            return 1;
        }

        private static void DisplayUserInfo(AuthenticationResult authResult)
        {
            if (authResult != null)
            {
                JObject user = Helpers.ParseIdToken(authResult.IdToken);

                Console.WriteLine($"Name: {user["name"]}");
                Console.WriteLine($"User Identifier: {user["oid"]}");
                Console.WriteLine($"Street Address: {user["streetAddress"]}");
                Console.WriteLine($"City: {user["city"]}");
                Console.WriteLine($"State: {user["state"]}");
                Console.WriteLine($"Country: {user["country"]}");
                Console.WriteLine($"Job Title: {user["jobTitle"]}");

                if (user["emails"] is JArray emails)
                {
                    Console.WriteLine($"Emails: {emails[0]}");
                }
                Console.WriteLine($"Identity Provider: {user["iss"]}");
            }
        }

        private static void Log(LogLevel level, string message, bool containsPii)
        {
            string logs = ($"{level} {message}");
            StringBuilder sb = new StringBuilder();
            sb.Append(logs);
            File.AppendAllText(System.Reflection.Assembly.GetExecutingAssembly().Location + ".msalLogs.txt", sb.ToString());
            sb.Clear();
        }
    }
}
