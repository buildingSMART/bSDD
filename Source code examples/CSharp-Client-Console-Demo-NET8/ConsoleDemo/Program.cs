using ConsoleDemo;

// Demo of the two ways to call the secured bSDD API:
// - as a user: the user signs in and the API uses the e-mail address in the token (see UserAuthentication)
// - as an application (machine to machine): the app registration signs in with its secret (see ApplicationAuthentication)
// Run with --help to see all options.

if (!CommandLineOptions.TryParse(args, out var options, out var error))
{
    if (error != null)
    {
        Console.WriteLine(error);
        Console.WriteLine();
    }

    Console.WriteLine(CommandLineOptions.GetUsage());
    return error == null ? 0 : 1;
}

try
{
    if (options.MachineToMachine)
    {
        return await RunMachineToMachineDemoAsync(options);
    }

    return await RunUserDemoAsync(options);
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
    return 1;
}

// The user signs in and searches in a dictionary
static async Task<int> RunUserDemoAsync(CommandLineOptions options)
{
    Console.WriteLine("Signing in as user...");
    var accessToken = await new UserAuthentication().GetAccessTokenAsync();

    if (options.ShowToken)
    {
        TokenDisplay.Show(accessToken);
    }

    var apiClient = new BsddApiClient(accessToken, options.ApiUrl);
    await apiClient.SearchInDictionaryAsync("https://identifier.buildingsmart.org/uri/bs-agri/testpriv/1.0");
    return 0;
}

// The application signs in with its own client id and secret and uploads an import file
static async Task<int> RunMachineToMachineDemoAsync(CommandLineOptions options)
{
    if (!TryGetFileToUpload(options.FilePath, out var filePath, out var fileError))
    {
        Console.WriteLine(fileError);
        return 1;
    }

    Console.WriteLine($"Getting an access token for application '{options.ClientId}'...");
    var authentication = new ApplicationAuthentication(options.ClientId!, options.ClientSecret!, options.Policy, options.Hostname, options.Scope);
    var accessToken = await authentication.GetAccessTokenAsync();

    if (options.ShowToken)
    {
        TokenDisplay.Show(accessToken);
    }

    Console.WriteLine($"Uploading '{filePath}' for organization '{options.OrganizationCode}'...");
    var apiClient = new BsddApiClient(accessToken, options.ApiUrl);
    var isOk = await apiClient.UploadImportFileAsync(filePath, options.OrganizationCode!, options.ValidateOnly, options.IsTest);

    return isOk ? 0 : 1;
}

// Uses the given file or else the first json file in the current directory
static bool TryGetFileToUpload(string? filePath, out string fileToUpload, out string? error)
{
    fileToUpload = string.Empty;
    error = null;

    if (!string.IsNullOrWhiteSpace(filePath))
    {
        if (!File.Exists(filePath))
        {
            error = $"File '{filePath}' does not exist.";
            return false;
        }

        fileToUpload = Path.GetFullPath(filePath);
        return true;
    }

    var currentDirectory = Directory.GetCurrentDirectory();
    var firstJsonFile = Directory.EnumerateFiles(currentDirectory, "*.json").Order(StringComparer.OrdinalIgnoreCase).FirstOrDefault();
    if (firstJsonFile == null)
    {
        error = $"No json file found in '{currentDirectory}'. Use --file <path> to specify the import file to upload.";
        return false;
    }

    fileToUpload = firstJsonFile;
    Console.WriteLine($"No file given, using the first json file in the current directory: '{Path.GetFileName(firstJsonFile)}'");
    return true;
}
