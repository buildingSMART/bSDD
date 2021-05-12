using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;

namespace bSDD.DemoClientConsole
{
    public static class Helpers
    {
        public static JObject ParseIdToken(string idToken)
        {
            // Parse the idToken to get user info
            idToken = idToken.Split('.')[1];
            idToken = Base64UrlDecode(idToken);
            return JObject.Parse(idToken);
        }

        public static async Task<string> GetHttpContent(string url)
        {
            var httpClient = new HttpClient();
            HttpResponseMessage response;
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                response = await httpClient.SendAsync(request).ConfigureAwait(false);

                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    return $"{response.StatusCode} - {response.ReasonPhrase} - {content}";
                }
                return content;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// Perform an HTTP GET request to a URL using an HTTP Authorization header
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="token">The token</param>
        /// <returns>String containing the results of the GET operation</returns>
        public static async Task<string> GetHttpContentWithToken(string url, string token)
        {
            var httpClient = new HttpClient();
            HttpResponseMessage response;
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                response = await httpClient.SendAsync(request).ConfigureAwait(false);

                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    return $"{response.StatusCode} - {response.ReasonPhrase} - {content}";
                }
                return content;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// Perform an HTTP POST request to a URL using an HTTP Authorization header
        /// </summary>
        /// <returns>String containing the results of the GET operation</returns>
        public static async Task<string> PostHttpContentWithToken(string url, string token, string jsonContent)
        {
            var httpClient = new HttpClient();
            HttpResponseMessage response;
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var stringContent = new StringContent(jsonContent);
                stringContent.Headers.Remove("Content-Type");
                stringContent.Headers.Add("Content-Type", $"application/json");
                response = await httpClient.PostAsync(url, stringContent);

                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return $"Not OK: {response.StatusCode} - {response.ReasonPhrase} - {content}";
                }
                return content;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static async Task<AuthenticationResult> SignIn(IPublicClientApplication app, string[] apiScopes, string authorityResetPassword, IntPtr? windowHandle)
        {
            AuthenticationResult authResult;
            try
            {
                if (windowHandle != null)
                {
                    authResult = await app.AcquireTokenInteractive(apiScopes)
                         .WithParentActivityOrWindow(windowHandle)
                         .ExecuteAsync()
                         .ConfigureAwait(false);
                }
                else
                {
                    authResult = await app.AcquireTokenInteractive(apiScopes)
                        // .WithParentActivityOrWindow(new WindowInteropHelper(this).Handle)
                        .ExecuteAsync()
                        .ConfigureAwait(false);
                }

                return authResult;
            }
            catch (MsalException ex)
            {
                if (ex.Message.Contains("AADB2C90118"))
                {
                    authResult = await app.AcquireTokenInteractive(apiScopes)
                        // .WithParentActivityOrWindow(new WindowInteropHelper(this).Handle)
                        .WithPrompt(Prompt.SelectAccount)
                        .WithB2CAuthority(authorityResetPassword)
                        .ExecuteAsync()
                        .ConfigureAwait(false);

                    return authResult;
                }

                throw;
            }
        }

        private static string Base64UrlDecode(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(s);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
            return decoded;
        }
    }
}
