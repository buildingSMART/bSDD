# bSDD and GraphQL

## Notification

We've applied several changes in naming:

1. "Domain" --> "Dictionary"
2. "Classification" --> "Class"
3. "NamespaceUri" --> "Uri"
4. "IncludeChilds" --> "IncludeChildren"

To be consistent, names in our GraphQL API have also been changed.
But we do support the old naming until at least April 2024.

## Short intro on GraphQL

A 'regular' API is quite static: you do a request and it returns a predefined set of data. If you need some more info you probably need to do another API call. And then maybe another call until you have got all the data you want. GraphQL is designed to overcome that issue: it is a query language with which you can specify the data you need.

For more info on GraphQL have a look at, for example:
- https://dev.to/davinc/graphql-for-beginners-3f1a
- https://www.freecodecamp.org/news/a-beginners-guide-to-graphql-60e43b0a41f5/

For some scenario's using GraphQL can be more efficient, but there are still lots of scenario's where a regular API is the most efficient solution.

## bSDD GraphQL endpoints

The bSDD API also provides a GraphQL endpoint and the test environment also has a playground:

Playground: https://test.bsdd.buildingsmart.org/graphiql/
Test GraphQL endpoint: https://test.bsdd.buildingsmart.org/graphql/
Test GraphQL secured endpoint: https://test.bsdd.buildingsmart.org/graphqls/

Production GraphQL secured endpoint: https://api.bsdd.buildingsmart.org/graphqls/

See document https://github.com/buildingSMART/bSDD/blob/master/Documentation/bSDD%20API.md for info how to access secured APIs. For accessing the secured GraphQL endpoint it is the same.

## Example data queries

-- get the list of available languages:
```
{
  languages {
    isocode
  }
}
```
----

-- get list of country codes:
```
{
  countries {
    code
  }
}
```
----

You can combine those queries into one:
```
{
  languages {
    isocode
  }

  countries {
    code
  }
}
```
----

-- search for classes within a dictionary:
```
{
  dictionary(uri : "https://identifier.buildingsmart.org/uri/sbe/swedishmaterials-1") {
    uri
    copyrightNotice
    languageCode
    classSearch(searchText: "asfaltbetong", languageCode: "sv-SE") {
      name
      uri
      synonyms
      relatedIfcEntityNames
      properties {
        name
        isRequired
        pattern
      }
    }
  }
}
```
----

-- get all classes with their properties of a dictionary:

ATTENTION: this query will take a long time to execute for dictionaries with many classes
```
{
  dictionary(uri : "https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/1.0") {
    name
    version
    uri
    copyrightNotice
    languageCode
    status
    releaseDate
    license
    licenseUrl
    moreInfoUrl
    
    classSearch {
      code
      name
      uri
      definition
      documentReference
      synonyms
      relatedIfcEntityNames
      properties {
        code
        name
        uri
        description
        definition
        documentReference
        isRequired
        dataType
        example
        dimension
        physicalQuantity
        pattern
        allowedValues {
          code
          value
        }
        units
      }
    }
  }
}
```
----

-- get details for a class, using variables:
```
query ($dictionaryUri: String!, $uri: String!) {
  dictionary(uri: $dictionaryUri) {
    name
    uri
    class(uri: $uri, includeChildren: true) {
      activationDateUtc
      childs {
        name
        uri
      }
      classType
      code
      countriesOfUse
      countryOfOrigin
      creatorLanguageCode
      deActivationDateUtc
      definition
      deprecationExplanation
      documentReference
      name
      uri
      properties {
        name
        uri
      }
      relatedIfcEntityNames
      relations {
        relatedClassName
        relatedClassUri
        relationType
      }
      replacedObjectCodes
      replacingObjectCodes
      revisionDateUtc
      status
      subdivisionsOfUse
      synonyms
      uid
      versionDateUtc
      versionNumber
      visualRepresentationUri
    }
  }
}
```
The query variable section defines the variables:
```
{
  "dictionaryUri": "https://identifier.buildingsmart.org/uri/sbe/swedishmaterials/1",
  "uri": "https://identifier.buildingsmart.org/uri/sbe/swedishmaterials/1/class/ACDE"
}
```
## Example meta data queries

In GraphQL you can also execute queries on the GraphQL schema (also known as "Introspection"). You can use that to get, for example, the available fields or queries:
```
{
  __schema {
    types {
      name
      fields {
        name
        description
      }
    }
  }
}

query availableQueries {
  __schema {
    queryType {
      fields {
        name
        description
        type {
          kind
          name
          fields {
            name
            description
          }
        }
      }
    }
  }
}
```
More examples of Introspection can be found at: https://graphql.org/learn/introspection/