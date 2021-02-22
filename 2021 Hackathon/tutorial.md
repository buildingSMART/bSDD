# March 2021 Hackathon Tutorial

How to use the buildingSMART Data Dictionary API?

You can find a lot of info regarding the bSDD API on https://github.com/buildingSMART/bSDD/tree/master/2020%20prototype.
But where to start using the API?

## Context

## Main use-case

## Authentication

Some of the bSDD APIs are publicly available, for others you need to be authenticated.
Being authenticated means that you have identified yourself to the buildingsmartservices Active Directory. This Active Directory is an Azure service known as Azure AD B2C (with B2C meaning Business to Consumer). A user can sign up himself with the only requirement are providing valid e-mail address and name.


## Resources

Main resource location of the bSDD API: https://github.com/buildingSMART/bSDD/tree/master/2020%20prototype.
Authenticated call to the bSDD API from a .NET Console App: https://github.com/buildingSMART/bSDD/tree/master/2020%20prototype/CSharp-Client-Console-Demo
Azure AD B2C documentation home page: http://aka.ms/aadb2c
More Azure AD B2C code samples: https://docs.microsoft.com/en-us/azure/active-directory-b2c/code-samples

# Steps

- Request an Application ID (aka Client ID) for getting access to the bSDD API from your app. For now you can use application ID of the demo application: 4aba821f-d4ff-498b-a462-c2837dbbba70 (this Client ID is only applicable for the Prototype environment and has a limited lifetime). If you want to go live with your application, request a Client ID at bsdd_support@buildingsmart.org.

- Configure your application

- Most bSDD API calls do not require any form of authentication yet. If you do not have any experience with doing authenticated Http calls, you can start without authentication.

## Part 1

Prepare your app for doing a HTTP request to the buildingSMART Data Dictionary API.
How to do this depends a lot on the language you use for building your app. If you're using .NET and C#, have a look at the demo console app: https://github.com/buildingSMART/bSDD/tree/master/2020%20prototype/CSharp-Client-Console-Demo

Call the Domain API to get a list of all domains

Present the list to the user

## Part 2

Call the SearchList API or if you're not implementing the authorization part, use SearchListOpen. Note: you should not use the SearchListOpen for production purposes.
Provide one of the Domain NamespaceUris from the list received in the previous step.
Optionally provide some text to search for or provide an official IFC entity name to get even more specific results.
Not all domains have RelatedIfcName(s) available.

## Part 3

Get the details of classification:
Call the Classification API with one of the Classification NamespaceUris received via the SearchList API.

## Further development (out of scope of hackathon)

For a stable app and great user experience in a lot of circumstances you should make your app resilient to temporary failures. Depending on the failure it can make sense to try again in a few seconds.

Needing more flexibility? Have a look at our GraphQL implementation: https://bs-dd-api-prototype.azurewebsites.net/graphiql 