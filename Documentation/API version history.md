# bSDD API version history

This is the version history of the API at https://api.bsdd.buildingsmart.org.

New APIs and updates will always first be published to the bSDD test environment: https://test.bsdd.buildingsmart.org

For planned updates and other tech discussions, see [bSDD tech updates forum](https://forums.buildingsmart.org/t/bsdd-tech-updates/4889).

#
# Versioning strategy
A new version will only be created if it 'breaks' the current version. For example, adding a new field to the output of an API does not (or should not) break your app. Removing an output field on the other hand is a breaking change and will result in a new version of that API.

If there is a new version of an API the previous version will be supported for at least 6 months after releasing the new version. 

## 2024-03-01

- On uploading a dictionary user can now indicate that it is for testing purposes.

Changend APIs:
 * api/Class/v1: 
    - Option IncludeReverseRelations added
 * api/Dictionary/v1: 
    - Option IncludeTestDictionaries added
 * api/UploadImportFile/v1:
    - Option IsTest added
 * api/TextSearch/v1:
    - Text search with multiple word(parts) now also finds results if first word partly ('startswith') matches. Previously it would find only exact matches on first word(s)


## 2023-11-08

Name changes:
 * Classification ==> Class
 * Domain ==> Dictionary
 * NamespaceUri ==> Uri
 * IncludeChilds ==> IncludeChildren

This involves all APIs either with one of these names in the API name itself, in the input contract or in the output contract. For all these APIs new versions, some with new names, have been created. Existing APIs will remain for at least 6 months after go live but we advise you to use the new APIs.

Other changes:
 * "Materials" are not treated separately anymore, they are just Classes with type being Material.
 * Import field ClassificationProperty.ExternalPropertyUri has been removed completely. The field PropertyNamespaceUri (which is now called PropertyUri) already replaced it.
 * Search APIs now support pagination

Changend APIs:
 * api/Class/v1: new, replaces api/Classification/v4
    - Option includeClassProperties added. If true, classProperties will be fetched. Default is false.
    - Option includeClassRelations added. If true, classRelations will be fetched. Default is false.
    - New output field: Class.Description
 * api/Class/Search/v1: new, replaces api/ClassificationSearchOpen/v1.
    - Return contract now contains just one dictionary instead of a list of dictionaries which always contains one item.
    - Supports pagination
 * api/Dictionary/v1: new, replaces api/Domain/v3
    - Supports pagination
 * api/Dictionary/v1/Classes: new, replaces api/Domain/v3/Classifications.
    - Materials are not separately listed anymore
    - Supports pagination
    - Optional filter on ClassType
 * api/Dictionary/v1/Properties: new
    - Supports pagination
 * api/Dictionary/v1 PUT, DELETE: new, replaces api/Domain/v1
 * api/DictionaryDownload/sketchup/v1: new, replaces api/RequestExportFile/preview
 * api/Material has been replaced by api/Class
 * api/Property/v4: new, replaces api/Property/v3
 * api/SearchInDictionary/v1: new, replaces api/SearchList(Open)/v2
    - Supports pagination
 * api/TextSearch/v1: new, replaces api/TextSearchListOpen/v6
    - Supports pagination
  * api/UploadImportFile/v1: updated, it accepts both old and new import json. Support for old import json will become deprecated.

All replaced APIs still work for now but are marked as obsolete, as can be seen on the swagger page https://test.bsdd.buildingsmart.org/swagger.

## 2023-08-10

 * Added: api/Domain/v3/{organizationCode}/{code}/{version} - put: to update the status of a domain version
 * Added: api/Domain/v3/{organizationCode}/{code}/{version} - delete: to delete a domain version
 * Added: api/Domain/v3/{organizationCode}/{code} - delete: to delete a domain
 * Change: api/Classification/v4: now includes "namespaceUri" in result contracts of the classification property and classification relation
 * Change: api/Property/v3: now includes "namespaceUri" in result contract of the property relation

## 2023-05-10

 * Change: api/Domain/v3: now includes "OrganizationCodeOwner" in result contract
 * Fix: the swagger documentation for api/Classification/v4 has been corrected

## 2022-12-29

 * New version: api/Domain/v3: is same as v2
 * New version: api/Domain/v3/Classifications: output contract has changed - materials are now returned in a separate list
 * New version: api/TestSearchListOpen/v6: output contract has changed - materials are now returned in a separate list; input contract now also accepts "Materials" in TypeFilter; TypeFilter values are now case insensitive
 * Change: api/TestSearchListOpen/v5: TypeFilter values are now case insensitive

 Previous versions of new APIs will remain available until at least September 2023.

## 2022-10-23

 * New version: api/Classification/v4: attribute PossibleValues has been renamed into AllowedValues (is now consistent with import attribute name)
 * New version: api/Material/v2: attribute PossibleValues has been renamed into AllowedValues (is now consistent with import attribute name)
 * New version: api/Property/v3: attribute PossibleValues has been renamed into AllowedValues (is now consistent with import attribute name); supports returning RDF format
 
 Previous versions of new APIs will remain available until at least July 2023.

## 2022-09-08

ATTENTION: for accessing secured API's you must use **https://authentication.buildingsmart.org** instead of https://buildingsmartservices.b2clogin.com !

## 2022-09-05

 * New: api/ClassificationSearchOpen/v1, optimized API for searching for classifications
 * Update: api/Domain/v2 and api/Domain/v2/Classifications returns LastUpdatedUtc with date and time the data in bSDD was last updated
 * Update: "http://idenfitier..." has been replaced by "https://identifier...". Searching for "http://identifier..." is for the time being auto-matched with "https://idenfitier..."

## 2022-08-23

 * Update: api/Domain/v2/Classifications now supports Accept-Language header
 * Update: api/Domain/v2 and api/Domain/v2/Classifications output fields ReleaseDate, MoreInfoUrl and Status added
 * Update: api/Classification/v3 output field Fraction added (in type ClassificationRelation)

## 2022-07-01 
 * Update: api/Classification/v3 now supports the Accept-Language header to request data in a different language
 * Update: api/Property/v2 now supports the Accept-Language header to request data in a different language
 * Update: api/Property/v2 and api/Classification/v3 now also returns the QUDT code(s) for units, if available
 * Update: api/RequestExportfile/preview, SketchUp output files are now cached

## 2022-04-30
* New: api/Material/v1 for getting Material details
* New: api/Material/SearchOpen/preview for searching Materials
* Update: api/Classification/v3 can now return data in RDF-XML, Turtle or Html format:

| Accept header | Output format |
|--|--|
| [default] | json |
| application/rdf+xml | RDF XML |
| application/x-turtle | turtle |
| text/html | html |
| text/turtle | turtle |

## 2021-11-01
* New: api/Domain/v2/Classifications for getting the list of classifications for a domain

## 2021-09-01
* Official first release
