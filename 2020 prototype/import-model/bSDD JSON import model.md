# buildingSMART Data Dictionary JSON import model

You can deliver data for the buildingSMART Data Dictionary by using the bSDD
JSON import model format. This document explains this format.

Click on the link to get the list of allowed codes for [countries](https://github.com/buildingSMART/bSDD/blob/master/2020%20prototype/import-model/reference-lists/countries.csv), [languages](https://github.com/buildingSMART/bSDD/blob/master/2020%20prototype/import-model/reference-lists/languages.csv), [units](https://github.com/buildingSMART/bSDD/blob/master/2020%20prototype/import-model/reference-lists/units.csv), [reference documents](https://github.com/buildingSMART/bSDD/blob/master/2020%20prototype/import-model/reference-lists/reference-documents.csv) and [ifc classification names](https://github.com/buildingSMART/bSDD/blob/master/2020%20prototype/import-model/reference-lists/ifc-classification-names.csv).
If you think there are reference items missing, please let us know.

## Domain type

Contains general information about the domain and the delivered data.

| Field            | DataType               | Required? | Translatable? | Description                                                                                                                                                                  |
|------------------|------------------------|-----------|---------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| OrganizationCode | Text                   | Yes       | No            | Official name of the organization that’s the owner of the data. E.g. “buildingSMART”                                                                                         |
| DomainCode       | Text                   | Yes       | No            | Code of the domain E.g. “ifc”                                                                                                                                                |
| DomainVersion    | Text                   | Yes       | No            | Version of the data E.g. “4.3”                                                                                                                                               |
| DomainName       | Text                   | If new domain or version | No  | Name of the domain. If the domain exists supplying this name is not necessary |
| ReleaseDate      | Date                   | No        | No            | Date of release of the version E.g. “2017-10-01”                                                                                                                             |
| Status      | Text                   | No        | No            | State of this release E.g. “Preview”                                                                                                                             |
| LicenseUrl      | Text                   | No        | No            | Url to a web page with the full license text                                                                                                                         |
| MoreInfoUrl      | Text                   | No        | No            | Url to web page with more info about the domain                                                                                                                         |
| LanguageIsoCode  | Text                   | Yes       | No            | ISO language code: indicates the language of the data. If you want to deliver data in multiple language use a json file per language. See reference list [languages](https://github.com/buildingSMART/bSDD/blob/master/2020%20prototype/import-model/reference-lists/languages.csv). \* E.g. “de-DE” |
| LanguageOnly     | Boolean                | Yes       | No            | true if json contains only language specific information, no otherwise \*                                                                                                |
| License          | Text                   | No        | No            | Description of the license the data will be made available E.g. “No license”                                                                                                 |
| Classifications  | List of Classification | Yes       |               | List of objects of type “Classification”. See next section                                                                                                                   |
| Properties       | List of Property       | Yes       |               | List of objects of type “Property”. See next sections                                                                                                                        |

\* For delivering data in additional languages it is sufficient to fill the
Domain type fields, all “Code” fields and the fields marked with “Translatable?
= Yes” of the other types. Make sure that the OrganizationCode, DomainCode and
DomainVersion are exactly the same and if the data is for adding a language to
an existing Domain, set field “LanguageOnly” to true.

## Classifications

A classification can be any (abstract) object (e.g. “IfcWall”), abstract concept
(e.g. “Costing”) or process (e.g. “Installation”). Classifications can be
organized in a tree like structure. For example: “IfcCurtainWall” is a more
specific classification of “IfcWall”. We use the term “parent” to identify this
relation: the parent of “IfcCurtainWall” is “IfcWall”.

| Field                     | DataType                       | Required? | Translatable? | Description                                                                                                        |
|---------------------------|--------------------------------|-----------|---------------|--------------------------------------------------------------------------------------------------------------------|
| Code                      | Text                           | Yes       | No            | Unique identification within the domain of the classification E.g. “ifc-00123-01”                                  |
| Name                      | Text                           | Yes       | Yes           | Name of the classification E.g. “IfcCurtainWall”                                                                   |
| Definition                | Text                           | No        | Yes           | Definition of the Classification                                                                                   |
| Status                    | Text                           | No        | No            | Status of the Classification: “Active” (default) or “Inactive”                                                     |
| ActivationDateUtc         | Date                           | No        | No            |                                                                                                                    |
| RevisionDateUtc           | Date                           | No        | No            |                                                                                                                    |
| VersionDateUtc            | Date                           | No        | No            |                                                                                                                    |
| DeactivationDateUtc       | Date                           | No        | No            |                                                                                                                    |
| VersionNumber             | Integer                        | No        | No            |                                                                                                                    |
| RevisionNumber            | Integer                        | No        | No            |                                                                                                                    |
| ReplacedObjectCodes       | List of text                   | No        | No            | List of Classification Codes this Classification replaces                                                          |
| ReplacingObjectCodes      | List of text                   | No        | No            | List of Classification Codes this classification is replaced by                                                    |
| DeprecationExplanation    | Text                           | No        | Yes           |                                                                                                                    |
| CreatorLanguageIsoCode    | Text                           | No        | No            | Language ISO code of the creator. See reference list [languages](https://github.com/buildingSMART/bSDD/blob/master/2020%20prototype/import-model/reference-lists/languages.csv).                                                                                   |
| VisualRepresentationUri   | Text                           | No        | Yes           |                                                                                                                    |
| CountriesOfUse            | List of text                   | No        | No            | List of country ISO codes this Classification is being used. See reference list [countries](https://github.com/buildingSMART/bSDD/blob/master/2020%20prototype/import-model/reference-lists/countries.csv).                                    |
| SubdivisionsOfUse         | List of text                   | No        | Yes           | List of geographical regions of use E.g. “US-MT”                                                                   |
| CountryOfOrigin           | Text                           | No        | No            | ISO Country Code of the country of origin of this classification. See reference list [countries](https://github.com/buildingSMART/bSDD/blob/master/2020%20prototype/import-model/reference-lists/countries.csv).                                         |
| DocumentReference         | Text                           | No        | No            | Reference to document with full or official definition of the Classification. See reference list [reference documents](https://github.com/buildingSMART/bSDD/blob/master/2020%20prototype/import-model/reference-lists/reference-documents.csv).                                       |
| ClassificationType        | Text                           | No        | No            | Must be one of: Class ComposedProperty Domain ReferenceDocument AlternativeUse                                     |
| ParentClassificationCode  | Text                           | No        | No            | Reference to the parent Classification. The ID in this field MUST exist in the data delivered. E.g. “ifc-00123-00” |
| RelatedIfcEntityNamesList | List of text                   | No        | No            | References to the IFC equivalent of this Classification. See reference list [ifc classification names](https://github.com/buildingSMART/bSDD/blob/master/2020%20prototype/import-model/reference-lists/ifc-classification-names.csv).                                      |
| Synonyms                  | List of text                   | No        | Yes           |                                                                                                                    |
| ClassificationRelations   | List of ClassificationRelation | No        | No            | See next sections                                                                                                  |
| ClassificationProperties  | List of ClassificationProperty | No        | No            | See next sections                                                                                                  |

## Property type

A classification can have multiple properties and a property can be part of many
classifications

| Field                         | DataType     | Required? | Translatable? | Description                                                                                                                                          |
|-------------------------------|--------------|-----------|---------------|------------------------------------------------------------------------------------------------------------------------------------------------------|
| Code                          | Text         | Yes       | No            | Unique identification within the domain of the property E.g. “ifc-99088-01”                                                                          |
| Name                          | Text         | Yes       | Yes           | Name of the Property E.g. “IsExternal”                                                                                                               |
| Definition                    | Text         | No        | Yes           | Definition of the Property                                                                                                                           |
| Status                        | Text         | No        | No            | Status of the Property: “Active” (default) or “Inactive”                                                                                             |
| ActivationDateUtc             | Date         | No        | No            |                                                                                                                                                      |
| RevisionDateUtc               | Date         | No        | No            |                                                                                                                                                      |
| VersionDateUtc                | Date         | No        | No            |                                                                                                                                                      |
| DeactivationDateUtc           | Date         | No        | No            |                                                                                                                                                      |
| VersionNumber                 | Integer      | No        | No            |                                                                                                                                                      |
| RevisionNumber                | Integer      | No        | No            |                                                                                                                                                      |
| ReplacedObjectCodes           | List of text | No        | No            | List of Classification Codes this Classification replaces                                                                                            |
| ReplacingObjectCodes          | List of text | No        | No            | List of Classification Codes this classification is replaced by                                                                                      |
| DeprecationExplanation        | Text         | No        | Yes           |                                                                                                                                                      |
| CreatorLanguageIsoCode        | Text         | No        | No            | Language ISO code of the creator. See reference list [languages](https://github.com/buildingSMART/bSDD/blob/master/2020%20prototype/import-model/reference-lists/languages.csv).                                                                                                                     |
| VisualRepresentationUri       | Text         | No        | Yes           |                                                                                                                                                      |
| CountriesOfUse                | Text         | No        | No            | Semicolon separated list of country ISO codes this Classification is being used. See reference list [countries](https://github.com/buildingSMART/bSDD/blob/master/2020%20prototype/import-model/reference-lists/countries.csv).                                                      |
| SubdivisionsOfUse             | Text         | No        | Yes           | Semicolon separated list of geographical regions of use E.g. “US-MT”                                                                                 |
| CountryOfOrigin               | Text         | No        | No            | ISO Country Code of the country of origin of this classification. See reference list.                                                                           |
| DocumentReference             | Text         | No        | No            | Reference to document with full or official definition of the Property. See reference list [reference documents](https://github.com/buildingSMART/bSDD/blob/master/2020%20prototype/import-model/reference-lists/reference-documents.csv).                                                                               |
| Description                   | Text         | Yes       | Yes           |                                                                                                                                                      |
| Example                       | Text         | No        | Yes           | Example of the Property                                                                                                                              |
| ConnectedPropertyCodes        | List of text | No        | No            | List of codes of connected properties                                                                                                                |
| PhysicalQuantity              | Text         | No        | Yes           | Name of the physical quantity of the property E.g. “without” or “mass”                                                                               |
| Dimension                     | Text         | No        | No            | In case of a physical quantity, dimension according to ISO 80000 (all parts) E.g. “1 0 -2 0 0 0 0”                                                   |
| MethodOfMeasurement           | Text         | No        | Yes           | E.g. “Thermal transmittance according to ISO 10077-1”                                                                                                |
| DataType                      | Text         | No        | No            | The datatype the property is expressed in. Must be one of:  Boolean,  Character,  Enumeration,  Integer,  Real,  String,  Time                       |
| IsDynamic                     | Boolean      | No        | No            | Default: false If this is a dynamic property, the value is dependent on the parameters provided in field DynamicParameterProperties                  |
| DynamicParameterPropertyCodes | List of text | No        | No            | List of codes of properties which are parameters of the function for a dynamic property                                                              |
| Units                         | List of text | No        | No            | The units to represent a scale that enables a value to be measured (ISO 80000 or ISO 4217 or ISO 8601). List of values. See reference list [units](https://github.com/buildingSMART/bSDD/blob/master/2020%20prototype/import-model/reference-lists/units.csv).            |
| DefiningValues                | List of text | No        | Yes           | In case the value of the property is restricted to a limited list of values, list of values E.g. “Heating”, “Cooling”, “Heating and Cooling”         |
| TextFormat                    | Text         | No        | No            | Pair for text type (encoding, number of characters) The encoding is set according to “Name of encoding standard” of IANA, RFC 2978 E.g. “(UTF-8,32)” |

## ClassificationProperty type

| Field               | DataType | Required? | Translatable? | Description                                                                                                            |
|---------------------|----------|-----------|---------------|------------------------------------------------------------------------------------------------------------------------|
| Code                | Text     | No        | No            | Unique identification within the domain of this classification property                                                |
| PropertyCode        | Text     | No \*     | No            | Reference to the property in the same domain                                                                           |
| ExternalPropertyUri | Text     | No \*     | No            | Reference to the property in a different domain                                                                        |
| Unit                | Text     | No        | No            | See reference list [units](https://github.com/buildingSMART/bSDD/blob/master/2020%20prototype/import-model/reference-lists/units.csv).                                                                                                                       |
| SortNumber          | Integer  | No        | No            | Sort number of this property within the classification                                                                 |
| Symbol              | Text     | No        | No            |                                                                                                                        |
| PropertyType        | Text     | No        | No            | Type of the Property for the classification: “Property” (default) or “Dependency”                                      |
| PropertySet         | Text     | No        | No            |                                                                                                                        |
| PredefinedValue     | Text     | No        | No            | Predefined value for this property. E.g. value for property “IsLoadBearing” can be “true” for classification “IfcWall” |
| MinValue            | Real     | No        | No            |                                                                                                                        |
| MaxValue            | Real     | No        | No            |                                                                                                                        |

\* One of those is required.

## ClassificationRelation type

| Field                    | DataType | Required? | Translatable? | Description                                                                 |
|--------------------------|----------|-----------|---------------|-----------------------------------------------------------------------------|
| RelationType             | Text     | Yes       | No            | One of:  HasReference,  IsEqualTo,  IsSynonymOf,  IsParentOf,  IsChildOf    |
| RelatedClassificationUri | Text     | Yes       | No            | Full URI of the related classification. Can be to same or different domain. |

## Table IfcClassifications

This table should not be altered. It contains the IFC classification names you
can choose from when linking your classifications to IFC classifications.
