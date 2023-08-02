# buildingSMART Data Dictionary model

`data dictionary` - centralized repository of information about data such as meaning, relationships to other data, origin usage and format.<sup>1<sup>3.9</sup></sup>. The bSDD is a service to facilitate distribution of such dictionaries.

The content in bSDD is structured in `Domains` published by owner organizations. Each `Domain` consists of `Classifications` and `Properties`, which could be related with each other or with other `Domains`. 

> `Domain` - area of activity covering a science, a technique, a material, etc. A domain can be associated with a group to which the property applies. <sup>1<sup>3.11</sup></sup>
> 
> `Class` - description of a set of objects that share the same characteristics.<sup>1<sup>3.7</sup></sup> 
>
>  `Property` - inherent or acquired feature of an item. Example: `Thermal efficiency`, `heat flow`, (...) `colour`. <sup>1<sup>3.17</sup></sup>

If we use IFC as an example, IFC 4.3 is the `Domain`, "IfcWall" is a `Class` and "AcousticRating" is a `Property`. A `Class` can have zero or more `Properties`. 

The diagram below shows the simplified data model behind the bSDD:

<!-- <img src="https://github.com/buildingSMART/bSDD/blob/master/Documentation/graphics/bSDD%20database%20diagram.png" alt="bSDD entity diagram"/> -->
<img src="https://github.com/buildingSMART/bSDD/blob/master/Documentation/graphics/bSDD_data_model.png" alt="bSDD entity diagram" style="width: 550px"/>

Here is an example demonstrating usage of the above concepts:

<img src="https://github.com/buildingSMART/bSDD/blob/master/Documentation/graphics/bSDD_data_example.png" alt="bSDD entity diagram" style="width: 800px"/>

We also have a demonstration domain: ["Fruit and vegetables"](https://search.bsdd.buildingsmart.org/uri/bs-agri/fruitvegs/1.0.0).

# JSON import

You can deliver data for the buildingSMART Data Dictionary in the JSON file following our standard, which we explain in this document. You can also find the JSON and Excel templates in [/Model/Import Model](/Model/Import%20Model).

Click on the link to get the list of allowed codes for [countries](https://api.bsdd.buildingsmart.org/api/Country/v1), [languages](https://api.bsdd.buildingsmart.org/api/Language/v1), [units](https://api.bsdd.buildingsmart.org/api/Unit/v1), [reference documents](https://api.bsdd.buildingsmart.org/api/ReferenceDocument/v1) and [ifc classification](https://api.bsdd.buildingsmart.org/api/Domain/v2/Classifications?namespaceUri=https%3A%2F%2Fidentifier.buildingsmart.org%2Furi%2Fbuildingsmart%2Fifc%2F4.3).
If you think there are reference items missing, please let us know.

If you are not familiar with JSON, reading [Introduction to JSON](https://javaee.github.io/tutorial/jsonp001.html) is a good idea. And please note that JSON is a format meant for computer systems to exchange data. If you have your domain data in a computer system then it's best to let the system create the json for you.

## General notes

* Default values will only be applied if a field is not specified. If you specify a field value of "null", the default will not be applied. Note that "null" is not allowed for all fields.

* For codes only characters a to z, A to Z, numbers, underscore, dot and dash are allowed. Codes are not case sensitive.
Some examples of valid codes are:
  - bs-agri
  - apple
  - one.X

  Some examples of invalid codes are:
  - ДДb    (only characters a-z and A-Z allowed)
  - ab$    ($ not allowed)
  - test-% (% not allowed) 


## Domain

Contains general information about the `Domain` and the delivered data.

| Field            | DataType               | Requ- ired? | Trans- latable? | Description                                                                                                                                                                  |
|------------------|------------------------|-----------|---------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Classifications  | List of Classification | ✅       |               | List of objects of type `Classification`. See section [Classification](#classification)  |
| DomainCode       | Text                   | ✅       |             | Code of the domain, preferably short, only alphabetical characters and numbes allowed, must start with alphabetical character E.g. "ifc"   |
| DomainName       | Text                   | ✅\* |   | Name of the domain. \*If the domain exists supplying this name is not necessary. |
| DomainNamespaceUri      | Text                   |         |       | Required if UseOwnUri = true. Supply the globally unique namespace that's the first part of all Classifications and Properties namespaces, e.g. "urn:mycompany:mydomain" |
| DomainVersion    | Text                   | ✅       |             | Version of the domain data. Allowed format: up to three dot-separated numbers, e.g.: 1.0.1. Allowed: "12", "10.1", "1.2.3". Not allowed: "1.2.3.4", "Beta", "2x3". We recommend following [Semantic Versioning](https://semver.org/) approach.   |
| LanguageIsoCode  | Text                   | ✅       |             | ISO language code: indicates the language of the data. If you want to deliver data in multiple language use a json file per language. See reference list [languages](https://api.bsdd.buildingsmart.org/api/Language/v1). \* E.g. "de-DE" |
| LanguageOnly     | Boolean                | ✅       |             | true if json contains only language specific information, no otherwise \*  |
| License          | Text                   |         |             | Name of the license to the content. We suggest choosing from [Creative Commons](https://creativecommons.org/choose/) or [OSI Approved Licenses](https://opensource.org/licenses/). E.g. "MIT" or "CC BY 4.0". Also helpful resource is [ChooseALicense.com](https://choosealicense.com/).  |
| LicenseUrl      | Text                   |         |             | Url to a web page with the full license text   |
| Materials      | List of Material                   |         |             | List of objects of type `Material`. See section [Material](#material)   |
| MoreInfoUrl      | Text                   |         |             | Url to web page with more info about the domain |
| OrganizationCode | Text                   | ✅       |             | Code of the Organization, preferably short, as it will appear in all the URI links. Only alphabetical characters and numbers are allowed. Can't start with a digit. E.g. "ifc". If you do not have a code for your organization yet, request one at [bSDD User Helpdesk](https://bsi-technicalservices.atlassian.net/servicedesk/customer/portal/3/group/4/create/25)                                                                                         |
| Properties       | List of Property       | ✅       |               | List of objects of type `Property`. See section [Property](#property) |
| QualityAssuranceProcedure          | Text                   |         |             | Name or short description of the quality assurance procedure used for the domain, e.g. "ETIM international", "AFNOR NF XP P07-150 (PPBIM)", "bSI process", "UN GHS 2015", "UN CPC 1.1", "Private", "Unknown" |
| QualityAssuranceProcedureUrl      | Text                   |         |             | Url to a web page with more detailed info on the quality assurance procedure, e.g. "https://www.buildingsmart.org/about/bsi-process"  |
| ReleaseDate                   | DateTime        |             | | Date of release of the version, See [Date Time format](#datetime-format).  |
| Status      | Text                   |         |             | Possible version statuses: `Preview`, `Active`, `Inactive`. When uploading a new version, it should always be in `Preview`. You can then activate or deactivate content via [the API](https://app.swaggerhub.com/apis/buildingSMART/Dictionaries/v1) or [Management Portal](https://manage.bsdd.buildingsmart.org/). Read more: [the lifecycle of the bSDD content](https://github.com/buildingSMART/bSDD/blob/master/Documentation/bSDD%20import%20tutorial.md#the-lifecycle-of-the-bsdd-dictionary-version)  |
| UseOwnUri      | Boolean                   | ✅        |             | Use your own namespace uri for global unique identification of Classifications and Properties. If you don't use your own namespace URI a namespace URI starting with "https://identifier.buildingsmart.org" will be assigned to each `Classification` and `Property` |

\* For delivering data in additional languages it is sufficient to fill the `Domain` type fields, all `Code` fields and the fields marked with `Translatable?` = "Yes" of the other types. Make sure that the `OrganizationCode`, `DomainCode` and `DomainVersion` are exactly the same and if the data is for adding a language to an existing `Domain`, set field `LanguageOnly` to true.

## Classification

A `Classification` can be any (abstract) object (e.g. "IfcWall"), abstract concept
(e.g. "Costing") or process (e.g. "Installation"). 

| Field                     | DataType                       | Requ- ired? | Trans- latable? | Description                                                                                                        |
|---------------------------|--------------------------------|-----------|---------------|--------------------------------------------------------------------------------------------------------------------|
| ActivationDateUtc         | DateTime                           |         |             | See [Date Time format](#datetime-format). |
| ClassificationProperties  | List of ClassificationProperty |         |             | See section [ClassificationProperty](#classificationproperty) |
| ClassificationRelations   | List of ClassificationRelation |         |             | See section [ClassificationRelation](#classificationrelation) |
| ClassificationType        | Text                           | ✅*        |             | Must be one of: `Class` `GroupOfProperties` `AlternativeUse`. Read more about [classification types](#classification-types). If not specified, the `Class` type will be used by default. We've deprecated types `ReferenceDocument`, `ComposedProperty` and `Domain`, cannot use these anymore for upload but may be present in API results. |
| Code                      | Text                           | ✅       |             | Unique identification within the domain of the classification E.g. "ifc-00123-01"                                  |
| ReferenceCode             | Text                           |         |             | Reference code, can have domain specific usage. If null, then the value of `Code` is used to fill the field. To make `ReferenceCode` empty use empty string "".  |
| CountriesOfUse            | List of text                   |         |             | List of country ISO codes this `Classification` is being used. See reference list [countries](https://api.bsdd.buildingsmart.org//api/Country/v1).                                    |
| CountryOfOrigin           | Text                           |         |             | ISO Country Code of the country of origin of this `Classification`. See reference list [countries](https://api.bsdd.buildingsmart.org//api/Country/v1).                                         |
| CreatorLanguageIsoCode    | Text                           |         |             | Language ISO code of the creator. See reference list [languages](https://api.bsdd.buildingsmart.org/api/Language/v1). |
| DeActivationDateUtc       | DateTime                           |         |             | See [Date Time format](#datetime-format). |
| Definition                | Text                           |         | ✅           | Definition of the `Classification`|
| DeprecationExplanation    | Text                           |         | ✅           |  |
| DocumentReference         | Text                           |         |             | Reference to document with full or official definition of the `Classification`. See reference list [reference documents](https://api.bsdd.buildingsmart.org/api/ReferenceDocument/v1). |
| Name                      | Text                           | ✅       | ✅           | Name of the `Classification` E.g. "IfcCurtainWall"                                                                   |
| OwnedUri                | Text                           |         |            | If you specified `UseOwnUri = true` at domain level you must supply the namepsace URI that globally uniquely identifies the `Classifciation`  |
| ParentClassificationCode  | Text                           |         |             | Reference to the parent `Classification`. The ID in this field MUST exist in the data delivered. E.g. "ifc-00123-00". See section [How to define relations?](#how-to-define-relations) |
| RelatedIfcEntityNamesList | List of text                   |         |             | References to the IFC equivalent of this `Classification`. See bSDD API [ifc classifications](https://api.bsdd.buildingsmart.org/api/Domain/v3/Classifications?namespaceUri=https%3A%2F%2Fidentifier.buildingsmart.org%2Furi%2Fbuildingsmart%2Fifc%2F4.3%2F). See section [How to define relations?](#how-to-define-relations)                                      |
| ReplacedObjectCodes       | List of text                   |         |             | List of Classification Codes this Classification replaces                                                          |
| ReplacingObjectCodes      | List of text                   |         |             | List of Classification Codes this classification is replaced by                                                    |
| RevisionDateUtc           | DateTime                           |         |             | See [Date Time format](#datetime-format). |
| RevisionNumber            | Integer                        |         |             |  |
| Status                    | Text                           |         |             | Status of the `Classification`: `Active` (default) or `Inactive` |
| SubdivisionsOfUse         | List of text                   |         | ✅           | List of geographical regions of use E.g. "US-MT"  |
| Synonyms                  | List of text                   |         | ✅           | List of alternative names of this classification for easier finding.|
| Uid                  | Text                   |         |            | Unique identification (ID), in case the URI is not enough. |
| VersionDateUtc            | DateTime                           |         |             | By default takes the date of import. See [Date Time format](#datetime-format). |
| VersionNumber             | Integer                        |         |             |  |
| VisualRepresentationUri   | Text                           |         | ✅           |  |


## Material

A `Material` is similar to a `Classification`.
Differences in model are:
- no `ClassificationType` and `RelatedIfcEntityNamesList` fields
- `ParentMaterialCode` instead of `ParentClassificationCode`
- `MaterialProperties` instead of `ClassificationProperties`

A `Domain` can have both `Material`(s) and `Classification`(s). Please note that the "code" of both `Material`(s) and `Classification`(s) must be unique within the domain. You can't have a `Domain` with `Material` with code "abcd" and a `Classification` with the same code "abcd".

## Property

A `Classification` can have multiple properties and a `Property` can be part of many
classifications

| Field                         | DataType     | Requ- ired? | Trans- latable? | Description                                                                                                                                          |
|-------------------------------|--------------|-----------|---------------|------------------------------------------------------------------------------------------------------------------------------------------------------|
| ActivationDateUtc             | DateTime         |         |             | See [Date Time format](#datetime-format). |
| AllowedValues              | List of AllowedValue  |   | ✅           | List of allowed values for the property. Note: do not use this one for properties of type boolean. See section [AllowedValue](#allowedvalue). |
| Code                          | Text         | ✅       |             | Unique identification within the domain of the property E.g. "ifc-99088-01"                                                                          |
| ConnectedPropertyCodes        | List of text |         |             | List of codes of connected properties                                                                                                                |
| CountriesOfUse                | List of text         |         |             |  List of country ISO codes this `Property` is being used. See reference list [countries](https://api.bsdd.buildingsmart.org/api/Country/v1).                                                      |
| CountryOfOrigin               | Text         |         |             | ISO Country Code of the country of origin of this classification. See reference list.                                                                           |
| CreatorLanguageIsoCode        | Text         |         |             | Language ISO code of the creator. See reference list (json)[languages](https://api.bsdd.buildingsmart.org/api/Language/v1)  |
| DataType                      | Text         |         |             | The datatype the property is expressed in. Must be one of:  `Boolean`,  `Character`,  `Integer`,  `Real`,  `String`,  `Time`                       |
| DeActivationDateUtc           | DateTime         |         |             | See [Date Time format](#datetime-format). |
| Definition                    | Text         |         | ✅           | Definition of the `Property` |
| DeprecationExplanation        | Text         |         | ✅           |  |
| Description                   | Text         | ✅       | ✅           | |
| Dimension                     | Text         |         |             | In case of a physical quantity, specify dimension according to [International_System_of_Quantities](https://en.wikipedia.org/wiki/International_System_of_Quantities), as defined in ISO 80000-1. The order is: `length`, `mass`, `time`, `electric current`, `thermodynamic temperature`, `amount of substance`, and `luminous intensity`. For example speed (m/s) would be denoted as "1 0 -1 0 0 0 0". More examples in [IDS docs](https://github.com/buildingSMART/IDS/blob/master/Documentation/units.md) |
| DimensionLength               | Integer      |         |             | The Length dimension; either use the field `Dimension` to specifiy all parts, or specify all parts separately|
| DimensionMass               | Integer      |         |             | The Mass dimension; either use the field `Dimension` to specifiy all parts, or specify all parts separately|
| DimensionTime               | Integer      |         |             | The Time dimension; either use the field `Dimension` to specifiy all parts, or specify all parts separately|
| DimensionElectricCurrent               | Integer      |         |             | The ElectricCurrent dimension; either use the field `Dimension` to specifiy all parts, or specify all parts separately|
| DimensionThermodynamicTemperature               | Integer      |         |             | The ThermodynamicTemperature dimension; either use the field `Dimension` to specifiy all parts, or specify all parts separately|
| DimensionAmountOfSubstance               | Integer      |         |             | The AmountOfSubstance dimension; either use the field `Dimension` to specifiy all parts, or specify all parts separately|
| DimensionLuminousIntensity               | Integer      |         |             | The LuminousIntensity dimension; either use the field `Dimension` to specifiy all parts, or specify all parts separately|
| DocumentReference             | Text         |         |             | Reference to document with full or official definition of the `Property`. See reference list (json) [reference documents](https://api.bsdd.buildingsmart.org/api/ReferenceDocument/v1).                                                                               |
| DynamicParameterPropertyCodes | List of text |         |             | List of codes of properties which are parameters of the function for a dynamic property                                                              |
| Example                       | Text         |         | ✅           | Example of the `Property` |
| IsDynamic                     | Boolean      |         |             | Default: false If this is a dynamic property, the value is dependent on the parameters provided in field DynamicParameterProperties                  |
| MaxExclusive            | Real     |         |             | Maximum allowed value, exclusive - do not fill both 'inclusive' and 'exclusive' values |
| MaxInclusive            | Real     |         |             | Maximum allowed value, inclusive - do not fill both 'inclusive' and 'exclusive' values |
| MinExclusive            | Real     |         |             | Minimum allowed value, exclusive |
| MinInclusive            | Real     |         |             | Minimum allowed value, inclusive |
| MethodOfMeasurement           | Text         |         | ✅           | E.g. "Thermal transmittance according to ISO 10077-1"                                                                                                |
| Name                          | Text         | ✅       | ✅           | Name of the Property E.g. "IsExternal"                                                                                                               |
| OwnedUri                | Text                           |         |            | If you specified `UseOwnUri = true` at domain level you must supply the namepsace URI that globally uniquely identifies the Property  |
| Pattern            | Text     |         |             | An [XML Schema regular expression](https://www.regular-expressions.info/xml.html) to limit allowed values |
| PhysicalQuantity              | Text         |         | ✅           | Name of the physical quantity of the property E.g. "without" or "mass"                                                                               |
| PropertyValueKind             | Text         |         |             | Must be one of:  `Single` (one value, is default),  `Range` (two values),  `List` (multiple values), `Complex` (consists of multiple properties, use ConnectedProperties), `ComplexList` (list of complex values)                       |
| ReplacedObjectCodes           | List of text |         |             | List of Property Codes this `Property` replaces                                                                                            |
| ReplacingObjectCodes          | List of text |         |             | List of Property Codes this `Property` is replaced by                                                                                      |
| RevisionDateUtc               | DateTime         |         |             | See [Date Time format](#datetime-format). |
| RevisionNumber                | Integer      |         |      |  |
| Status                        | Text         |         |             | Status of the Property: `Active` (default) or `Inactive`    |
| SubdivisionsOfUse             | List of text         |         | ✅           | List of geographical regions of use E.g. "US-MT"                                                                                 |
| TextFormat                    | Text         |         |             | Pair for text type (encoding, number of characters) The encoding is set according to "Name of encoding standard" of IANA, RFC 2978 E.g. "(UTF-8,32)" |
| Uid                  | Text                   |         |            | Unique identification (ID), in case the URI is not enough. |
| Units                         | List of text |         |             | The units to represent a scale that enables a value to be measured (ISO 80000 or ISO 4217 or ISO 8601). List of values. See reference list (json) [units](https://api.bsdd.buildingsmart.org/api/Unit/v1).  We are working on supporting the [QUDT](http://www.qudt.org/) vocabulary. If you would like to import using QUDT units or want to have the QUDT units in the API output pls let us know. |
| VersionDateUtc                | DateTime         |         |             | By default takes the date of import. See [Date Time format](#datetime-format). |
| VersionNumber                 | Integer      |         |             |  |
| VisualRepresentationUri       | Text         |         | ✅           |  |
| PropertyRelations              | List of PropertyRelation  |   | ✅           | List of related properties. See section [PropertyRelation](#propertyrelation) |

## ClassificationProperty

| Field               | DataType | Requ- ired? | Trans- latable? | Description                                                                                                            |
|---------------------|----------|-----------|---------------|------------------------------------------------------------------------------------------------------------------------|
| AllowedValues              | List of AllowedValue  |   | ✅           | List of allowed values for the `ClassificationProperty`. Overrides the values defined for the `Property`. Do not use this one for properties of type boolean. See section [AllowedValue](#allowedvalue)  |
| Code                | Text     | ✅        |             | Unique identification within the domain of this `ClassificationProperty`                                                |
| Description         | Text     |         | ✅           | You can supply the property description specific for the classification. If left out, the 'common' description of the property will be shown where applicable |
| ~~ExternalPropertyUri~~ | ~~Text~~     |       |             | DEPRECATED - Use `PropertyNamespaceUri` instead                |
| IsRequired              | Boolean  |   |            | Indicates if this is a required `Property` of the `Classification` |
| IsWritable              | Boolean  |   |            | Indicates if the value of this `Property` of the `Classification` can be changed |
| MaxExclusive            | Real     |         |             | Maximum allowed value, exclusive. Overrides the value defined for the `Property`. Do not fill both 'inclusive' and 'exclusive' values |
| MaxInclusive            | Real     |         |             | Maximum allowed value, inclusive. Overrides the value defined for the `Property`. Do not fill both 'inclusive' and 'exclusive' values. |
| MinExclusive            | Real     |         |             | Minimum allowed value, exclusive. Overrides the value defined for the `Property` |
| MinInclusive            | Real     |         |             | Minimum allowed value, inclusive. Overrides the value defined for the `Property` |
| Pattern            | Text     |         |             | An [XML Schema regular expression](https://www.regular-expressions.info/xml.html) to limit allowed values. Overrides the pattern defined for the Property |
| PredefinedValue     | Text     |         |             | Predefined value for this `Property`. E.g. value for property "IsLoadBearing" can be "true" for classification "IfcWall" |
| PropertyCode        | Text     |  ✅\*     |             | Reference to the `Property` if it is in the same `Domain`. Not required if you fill the PropertyNamespaceUri  |
| PropertyNamespaceUri        | Text     |  ✅\*     |             | Reference to the `Property` if it is in a different `Domain`, e.g. [https://identifier.buildingsmart.org/uri/buildingsmart/ifc/4.3/prop/ClearWidth](https://identifier.buildingsmart.org/uri/buildingsmart/ifc/4.3/prop/ClearWidth) Not required if you fill the PropertyCode       |
| PropertySet         | Text     |         |             | Code validation will be applied.<br/> Name of the "property set" in which the property should be placed during IFC export. When the property should be placed in an IFC entity you should use that. For example, when a property is a material, you should use the value IfcMaterial". |
| PropertyType        | Text     |         |             | Type of the `Property` for the `Classification`: `Property` (default) or `Dependency`                                      |
| SortNumber          | Integer  |         |             | Sort number of this `Property` within the `Classification`                                                                 |
| Symbol              | Text     |         |             |                                                                                                                        |
| Unit                | Text     |         |             | See reference list (json) [units](https://api.bsdd.buildingsmart.org/api/Unit/v1).                                                                                                                       |


\* One of those is required.

## ClassificationRelation

`Classification`s and `Material`s can be linked by relations. See section [How to define relations?](#how-to-define-relations)

| Field                    | DataType | Requ- ired? | Trans- latable? | Description                                                                 |
|--------------------------|----------|-----------|---------------|-----------------------------------------------------------------------------|
| RelatedClassificationUri | Text     | ✅       |             | Full namespace URI of the related `Classification`. Can be to same or different `Domain`. Example: https://identifier.buildingsmart.org/uri/etim/etim/8.0/class/EC002987|
| RelatedClassificationName | Text     |        |             |  |
| RelationType             | Text     | ✅       |             | One of:  `HasMaterial`, `HasReference`,  `IsEqualTo`,  `IsSynonymOf`,  `IsParentOf`,  `IsChildOf`, `HasPart`    |
| Fraction       | Real     |        |             | Optional provision of a fraction of the total amount (e.g. volume or weight) that applies to the Classification owning the relations. The sum of Fractions per classification/relationtype must be 1. Similar to Fraction in [IfcMaterialConstituent](http://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/lexical/IfcMaterialConstituent.htm)|


## AllowedValue

Note: adding translations of the `AllowedValue` is not supported yet

| Field                    | DataType | Requ- ired? | Trans- latable? | Description                                                                 |
|--------------------------|----------|-----------|---------------|-----------------------------------------------------------------------------|
| Code             | Text     | ✅       |             | Code as unique identification of the value (max 20 characters). If you want to add translations of Values or their Descriptions, you must supply a Code for each Value    |
| Description | Text     |        | ✅       | A description of the value|
| NamespaceUri| Text |  |  | You can provide your own Namespace Uri (must be globally unique).|
| SortNumber | Integer     |        |             | SortNumber of the Value in the list of Values of the `Property` it belongs to|
| Value | Text     | ✅       | ✅       | One of the Values the property can have, e.g. "Green" in case the Property is something like "Color"|


## PropertyRelation

| Field                    | DataType | Required? | Translatable? | Description                                                                 |
|--------------------------|----------|-----------|---------------|-----------------------------------------------------------------------------|
| RelatedPropertyName | Text     |        |             | Name of the related `Property`.|
| RelatedPropertyUri | Text     | ✅       |             | Full namespace URI of the related `Property`. Can be to same or different `Domain`.|
| RelationType             | Text     | ✅       |             | One of:  `HasReference`,  `IsEqualTo`,  `IsSynonymOf`,  `IsParentOf`,  `IsChildOf`, `HasPart`    |

---

# Additional explanations

### Classification types

Each classification must have a specific type. Below is the explanation of what each type means, according to the ISO 12006-3<sup>1</sup>:
* `class` - description of a set of objects that share the same characteristics <sup>1<sup>3.7</sup></sup>. This is the most common type in bSDD.
* `group of properties` - collection enabling the properties to be prearranged or organized.<sup>1<sup>3.14</sup></sup>
  * A Property Set as defined in ISO 16739-1 is a group of properties, but a group of properties is not necessarily a Property Set.
  * There are five categories of possible groups of properties: class, domain, reference document, composed property, alternative use.
  * A property can be member of several groups of properties. A property cannot be member of several Property Sets as defined in ISO 16739-1.
* `reference document` - publication that is consulted to find specific information, particularly in a technical or scientific domain.<sup>1<sup>3.18</sup></sup>
  * A reference document can be associated with any data present in a data dictionary.
  * In bSDD we also have a [reference documents](https://api.bsdd.buildingsmart.org/api/ReferenceDocument/v1) list with most common standards that can be used as reference. 
* `composed property` - category of group of properties corresponding to a feature needing multiple properties to be defined.<sup>1<sup>3.8</sup></sup>
  * Using this category of group of properties requires to fill all the properties part of the composed property. There is no value attached to the group of properties. 
  * Example: To describe the characteristic "concrete facing quality" it is mandatory to describe 3 properties: concrete planarity, concrete hue, concrete texture.	
* `alternative use` - type to be used if no other type fits the needs.<sup>1<sup>3.1</sup></sup>


### How to define relations?

`ParentClassificationCode` - `Classification`s and `Material`s within the same domain can be organized in a tree like hierarchy structure. For example: “IfcCurtainWall” is a more
specific classification of “IfcWall”. In bSDD terminology, we say that “IfcWall” is a **parent of** “IfcCurtainWall”. To define such specialization relation, use the `ParentClassificationCode` attribute on the child object.

`ClassificationRelation` and `PropertyRelation`- use those to link your concepts with each other. Relations allow to define parent-child links also with other domains. Apart from specialization, you can also define other types of relations, such as decomposition (`HasPart` type, see the list of possible types: [Relation types](#relation-types)).

`RelatedIfcEntityNamesList` - IFC is a top-level schema (foundation classes) used for exchanging information between software. Because of that, the bSDD provides a special way to relate your classification to IFC. Use `RelatedIfcEntityNamesList` to show which entities from IFC you are refering to or extending. For example, “Signaling LED diode” relates to “IfcLamp” from IFC. `RelatedIfcEntityNamesList` can be used by bSDD-related tools to filter the list of possible classifications to a particular IFC category.

### Relation types

`Properties` and `Classifications` must have a specific type. Below is the explanation of what each type means:
* `IsEqualTo` - if two concepts are unequivocal and have the same name
* `IsSynonymOf` - if two concepts are unequivocal but have a different name
* `IsChildOf` - equivalent of "subtype" relationship from ISO 12006<sup>1<sup>F.3.1</sup></sup>. For example: "Electrical motor" and a "Combustion motor" are children (subtypes) of a generic concept "Motor". 
* `IsParentOf` - the opposite relation to `IsChildOf`.
* `HasPart` - for example, an electric motor can be composed of elements such as stators, rotors, etc.<sup>1<sup>F.3.2</sup></sup>.
* `HasMaterial` - a class can be associated with particular material. For example: "Steel Beam" could be related to material "Steel". This type is only available for `Classes`, not `Properties`.
* `HasReference` - if there is another type of relation between concepts, for example "wall light" (or "sconce") is referencing a wall, even though those are different concepts and there is no hierarchy between them. 

### DateTime format

The date-time format according to the ISO 8601 series should be used: `YYYY-MM-DDThh:mm:ssTZD`. Import allows both: `2023-05-10`, `2023-05-10T15:10:12Z` and `2023-05-10T15:10:12+02:00`.

### Property inheritance

* Parent `Class` → child `Class`  
The child `Class` does not inherit properties from the parent `Class`. If authors want child classes to also have properties of parent classes, they should specify them intentionally in import files.  
For example, the [IfcWall](https://search.bsdd.buildingsmart.org/uri/buildingsmart/ifc/4.3/class/IfcWall) is a parent class of [IfcWallStandardCase](https://search.bsdd.buildingsmart.org/uri/buildingsmart/ifc/4.3/class/IfcWallStandardCase). While [IfcWall](https://search.bsdd.buildingsmart.org/uri/buildingsmart/ifc/4.3/class/IfcWall) has the property [AcousticRating](https://search.bsdd.buildingsmart.org/uri/buildingsmart/ifc/4.3/class/IfcWall/prop/Pset_WallCommon/AcousticRating), the [IfcWallStandardCase](https://search.bsdd.buildingsmart.org/uri/buildingsmart/ifc/4.3/class/IfcWallStandardCase) doesn't.

* `Property` → `ClassificationProperty`  
`ClassificationProperty` is an instantiation of general `Property` for a particular `Class`. The attributes of a property, such as `AllowedValue` and min/max restrictions,  are by default passed to `ClassificationProperty`. The values of the `ClassificationProperty` can be modified without influencing the origin `Property`.  
For example, the [Height](https://search.bsdd.buildingsmart.org/uri/bs-agri/fruitvegs/1.0.0/prop/height) has an upper limit of 100 cm. When applied to the "Apple" class, the [Apple-Height](https://search.bsdd.buildingsmart.org/uri/bs-agri/fruitvegs/1.0.0/class/apple/prop/SizeSet/height) has a lower limit - 25cm. 

### 🚧 How to group properties?

`GroupOfProperties`...

`PropertySet`...

`ConnectedPropertyCodes`...

`ComposedProperty`...

### 🚧 How to restrict property values?
`AllowedValues`...

`Min/MaxInc/Exclusive`...

`Pattern`...

### 🚧 How are bSDD resources identified?  
`URI`...

`UID`(GUID)...

### 🚧 How to specify units?
`Unit(s)`...

`Dimension`...

`PhysicalQuantity`...

### 🚧 DynamicProperty vs ComposedProperty
`DynamicProperty`...

`ComposedProperty`...

--- 
<sup>[1] ISO 12006-3:2022 "Building construction — Organization of information about construction works — Part 3: Framework for object-oriented information"</sup>

# Notifications


**2023-07 - Important notification:**

> As we're continuously improving bSDD we've updated all identifiers: the dash between domain code and domain version has been replaced by a dash, e.g.:
>  https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs-1.0.0/class/apple will now be https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/1.0.0/class/apple
> 
> We will support supplying and retrieving data using the dash between domain code and version for (at least) 4 months. But please do note that only identifiers in the new format are returned by the bSDD API's.


**2022-08 - Important notification:**

> The bSDD is in the process of moving from identifiers (aka "namespace URI") starting with "http://identifier.buildingsmart.org" to "https://identifier.buildingsmart.org" ("http" to "https"). This is to ease the use of these identifiers as hyperlinks as well.
> 
> Support for using the old "http" identifiers will be deprecated soon!
