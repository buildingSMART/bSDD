## The bSDD API
The bSDD API offers methods to retrieve `Class` and `Property` information of many dictionaries (standards) like IFC and ETIM. An example flow is:
* User opens screen to search for a Class and its Properties
* After opening screen the app calls the “Dictionary”-method of the API to retrieve a list of available Dictionaries. This list can then be presented to the user to make a selection.
* The user selects a Dictionary and enters some text to find the required Class
* The user presses Search, and the app sends the request to the bSDD API (“SearchList”-method)
* The result is a list of Classes
* The user can pick the one needed
* The app sends a request for Class details and Properties to the bSDD API (“Class”-method)
* The API returns Class details and Properties, which the app shows to the user

A typical use case is demonstrated in SketchUp. A video of the SketchUp use-case and bSDD plugin is available at https://vimeo.com/446417661/ff8b6605d3

**The bSDD API is regularly updated.** If there are breaking changes to the API, we create a new version and support both versions for 6 months after the change occurred. Additions to an existing API usually don't mean a breaking change and can be introduced to the same version.

## API contracts and testing the API
You can get the API contract information at [bSDD API contract, official release](https://app.swaggerhub.com/apis/buildingSMART/Dictionaries/v1). This information is available without the need for you to log in. You can also test the API methods. Secured methods are marked with a lock. To access secured methods, you need to log in via the UI by using the Authorize button:

<img src="https://raw.githubusercontent.com/buildingSMART/bSDD/master/Documentation/graphics/swagger-authorize2.png" alt="Swagger authorization" style="width: 550px" />

Fill in the following client_id: b222e220-1f71-4962-9184-05e0481a390d
Don’t forget to check the “read” scope!

## Using https://identifier.buildingsmart.org
You can access the data of the `Class` or `Property` also directly via the URI of the `Class` or `Property`. For example, you can navigate in the browser to https://identifier.buildingsmart.org/uri/buildingsmart/ifc/4.3/class/IfcWall, and then you will see a visual representation of the data of that Class. If you would like the output in JSON format, then sending an "Accept" header with "application/JSON" will give you a result in JSON. The content of this JSON result differs from the HTML result!

IMPORTANT: Do not use these identifier URIs for system-to-system communication! First of all, it introduces an extra 'hop' from server to server. Second, you do not have control over the version of the API it's using. The result may differ after a new release of bSDD has been published with the result from before the release.

> Note: Getting the data in JSON format by directly calling the https://identifier.buildingsmart.org URL is now DEPRECATED. Use api/Class/vX or api/Property/vX instead.

## bSDD test environment

The bSDD has a TEST environment for testing new developments of the bSDD. Although meant for internal use, developers wanting to use the bSDD APIs are welcome to use the TEST environment for development purposes. We do not have an SLA for that environment and we don't recommend showing its content to users. If you're a Dictionary owner and want to check your data or test the upload process, please use the official bSDD.

## GraphQL
The data can also be accessed via GraphQL. You can try it out here:
[GraphiQL TEST playground](https://test.bsdd.buildingsmart.org/graphiql).


The URL to send GraphQL requests to is:
- official release: https://api.bsdd.buildingsmart.org/graphqls (secured, note the "s" at the end)
- test version: https://test.bsdd.buildingsmart.org/graphql (not secured)
- test version: https://test.bsdd.buildingsmart.org/graphqls (secured)
Note: those URLs are not hyperlinks and do not work in a browser. You need to send a POST request with the query data (the GET request does not work).

Here you can find an example code for accessing a secured bSDD API: [bSDD GraphQL examples](https://github.com/buildingSMART/bSDD/blob/master/Documentation/bSDD%20and%20GraphQL.md). Contact us if you need assistance implementing this.
 
## For client developers

### Http header "(X-)User-Agent"
Please include your application's name and version in the HTTP header "User-Agent" (or "X-User-Agent") for each HTTP call. This will allow us to better track bSDD usage and provide you with some statistics regarding your application using the bSDD API. The preferred format is "application/version," e.g., "Autodesk.Revit/2024.".

### Secure APIs
If you are going to build a client that uses secured APIs, you must request a Client ID. You can do so by sending us an email and give:
- the name of the client application
- type of application:
  - Web application
  - Single-page application
  - iOS / macOS, Objective-C, Swift, Xamarin
  - Android - Java, Kotlin, Xamarin
  - Mobile/Desktop
- which language are you using? (sometimes the redirectUri to be configured depends on the used library)
- if it is a website or SPA, specify the return URL (the login page will redirect to this URL after the user has logged in)

If you don't use the secured APIs but want to call the other APIs from your website or SPA, we need your website's URL to allow CORS.
If you're creating a desktop client that only calls the non-secured APIs, you're ready to go.

### Authentication
For authentication, we use Azure Active Directory B2C.
At this moment, you need to authenticate only a few methods. This might change.

If you’re developing a Javascript, Java, Angular, React, Python, or .NET application, connecting with the buildingSMART Data Dictionary API is easiest if you use the Microsoft Authentication Library (MSAL).
See [Active directory B2C code samples](https://docs.microsoft.com/en-us/azure/active-directory-b2c/code-samples) for ready-to-use examples on how to use the MSAL. You can find the bSDD API-specific settings in one of the next sections of this document. Make sure you have the settings in an easy-to-update settings file. 
You can find the code for a small .NET console application that accesses the bSDD API (authenticated) in this repository: [.NET console example](https://github.com/buildingSMART/bSDD/tree/master/Source%20code%20examples/CSharp-Client-Console-Demo).

React:  https://docs.microsoft.com/en-us/azure/active-directory/develop/tutorial-v2-react
        https://github.com/Azure-Samples/ms-identity-javascript-react-tutorial/blob/main/1-Authentication/2-sign-in-b2c/README.md
Angular: https://docs.microsoft.com/en-us/azure/active-directory/develop/tutorial-v2-angular-auth-code
Java: https://docs.microsoft.com/en-us/samples/azure-samples/ms-identity-java-webapp/ms-identity-java-webapp/ 
Python: https://docs.microsoft.com/en-us/python/api/overview/azure/active-directory 

If you're developing using other languages, you can still connect to the bSDD API as the API is according to the standards OpenAPI, OAuth2, and OpenID Connect.

To access a secured API, a user must first register themself. When you’re using MSAL, there’s nothing special you need to do for this. The user will be prompted to log in via a browser window. If the user does not have a buildingSMART API account, he can sign up:

<img src="https://raw.githubusercontent.com/buildingSMART/bSDD/master/Documentation/graphics/bs-signupsignin.png" alt="bSDD sign up / sign in" style="width: 350px" />

The user will be registered in the buildingSMART Azure B2C Active Directory.
Currently there’s no further authorization required to be able to use the API.

### Settings
These are the settings you can use for demonstration purposes for a Dekstop client app :
* Tenant: "buildingsmartservices.onmicrosoft.com"
* AzureAdB2Chostname: "authentication.buildingsmart.org"
* ClientId: "4aba821f-d4ff-498b-a462-c2837dbbba70"
* RedirectUri: "com.onmicrosoft.bsddprototypeb2c.democonsoleapp://oauth/redirect"
* PolicySignUpSignIn: "b2c_1a_signupsignin_c"
* PolicyEditProfile: "b2c_1a_profileedit_c"
* PolicyResetPassword: "b2c_1a_passwordreset_c"

* ApiScope : "https://buildingsmartservices.onmicrosoft.com/api/read"
* BsddApiUrl: "https://test.bsdd.buildingsmart.org"

The full B2C authority URL is: https://authentication.buildingsmart.org/tfp/buildingsmartservices.onmicrosoft.com/b2c_1a_signupsignin_c (note the "tfp" part!).

For using the official release, you should use the settings as above except:
* ClientId: request a Client ID using [CONTACT FORM](https://share.hsforms.com/1RtgbtGyIQpCd7Cdwt2l67A2wx5h)
* RedirectUri: let us know what kind of app you are making and with which technology
* ApiScope : "https://buildingsmartservices.onmicrosoft.com/bsddapi/read"
* BsddApiUrl: "https://api.bsdd.buildingsmart.org"


If you are developing a Web App that’s going to use the bSDD API, let us know ([CONTACT FORM](https://share.hsforms.com/1RtgbtGyIQpCd7Cdwt2l67A2wx5h)). The RedirectURI needs to be configured in Azure AD.

### Additional information
Language-independent description of the authorization flow: [Authorization code flow](https://docs.microsoft.com/en-us/azure/active-directory-b2c/authorization-code-flow)

High-level descriptions of the various authentication flows: [AD B2C application types](https://docs.microsoft.com/en-us/azure/active-directory-b2c/application-types)

Oauth2 and OpenId protocol descriptions: [AD B2C protocols overview](https://docs.microsoft.com/en-us/azure/active-directory-b2c/protocols-overview)

