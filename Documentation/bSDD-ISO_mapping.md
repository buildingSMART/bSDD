# Mapping of the attributes between bSDD and ISO standards

**⚠️ THIS PAGE IS A WORK IN PROGRESS AND SHOULD NOT BE USED FOR REFERENCE ⚠️**

The bSDD is based on ISO12006-3 and ISO23386 standards, which define data dictionaries. For ease of integration with openBIM workflows, the bSDD is stripped down to the essential aspects: defining interrelated terms and properties describing the built environment. The bSDD constraints include the imposed units list, languages list, and types of relations between concepts (ISO leaves freedom of definition to users, hindering interpretation by software). The inheritance structure of ISO12006-3 (Root→Object→Concept→Subject/Property) is simplified in bSDD to one level: Class and Property. 

The ISO allows the versioning of each concept individually, which is important for the development of a data dictionary. In bSDD, to support contractual agreements, each change results in a new version of the complete dictionary. This does not apply if a dictionary is not activated. Read more about [the content lifecycle in bSDD](https://github.com/buildingSMART/bSDD/blob/doc_update/Documentation/bSDD%20import%20tutorial.md).

Below is the table mapping the attributes of bSDD and ISO standards. bSDD attributes are defined in [the bSDD data model](https://github.com/buildingSMART/bSDD/blob/doc_update/Documentation/bSDD%20JSON%20import%20model.md). 

| **bSDD** | **ISO23386:2020** | **ISO12006-3:2022** | **Comment** |
|---|---|---|---|
| Property/Class: Uid,   NamespaceUri | Property/GroupOfProperties: Globally unique identifier | xtdRoot: UniqueId | _(G)UID is optional in bSDD, required in ISO. In bSDD role of   UID was replaced with URI, and UID is only to support use cases needing it.   URI allows viewing the metadata of a property._ |
| Property/Class/Dictionary:   Status | Property/GroupOfProperties: Status | ✖️ | _bSDD and ISO have “Active” and “Inactive”. bSDD also has   “Preview”, so extends ISO._ |
| ✖️ | Property/GroupOfProperties: Date of creation | xtdObject: DateOfCreation | _Not directly in bSDD, but could be derived as date of the   first version upload._ |
| Property/Class:   ActivationDateUtc | Property/GroupOfProperties: Date of activation | ✖️ |  |
| ✖️ | Property/GroupOfProperties: Date of last change | ✖️ | _Not directly in bSDD, but could be derived as date of the last   version where a property/class changed._ |
| Property/Class:   RevisionDateUtc | Property/GroupOfProperties: Date of revision | ✖️ |  |
| Property/Class: VersionDateUtc | Property/GroupOfProperties: Date of version | ✖️ |  |
| Property/Class:   DeActivationDateUtc | Property/GroupOfProperties: Date of deactivation | ✖️ |  |
| Property/Class: VersionNumber | Property/GroupOfProperties: Version number | xtdObject: MajorVersion | _Version number in ISO23386 is what Major version in ISO12006-3 (similarly Revision number is MinorVersion). In bSDD, the attributes are named like in ISO23386, but the version already includes three numbers: 1.2.3 - Major, Minor and Patch (read more on Semantic Versioning at https://semver.org/)._ |
| Property/Class: RevisionNumber | Property/GroupOfProperties: Revision number | xtdObject: MinorVersion | _See row above. The revision number is redundant in bSDD, but can be used to say how many revision of a certain field has been made._ |
| Property/Class:   ReplacedObjectCodes | Property/GroupOfProperties: List of replaced properties | xtdObject: ReplacedObjects |  |
| Property/Class:   ReplacingObjectCodes | Property/GroupOfProperties: List of replacing properties | ✖️ |  |
| Property/Class:   DeprecationExplanation | Property/GroupOfProperties: Deprecation explanation | xtdObject: DeprecationExplanation |  |
| Property/Class:   CreatorLanguageIsoCode | Property/GroupOfProperties: Creator’s language | xtdConcept: LanguageOfCreator  | _In ISO an xtdLanguage object with: EnglishName (acc. ISO 639   series), NativeName, Comments, Code      In bSDD a bSI managed list with: IsoCode, Name   (https://api.bsdd.buildingsmart.org/api/Language/v1 )_ |
| Property/Class: Name | Property/GroupOfProperties: Names in language N | xtdObject: Names |  |
| Property/Class: Definition | Property/GroupOfProperties: Definitions in language N | xtdConcept: Definition  |  |
| Property: Description | Property: Descriptions in language N | xtdConcept: Descriptions  |  |
| Property: Example | Property: Examples in language N | xtdConcept: Examples |  |
| Property:   ConnectedPropertyCodes | Property: Connected properties | ✖️ |  |
| (schema/API) | Property: Group(s) of properties | ✖️ | _In bSDD Class Property can be within Group of Property (type   of Class)._ |
| Property/Class:   VisualRepresentationUri | Property/GroupOfProperties: Visual representation | xtdConcept: VisualRepresentations | _In ISO it’s a Media object, in bSDD only link to external   visual representation allowed._ |
| Property/Class: CountriesOfUse | Property/GroupOfProperties: Country of use | ✖️ | _In bSDD a predefined list governed by bSI._ |
| Property/Class:   SubdivisionsOfUse | Property/GroupOfProperties: Subdivision of use | ✖️ |  |
| Property/Class:   CountryOfOrigin | Property/GroupOfProperties: Country of origin | xtdConcept: CountryOfOrigin  | _In bSDD a predefined list governed by bSI._ |
| Property: PhysicalQuantity | Property: Physical quantity | xtdProperty: QuantityKinds |  |
| Property: Dimension | Property: Dimension | xtdProperty: Dimension |  |
| Property: MethodOfMeasurement | Property: Method of measurement | ✖️ |  |
| Property: DataType | Property: Data type | xtdProperty: DataType  | _The same, but bSDD missing: XTD_RATIONAL and XTD_COMPLEX for   simplicity of implementation._ |
| Property: IsDynamic | Property: Dynamic Property | ✖️ |  |
| Property:   DynamicParameterPropertyCodes | Property: Parameters of the dynamic property | ✖️ |  |
| Property: Units | Property: Units | xtdProperty: Units  | _In ISO has: Dimension, Symbol, Coefficient, Scale, Base,   Offset      In bSDD a bS managed list: https://api.bsdd.buildingsmart.org/api/Unit/v1,   with Code and Name._ |
| ✖️ | Property: Names of the defining values | ✖️ | _Defining values in ISO are extending the list with any custom   attributes. In bSDD that would limit the interoperability._ |
| ✖️ | Property: Defining values | ✖️ | _Defining values in ISO are extending the list with any custom   attributes. In bSDD that would limit the interoperability._ |
| ✖️ | Property: Tolerance | ✖️ | _ISO23386: For numerical values; the total amont that a   specific unit is permitted to vary; it is the difference between the maxium   and the minimum limits per unit._ |
| ✖️ | Property: Digital format | ✖️ | _In ISO23386 it is a pair of precision and unit for digital   text type. Not to be confused with DataFormat pattern._ |
| Property: TextFormat | Property: Text format | ✖️ |  |
| Property: AllowedValues | Property: List of possible values in language N | xtdProperty: PossibleValues  | _ISO: ‘the description of a value of an xtdProperty’. ISO has   ‘NominalValue’, while bSDD has Description, Value, SortNumber, NamespaceUri,   Code._ |
| Property: MaxExclusive,   MaxInclusive, MinExclusive, MinInclusive | Property: Boundary values | xtdProperty: BoundaryValues | _In ISO xtdInterval object which contains: Minimum,   MinimumIncluded, Maximum, MaximumIncluded.       In bSDD separate attributes to do the same:       MinExclusive, MinInclusive, MaxExclusive, MaxInclusive._ |
| PropertyRelation:   (RelationType == IsSynonymOf) | ✖️ | xtdConcept: SimilarTo | _In bSDD solved with relations of type "IsSynonymOf"_ |
| (schema/API) | ✖️ | xtdObject: Dictionary | _In bSDD property is located in a certain dictionary._ |
| Property/Class:   DocumentReference | ✖️ | xtdConcept: ReferenceDocuments | _In ISO xtdExternalDocument, but in bSDD a string from bSI   managed list: https://api.bsdd.buildingsmart.org/api/ReferenceDocument/v1_ |
| Property/Class: Code | ✖️ | ✖️ | _Code is used to generate URI and can be used for   identification within a dictionary._ |
| Property: DimensionLength | ✖️ | ✖️ |  |
| Property: DimensionMass | ✖️ | ✖️ |  |
| Property: DimensionTime | ✖️ | ✖️ |  |
| Property:   DimensionElectricCurrent | ✖️ | ✖️ |  |
| Property:   DimensionThermodynamicTemperature | ✖️ | ✖️ |  |
| Property:   DimensionAmountOfSubstance | ✖️ | ✖️ |  |
| Property:   DimensionLuminousIntensity | ✖️ | ✖️ |  |
| Property/Class: OwnedUri | ✖️ | ✖️ |  |
| Property: Pattern | ✖️ | xtdProperty: DataFormat  | _Pattern for the property values, the meaning of the pattern is   implementation dependant_ |
| Property: PropertyValueKind | ✖️ | ✖️ | _In bSDD: Single/Range/List/Complex/ComplexList_ |
| Property: PropertyRelations | Property: Relation of the property identifiers in the   interconnected data dictionaries | ✖️ |  |
| ClassProperty: IsRequired | ✖️ | ✖️ |  |
| ClassProperty: IsWritable | ✖️ | ✖️ |  |
| ClassProperty: PredefinedValue | ✖️ | ✖️ |  |
| ClassProperty: PropertyCode | ✖️ | ✖️ |  |
| ClassProperty:   PropertyNamespaceUri | ✖️ | ✖️ |  |
| ClassProperty: PropertySet | ✖️ | ✖️ |  |
| ClassProperty: PropertyType | ✖️ | ✖️ | _In bSDD: Property/Dependency_ |
| ClassProperty: SortNumber | ✖️ | ✖️ | _ISO has xtdOrderedValue object: “to connect a value with its   order in a list of predefined values.”. In bSDD AllowedValues have this   optional order attribute._ |
| ClassProperty: Symbol | Property: Symbols of the property in a given property group | xtdProperty: Symbols  | _ISO has: Symbol, Subject      In bSDD: text attribute._ |
| ClassProperty: Unit | ✖️ | ✖️ | _Singular for ClassProp, while plural for Property. See 'Units'   for explanation._ |
| ClassRelation | GroupOfProperties: Relation of the group of properties   identifiers in the interconnected data dictionaries | ✖️ | _In bSDD solved with relations._ |
| Class: ClassificationType | GroupOfProperties: Category of group of properties | ✖️ | _See ISO23386 Category of GroupOfProperties vs bSDD ClassType_ |
| Class:   ParentClassificationCode | GroupOfProperties: Parent group of properties | ✖️ |  |
| Class:   ClassificationProperties | ✖️ | xtdSubject: Properties |  |
| Class: ClassificationRelations | ✖️ | xtdSubject: ConnectedSubjects  |  |
| Class: ReferenceCode | ✖️ | ✖️ |  |
| Class:   RelatedIfcEntityNamesList | ✖️ | ✖️ | _To support the use case of extending IFC schema with custom   classes._ |
| Class: Synonyms | ✖️ | ✖️ | _There are two ways to define synonyms in bSDD: with that   attribute or with relations of type "IsSynonymOf"._ |
| ✖️ | ✖️ | xtdSubject: Filters | _In bSDD relations serve similar purpose, but the concept of   Filter is not implemented for simplicity of use._ |
| ClassRelation:   RelatedClassificationUri | ✖️ | ✖️ |  |
| ClassRelation:   RelatedClassificationName | ✖️ | ✖️ |  |
| ClassRelation: RelationType | ✖️ | ✖️ | _See xtdConcept/SimilarTo in ISO12006-3_ |
| ClassRelation: Fraction | ✖️ | ✖️ | _ Optional provision of a   fraction of the total amount (e.g. volume or weight) that applies to the   Classification owning the relations. The sum of Fractions per   classification/relationtype must be 1._ |
| PropertyRelation:   RelatedPropertyName | ✖️ | ✖️ |  |
| PropertyRelation:   RelatedPropertyUri | ✖️ | ✖️ |  |
| PropertyRelation: RelationType | ✖️ | ✖️ |  |
| AllowedValue: Code | ✖️ | ✖️ | _Code is used to generate URI and can be used for   identification within a dictionary._ |
| AllowedValue: Description | ✖️ | ✖️ |  |
| AllowedValue: NamespaceUri | ✖️ | ✖️ |  |
| AllowedValue: SortNumber | ✖️ | ✖️ |  |
| AllowedValue: Value | ✖️ | ✖️ |  |
| Dictionary: DictionaryCode | ✖️ | ✖️ | _Code is used to generate URI and can be used for   identification within an organisation._ |
| Dictionary: DictionaryName | ✖️ | ✖️ |  |
| Dictionary:   DictionaryNamespaceUri | ✖️ | ✖️ |  |
| Dictionary: DictionaryVersion | ✖️ | ✖️ |  |
| Dictionary: LanguageIsoCode | ✖️ | ✖️ |  |
| Dictionary: License | ✖️ | ✖️ |  |
| Dictionary: LicenseUrl | ✖️ | ✖️ |  |
| Dictionary: MoreInfoUrl | ✖️ | ✖️ |  |
| Dictionary: OrganizationCode | ✖️ | ✖️ |  |
| Dictionary:   QualityAssuranceProcedure | ✖️ | ✖️ |  |
| Dictionary:   QualityAssuranceProcedureUrl | ✖️ | ✖️ |  |
| Dictionary: ReleaseDate | ✖️ | ✖️ | _Date of the current version._ |

## Units
TBD...

## ISO23386 Category of GroupOfProperties vs bSDD ClassType
TBD...
