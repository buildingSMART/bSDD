IMPORTANT: naming and (some) example API calls in this file are outdated! Please check the [swagger information page](https://test.bsdd.buildingsmart.org/swagger) for up-to-date API information.

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
The API swagger page: https://test.bsdd.buildingsmart.org/swagger

## Steps

For following this tutortial we assume you have some experience with programming and using APIs over HTTP. So we are not going into all programming details of executing an API request.

For demo, and this tutorial, purposes it is fine to have settings like the base URL of the bSDD API in your code. But if you go along with using bSDD it is better to have these settings configurable so you can, for example, easily switch from the bSDD prototype API to the production API.

Most bSDD API calls do not require any form of authentication yet, so for this tutorial we're going to skip doing authorized calls.

## Part 1 - get a list of the domains

Prepare your app for doing a HTTP request to the buildingSMART Data Dictionary API.
How to do this depends a lot on the language you use for building your app. If you're using .NET and C#, have a look at the demo console app: https://github.com/buildingSMART/bSDD/tree/master/2020%20prototype/CSharp-Client-Console-Demo

Call the Domain API to get a list of all domains.
API url: https://test.bsdd.buildingsmart.org/api/Domain/v2
query parameters: -none-

Response body looks like:
```
[
  {
    "namespaceUri": "https://identifier.buildingsmart.org/uri/buildingsmart/ifc/4.3",
    "name": "IFC",
    "version": "4.3",
    "organizationNameOwner": "buildingSMART",
    "defaultLanguageCode": "EN",
    "license": "Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International",
    "licenseUrl": "https://creativecommons.org/licenses/by-nc-nd/4.0/",
    "qualityAssuranceProcedure": "bSI process",
    "qualityAssuranceProcedureUrl": "https://www.buildingsmart.org/about/bsi-process"
  },
  {
    "namespaceUri": "https://identifier.buildingsmart.org/uri/etim/etim/7.0",
    "name": "ETIM",
    "version": "7.0",
    "organizationNameOwner": "ETIM International",
    "defaultLanguageCode": "nl-BE",
    "qualityAssuranceProcedure": "ETIM international",
    "qualityAssuranceProcedureUrl": "https://www.etim-international.com/wp-content/uploads/2019/11/Statutes-ETIM-International-version-EN-10-2017.pdf"
  },
  ...
```

Present the list to the user

## Part 2 - search for classifications within a domain

Call the SearchListOpen API (for production purposes you should use the secured SearchList API).
Provide one of the Domain NamespaceUris from the list received in the previous step.
Optionally provide some text to search for or provide an official IFC entity name to get even more specific results.
Not all domains have RelatedIfcName(s) available.

API url: https://test.bsdd.buildingsmart.org/api/SearchListOpen/v2
query parameters: DomainNamespaceUri=http%3A%2F%2Fidentifier.buildingsmart.org%2Furi%2Fbuildingsmart%2Fifc/4.3&SearchText=bridge

Response body looks like:
```
{
  "numberOfClassificationsFound": 21,
  "domains": [
    {
      "name": "IFC",
      "namespaceUri": "https://identifier.buildingsmart.org/uri/buildingsmart/ifc/4.3",
      "classifications": [
        {
          "name": "IfcBridge",
          "namespaceUri": "https://identifier.buildingsmart.org/uri/buildingsmart/ifc/4.3/class/IfcBridge",
          "definition": "A Bridge is civil engineering works that affords passage to pedestrians, animals, vehicles, and services above obstacles or between two points at a height above ground. NOTE Definition from ISO 6707 1 2014 Civil engineering works that affords passage to pedestrians, animals, vehicles, and services above obstacles or between two points at a height above ground. bSI Documentation"
        },
        {
          "name": "IfcBridge.ARCHED",
          "namespaceUri": "https://identifier.buildingsmart.org/uri/buildingsmart/ifc/4.3/class/IfcBridge.ARCHED"
        },
        {
          "name": "IfcBridge.CABLE_STAYED",
          "namespaceUri": "https://identifier.buildingsmart.org/uri/buildingsmart/ifc/4.3/class/IfcBridge.CABLE_STAYED"
        },
...
```

## Part 3 - get the details of a classification

Call the Classification API with one of the Classification NamespaceUris received via the SearchList API.

API url: https://test.bsdd.buildingsmart.org/api/Classification/v2
query parameters: namespaceUri=http%3A%2F%2Fidentifier.buildingsmart.org%2Furi%2Fbuildingsmart%2Fifc%2F4.3%2Fclass%2FIfcBridge&includeChildClassificationReferences=true

Response body looks like:
```
{
  "parentClassificationReference": {
    "namespaceUri": "https://identifier.buildingsmart.org/uri/buildingsmart/ifc/4.3/class/IfcFacility",
    "name": "IfcFacility",
    "code": "IfcFacility"
  },
  "classificationProperties": [
    {
      "propertyDomainName": "IFC",
      "propertyNamespaceUri": "https://identifier.buildingsmart.org/uri/buildingsmart/ifc/4.3/prop/StructureIndicator",
      "name": "StructureIndicator",
      "propertySet": "Pset_BridgeCommon",
      "dataType": "PEnum_StructureIndicator",
      "values": [
        {
          "value": "COMPOSITE"
        },
        {
          "value": "HOMOGENEOUS"
        },
        {
          "value": "COATED"
        }
      ]
    }
  ],
  "childClassificationReferences": [
    {
      "namespaceUri": "https://identifier.buildingsmart.org/uri/buildingsmart/ifc/4.3/class/IfcBridge.CULVERT",
      "name": "IfcBridge.CULVERT",
      "code": "IfcBridge.CULVERT"
    },
    {
      "namespaceUri": "https://identifier.buildingsmart.org/uri/buildingsmart/ifc/4.3/class/IfcBridge.CABLE_STAYED",
      "name": "IfcBridge.CABLE_STAYED",
      "code": "IfcBridge.CABLE_STAYED"
    },
...
```

## Further development (out of scope of hackathon)

For a stable app and great user experience in a lot of circumstances you should make your app resilient to temporary failures. Depending on the failure it can make sense to try again in a few seconds. Most likely there are libraries available you can use to make your app more resilient to transient failures. For .NET for example, you should have a look at the library Polly.

Needing more flexibility in the results returned from the API?
Have a look at our GraphQL implementation: https://test.bsdd.buildingsmart.org/graphiql.
With GraphQL you can specify the fields you need. Only those fields will be returnd by GraphQL.

## More good to know

You've probably noted the "v2" in the API urls. In case your'e wondering why this is, this is the version number of the API method. If we change the behaviour of the API, or if we change the request parameters and/or response in a way that it is not backwards compatible anymore, the version number will be increased. So it is not necessary for you to immediately adjust your software. The old version will be supported for a long period afterwards. But you need to adjust your software to be able to take advantage of the changes.
Please be aware that we can change the request parameters and/or response without increasing the version number, but only if it is an addition: your software does not need to be changed. For example, if we add an extra field to the response, the version number will not be increased. Your software should not fail if an extra field is returned. It probably will ignore it, but that's no problem, the behaviour stays the same.