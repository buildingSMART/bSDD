﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using bSDD.DemoClientConsole.Contract;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace bSDD.DemoClientConsole
{
    public static class Program
    {
        // For authentication & authorization (those items should be in a config file)
        public static readonly string TenantName = "buildingsmartservices";
        private static readonly string Tenant = $"{TenantName}.onmicrosoft.com";
        private static readonly string AzureAdB2CHostname = $"{TenantName}.b2clogin.com";
        public static readonly string ClientId = "4aba821f-d4ff-498b-a462-c2837dbbba70";
        // public static readonly string RedirectUri = "com.onmicrosoft.bsddprototypeb2c.democonsoleapp://oauth/redirect";
        public static readonly string RedirectUri = "https://buildingsmartservices.b2clogin.com/oauth2/nativeclient";

        // Not case sensitive
        public static string PolicySignUpSignIn = "b2c_1_signupsignin";
        public static string PolicyEditProfile = "b2c_1_profileediting";
        public static string PolicyResetPassword = "b2c_1_passwordreset";

        // Not case sensitive
        public static string[] ApiScopes = { "https://buildingsmartservices.onmicrosoft.com/api/read" };

        private static string AuthorityBase = $"https://{AzureAdB2CHostname}/tfp/{Tenant}/";
        public static string AuthoritySignUpSignIn = $"{AuthorityBase}{PolicySignUpSignIn}";
        public static string AuthorityEditProfile = $"{AuthorityBase}{PolicyEditProfile}";
        public static string AuthorityResetPassword = $"{AuthorityBase}{PolicyResetPassword}";

        // For accessing API endpoint
        public static string ApiEndpointSecure = "https://bsdd-prototype.azure-api.net/api/SearchList/v2?DomainNamespaceUri=" + WebUtility.UrlEncode("http://identifier.buildingsmart.org/uri/etim/etim-7.0") + "&SearchText=room";

        private static IPublicClientApplication publicClientApp;

        public static int Main(string[] args)
        {
            publicClientApp = PublicClientApplicationBuilder.Create(ClientId)
                .WithB2CAuthority(AuthoritySignUpSignIn)
                .WithRedirectUri(RedirectUri)
                .WithLogging(Log, LogLevel.Info, false) // don't log PII details on a regular basis
                .Build();

            TokenCacheHelper.Bind(publicClientApp.UserTokenCache);

            Console.WriteLine("Press L to clear the token cache before continuing. Any other character just continues.");
            var keyInfo = Console.ReadKey();

            if (keyInfo.KeyChar == 'l' || keyInfo.KeyChar == 'L')
            {
                Console.WriteLine();
                Console.WriteLine("Clearing token cache...");
                ClearTokenCache().GetAwaiter().GetResult();
            }

            Console.WriteLine("Reading data...");
            if (SecuredExample(ApiEndpointSecure, out var resultText, out var exitWithError))
            {
                return exitWithError;
            }

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

        private static bool SecuredExample(string fullUrl, out string resultText, out int exitWithError)
        {
            AuthenticationResult authResult;
            try
            {
                var accounts = publicClientApp.GetAccountsAsync(PolicySignUpSignIn).GetAwaiter().GetResult();
                var account = accounts.FirstOrDefault();
                authResult = publicClientApp.AcquireTokenSilent(ApiScopes, account)
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
                    exitWithError = ExitWithError(e.ToString());
                    resultText = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                exitWithError = ExitWithError($"Error Acquiring Token Silently:{Environment.NewLine}{ex}");
                resultText = string.Empty;
                return true;
            }

            Console.WriteLine($"Calling {fullUrl}...");
            resultText = Helpers.GetHttpContentWithToken(fullUrl, authResult.AccessToken).GetAwaiter().GetResult();
            if (resultText.Contains("Unauthorized"))
            {
                throw new UnauthorizedAccessException(resultText);
            }

            exitWithError = 0;
            return false;
        }

        private static async Task ClearTokenCache()
        {
            var accounts = (await publicClientApp.GetAccountsAsync(PolicySignUpSignIn)).ToList();

            // clear the cache
            while (accounts.Any())
            {
                await publicClientApp.RemoveAsync(accounts.First());
                accounts = (await publicClientApp.GetAccountsAsync(PolicySignUpSignIn)).ToList();
            }
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
                var user = Helpers.ParseIdToken(authResult.IdToken);

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
            var logs = ($"{level} {message}");
            var sb = new StringBuilder();
            sb.Append(logs);
            File.AppendAllText(System.Reflection.Assembly.GetExecutingAssembly().Location + ".msalLogs.txt", sb.ToString());
            sb.Clear();
        }
    }
}
