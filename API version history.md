# bSDD API version history

This is the version history of the API at https://api.bsdd.buildingsmart.org.

New APIs and updates will always first be published to the bSDD test environment: https://test.bsdd.buildingsmart.org

## 2202-09-05

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
* Initial release


#
# Versioning strategy
A new version will only be created if it 'breaks' the current version. For example, adding a new field to the output of an API does not (or should not) break your app. Removing an output field on the other hand is a breaking change and will result in a new version of that API.

If there is a new version of an API the previous version will be supported for 6 to 12 months after releasing the new version. 
