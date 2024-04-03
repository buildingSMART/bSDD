# buildingSMART Data Dictionary model

## Table of contents:

* [Glossary](#glossary)
* [Data model](#data-model)
* [JSON format](#json-format)
* [List of fields](#list-of-fields)
    * [Dictionary](#dictionary)  
    * [Class](#class)  
    * [Property](#property)  
    * [ClassProperty](#classproperty)  
    * [ClassRelation](#classrelation)  
    * [AllowedValue](#allowedvalue)  
    * [PropertyRelation](#propertyrelation)  
* [Additional explanations](#additional-explanations)
* [Notifications](#notifications)

## Glossary

* `Data dictionary` - '_a centralized repository of information about data such as meaning, relationships to other data, origin usage and format._' [ISO23386]. '_database that contains metadata_' [ISO12006-3]. The bSDD is a service to facilitate the distribution of such dictionaries. The content in bSDD is structured in `Dictionaries` (previously `Dictionarys`) published by different organizations. Each `Dictionary` (previously `Dictionary`) consists of `Classes` (previously `Classes`) and `Properties`, which could be related to each other or with other `Dictionaries` (previously `Dictionarys`). 

* `Class` - '_description of a set of objects that share the same characteristics._' [ISO23386]

* `Property` - '_an inherent or acquired feature of an item. Example: Thermal efficiency, heat flow, (...), colour._' [ISO23386].

## Data model

The diagram below shows the simplified data model behind the bSDD:

<img src="https://github.com/buildingSMART/bSDD/blob/master/Documentation/graphics/bSDD_data_model.png" alt="bSDD entity diagram" style="width: 650px"/>

See our example demonstrating the usage of the above concepts: [bSDD data example](https://github.com/buildingSMART/bSDD/blob/master/Documentation/graphics/bSDD_data_example.png):
<img src="https://github.com/buildingSMART/bSDD/blob/master/Documentation/graphics/bSDD_data_example.png" alt="bSDD entity diagram" style="width: 700px"/>

We also have a demonstration dictionary: ["Fruit and vegetables"](https://search.bsdd.buildingsmart.org/uri/bs-agri/fruitvegs/1.0.0).

📢 Read about the latest technical updates in the dedicated forum topic: https://forums.buildingsmart.org/t/bsdd-tech-updates/4889

## JSON format

You can deliver data for the buildingSMART Data Dictionary in the JSON file following our standard, which we explain in this document. You can also find the JSON and Excel templates in [/Model/Import Model](/Model/Import%20Model).

Click on the link to get the list of allowed codes for [countries](https://api.bsdd.buildingsmart.org/api/Country/v1), [languages](https://api.bsdd.buildingsmart.org/api/Language/v1), [units](https://api.bsdd.buildingsmart.org/api/Unit/v1), [reference documents](https://api.bsdd.buildingsmart.org/api/ReferenceDocument/v1) and [ifc class](https://api.bsdd.buildingsmart.org/api/Dictionary/v2/Classes?uri=https%3A%2F%2Fidentifier.buildingsmart.org%2Furi%2Fbuildingsmart%2Fifc%2F4.3).
If you think there are reference documents missing, please let us know.

If you are unfamiliar with JSON, we recommend reading [Introduction to JSON](https://javaee.github.io/tutorial/jsonp001.html). Please note that JSON is a format meant for computer systems to exchange data. If you have your dictionary data in a computer system, then it's best to let the system create the JSON for you.

## List of fields

NB Default values will only be applied if a field is not specified. If you specify a field value of "null", the default will not be applied. Note that "null" is not allowed for all fields.

### Dictionary

Contains general information about the `Dictionary` and the delivered data.

| Field            | DataType         | Requ- ired? | Trans- latable? | Description                                                                                                                                                                  |
|------------------|------------------|-----------|---------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| OrganizationCode | Text             | ✅    | | The organisation's code received when registering in bSDD. If your organisation doesn't have a code yet, request one at [bSDD User Helpdesk](https://bsi-technicalservices.atlassian.net/servicedesk/customer/portal/3/group/4/create/25). The code should be preferably short, as it will appear in all the URI links. See section [Code format](#code-format) explaining allowed characters. This code can't start with a digit. Example code: "ifc".   |
| DictionaryCode   | Text             | ✅    | | Code of the dictionary, preferably short, E.g. "ifc". See section [Code format](#code-format) |
| DictionaryName   | Text             | ✅\*  | | Name of the dictionary. \*If the dictionary exists, supplying this name is not necessary. |
| DictionaryVersion| Text             | ✅   | | Version of the dictionary data. Allowed format: up to three dot-separated numbers, e.g.: 1.0.1. Allowed: "12", "10.1", "1.2.3". Not allowed: "1.2.3.4", "Beta", "2x3". We recommend following [Semantic Versioning](https://semver.org/) approach.   |
| LanguageIsoCode  | Text             | ✅   | | ISO language code: indicates the language of the data. If you want to deliver data in multiple languages, use a JSON file per language. See reference list [languages](https://api.bsdd.buildingsmart.org/api/Language/v1). \* E.g. "de-DE" |
| LanguageOnly     | Boolean          | ✅   | | true if JSON contains only language-specific information, no otherwise \*  |
| UseOwnUri      | Boolean                   | ✅        |             | Default: false. Use your own URIs for globally unique identification of Classes and Properties. If you don't use your own URI a URI starting with "https://identifier.buildingsmart.org" will be assigned to each `Class` and `Property` |
| DictionaryUri    | Text             |  ✅\*     | | Required if UseOwnUri = true. Supply the globally unique that's the first part of all Classes and Properties uris, e.g. "urn:mycompany:mydictionary" or "https://mycompany.com/mydictionary" |
| License          | Text             |       | | Name of the license to the content. We suggest choosing from [Creative Commons](https://creativecommons.org/choose/) or [OSI Approved Licenses](https://opensource.org/licenses/). E.g. "MIT" or "CC BY 4.0". Also, helpful resource is [ChooseALicense.com](https://choosealicense.com/).  |
| LicenseUrl       | Text             |       | | Url to a web page with the full license text   |
| ChangeRequestEmailAddress     | Text             |       | | Single email address for receiving change requests from users. |
| ModelVersion     | Text             |       | | Version number of the input JSON template. |
| MoreInfoUrl      | Text             |       | | Url to a web page with more info about the dictionary |
| QualityAssuranceProcedure      | Text      | | | Name or short description of the quality assurance procedure used for the dictionary, e.g. "ETIM international", "AFNOR NF XP P07-150 (PPBIM)", "bSI process", "UN GHS 2015", "UN CPC 1.1", "Private", "Unknown" |
| QualityAssuranceProcedureUrl   | Text      | | | Url to a web page with more detailed info on the quality assurance procedure, e.g. "https://www.buildingsmart.org/about/bsi-process"  |
| ReleaseDate                    | DateTime  | | | Date of release of the version, See [Date Time format](#datetime-format).  |
| Status                         | Text      | | | Possible version statuses: `Preview`, `Active`, `Inactive`. When uploading a new version, it should always be in `Preview`. You can then activate or deactivate content via [the API](https://app.swaggerhub.com/apis/buildingSMART/Dictionaries/v1) or [Management Portal](https://manage.bsdd.buildingsmart.org/). Read more: [the lifecycle of the bSDD content](https://github.com/buildingSMART/bSDD/blob/master/Documentation/bSDD%20import%20tutorial.md#the-lifecycle-of-the-bsdd-dictionary-version)  |
| Classes          | List of Class    | ✅    | | List of objects of type `Class`. See section [Class](#class)  |
| Properties       | List of Property | ✅    | | List of objects of type `Property`. See section [Property](#property) |


\* For delivering data in additional languages, it is sufficient to fill the `Dictionary` type fields, all `Code` fields and the fields marked with `Translatable?` = "Yes" of the other types. Ensure that the `OrganizationCode`, `DictionaryCode` and `DictionaryVersion` are exactly the same and if the data is for adding a language to an existing `Dictionary`, set the field `LanguageOnly` to true.

### Class

A `Class` can be any (abstract) object (e.g. "wall") or abstract concept (e.g. "time") or process (e.g. "installation").

| Field                     | DataType                       | Requ- ired? | Trans- latable? | Description                                                                                                        |
|---------------------------|--------------------------------|-------------|-----------------|--------------------------------------------------------------------------------------------------------------------|
| Code                      | Text                           | ✅         |            | Unique identification within the dictionary of the class E.g. "ifc-00123-01". See section [Code format](#code-format).                                |
| Name                      | Text                           | ✅         | ✅         | Name of the `Class,` E.g. "IfcCurtainWall"                                                                   |
| ClassType                 | Text                           | ✅*        |            | Must be one of: `Class`, `Material`, `GroupOfProperties`, `AlternativeUse`. Read more about [class types](#class-types). If not specified, the `Class` type will be used by default. The types `ReferenceDocument`, `ComposedProperty` and `Dictionary` were deprecated and can not be used on upload but may be present in API results for the duration of transition time. |
| Definition                | Text                           |            | ✅          | Definition of the `Class`, explaining the semantic meaning. A required field according to the ISO. Supports [double square bracket links](#double-square-bracket-links).  |
| Description               | Text                           |            | ✅          | Additional field for supplementary explanation. Please only use it if the _Definition_ comes from a standard and requires further explanation.  |
| ParentClassCode           | Text                           |            |             | Reference to the parent `Class`. The ID in this field MUST exist in the data delivered. E.g. "ifc-00123-00". See section [How to define relations?](#defining-relations) |
| RelatedIfcEntityNamesList | List of text                   |            |             | Codes of IFC classes to use as a representation of this `Class`. For example: ['IfcWall']. See bSDD API [ifc classs](https://api.bsdd.buildingsmart.org/api/Dictionary/v3/Classes?uri=https%3A%2F%2Fidentifier.buildingsmart.org%2Furi%2Fbuildingsmart%2Fifc%2F4.3%2F). See section [How to define relations?](#defining-relations)                                      |
| Synonyms                  | List of text                   |            | ✅         | List of alternative names of this class for easier finding.|
| ActivationDateUtc         | DateTime                       |            |             | See [Date Time format](#datetime-format). |
| ReferenceCode             | Text                           |            |             | Reference code can have dictionary-specific usage. If null, then the value of `Code` is used to fill the field. To make `ReferenceCode` empty, use empty string "".  |
| CountriesOfUse            | List of text                   |            |             | List of country ISO codes this `Class` is being used. See reference list [countries](https://api.bsdd.buildingsmart.org//api/Country/v1).                                    |
| CountryOfOrigin           | Text                           |            |             | ISO Country Code of the country of origin of this `Class`. See reference list [countries](https://api.bsdd.buildingsmart.org//api/Country/v1).                                         |
| CreatorLanguageIsoCode    | Text                           |            |             | Language ISO code of the creator. See reference list [languages](https://api.bsdd.buildingsmart.org/api/Language/v1). |
| DeActivationDateUtc       | DateTime                       |            |             | See [Date Time format](#datetime-format). |
| DeprecationExplanation    | Text                           |            | ✅         | Only fill for deprecated definitions. |
| DocumentReference         | Text                           |            |             | Reference to document with the full or official definition of the `Class`. See reference list [reference documents](https://api.bsdd.buildingsmart.org/api/ReferenceDocument/v1). |
| OwnedUri                  | Text                           |            |             | If you specified `UseOwnUri = true` at the dictionary level, you must supply the URI that globally uniquely identifies the `Class`  |
| ReplacedObjectCodes       | List of text                   |            |             | List of Class Codes this Class replaces                                                          |
| ReplacingObjectCodes      | List of text                   |            |             | List of Class Codes this class is replaced by                                                    |
| RevisionDateUtc           | DateTime                       |            |             | See [Date Time format](#datetime-format). |
| RevisionNumber            | Integer                        |            |             |  |
| Status                    | Text                           |            |             | Status of the `Class`: `Active` (default) or `Inactive` |
| SubdivisionsOfUse         | List of text                   |            | ✅         | List of geographical regions of use E.g. "US-MT"  |
| Uid                       | Text                           |            |             | Unique identification (ID), in case the URI is not enough. |
| VersionDateUtc            | DateTime                       |            |             | By default takes the date of import. See [Date Time format](#datetime-format). |
| VersionNumber             | Integer                        |            |             |  |
| VisualRepresentationUri   | Text                           |            | ✅         |  |
| ClassProperties           | List of ClassProperty          |            |             | See section [ClassProperty](#classproperty) |
| ClassRelations            | List of ClassRelation          |            |             | See section [ClassRelation](#classrelation) |

### Material

A `Material` is a `Class` of type `Material`.
Since the release of November 2023, Materials are not treated separately anymore.

### Property

A `Class` can have multiple properties, and a `Property` can be part of many classs

| Field                         | DataType     | Requ- ired? | Trans- latable? | Description                                                                                                                                          |
|-------------------------------|--------------|-----------|---------------|------------------------------------------------------------------------------------------------------------------------------------------------------|
| Code                          | Text         | ✅       |             | Unique identification within the dictionary of the property E.g. "ifc-99088-01". See section [Code format](#code-format)                                                                          |
| Name                          | Text         | ✅       | ✅           | Name of the Property E.g. "IsExternal"                                                                                                               |
| Definition                    | Text         |         | ✅           | Definition of the `Property`, explaining the semantic meaning. A required field according to the ISO. Supports [double square bracket links](#double-square-bracket-links). |
| Description                   | Text         |         | ✅           | Additional field for supplementary explanation. Please only use it if the _Definition_ comes from a standard and requires further explanation.  |
| DataType                      | Text         |         |             | The datatype the property is expressed in. Must be one of:  `Boolean`,  `Character`,  `Integer`,  `Real`,  `String`,  `Time`                       |
| Units                         | List of text |         |             | The units represent a scale that enables a value to be measured (ISO 80000 or ISO 4217, or ISO 8601). List of values. See reference list (JSON) [units](https://api.bsdd.buildingsmart.org/api/Unit/v1).  We are working on supporting the [QUDT](http://www.qudt.org/) vocabulary. If you would like to import using QUDT units or want to have the QUDT units in the API output, please let us know. |
| Example                       | Text         |         | ✅           | Example value of the `Property` |
| ActivationDateUtc             | DateTime         |         |             | See [Date Time format](#datetime-format). |
| ConnectedPropertyCodes        | List of text |         |             | List of codes of one or more connected properties. Can also be full URI instead of code, in case it is a property of another dictionary. See [assembling properties](#assembling-properties)|
| CountriesOfUse                | List of text         |         |             |  List of country ISO codes this `Property` is being used. See reference list [countries](https://api.bsdd.buildingsmart.org/api/Country/v1).                                                      |
| CountryOfOrigin               | Text         |         |             | ISO Country Code of the country of origin of this `Property`. See reference list [countries](https://api.bsdd.buildingsmart.org//api/Country/v1).                                                                           |
| CreatorLanguageIsoCode        | Text         |         |             | Language ISO code of the creator. See reference list (JSON)[languages](https://api.bsdd.buildingsmart.org/api/Language/v1)  |
| DeActivationDateUtc           | DateTime         |         |             | See [Date Time format](#datetime-format). |
| DeprecationExplanation        | Text         |         | ✅           |  |
| Dimension                     | Text         |         |             | In case of a physical quantity, specify dimension according to [International_System_of_Quantities](https://en.wikipedia.org/wiki/International_System_of_Quantities), as defined in ISO 80000-1. The order is: `length`, `mass`, `time`, `electric current`, `thermodynamic temperature`, `amount of substance`, and `luminous intensity`. For example, speed (m/s) would be denoted as "1 0 -1 0 0 0 0". More examples in [IDS docs](https://github.com/buildingSMART/IDS/blob/master/Documentation/units.md) |
| DimensionLength               | Integer      |         |             | The Length dimension; either use the field `Dimension` to specify all parts or specify all parts separately|
| DimensionMass               | Integer      |         |             | The Mass dimension; either use the field `Dimension` to specify all parts or specify all parts separately|
| DimensionTime               | Integer      |         |             | The Time dimension; either use the field `Dimension` to specify all parts or specify all parts separately|
| DimensionElectricCurrent               | Integer      |         |             | The ElectricCurrent dimension; either use the field `Dimension` to specify all parts or specify all parts separately|
| DimensionThermodynamicTemperature               | Integer      |         |             | The ThermodynamicTemperature dimension; either use the field `Dimension` to specify all parts or specify all parts separately|
| DimensionAmountOfSubstance               | Integer      |         |             | The AmountOfSubstance dimension; either use the field `Dimension` to specify all parts or specify all parts separately|
| DimensionLuminousIntensity               | Integer      |         |             | The LuminousIntensity dimension; either use the field `Dimension` to specify all parts or specify all parts separately|
| DocumentReference             | Text         |         |             | Reference to document with the full or official definition of the `Property`. See reference list (JSON) [reference documents](https://api.bsdd.buildingsmart.org/api/ReferenceDocument/v1).                                                                               |
| DynamicParameterPropertyCodes | List of text |         |             | List of codes of properties which are parameters of the function for a dynamic property. See [assembling properties](#assembling-properties).       |
| IsDynamic                     | Boolean      |         |             | Default: `false`. If this is a dynamic property, the value is dependent on the parameters provided in the field `DynamicParameterPropertyCodes`. See [assembling properties](#assembling-properties).     |
| MaxExclusive            | Real     |         |             | Maximum allowed value, exclusive - do not fill both inclusive and exclusive values |
| MaxInclusive            | Real     |         |             | Maximum allowed value, inclusive - do not fill both inclusive and exclusive values |
| MinExclusive            | Real     |         |             | Minimum allowed value, exclusive |
| MinInclusive            | Real     |         |             | Minimum allowed value, inclusive |
| MethodOfMeasurement           | Text         |         | ✅           | E.g. "Thermal transmittance according to ISO 10077-1"                                                                                                |
| OwnedUri                | Text                           |         |            | If you specified `UseOwnUri = true` at the dictionary level, you must supply the URI that globally uniquely identifies the Property  |
| Pattern            | Text     |         |             | An [XML Schema regular expression](https://www.regular-expressions.info/xml.html) to limit allowed values |
| PhysicalQuantity              | Text         |         | ✅           | Name of the physical quantity of the property, E.g. "without" or "mass"                                                                               |
| PropertyValueKind             | Text         |         |             | Must be one of:  `Single` (one value; this is the default),  `Range` (two values),  `List` (multiple values), `Complex` (neither single/range/list, for example an object like IfcActor or an aggregation of connected properties - see [assembling properties](#assembling-properties)), `ComplexList` (list of complex values).   |
| ReplacedObjectCodes           | List of text |         |             | List of Property Codes this `Property` replaces                                                                                            |
| ReplacingObjectCodes          | List of text |         |             | List of Property Codes this `Property` is replaced by                                                                                      |
| RevisionDateUtc               | DateTime         |         |             | See [Date Time format](#datetime-format). |
| RevisionNumber                | Integer      |         |      |  |
| Status                        | Text         |         |             | Status of the Property: `Active` (default) or `Inactive`    |
| SubdivisionsOfUse             | List of text         |         | ✅           | List of geographical regions of use E.g. "US-MT"                                                                                 |
| TextFormat                    | Text         |         |             | Pair for text type (encoding, number of characters) The encoding is set according to "Name of encoding standard" of IANA, RFC 2978, E.g. "(UTF-8,32)" |
| Uid                  | Text                   |         |            | Unique identification (ID), in case the URI is not enough. |
| VersionDateUtc                | DateTime         |         |             | By default takes the date of import. See [Date Time format](#datetime-format). |
| VersionNumber                 | Integer      |         |             |  |
| VisualRepresentationUri       | Text         |         | ✅           |  |
| PropertyRelations              | List of PropertyRelation  |   | ✅           | List of related properties. See section [PropertyRelation](#propertyrelation) |
| AllowedValues              | List of AllowedValue  |   | ✅           | List of allowed values for the property. Note: do not use this one for properties of type boolean. See section [AllowedValue](#allowedvalue). |


### ClassProperty

| Field               | DataType | Requ- ired? | Trans- latable? | Description                                                                                                            |
|---------------------|----------|-----------|---------------|------------------------------------------------------------------------------------------------------------------------|
| Code                | Text     | ✅     |     | Unique identification within the dictionary of this `ClassProperty`. See section [Code format](#code-format).                                                |
| PropertyCode        | Text     | ✅\*   |     | Reference to the `Property` if it is in the same `Dictionary`. Not required if you fill in the PropertyUri  |
| PropertyUri         | Text     | ✅\*   |     | Reference to the `Property` if it is in a different `Dictionary`, e.g. [https://identifier.buildingsmart.org/uri/buildingsmart/ifc/4.3/prop/ClearWidth](https://identifier.buildingsmart.org/uri/buildingsmart/ifc/4.3/prop/ClearWidth) Not required if you fill the PropertyCode       |
| Description         | Text     |         | ✅ | You can supply the property description specific to the class. If left out, the 'common' description of the property will be shown where applicable |
| PropertySet         | Text     |         |     | Code validation will be applied. Name of the "property set" where the property should be placed during IFC export. The prefix 'Pset_' is only reserved for the official IFC. See [assembling properties](#assembling-properties). |
| Unit                | Text     |         |     | See reference list (json) [units](https://api.bsdd.buildingsmart.org/api/Unit/v1).                                                                                                                       |
| PredefinedValue     | Text     |         |     | Predefined value for this `Property`. E.g. value for property "IsLoadBearing" can be "true" for class "IfcWall" |
| IsRequired          | Boolean  |         |     | Indicates if this is a required `Property` of the `Class` |
| IsWritable          | Boolean  |         |     | Indicates if the value of this `Property` of the `Class` can be changed |
| MaxExclusive        | Real     |         |     | Maximum allowed value, exclusive. Overrides the value defined for the `Property`. Do not fill both 'inclusive' and 'exclusive' values. |
| MaxInclusive        | Real     |         |     | Maximum allowed value, inclusive. Overrides the value defined for the `Property`. Do not fill both 'inclusive' and 'exclusive' values. |
| MinExclusive        | Real     |         |     | Minimum allowed value, exclusive. Overrides the value defined for the `Property`. Do not fill both 'inclusive' and 'exclusive' values. |
| MinInclusive        | Real     |         |     | Minimum allowed value, inclusive. Overrides the value defined for the `Property`. Do not fill both 'inclusive' and 'exclusive' values. |
| Pattern             | Text     |         |     | An [XML Schema regular expression](https://www.regular-expressions.info/xml.html) to limit allowed values. Overrides the pattern defined for the Property |
| OwnedUri                | Text                           |         |            | If you specified `UseOwnUri = true` at the dictionary level, you must supply the URI that globally uniquely identifies the ClassProperty  |
| PropertyType        | Text     |         |     | Type of the `Property` for the `Class`: `Property` (default) or `Dependency`                                      |
| SortNumber          | Integer  |         |     | Sort number of this `Property` within the `Class`                                                                 |
| Symbol              | Text     |         |     |                                                                                                                        |
| AllowedValues       | List of AllowedValue  |  | ✅           | List of allowed values for the `ClassProperty`. Overrides the values defined for the `Property`. Do not use this one for properties of type boolean. See section [AllowedValue](#allowedvalue)  |
| ~~ExternalPropertyUri~~ | ~~Text~~     |       |             | DEPRECATED - Use `PropertyUri` instead                |


\* One of those is required.

### ClassRelation

`Classes` can be linked by relations. See section [How to define relations?](#defining-relations)

| Field                    | DataType | Requ- ired? | Trans- latable? | Description                                                                 |
|--------------------------|----------|-----------|---------------|-----------------------------------------------------------------------------|
| RelationType             | Text     | ✅       |             | One of:  `HasMaterial`, `HasReference`,  `IsEqualTo`,  `IsSimilarTo`,  `IsParentOf`,  `IsChildOf`, `HasPart`, `IsPartOf`. Read more about [Relation types](#relation-types).    |
| RelatedClassUri | Text     | ✅       |             | Full URI of the related `Class`. It can be to same or a different `Dictionary`. Example: https://identifier.buildingsmart.org/uri/etim/etim/8.0/class/EC002987|
| RelatedClassName | Text     |        |             |  |
| Fraction       | Real     |        |             | Only applicable to `HasMaterial` relation. Optional provision of a fraction of the total amount (e.g. volume or weight) that applies to the Class owning the relations. The sum of Fractions per class/relationtype must be 1. Similar to Fraction in [IfcMaterialConstituent](http://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/lexical/IfcMaterialConstituent.htm)|
| OwnedUri                | Text                           |         |            | If you specified `UseOwnUri = true` at the dictionary level, you must supply the URI that globally uniquely identifies the ClassRelation  |


### AllowedValue

Note: adding translations of the `AllowedValue` is not supported yet

| Field                    | DataType | Requ- ired? | Trans- latable? | Description                                                                 |
|--------------------------|----------|-----------|---------------|-----------------------------------------------------------------------------|
| Code             | Text     | ✅       |             | Code is a unique identification of the value (max 20 characters). If you want to add translations of Values or their Descriptions, you must supply a Code for each Value. See section [Code format](#code-format) |
| Value | Text     | ✅       | ✅       | One of the Values the property can have, e.g. "Green" in case the Property is something like "Color"|
| Description | Text     |        | ✅       | A description of the value|
| Uri*| Text |  |  | *To be deprecated in the new model version, as it overlaps with the OwnedUri. |
| SortNumber | Integer     |        |             | SortNumber of the Value in the list of Values of the `Property` it belongs to|
| OwnedUri                | Text                           |         |            | If you specified `UseOwnUri = true` at the dictionary level, you can supply the URI that globally uniquely identifies the AllowedValue  |


### PropertyRelation

| Field                    | DataType | Required? | Translatable? | Description                                                                 |
|--------------------------|----------|-----------|---------------|-----------------------------------------------------------------------------|
| RelatedPropertyName | Text     |        |             | Name of the related `Property`.|
| RelatedPropertyUri | Text     | ✅       |             | Full URI of the related `Property`. It can be to same or a different `Dictionary`.|
| RelationType             | Text     | ✅       |             | One of:  `HasReference`,  `IsEqualTo`,  `IsSimilarTo`, ~~IsParentOf,  IsChildOf, HasPart~~. Read more about [Relation types](#relation-types).  |
| OwnedUri                | Text                           |         |            | If you specified `UseOwnUri = true` at the dictionary level, you must supply the URI that globally uniquely identifies the PropertyRelation  |

---

## Additional explanations

### Code format

For codes, only characters, numbers, underscore, dot, and dash are allowed (a-z, A-Z, 0-9, "_", ".", "-"). Codes are not case-sensitive.
Some examples of valid codes are: "bs-agri", "apple", "one.X". 
Some examples of invalid codes are: "my test" (whitespace not allowed), "ДДb$%" (only characters a-z and A-Z allowed, not %, $, or Д).

### Class types

Each class must have a specific type. Below is the explanation of what each type means, according to ISO 12006-3:
* `Class` - description of a set of objects that share the same characteristics <sup>[ISO12006-3,3.7]</sup>. This is the most common type in bSDD. (e.g. wall, space)
* `GroupOfProperties` - collection enabling the properties to be prearranged or organized <sup>[ISO12006-3,3.14]</sup>. For example, 'environmental properties'. See [assembling properties](#assembling-properties).
* `Material` - a physical substance that things can be made from (e.g. steel, glass)
* `AlternativeUse` - type to be used if no other type fits the needs.<sup>[ISO12006-3,3.1]</sup>.
   * Be aware that most software implementations disregard this class type, as it is not straightforward to interpret.
* **DEPRECATED** ~~ReferenceDocument - a publication that is consulted to find specific information, particularly in a technical or scientific dictionary. <sup>[ISO12006-3,3.18]</sup>. A reference document can be associated with any data present in a data dictionary.~~
  * In bSDD we have a global list of [reference documents](https://api.bsdd.buildingsmart.org/api/ReferenceDocument/v1), which includes the most common standards that can be used as reference. This is to avoid having duplicate references with different naming. If you don't find the reference you are looking for, and think it should be added to the list - let us know: <a href="mailto:bsdd_support@buildingsmart.org">bsdd_support@buildingsmart.org</a>.
* **DEPRECATED**  ~~ComposedProperty - (...) corresponding to a feature needing multiple properties to be defined. <sup>[ISO12006-3,3.8]</sup>.~~
  * ~~Example: To describe the characteristic "concrete facing quality", it is mandatory to describe 3 properties: concrete planarity, concrete hue, and concrete texture.~~
  * Use `GroupOfProperties` instead.


### Defining relations

`ParentClassCode` - `Class`es within the same dictionary can be organized in a tree-like hierarchy structure. For example: “IfcCurtainWall” is a more
specific class of “IfcWall”. In bSDD terminology, we say that “IfcWall” is a **parent of** “IfcCurtainWall”. To define such specialization relation, use the `ParentClassCode` attribute on the child object.

`ClassRelation` and `PropertyRelation`- use those to link your concepts with each other. Relations allow us to define parent-child links also with other dictionaries. Apart from specialization, you can also define other types of relations, such as decomposition (`HasPart` type, see the list of possible types: [Relation types](#relation-types)).

`RelatedIfcEntityNamesList` - IFC is a top-level schema (foundation classes) used for exchanging information between software. Because of that, the bSDD provides a special way to relate your class to IFC. Use `RelatedIfcEntityNamesList` to show which entities from IFC you are referring to or extending. For example, “Signaling LED diode” relates to “IfcLamp” from IFC. `RelatedIfcEntityNamesList` can be used by bSDD-related tools to filter the list of possible classes to a particular IFC category.


### Relation types

`Properties` and `Classes` can be related to each other. Each relation must have a specific type to allow software to interpret it. Below is an explanation of what each type means:
* `IsEqualTo` - if two concepts are unequivocal and have the same name, code, definition and description. Classes also need to share the same class properties. It is quite rare for concepts to be equal. An example of usage is when a concept doesn't have an official translation, but someone defines a new dictionary with that concept in a new language and wants to say it is exactly the same as the original. (We always recommend proposing translations and improvements to the original data dictionaries instead of building duplicate ones). 
* `IsSimilarTo` - if two concepts are almost equal but differ by name, code, definition, description or set of class properties. This is a very common relationship type. Used, for example, to say that 'IfcWall' is a similar concept to 'Wall System' from CCI. The downside of such a relation is that it doesn't inform on the level of similarity – is it slightly differing by the wording of the definition, or is the difference huge?
* `HasReference` - if two concepts relate to each other, but other relation types do not apply. For example, "wall lamp" (or "sconce") is referencing a wall, even though those are different concepts and there is no hierarchy between them.
* **DEPRECATED** ~~IsSynonymOf - if two concepts are unequivocal but have a different name.~~

Only applicable to classes (not properties):
* `IsChildOf` - specialisation relation. The equivalent of the "subtype" relationship <sup>[ISO12006-3, F3.1]</sup>. For example: "Electrical motor" and a "Combustion motor" are children (subtypes) of the generic concept "Motor".
* `IsParentOf` - the opposite relation to `IsChildOf`.
* `HasPart` - composition relation. For example, an electric motor can be composed of elements such as stators, rotors, etc. <sup>[ISO12006-3, F3.2]</sup>.
* `IsPartOf` - reverse of `HasPart`.
* `HasMaterial` - a class that can be associated with a particular material. For example: "Steel Beam" could be related to the material "Steel".

  
### DateTime format

The date-time format according to the ISO 8601 series should be used: `YYYY-MM-DDThh:mm:ssTZD`. Import allows both: `2023-05-10`, `2023-05-10T15:10:12Z` and `2023-05-10T15:10:12+02:00`.


### Property inheritance

* Parent `Class` → child `Class`  
The child `Class` does not inherit properties from the parent `Class`. If authors want child classes to also have properties of parent classes, they should specify them intentionally in import files.  
For example, the [IfcWall](https://search.bsdd.buildingsmart.org/uri/buildingsmart/ifc/4.3/class/IfcWall) is a parent class of [IfcWallStandardCase](https://search.bsdd.buildingsmart.org/uri/buildingsmart/ifc/4.3/class/IfcWallStandardCase). While [IfcWall](https://search.bsdd.buildingsmart.org/uri/buildingsmart/ifc/4.3/class/IfcWall) has the property [AcousticRating](https://search.bsdd.buildingsmart.org/uri/buildingsmart/ifc/4.3/class/IfcWall/prop/Pset_WallCommon/AcousticRating), the [IfcWallStandardCase](https://search.bsdd.buildingsmart.org/uri/buildingsmart/ifc/4.3/class/IfcWallStandardCase) doesn't.

* `Property` → `ClassProperty`  
`ClassProperty` is an instantiation of general `Property` for a particular `Class`. The attributes of a property, such as `AllowedValue` and min/max restrictions,  are by default passed to `ClassProperty`. The values of the `ClassProperty` can be modified without influencing the origin `Property`.  
For example, the [Height](https://search.bsdd.buildingsmart.org/uri/bs-agri/fruitvegs/1.0.0/prop/height) has an upper limit of 100 cm. When applied to the "Apple" class, the [Apple-Height](https://search.bsdd.buildingsmart.org/uri/bs-agri/fruitvegs/1.0.0/class/apple/prop/SizeSet/height) has a lower limit - 25cm. 


### Latest version
In bSDD, all resources get a unique identifier - URI. The URI, among other information, contains codes of the organisation, the dictionary and the version number, for example .../uri/bs-agri/fruitvegs/**1.0.0**/class/fruit
If you want to reference specific resources but are not sure of the version or want to always point to the most recent version, we implemented the "latest" feature. Now, it is possible to use "latest" instead of a version number, and bSDD will resolve the link to the latest active or preview version containing that resource: 
.../uri/bs-agri/fruitvegs/**latest**/class/fruit. 

<img src="https://github.com/buildingSMART/bSDD/blob/master/Documentation/graphics/latest_example.jpg" alt="bSDD latest" style="width: 750px"/>

Try it out:
https://search.bsdd.buildingsmart.org/uri/bs-agri/fruitvegs/latest/class/fruit

⚠️ The "latest" points to the most recent resource, meaning that it will change once a new version is present. Use with caution as it is not an immutable URI, and the content can change. For contractual agreements, we suggest using specific version numbers.


### Assembling properties

**Groups of Properties** (use `Class`.`ClassType`:`GroupOfProperties`) "collection enabling the properties to be prearranged or organized" <sup>[ISO12006-3,3.14]</sup>. In bSDD, implemented as a Type of Class meant to group multiple Properties.

Use Group of Properties to organize properties in a data dictionary.

Example: _'[Global Warming Potential](https://search.bsdd.buildingsmart.org/uri/LCA/LCA/3.0/class/GlobalWarmingPotential)' class from '[LCA indicators and modules](https://search.bsdd.buildingsmart.org/uri/LCA/LCA/3.0)' groups four properties: '[...total](https://search.bsdd.buildingsmart.org/uri/LCA/LCA/3.0/prop/GWP_total)', '[...biogenic](https://search.bsdd.buildingsmart.org/uri/LCA/LCA/3.0/prop/GWP_biogenic)', '[...fossil fuels](https://search.bsdd.buildingsmart.org/uri/LCA/LCA/3.0/prop/GWP_fossil)' and '[...land use...](https://search.bsdd.buildingsmart.org/uri/LCA/LCA/3.0/prop/GWP_luluc)'._

**Sets of properties** (use `ClassProperty`.`PropertySet`) - a concept from the IFC standard for grouping properties. In bSDD represented as a text field defined for Class Property, telling in which set this Class Property should appear when serialised to IFC data. 
  * A Property Set, as defined in ISO 16739-1, is a group of properties, but a group of properties is not necessarily a Property Set.
  * A property can be a member of several groups of properties. A class property cannot be a member of several Property Sets.
  * The prefix 'Pset_' is only reserved for the official IFC.

Use Property Set to define where to place a property in an IFC dataset.

Example: _A property 'Concrete Cover' of 'IfcWall' is located in property set: 'Pset_ConcreteElementGeneral'._

**Connected properties** (use `Property`.`ConnectedPropertyCodes`) "List of properties connected to the current property. The connection can be a specialization or a dependency." <sub>[ISO12006-3, 5.3.29]</sub> 

Use Connected Properties if the value of one property depends on the value of another property.

Example: _The property ['Global Warming Potential - total' (GWP)](https://search.bsdd.buildingsmart.org/uri/LCA/LCA/3.0/prop/GWP_total) should be defined for each phase of the life cycle of a product. Defining the property separately for each phase (GWP_A1, GWP_A2, ...) is not desired. Instead, it is connected to another property - '[information module (PHASE)](https://search.bsdd.buildingsmart.org/uri/LCA/LCA/3.0/prop/information_module)', taking 18 possible values (A1, A2, C3...). To interpret the meaning of GWP value, one should look at pairs of values: {GWP=1.0, PHASE=A1}, {GWP=15.0, PHASE=A3}, etc. _

⚠️ This feature comes from the ISO standard but is rarely supported by software implementation. The IFC also doesn't support multiple properties with the same name under one property set. Consider avoiding Connected Properties to make the data dictionary more accessible.

**Dynamic properties** (use `Property`.`IsDynamic` and .`DynamicParameterPropertyCodes`) "properties which are parameters of the function for a dynamic property" <sub>[ISO23386, 5.3.29]</sub>. In other words, the value of a dynamic property is dependent on the values of properties specified in `DynamicParameterPropertyCodes`. There is no field in bSDD to define the exact equation of the formula in a machine-interpretable form. 

Use Dynamic Properties to tell which other properties influence the value of the particular property.

Example: _The 'Area' of a wall depends on its 'Height' and 'Length', following the formula: A = H * L._

⚠️ This feature comes from the ISO standard but is rarely supported by software implementation. Consider avoiding Dynamic Properties to make the data dictionary more accessible.

### 🚧 Restricting property values
`AllowedValues`...

`Min/MaxInc/Exclusive`...

`Pattern`...


### 🚧 How are bSDD resources identified?  
`URI`... Can be generated by bSDD or external.

`Code`...  See section [Code format](#code-format).

`UID`(GUID)...


### 🚧 How to specify units?
`Unit(s)`...

`Dimension`...

`PhysicalQuantity`...

### Double square bracket links
It is possible to reference other resources from the same dictionary using double square brackets, and the platform will replace the brackets with hyperlinks pointing to that resource. In cases where the same code exists for both class and property, the hyperlink will point to the class. If the code is not found, the square brackets are omitted. The API returns the definition with square brackets. 

## Notifications

**2023-07 - Important notification:**

> As we're continuously improving bSDD, we've updated all identifiers: the dash between dictionary code and dictionary version has been replaced by a dash, e.g.:
>  https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs-1.0.0/class/apple will now be https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/1.0.0/class/apple
> 
> We will support supplying and retrieving data using the dash between dictionary code and version for (at least) 4 months. But please do note that only identifiers in the new format are returned by the bSDD APIs.

**2022-08 - Important notification:**

> The bSDD is in the process of moving from identifiers (aka "URI") starting with "http://identifier.buildingsmart.org" to "https://identifier.buildingsmart.org" ("http" to "https"). This is to ease the use of these identifiers as hyperlinks as well.
> 
> Support for using the old "http" identifiers will be deprecated soon!

📢 Read more about the latest tech updates in the dedicated forum topic: https://forums.buildingsmart.org/t/bsdd-tech-updates/4889
