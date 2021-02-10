# March 2021 Hackathon Tutorial

## Context

## Main use-case

## Authentication

Some of the bSDD APIs are publicly available, for others you need to be authenticated.
Being authenticated means that you have identified yourself to the buildingsmartservices Active Directory. This Active Directory is an Azure service known as Azure AD B2C (with B2C meaning Business to Consumer). A user can sign up himself with the only requirement are providing valid e-mail address and name.



## Resources

Authenticated call to the bSDD API from a .NET Console App: https://github.com/buildingSMART/bSDD/tree/master/2020%20prototype/CSharp-Client-Console-Demo
Azure AD B2C documentation home page: http://aka.ms/aadb2c
More Azure AD B2C code samples: https://docs.microsoft.com/en-us/azure/active-directory-b2c/code-samples

# Steps

- Request an Application ID (aka Client ID) for getting access to the bSDD API from your app. For now you can use application ID of the demo application: xxx-xxx-xx-xx (do not use this one for production purposes as we can take it offline without notice)

- Configure your application

## Step 1

Call the Domain API to get a list of all domains

## Step 2

Call the SearchList API (not the SearchListOpen one, that one will be taken offline without notice).
Provide one of the Domain NamespaceUris from the list received in the previous step.
Optionally provide some text to search for or provide an official IFC entity name to get even more specific results.
Not all domains have RelatedIfcName(s) available.

## Step 3

Get the details of classification:
Call the Classification API with one of the Classification NamespaceUris received in the previous step.
