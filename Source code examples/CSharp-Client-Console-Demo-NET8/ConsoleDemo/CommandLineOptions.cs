namespace ConsoleDemo;

/// <summary>
/// Options given on the command line, see <see cref="GetUsage"/>.
/// </summary>
public class CommandLineOptions
{
    /// <summary>
    /// Use the machine to machine scenario (client credentials) instead of signing in as a user
    /// </summary>
    public bool MachineToMachine { get; private set; }

    /// <summary>
    /// Client id of your own app registration, only for the machine to machine scenario
    /// </summary>
    public string? ClientId { get; private set; }

    /// <summary>
    /// Secret of your own app registration, only for the machine to machine scenario
    /// </summary>
    public string? ClientSecret { get; private set; }

    /// <summary>
    /// Policy used for the client credentials flow
    /// </summary>
    public string Policy { get; private set; } = BsddSettings.PolicyClientCredentials;

    /// <summary>
    /// The import file to upload. If not given, the first json file in the current directory is used.
    /// </summary>
    public string? FilePath { get; private set; }

    /// <summary>
    /// Code of the organization to upload for
    /// </summary>
    public string? OrganizationCode { get; private set; }

    /// <summary>
    /// Only validate the file, do not import it
    /// </summary>
    public bool ValidateOnly { get; private set; }

    /// <summary>
    /// Upload as test data
    /// </summary>
    public bool IsTest { get; private set; } = true;

    /// <summary>
    /// Show the contents (claims) of the access token
    /// </summary>
    public bool ShowToken { get; private set; }

    /// <summary>
    /// Host of the B2C tenant, to be able to test the custom domain as well as b2clogin.com
    /// </summary>
    public string? Hostname { get; private set; }

    /// <summary>
    /// Scope to request, default the .default scope of the bSDD API
    /// </summary>
    public string Scope { get; private set; } = BsddSettings.ApplicationScope;

    /// <summary>
    /// Base url of the API to call, e.g. https://localhost:44392 to debug your local API
    /// </summary>
    public string ApiUrl { get; private set; } = BsddSettings.ApiBaseUrl;

    public static bool TryParse(string[] args, out CommandLineOptions options, out string? error)
    {
        options = new CommandLineOptions();
        error = null;

        for (var i = 0; i < args.Length; i++)
        {
            var argument = args[i].ToLowerInvariant();
            switch (argument)
            {
                case "--machine-to-machine":
                case "-m":
                    options.MachineToMachine = true;
                    break;
                case "--client-id":
                    if (!TryGetValue(args, ref i, out var clientId, out error))
                    {
                        return false;
                    }
                    options.ClientId = clientId;
                    break;
                case "--secret":
                    if (!TryGetValue(args, ref i, out var secret, out error))
                    {
                        return false;
                    }
                    options.ClientSecret = secret;
                    break;
                case "--policy":
                    if (!TryGetValue(args, ref i, out var policy, out error))
                    {
                        return false;
                    }
                    options.Policy = policy;
                    break;
                case "--api-url":
                    if (!TryGetValue(args, ref i, out var apiUrl, out error))
                    {
                        return false;
                    }
                    options.ApiUrl = apiUrl.TrimEnd('/');
                    break;
                case "--scope":
                    if (!TryGetValue(args, ref i, out var scope, out error))
                    {
                        return false;
                    }
                    options.Scope = scope;
                    break;
                case "--b2c-host":
                    if (!TryGetValue(args, ref i, out var hostname, out error))
                    {
                        return false;
                    }
                    options.Hostname = hostname;
                    break;
                case "--file":
                case "-f":
                    if (!TryGetValue(args, ref i, out var file, out error))
                    {
                        return false;
                    }
                    options.FilePath = file;
                    break;
                case "--organization":
                case "-o":
                    if (!TryGetValue(args, ref i, out var organizationCode, out error))
                    {
                        return false;
                    }
                    options.OrganizationCode = organizationCode;
                    break;
                case "--validate-only":
                    options.ValidateOnly = true;
                    break;
                case "--no-test":
                    options.IsTest = false;
                    break;
                case "--show-token":
                case "-t":
                    options.ShowToken = true;
                    break;
                case "--help":
                case "-h":
                case "-?":
                    error = null;
                    return false;
                default:
                    error = $"Unknown argument '{args[i]}'";
                    return false;
            }
        }

        if (!options.MachineToMachine)
        {
            return true;
        }

        if (string.IsNullOrWhiteSpace(options.ClientSecret))
        {
            error = "The machine to machine scenario needs the secret of your app registration: --secret <secret>";
            return false;
        }

        if (string.IsNullOrWhiteSpace(options.ClientId))
        {
            error = "The machine to machine scenario needs the client id of your app registration: --client-id <client id>";
            return false;
        }

        if (string.IsNullOrWhiteSpace(options.OrganizationCode))
        {
            error = "The machine to machine scenario needs the organization to upload for: --organization <organization code>";
            return false;
        }

        return true;
    }

    public static string GetUsage()
    {
        return """
               bSDD console demo

               Usage:
                 ConsoleDemo                                     Sign in as a user and search in a dictionary
                 ConsoleDemo --machine-to-machine [options]      Get a token for an application and upload an import file

               Options for both scenarios:
                 --show-token                Show the claims in the access token (the token itself is not shown)
                 --api-url <url>             API to call. Default: https://test.bsdd.buildingsmart.org
                                             Use https://localhost:44392 to call (and debug) your local API

               Options for the machine to machine scenario:
                 --client-id <client id>     Client id of your app registration (required)
                 --secret <secret>           Secret of your app registration (required)
                 --organization <code>       Code of the organization to upload for (required)
                 --file <path>               Import file to upload. Default: the first *.json file in the current directory
                 --policy <policy>           Policy for the client credentials flow. Default: B2C_1A_CLIENTCREDENTIALSFLOW
                 --b2c-host <hostname>       Host of the B2C tenant. Default: authentication.buildingsmart.org
                 --scope <scope>             Scope to request. Default: the .default scope of the bSDD API (see BsddSettings)
                 --validate-only             Only validate the file, do not import it
                 --no-test                   Upload as real data instead of test data

               Note: a secret on the command line can be read by other users of the machine and is stored in your
               command history. Use it for this demo only, in your own application use a certificate or a key vault.
               """;
    }

    private static bool TryGetValue(string[] args, ref int index, out string value, out string? error)
    {
        value = string.Empty;
        error = null;
        if (index + 1 >= args.Length)
        {
            error = $"Argument '{args[index]}' needs a value";
            return false;
        }

        index++;
        value = args[index];
        return true;
    }
}
