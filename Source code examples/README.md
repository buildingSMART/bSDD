

**IMPORTANT change per June 30, 2021** For developers using the secured sections: the security policies will change a bit.
Per June 30, 2021 you have to replace "b2c_1_signupsignin" by "b2c_1a_signupsignin_c". If you also implemented the password reset and/or profile edit flow, you also need to change "b2c_1_passwordreset" into "b2c_1a_passwordreset_c" and "b2c_1_profileediting" into "b2c_1a_profileedit_c".
It works the same, the only change is that the user needs to consent to the terms of use once at log in.

**IMPORANT for GraphQL users** There is now a secured graphql API available: https://bs-dd-api-prototype.azurewebsites.net/graphqls (note the 's' at the end). When we're ready for production, only the secured API will be available! You can find example code how to access a secured bSDD API in this repository. Contact us if you need assistance implementing accessing the secured API.


The bSDD API is currently under development. This means things may change. If you’re actively going to use the API please let us know if you would like to be notified of changes. Send a mail to bsdd_support@buildingsmart.org.



## The bSDD API
The bSDD API offers methods to retrieve Classification and Property information for several Standards (also known as Domains), for example IFC and ETIM.
An example flow is:
* User opens screen to search for a Classification and its Properties
* After opening screen the app calls the “Domain”-method of the API to retrieve a list of available Domains. This list can then be presented to the user to make a selection.
* The user selects a Domain and enters some text to find the required Classification
* The user press Search and the app sends the request to the bSDD API (“SearchList”-method)
* The result is a list of Classifications
* The user can pick the one needed
* The app sends a request for Classification details and Properties to the bSDD API (“Classification”-method)
* The API returns Classification details and Properties, which the app shows to the user

A typical use-case is demonstrated in SketchUp. A video of the SketchUp use-case and bSDD plugin is availalbe on https://vimeo.com/446417661/ff8b6605d3

## API contracts and testing the API
You can get the API contract information at [bSDD API contract](https://bs-dd-api-prototype.azurewebsites.net/swagger). This information is available without the need for you to log in. You can also test the API methods. Secured methods are marked with a lock. To access secured methods you need to log in via the UI by using the Authorize button:

![Swagger authorization](https://bsddprototype2020.blob.core.windows.net/public/images/swagger-authorize2.png)

Don’t forget to check the “read” scope!

## GraphQL
The data can also be accessed via GraphQL.
[GraphiQL playground](https://bs-dd-api-prototype.azurewebsites.net/graphiql).
The url to send your GraphQL requests to: https://bs-dd-api-prototype.azurewebsites.net/graphql. For accessing this URL no authentication is needed. There is also a secured API available: https://bs-dd-api-prototype.azurewebsites.net/graphqls. When moving to production only the secured API will be avialable and no GraphiQL playground.

[Some bSDD GraphQL examples](https://github.com/buildingSMART/bSDD/blob/master/2020%20prototype/GraphQL/bSDD%20and%20GraphQL.md)

## Authentication
For authentication we use Azure Active Directory B2C.
At this moment only the SearchList API method is secured. This will change when we’re moving to version 1.0 of the API, then probably all API methods will be secured.

If you’re developing a Javascript, Java, Angular, Python or .NET application connecting with the buildingSMART Data Dictionary API is easiest by using the Microsoft Authentication Library (MSAL).
See [Active directory B2C code samples](https://docs.microsoft.com/en-us/azure/active-directory-b2c/code-samples) for ready to use examples on how to use the MSAL. You can find the bSDD API specific settings in one of the next sections of this document. Note: all settings will change. So make sure you have them in an easy to update settings file.
You can find the code for a small .NET console application that accesses the bSDD API (authenticated) in this repository: [.NET console example](https://github.com/buildingSMART/bSDD/tree/master/2020%20prototype/CSharp-Client-Console-Demo).

If you’re developing using one of the many other available languages it is still possible to connect to the bSDD API. The API is developed according to the standards OpenAPI, OAuth2 and OpenID Connect. Only now you need to do all the plumbing yourself.

To access a secured API a user must first register himself. When you’re using MSAL there’s nothing special you need to do for this. The user will be prompted to log in, via a browser window. If the user does not have a buildingSMART API account, he can sign up:

![bSDD sign up / sign in](https://bsddprototype2020.blob.core.windows.net/public/images/bs-signupsignin.png)

The user will be registered in the buildingSMART Azure B2C Active Directory.
Currently there’s no further authorization required to be able to use the API.

## Settings
These are the settings you can use for demonstration purposes for a Dekstop client app :
* Tenant: "buildingsmartservices.onmicrosoft.com "
* AzureAdB2Chostname: "buildingsmartservices.b2clogin.com"
* ClientId: "4aba821f-d4ff-498b-a462-c2837dbbba70"
* RedirectUri: "com.onmicrosoft.bsddprototypeb2c.democonsoleapp://oauth/redirect"
* PolicySignUpSignIn: "b2c_1a_signupsignin_c"
* PolicyEditProfile: "b2c_1a_profileedit_c"
* PolicyResetPassword: "b2c_1a_passwordreset_c"

* ApiScopes: { "https://buildingsmartservices.onmicrosoft.com/api/read" }
* BsddApiUrl: "https://bs-dd-api-prototype.azurewebsites.net"

The full B2C authority url is: https://buildingsmartservices.b2clogin.com/tfp/buildingsmartservices.onmicrosoft.com/b2c_1a_signupsignin_c (note the "tfp" part!).

If you are developing a Web App that’s going to use the bSDD API, let us know (bsdd_support@buildingsmart.org). The RedirectURI needs to be configured in Azure AD.

## Additional information
Language independent description of the authorization flow: [Authorization code flow](https://docs.microsoft.com/en-us/azure/active-directory-b2c/authorization-code-flow)

High level descriptions of the various authentication flows: [AD B2C application types](https://docs.microsoft.com/en-us/azure/active-directory-b2c/application-types)

Oauth2 and OpenId protocol descriptions: [AD B2C protocols overview](https://docs.microsoft.com/en-us/azure/active-directory-b2c/protocols-overview)

