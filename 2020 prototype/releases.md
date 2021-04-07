# bSDD API Releases

Full details of the contracts returned by the API's can be found on [bSDD swagger]https://bs-dd-api-prototype.azurewebsites.net/swagger/index.html|
If you need a more thorough explanation of the returned attributes, you can find most of them in Table 1 and Table 2 of the ISO 23386:2020(E) specifications.

## Classification v3
2021-04

|Attribute||v2|v3||
|   ClassificationProperties |||Changes within the ClassificationProperties object of the Classification API result|
||isDynamic|n/a|nullable bool|Indicates if it is a Dynamic property|
||dynamicParameterPropertyCodes|n/a|List<string>|List of property codes of properties that are a parameter of the dynamic function (only applicable if isDynamic is true)|
||methodOfMeasurement|string|<removed>  ** breaking change with v2|The Classification API returns the most direct used attributes. Attributes that are more related to 'get help' can be retrieved via the Property API.|
||propertyCode|n/a|string|Unique identification, within the domain, of the property|
||symbol|n/a|string|Symbol of the property|
||units|List<string>|List<string>|Fix: now returns list of Units as specified at property level if it is not overridden at  classprop level|
||isRequired|n/a|nullable bool|Indicates if the value of the property must be filled|
||isWritable|n/a|nullable bool|Indicates if the value of the property can be changed|
||values|List<ValueType>| <removed> as <replaced by possibleValues>** breaking change with v2||
||possibleValues|n/a|List<ClassificationPropertyValueContract>|List of possible values for the property. For details of the ClassificationPropertyValueContract contract see [bSDD swagger](https://bs-dd-api-prototype.azurewebsites.net/swagger/index.html)|
||propertyStatus|n/a|string|

## Property v2
2021-04

|Attribute|v1|v2||
|replacedObjectCodes|string (semicolon separated list of codes)|List<string> ** breaking change||
|replacingObjectCodes|string (semicolon separated list of codes)|List<string> ** breaking change||
|countriesOfUse|string (semicolon separated list of codes)|List<string> ** breaking change||
|subdivisionsOfUse|string (semicolon separated list of codes)|List<string> ** breaking change||
|units|string (semicolon separated list of codes)|List<string> ** breaking change||
|dynamicParameterPropertyCodes|n/a|List<string>|List of property codes of properties that are a parameter of the dynamic function (only applicable if isDynamic is true)|
|connectedProperties|string (semicolon separated list of codes)|<removed> as <replaced by connectedPropertyCodes>||
|connectedPropertyCodes|n/a|List<string>||
|possibleValuesList|List<string>|<replaced by possibleValues>  ** breaking change||
|possibleValues|n/a|List<PropertyValueContract>|For details of the PropertyValueContract contract see [bSDD swagger](https://bs-dd-api-prototype.azurewebsites.net/swagger/index.html)|
|dimensionAmountOfSubstance|n/a|integer|The Amount of substance value of the Dimension attribute|
|dimensionElectricCurrent|n/a|integer|The Electric current value of the Dimension attribute|
|dimensionLength|n/a|integer|The Length value of the Dimension attribute|
|dimensionLuminousIntensity|n/a|integer|The Luminous intensity value of the Dimension attribute|
|dimensionMass|n/a|integer|The Mass value of the Dimension attribute|
|dimensionThermodynamicTemperature|n/a|integer|The Thermodynamic temperature value of the Dimension attribute|
|dimensionTime|n/a|integer|The Time value of the Dimension attribute|
|propertyRelations|n/a|List<PropertyRelationContract>||

## UploadImportFile
2021-04

As there are no breaking changes, there's no new version number

Added:
|New|PropertyRelations attribute|Property relations can now be imported. See [bSDD import model](https://github.com/buildingSMART/bSDD/blob/master/2020%20prototype/import-model/bSDD%20JSON%20import%20model.md) for more details|
|New|AllowedValues attribute|Import of Property values is extended, use the new "AllowedValues" attribute. The "PossibleValues" attribute is still supported but you're encouraged to use the new "AllowedValues" attribute. See [bSDD import model](https://github.com/buildingSMART/bSDD/blob/master/2020%20prototype/import-model/bSDD%20JSON%20import%20model.md) for more details|
