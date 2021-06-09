# bSDD and GraphQL

## Short intro on GraphQL

A 'regular' API is quite static: you do a request and it returns a predefined set of data. If you need some more info you probably need to do another API call. And then maybe another call until you have got all the data you want. GraphQL is designed to overcome that issue: it is a query language with which you can specify the data you need.

For more info on GraphQL have a look at, for example:
- https://dev.to/davinc/graphql-for-beginners-3f1a
- https://www.freecodecamp.org/news/a-beginners-guide-to-graphql-60e43b0a41f5/

For some scenario's using GraphQL can be more efficient, but there are still lots of scenario's where a regular API is the most efficient solution.

## bSDD GraphQL endpoint

The bSDD API also provides a GraphQL endpoint and the prototype environment also has a playground:

Playground: https://bs-dd-api-prototype.azurewebsites.net/graphiql/
GraphQL Endpoint: https://bs-dd-api-prototype.azurewebsites.net/graphql/

## Example data queries

Query to get the list of available languages:

{
  languages {
    isocode
  }
}

----

Query to get list of country codes:

{
  countries {
    code
  }
}

----

You can combine those queries into one:

{
  languages {
    isocode
  }

  countries {
    code
  }
}

----

Query to search for classifications within a domain:

{
  domain(namespaceUri : "http://identifier.buildingsmart.org/uri/etim/etim-8.0") {
    namespaceUri
    copyrightNotice
    languageCode
    classificationSearch(searchText: "cotter", languageCode: "EN") {
      name
      namespaceUri
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

----

Query to get details for a classification, using variables:

query ($domainNamespaceUri: String!, $namespaceUri: String!) {
  domain(namespaceUri: $domainNamespaceUri) {
    name
    namespaceUri
    classification(namespaceUri: $namespaceUri, includeChilds: true) {
      activationDateUtc
      childs {
        name
        namespaceUri
      }
      classificationType
      code
      countriesOfUse
      countryOfOrigin
      creatorLanguageCode
      deActivationDateUtc
      definition
      deprecationExplanation
      documentReference
      name
      namespaceUri
      properties {
        name
        namespaceUri
      }
      relatedIfcEntityNames
      relations {
        relatedClassificationName
        relatedClassificationUri
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

The query variable section defines the variables:

{
  "domainNamespaceUri": "http://identifier.buildingsmart.org/uri/etim/etim-8.0",
  "namespaceUri": "http://identifier.buildingsmart.org/uri/etim/etim-8.0/class/EC003020"
}

## Example meta data queries

In GraphQL you can also execute queries on the GraphQL schema (also known as "Introspection"). You can use that to get, for example, the available fields or queries:

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

More examples of Introspection can be found at: https://graphql.org/learn/introspection/