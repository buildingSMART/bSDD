# bSDD C# console demo

Shows the two ways to call the secured bSDD API (test environment) with .NET 8.

| Scenario | Who authenticates | What the demo does |
| --- | --- | --- |
| User | A person signs in interactively | Search in a dictionary |
| Machine to machine | Your app registration, with client id and secret | Upload an import file |

## User scenario

```
ConsoleDemo
```

Your browser opens so you can sign in. The token contains your e-mail address, the API uses it to determine
what you are allowed to do. See `UserAuthentication.cs`.

## Machine to machine scenario

```
ConsoleDemo --machine-to-machine --client-id <client id> --secret <secret> --organization <organization code> [--file <path>]
```

The app registration authenticates itself with its secret (client credentials flow), no user is involved.
Because there is no e-mail address in the token, the API checks whether the client id of your app registration is
registered as a user of the organization. See `ApplicationAuthentication.cs`.

If you leave out `--file`, the first `*.json` file in the current directory is uploaded. If the directory holds no
json file, the demo reports that and stops.

Run `ConsoleDemo --help` for all options.

## Looking at the access token

Add `--show-token` to either scenario to print the header and the claims of the access token, for example to check
that an application token has `idtyp` `app`, your client id in `azp` and the granted permissions in `scp`.
The token itself is not printed and it is only decoded, not validated.

> A secret on the command line can be read by other users of the machine and ends up in your command history.
> That is fine for this demo; in your own application use a certificate or a key vault.

### What you need first

1. Ask bSDD support for an app registration in the buildingSMART B2C tenant, with a secret and the application
   permissions your integration needs (`read.all` to upload, `manage.dictionaries.all` to also change dictionary status).
2. Ask bSDD support to register the client id of that app registration as a user of your organization, with the
   right to upload. You can add e-mail addresses there to receive the import results, because an application has
   no mailbox of its own.
3. The client credentials flow uses the custom policy `B2C_1A_CLIENTCREDENTIALSFLOW` in Azure AD B2C. Use `--policy`
   if you need another one.
