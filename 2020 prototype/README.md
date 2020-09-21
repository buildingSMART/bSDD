This page provides some info about the 2020 prototype of the buildingSMART Data Dictionary.

**IMPORTANT**: the bSDD API is currently under development. This means things may change. If you’re actively going to use the API please let us know if you would like to be notified of changes. Send a mail to bsdd_support@buildingsmart.org.

## Summary

* A typical use-case for bSDD 2020 prototype demonstrated in SketchUp: https://vimeo.com/446417661/ff8b6605d3
* The Swagger API documentation for the bSDD 2020 prototype: https://bs-dd-api-prototype.azurewebsites.net/swagger/index.html
* Prototype UI to search bSDD 2020 prototype: https://bs-dd-search-prototype.azurewebsites.net/
* Presentation about the bSDD (and relation to IFC): https://www.slideshare.net/berlotti/20200903-the-2020-buildingsmart-data-dictionary-prototype-bsdd

This page is to help developers create a similar use-case and help connect to the API.

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

![Swagger authorization](https://bsddprototype2020.blob.core.windows.net/public/images/swagger-authorize.png)

Don’t forget to check the “read” scope!

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
* Tenant: "bsddprototype1.onmicrosoft.com"
* AzureAdB2Chostname: "bsddprototype1.b2clogin.com"
* ClientId: "e2d11588-bf15-47eb-bdf8-2c61541fb474"
* RedirectUri: "com.onmicrosoft.bsddprototypeb2c.democonsoleapp://oauth/redirect"
* PolicySignUpSignIn: "b2c_1_signupsignin1"
* PolicyEditProfile: "b2c_1_profileediting1"
* PolicyResetPassword: "b2c_1_passwordreset1"

* ApiScopes: { "https://bsddprototype1.onmicrosoft.com/api/read" }
* BsddApiUrl: "https://bs-dd-api-prototype.azurewebsites.net"

If you are developing a Web App that’s going to use the bSDD API, let us know (bsdd_support@buildingsmart.org). The RedirectURI needs to be configured in Azure AD.

## Additional information
Language independent description of the authorization flow: [Authorization code flow](https://docs.microsoft.com/en-us/azure/active-directory-b2c/authorization-code-flow)

High level descriptions of the various authentication flows: [AD B2C application types](https://docs.microsoft.com/en-us/azure/active-directory-b2c/application-types)

Oauth2 and OpenId protocol descriptions: [AD B2C protocols overview](https://docs.microsoft.com/en-us/azure/active-directory-b2c/protocols-overview)

