
The bSDD API is regularly updated. This means things may change. If there are breaking changes to an API a new version will be created. The 'old' version will be supported for, at least, 6 months after. Note that additions to an existing API usually don't mean a breaking change.

## bSDD test environment

The bSDD has a test environment for testing new developments of the bSDD. Although meant for internal use, developers wanting to use the bSDD APIs are welcome to use the test environment for development purposes. We do not have an SLA for that environment but we try to have an up time of the test environment of at least 95%.
If you're a Dictionary owner and want to check your data or test the upload process, please use the official release.

## The bSDD API
The bSDD API offers methods to retrieve Class and Property information for several Standards (also known as Dictionaries), for example IFC and ETIM.
An example flow is:
* User opens screen to search for a Class and its Properties
* After opening screen the app calls the “Dictionary”-method of the API to retrieve a list of available Dictionaries. This list can then be presented to the user to make a selection.
* The user selects a Dictionary and enters some text to find the required Class
* The user press Search and the app sends the request to the bSDD API (“SearchList”-method)
* The result is a list of Classes
* The user can pick the one needed
* The app sends a request for Class details and Properties to the bSDD API (“Class”-method)
* The API returns Class details and Properties, which the app shows to the user

A typical use-case is demonstrated in SketchUp. A video of the SketchUp use-case and bSDD plugin is availalbe on https://vimeo.com/446417661/ff8b6605d3

## API contracts and testing the API
You can get the API contract information at [bSDD API contract, official release](https://app.swaggerhub.com/apis/buildingSMART/Dictionaries/v1) or [bSDD API contract, test](https://test.bsdd.buildingsmart.org/swagger). This information is available without the need for you to log in. You can also test the API methods. Secured methods are marked with a lock. To access secured methods you need to log in via the UI by using the Authorize button:

![Swagger authorization](https://bsddprototype2020.blob.core.windows.net/public/images/swagger-authorize2.png)

Don’t forget to check the “read” scope!

## Using https://identifier.buildingsmart.org
You can access the data of Class or property also directly via the URI of the Class or property. For example, you can navigate in the browser to https://identifier.buildingsmart.org/uri/buildingsmart/ifc/4.3/class/IfcWall and then you will see a visual representation of the data of that Class. If you would like the output in json format, then sending an "Accept" header with "application/json" will give you a result in json. The content of this json result differs from the html result!

IMPORTANT: Do not use these identifier URIs for system to system communication! First of all, it introduces an extra 'hop' from server to server. Second, you do not have control over the version of the API it's using. The result may differ after a new release of bSDD has been published with the result from before the release.

!! Getting the data in JSON format by directly calling the https://identifier.buildingsmart.org URL will be DEPRECATED. User api/Class/vX or api/Property/vX instead. !!

## GraphQL
The data can also be accessed via GraphQL.
[GraphiQL playground](https://test.bsdd.buildingsmart.org/graphiql).
The url to send your GraphQL requests to:
- test: https://test.bsdd.buildingsmart.org/graphql

For using the official release the secured endpoint is (note the "s" at the end):
- official release: https://api.bsdd.buildingsmart.org/graphqls
For developers: the test environment also has a secured endpoint available:
- test: https://test.bsdd.buildingsmart.org/graphqls


You can find example code how to access a secured bSDD API in this repository. Contact us if you need assistance implementing accessing the secured API.


[Some bSDD GraphQL examples](https://github.com/buildingSMART/bSDD/blob/master/Source%20code%20examples/GraphQL/bSDD%20and%20GraphQL.md)

## For client developers

### Http header "(X-)User-Agent"
Can you put the the name and version of your application in the HTTP header "User-Agent" (or "X-User-Agent") for each HTTP call? This way we can provide you some statistics regarding your application using the bSDD API. The preferred format is "application/version", e.g. "Autodesk.Revit/2024".

### Secure APIs
If you are going to build a client that is going to use secured APIs, you must request a Client ID. You can do so by sending us an email and give:
- the name of the client application
- type of application:
  - Web application
  - Single-page application
  - iOS / macOS, Objecte-C, Swift, Xamarin
  - Adroid - Java, Kotlin, Xamarin
  - Mobile/desktop
- which language are you using? (sometimes the redirectUri to be configured depends on the used library)
- if it is a website or SPA, specify the return url (the login page will redirect to this url after user has logged in)

If you don't use the secured APIs but want to call the other APIs from your website or SPA, then we need the URL of your website to allow CORS.
If you're creating a desktop client that only calls the not secured APIs, you're ready to go.

### Authentication
For authentication we use Azure Active Directory B2C.
At this moment you need to authenticate only for a few methods. This might change.

If you’re developing a Javascript, Java, Angular, React, Python or .NET application connecting with the buildingSMART Data Dictionary API is easiest by using the Microsoft Authentication Library (MSAL).
See [Active directory B2C code samples](https://docs.microsoft.com/en-us/azure/active-directory-b2c/code-samples) for ready to use examples on how to use the MSAL. You can find the bSDD API specific settings in one of the next sections of this document. Make sure you have the settings in an easy to update settings file. 
You can find the code for a small .NET console application that accesses the bSDD API (authenticated) in this repository: [.NET console example](https://github.com/buildingSMART/bSDD/tree/master/Source%20code%20examples/CSharp-Client-Console-Demo).

React:  https://docs.microsoft.com/en-us/azure/active-directory/develop/tutorial-v2-react
        https://github.com/Azure-Samples/ms-identity-javascript-react-tutorial/blob/main/1-Authentication/2-sign-in-b2c/README.md
Angular: https://docs.microsoft.com/en-us/azure/active-directory/develop/tutorial-v2-angular-auth-code
Java: https://docs.microsoft.com/en-us/samples/azure-samples/ms-identity-java-webapp/ms-identity-java-webapp/ 
Python: https://docs.microsoft.com/en-us/python/api/overview/azure/active-directory 

If you’re developing using one of the many other available languages it is still possible to connect to the bSDD API. The API is developed according to the standards OpenAPI, OAuth2 and OpenID Connect. Only now you need to do all the plumbing yourself.

To access a secured API a user must first register himself. When you’re using MSAL there’s nothing special you need to do for this. The user will be prompted to log in, via a browser window. If the user does not have a buildingSMART API account, he can sign up:

![bSDD sign up / sign in](https://bsddprototype2020.blob.core.windows.net/public/images/bs-signupsignin.png)

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

The full B2C authority url is: https://authentication.buildingsmart.org/tfp/buildingsmartservices.onmicrosoft.com/b2c_1a_signupsignin_c (note the "tfp" part!).

For using the official release, you should use the settings as above except:
* ClientId: request a Client ID at bsdd_support@buildingsmart.org
* RedirectUri: let us know what kind of app you are making and with which technology
* ApiScope : "https://buildingsmartservices.onmicrosoft.com/bsddapi/read"
* BsddApiUrl: "https://api.bsdd.buildingsmart.org"


If you are developing a Web App that’s going to use the bSDD API, let us know (bsdd_support@buildingsmart.org). The RedirectURI needs to be configured in Azure AD.

### Additional information
Language independent description of the authorization flow: [Authorization code flow](https://docs.microsoft.com/en-us/azure/active-directory-b2c/authorization-code-flow)

High level descriptions of the various authentication flows: [AD B2C application types](https://docs.microsoft.com/en-us/azure/active-directory-b2c/application-types)

Oauth2 and OpenId protocol descriptions: [AD B2C protocols overview](https://docs.microsoft.com/en-us/azure/active-directory-b2c/protocols-overview)

