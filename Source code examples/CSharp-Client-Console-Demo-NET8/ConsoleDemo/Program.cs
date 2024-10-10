using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    private static readonly HttpClient httpClient = new HttpClient();

    static async Task Main(string[] args)
    {
        string accessToken = await GetAccessTokenAsync();
        if (!string.IsNullOrEmpty(accessToken))
        {
            string apiUrl = Constants.SearchListUrl;
            string response = await CallApiAsync(apiUrl, accessToken);
            Console.WriteLine(response);
        }
        else
        {
            Console.WriteLine("Failed to obtain access token.");
        }
    }

    static async Task<string> GetAccessTokenAsync()
    {
        try
        {
            string tokenUrl = Constants.AuthoritySignUpSignIn;
            var requestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("client_id", Constants.ClientId),
                new KeyValuePair<string, string>("username", "sanjay.kasar@schindler.com"),
                new KeyValuePair<string, string>("password", "Bsdd@1234"),
                new KeyValuePair<string, string>("scope", string.Join(" ", Constants.ApiScopes))
            });

            var response = await httpClient.PostAsync(tokenUrl, requestContent);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<TokenResponse>();
                return responseData.access_token;
            }
            else
            {
                Console.WriteLine($"Failed to obtain access token. Status Code: {response.StatusCode}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while getting access token: {ex.Message}");
            return null;
        }
    }

    static async Task<string> CallApiAsync(string apiUrl, string accessToken)
    {
        try
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                Console.WriteLine($"Failed to call API. Status Code: {response.StatusCode}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while calling API: {ex.Message}");
            return null;
        }
    }
}

public static class Constants
{
    public static readonly string TenantName = "buildingsmartservices";
    public static readonly string ClientId = "12c33ebc-677a-4834-9168-de8bae2d9549";
    public static readonly string RedirectUri = "https://authentication.buildingsmart.org/oauth2/nativeclient";
    private static readonly string Tenant = $"{TenantName}.onmicrosoft.com";
    private static readonly string AzureAdB2CHostname = "authentication.buildingsmart.org";
    public static string PolicySignUpSignIn = "b2c_1a_signupsignin_c";
    public static string PolicyEditProfile = "b2c_1a_profileedit_c";
    public static string PolicyResetPassword = "b2c_1a_passwordreset_c";
    public static string[] ApiScopes = { $"https://{TenantName}.onmicrosoft.com/api/read" };
    private static string AuthorityBase = $"https://{AzureAdB2CHostname}/tfp/{Tenant}/";
    public static string AuthoritySignUpSignIn = $"{AuthorityBase}{PolicySignUpSignIn}";
    public static string AuthorityEditProfile = $"{AuthorityBase}{PolicyEditProfile}";
    public static string AuthorityResetPassword = $"{AuthorityBase}{PolicyResetPassword}";
    public const string ApiBaseUrl = "https://test.bsdd.buildingsmart.org";
    public static string SearchListUrl = $"{ApiBaseUrl}/api/SearchList/v2?DomainNamespaceUri=" + WebUtility.UrlEncode("https://identifier.buildingsmart.org/uri/buildingsmart/ifc/4.3") + "&SearchText=room";
}

public class TokenResponse
{
    public string access_token { get; set; }
}






//using System;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading.Tasks;





//class Program
//{
//    private static readonly HttpClient httpClient = new HttpClient();

//    static Program()
//    {
//        //var httpClientHandler = new HttpClientHandler
//        //{
//        //    Proxy = new WebProxy("http://webgateway-eu.schindler.com:3128"),
//        //    UseProxy = true,
//        //    UseDefaultCredentials = false,
//        //    Credentials = new NetworkCredential("admkasarsa", "%") // Replace with actual username and password
//        //};

//        //httpClient = new HttpClient(httpClientHandler);
//    }
//    //private static readonly HttpClient httpClient = new HttpClient();

//    static async Task Main(string[] args)
//    {
//        string accessToken = await GetAccessTokenAsync();
//        if (!string.IsNullOrEmpty(accessToken))
//        {
//            string apiUrl = Constants.SearchListUrl;
//            string response = await CallApiAsync(apiUrl, accessToken);
//            Console.WriteLine(response);
//        }
//        else
//        {
//            Console.WriteLine("Failed to obtain access token.");
//        }
//    }

//    static async Task<string> GetAccessTokenAsync()
//    {
//        try
//        {
//            string tokenUrl = Constants.AuthoritySignUpSignIn;
//            var requestContent = new FormUrlEncodedContent(new[]
//            {
//                new KeyValuePair<string, string>("grant_type", "client_credentials"),
//                new KeyValuePair<string, string>("client_id", Constants.ClientId),
//                new KeyValuePair<string, string>("redirect_uri", Constants.RedirectUri),
//                new KeyValuePair<string, string>("scope", string.Join(" ", Constants.ApiScopes))
//            });

//            var response = await httpClient.PostAsync(tokenUrl, requestContent);
//            if (response.IsSuccessStatusCode)
//            {
//                var responseContent = await response.Content.ReadAsStringAsync();
//                // Parse access token from responseContent
//                return "YOUR_ACCESS_TOKEN";
//            }
//            else
//            {
//                Console.WriteLine($"Failed to obtain access token. Status Code: {response.StatusCode}");
//                return null;
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"An error occurred while getting access token: {ex.Message}");
//            return null;
//        }
//    }

//    static async Task<string> CallApiAsync(string apiUrl, string accessToken)
//    {
//        try
//        {
//            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
//            var response = await httpClient.GetAsync(apiUrl);
//            if (response.IsSuccessStatusCode)
//            {
//                return await response.Content.ReadAsStringAsync();
//            }
//            else
//            {
//                Console.WriteLine($"Failed to call API. Status Code: {response.StatusCode}");
//                return null;
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"An error occurred while calling API: {ex.Message}");
//            return null;
//        }
//    }
//}

//public static class Constants
//{
//    public static readonly string TenantName = "buildingsmartservices";
//    public static readonly string ClientId = "12c33ebc-677a-4834-9168-de8bae2d9549";
//    public static readonly string RedirectUri = "https://authentication.buildingsmart.org/oauth2/nativeclient";
//    private static readonly string Tenant = $"{TenantName}.onmicrosoft.com";
//    private static readonly string AzureAdB2CHostname = "authentication.buildingsmart.org";
//    public static string PolicySignUpSignIn = "b2c_1a_signupsignin_c";
//    public static string PolicyEditProfile = "b2c_1a_profileedit_c";
//    public static string PolicyResetPassword = "b2c_1a_passwordreset_c";
//    public static string[] ApiScopes = { $"https://{TenantName}.onmicrosoft.com/api/read" };
//    private static string AuthorityBase = $"https://{AzureAdB2CHostname}/tfp/{Tenant}/";
//    public static string AuthoritySignUpSignIn = $"{AuthorityBase}{PolicySignUpSignIn}";
//    public static string AuthorityEditProfile = $"{AuthorityBase}{PolicyEditProfile}";
//    public static string AuthorityResetPassword = $"{AuthorityBase}{PolicyResetPassword}";
//    public const string ApiBaseUrl = "https://test.bsdd.buildingsmart.org";
//    public static string SearchListUrl = $"{ApiBaseUrl}/api/SearchList/v2?DomainNamespaceUri=" + WebUtility.UrlEncode("https://identifier.buildingsmart.org/uri/buildingsmart/ifc/4.3") + "&SearchText=room";
//}
//class Program
//{
//static async Task Main(string[] args)
//{
//    string baseUrl = "https://bs-dd-api-prototype.azurewebsites.net/api/v3";
//    string apiKey = "12c33ebc-677a-4834-9168-de8bae2d9549"; // Replace "client id" with your actual API key

//    // Create HttpClient instance
//    HttpClient client = new HttpClient();

//    // Set the API key in the request headers
//    client.DefaultRequestHeaders.Add("ApiKey", apiKey);

//    try
//    {
//        // Create a new project (POST request)
//        string createProjectUrl = $"{baseUrl}/projects";
//        string newProjectJson = "{\"name\":\"New Project\",\"description\":\"Sample description\"}";

//        HttpResponseMessage createResponse = await client.PostAsync(createProjectUrl, new StringContent(newProjectJson, Encoding.UTF8, "application/json"));

//        if (createResponse.IsSuccessStatusCode)
//        {
//            // Read the response to get the created project ID
//            string createResponseBody = await createResponse.Content.ReadAsStringAsync();
//            Console.WriteLine("Project created successfully.");

//            // Extract project ID from the response if needed
//            // Example: string projectId = extractProjectId(createResponseBody);

//            // Retrieve information about the created project (GET request)
//            // Replace "{projectId}" with the actual ID of the created project
//            string getProjectUrl = $"{baseUrl}/project/{{projectId}}";
//            HttpResponseMessage getResponse = await client.GetAsync(getProjectUrl);

//            if (getResponse.IsSuccessStatusCode)
//            {
//                string getResponseBody = await getResponse.Content.ReadAsStringAsync();
//                Console.WriteLine("Project retrieved successfully:");
//                Console.WriteLine(getResponseBody);
//            }
//            else
//            {
//                Console.WriteLine($"Failed to retrieve project information. Status code: {getResponse.StatusCode}");
//            }
//        }
//        else
//        {
//            Console.WriteLine($"Failed to create project. Status code: {createResponse.StatusCode}");
//        }
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"An error occurred: {ex.Message}");
//    }
//    finally
//    {
//        // Dispose of the HttpClient instance
//        client.Dispose();
//    }
//}
//}