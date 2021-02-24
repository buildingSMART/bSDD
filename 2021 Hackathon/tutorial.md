# March 2021 Hackathon Tutorial

How to use the buildingSMART Data Dictionary API?

You can find a lot of info regarding the bSDD API on https://github.com/buildingSMART/bSDD/tree/master/2020%20prototype.
But where to start using the API?

## Context

### Main use-case

One of the main use-cases for using bSDD is having to link parts of a design to classifications or using properties of a specific domain.
As you've seen in the Sketchup demo some Nl-SfB properties needed to be specified for a wall.
The bSDD provides an API for getting the list of domains available.
And after selecting the required domain, there's an API to search for classifications within the selected domain.
In the demo the search is done via "Related IFC Entity Name". It is also possible to search using plain text.
Note that not all domains may have relations with IFC specified.

By then selecting the desired classification, details of that classification, including properties, can be requestedusing the Classification API.

### Authentication

Some of the bSDD APIs are publicly available, for others you need to be authenticated.
Being authenticated means for bSDD that you have identified yourself to the buildingsmartservices Active Directory. This Active Directory is an Azure service known as Azure AD B2C (with B2C meaning Business to Consumer). You can sign up yourself by providing valid e-mail address and name.

For this tutorial there is no need to sign up.

### Resources

Some resources for you to get to know more about bSDD and using the API:

Main resource location of the bSDD API: https://github.com/buildingSMART/bSDD/tree/master/2020%20prototype.
Authenticated call to the bSDD API from a .NET Console App: https://github.com/buildingSMART/bSDD/tree/master/2020%20prototype/CSharp-Client-Console-Demo
Azure AD B2C documentation home page: http://aka.ms/aadb2c
More Azure AD B2C code samples: https://docs.microsoft.com/en-us/azure/active-directory-b2c/code-samples
The API swagger page: https://bs-dd-api-prototype.azurewebsites.net/swagger

## Steps

For following this tutortial we assume you have some experience with programming and using APIs over HTTP. So we are not going into all programming details of executing an API request.

For demo, and this tutorial, purposes it is fine to have settings like the base URL of the bSDD API in your code. But if you go along with using bSDD it is better to have these settings configurable so you can, for example, easily switch from the bSDD prototype API to the production API.

Most bSDD API calls do not require any form of authentication yet, so for this tutorial we're going to skip doing authorized calls.

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