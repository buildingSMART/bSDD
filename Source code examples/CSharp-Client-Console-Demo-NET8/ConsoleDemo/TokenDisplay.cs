using System.Text;
using System.Text.Json;

namespace ConsoleDemo;

/// <summary>
/// Shows what is inside an access token (JWT). Handy to see which claims the bSDD API receives.
/// </summary>
/// <remarks>
/// The token is only decoded, not validated. Never log or share a token: anyone who has it can call the API as you.
/// </remarks>
public static class TokenDisplay
{
    public static void Show(string accessToken)
    {
        var parts = accessToken.Split('.');
        if (parts.Length < 2)
        {
            Console.WriteLine("The access token is not a JWT, cannot show its contents.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Token header:");
        Console.WriteLine(DecodePart(parts[0]));
        Console.WriteLine("Token payload:");
        Console.WriteLine(DecodePart(parts[1]));
        Console.WriteLine();
    }

    private static string DecodePart(string part)
    {
        try
        {
            var json = Encoding.UTF8.GetString(DecodeBase64Url(part));
            using var document = JsonDocument.Parse(json);
            return JsonSerializer.Serialize(document, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
        }
        catch (Exception exception) when (exception is FormatException or JsonException)
        {
            return $"(could not decode this part of the token: {exception.Message})";
        }
    }

    private static byte[] DecodeBase64Url(string value)
    {
        var base64 = value.Replace('-', '+').Replace('_', '/');
        base64 = (base64.Length % 4) switch
        {
            2 => base64 + "==",
            3 => base64 + "=",
            0 => base64,
            _ => throw new FormatException("invalid length")
        };

        return Convert.FromBase64String(base64);
    }
}
