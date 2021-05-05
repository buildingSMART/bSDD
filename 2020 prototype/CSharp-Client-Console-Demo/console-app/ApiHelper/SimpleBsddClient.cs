using System;
using System.Threading.Tasks;

namespace bSDD.DemoClientConsole.ApiHelper
{
    // Note: this is not the best designed piece of code, it is only showing the absolute minimum to do an API request
    public static class SimpleBsddClient
    {
        public static async Task<string> PostGraphQL(string baseUrl, string graphqlRequest, string accessToken)
        {
            var url = $"{baseUrl}/graphql";

            Console.WriteLine($"Calling {url}...");
            var jsonContent = @"{ ""query"": """ + graphqlRequest.Replace("\"", "\\\"") + @"""}";
            var resultText = await Helpers.PostHttpContentWithToken(url, accessToken, jsonContent);
            Console.WriteLine($"Result received: {resultText}");

            return resultText;
        }
    }
}
