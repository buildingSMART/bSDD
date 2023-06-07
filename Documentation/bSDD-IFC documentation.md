# Referencing bSDD in IFC and IDS

## Intro
To associate a classification from an external reference (such as bSDD) to objects in an IFC model, the following documentation shall be used.

The main IFC concept template to be used is: [Classification Association](http://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/concepts/Object_Association/Classification_Association/content.html)

The main entities involved are:
- _IfcClassification_
- _IfcClassificationReference_
- _IfcRelAssociatesClassification_

The next section indicates the mapping rules between bSDD data model and IFC concepts used for classification. To support this, snippets (:pill:) of IFC file and bSDD domain file are reported as example.

- :page_facing_up: Sample IFC snippets are taken from this [IFC file](https:// "title") [:construction:]
- :blue_book: Sample bSDD snippets are taken from this [bSDD domain](https:// "title") [:construction:]


## openBIM workflows
The buildingSMART Data Dictionary is a key component in many openBIM workflows. Among other things, it allows to:
- access all kinds of standards to **enrich an openBIM model**
- check data for compliance
- complement an Information Delivery Specifications (IDS)
- extend the IFC standard
- and much more

**The following documentation focuses on the [Enrich IFC data](https://miro.com/app/board/uXjVOtDnAk8=/?moveToWidget=3458764527377012946&cot=14) workflow**

:warning: **WARNING** :warning:

Adding information to a model that has been created by others can result in having conflicting information.
bSDD shall be used to add information, not changing existing one.
Management of conflicting information shall be handled at the project/organisation level.

> *For example, a project guideline could say:*
> - *If an instance of IfcMaterial already exist in the IFC file, for a certain object. Then the addition of other materials to the same object, using bSDD or other external references, is allowed (/not allowed).*
> - *If allowed, a certain naming convention shall be follow - to identify the source of (potentially conflicting) information.*
> - *Editing of existing IfcMaterial instances is not allowed.*

## bSDD - IFC mapping

Mapping rules are defined for the following concepts:

1. [bSDD domain](#1.-bSDD-domain)
2. [bSDD classifications (objects)](#2.-bSDD-classifications-(objects))
3. [bSDD properties](#3.-bSDD-properties)
4. [bSDD materials](#4.-bSDD-materials)

---

### 1. bSDD domain
**In bSDD**, a domain (a.k.a., classification system) is a standardised collection of object definitions, properties, materials, owned and maintained by one organisation. One organisation can own one or more domains.

**In IFC**, domain information are captured using _IfcClassification_. Below are the mapping rules, for different IFC versions.

|                    | bSDD                      | IFC 4.3                      | IFC 4                      | IFC 2x3                    | IDS   |
|--------------------|------------------------------|---------------------------------|-------------------------------|-------------------------------|---------|
| **Domain name**    | DomainName                   | IfcClassification.Name          | IfcClassification.Name        | IfcClassification.Name        |❎*    |
| **Domain source**  | *namespaceUri of the domain* | IfcClassification.Specification | IfcClassification.Location    | ❌ (IfcClassification.Source can be used as a workaround)   |uri      |
| **Domain version** | DomainVersion                | IfcClassification.Edition       | IfcClassification.Edition     | IfcClassification.Edition     |uri**      |
| **Domain owner**   | OrganizationCode             | IfcClassification.Source        | IfcClassification.Source      | IfcClassification.Source      |uri**      |
| **Domain date**    | ReleaseDate                  | IfcClassification.EditionDate   | IfcClassification.EditionDate | IfcClassification.EditionDate |❎*      |

> \* IDS references bSDD using URI, instead of copying its content. Thanks to that, the information is still accessible by following the URI. 
> 
> \*\* The IDS doesn't support a direct reference to the bSDD domains, but whenever a classification, property or material is referenced by "uri" attribute, those include information about their domains: uri="`http://identifier.buildingsmart.org/uri`/`OrganizationCode`/`DomainCode`-`DomainVersion`/..."

:pill: **Snippets**
<details><summary>bSDD</summary>
    
```
{
    "OrganizationCode": "text",
    "DomainCode": "text",
    "DomainVersion": "text",
    "DomainName": "text",
    "ReleaseDate": null,
    "Status": "text",
    "MoreInfoUrl": "text",
    "UseOwnUri": false,
    "DomainNamespaceUri": "text",
    "LanguageIsoCode": "text",
    "LanguageOnly": false,
    "License": "text",
    "LicenseUrl": "text",
    "QualityAssuranceProcedure": "text",
    "QualityAssuranceProcedureUrl": "text",
    "Classifications": [], 
    "Properties": [],
    "Materials": []
  }
```
</details>

<details><summary>IFC 4x3 <a href="https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/lexical/IfcClassification.htm">(IfcClassification)</a></summary>

```    
    IFCCLASSIFICATION(<Source>,<Edition>,<EditionDate>,<Name>,<Description>,<Specification>,<ReferenceTokens>);

    Example:
    #45 = IFCCLASSIFICATION('buildingSMART','4.3','2023-08-27','IFC','Industry Foundation Classes','https://identifier.buildingsmart.org/uri/buildingsmart/ifc-4.3',['.']);
```
</details>

<details><summary>IFC 4 <a href="https://standards.buildingsmart.org/IFC/RELEASE/IFC4/ADD2_TC1/HTML/">(IfcClassification)</a></summary>

```    
    IFCCLASSIFICATION(<Source>,<Edition>,<EditionDate>,<Description>,<Location>,<ReferenceTokens>);

    Example:
    #45 = IFCCLASSIFICATION('buildingSMART','4.3','2023-08-27','Industry Foundation Classes','https://identifier.buildingsmart.org/uri/buildingsmart/ifc-4.3',['.']);
```
</details>

<details><summary>IFC 2x3 <a href="https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcexternalreferenceresource/lexical/ifcclassification.htm">(IfcClassification)</a></summary>

```    
    IFCCLASSIFICATION(<Source>,<Edition>,<EditionDate>,<Name>);

    Example:
    #45 = IFCCLASSIFICATION('https://identifier.buildingsmart.org/uri/buildingsmart/ifc-2.3', '2.3', '2023-08-27','Industry Foundation Classes');
```
</details>
    

---

### 2. bSDD classifications (objects)
**In bSDD**, a classification can be any (abstract) object (e.g. _IfcWall_), abstract concept (e.g. _Costing_) or process (e.g. _Installation”_.

**In IFC**, classification information are captured using _IfcClassificationReference_. Below are the mapping rules, for different IFC versions.

|                           | bSDD                              | IFC 4.3                                | IFC 4                                  | IFC 2x3                               |IDS   |
|---------------------------|--------------------------------------|-------------------------------------------|-------------------------------------------|------------------------------------------|-------|
| **Classification name**   | name *of the classification*           | IfcClassificationReference.Name           | IfcClassificationReference.Name           | IfcClassificationReference.Name          |❎*      |
| **Classification code**   | code *of the classification*           | IfcClassificationReference.Identification | IfcClassificationReference.Identification | IfcClassificationReference.ItemReference |uri**      |
| **Classification source** | namespaceUri *of the classification* | IfcClassificationReference.Location       | IfcClassificationReference.Location       | IfcClassificationReference.Location      |uri      |

> \* IDS references bSDD using URI, instead of copying its content. Thanks to that, the information is still accessible by following the URI. 
> 
> \*\* Classification code is a part of the "uri" attribute: uri="`http://identifier.buildingsmart.org/uri`/`OrganizationCode`/`DomainCode`-`DomainVersion`/class/`code`"

:pill: **Snippets**
<details><summary>bSDD classification</summary>
    
```
{
	"Code": "text",
	"Uid": "text",
	"OwnedUri": "text",
	"Name": "text",
	"Definition": "text",
	"Status": "text",
	"ActivationDateUtc": "2022-05-12T00:00:00+02:00",
	"RevisionDateUtc": null,
	"VersionDateUtc": "2022-05-12T00:00:00+02:00",
	"DeActivationDateUtc": null,
	"VersionNumber": null,
	"RevisionNumber": null,
	"ReplacedObjectCodes": [],
	"ReplacingObjectCodes": [],
	"DeprecationExplanation": "text",
	"CreatorLanguageIsoCode": "text",
	"VisualRepresentationUri": "text",
	"CountriesOfUse": [],
	"SubdivisionsOfUse": [],
	"CountryOfOrigin": "text",
	"DocumentReference": "text",
	"Synonyms": [],
	"ReferenceCode": "text",
	"ClassificationRelations": [
	  {
		"RelationType": "text",
		"RelatedClassificationUri": "text",
		"RelatedClassificationName": "text",
		"Fraction": null
	  }
	],
	"ClassificationType": "text",
	"ParentClassificationCode": "text",
	"RelatedIfcEntityNamesList": [],
	"ClassificationProperties": [
	  {
		"AllowedValues": [
		  {
			"NamespaceUri": "text",
			"Code": "text",
			"Value": "text",
			"Description": "text",
			"SortNumber": null
		  }
		],
		"Code": "text",
		"Description": "text",
		"IsRequired": null,
		"IsWritable": null,
		"MaxExclusive": null,
		"MaxInclusive": null,
		"MinExclusive": null,
		"MinInclusive": null,
		"Pattern": "text",
		"PredefinedValue": "text",
		"PropertyCode": "text",
		"PropertyNamespaceUri": "text",
		"PropertySet": "text",
		"PropertyType": "text",
		"SortNumber": null,
		"Symbol": "text",
		"Unit": "text"
	  }
	]
}
```
</details>

<details><summary>IFC 4.3</summary>
    
#46 = IFCCLASSIFICATIONREFERENCE('http://identifier.buildingsmart.org/uri/buildingsmart/ifc-4.3/class/ifctrackelementsleeper','ifctrackelementsleeper','IfcTrackElement.SLEEPER',#45,$,$)

</details>

<details><summary>IFC 2x3</summary>
    
#39116 = IFCCLASSIFICATIONREFERENCE('https://identifier.buildingsmart.org/uri/buildingsmart/bSI-wd-0.1/class/BAR-WI', 'BAR-WI', 'MyWindow', #39115);

</details>

<details><summary>IDS</summary>

```
<ids:classification minOccurs="1" uri="https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs-1.0/class/apple" instructions="Those objects must be classified as apples.">     
    <ids:value>
        <ids:simpleValue>apple</ids:simpleValue>
    </ids:value>
    <ids:system>
        <ids:simpleValue>fruitvegs</ids:simpleValue>
    </ids:system>
</ids:classification>
    
```
</details>

---

### 3. bSDD properties
**In bSDD**, a classification (object) can have multiple properties and a property can be part of many classifications (objects).

**In IFC**, properties information are captured using _IfcProperty_ (and grouped using _IfcPropertySet_). Below are the mapping rules, for different IFC versions.
:construction: :construction: :construction:
|                                                | bSDD                                      | IFC 4.3                                      | IFC 4                                        | IFC 2x3                                      |IDS                                      |
|------------------------------------------------|-------------------------------------------|----------------------------------------------|----------------------------------------------|----------------------------------------------|-----------------------|
| **Property name**                              | Name *(of property)*                      | IfcProperty.Name                             | IfcProperty.Name                             | IfcProperty.Name                             |❎*      |
| **Property source**                            | *namespaceUri of the property*            | IfcProperty.Specification                    | IfcProperty.Description                      | IfcProperty.Description                      |uri      |
| **Property predefined value** (single value)              | PredefinedValue                           | IfcPropertySingleValue.NominalValue          | IfcPropertySingleValue.NominalValue          | IfcPropertySingleValue.NominalValue          |❎*      |
| **Property allowed values** (from enumeration) | AllowedValues                  | IfcPropertyEnumeratedValue.EnumerationValues | IfcPropertyEnumeratedValue.EnumerationValues | IfcPropertyEnumeratedValue.EnumerationValues |❎*      |
| **PropertySet name**                           | PropertySet *(of ClassificationProperty)* | IfcPropertySet.Name                          | IfcPropertySet.Name                          | IfcPropertySet.Name                          |❎*      |

> \* IDS references bSDD using URI, instead of copying its content. Thanks to that, the information is still accessible by following the URI. 
> 
> \*\* Property code is a part of the "uri" attribute

:o: **IMPORTANT** :o:
In bSDD, properties exist independently form the classification (object) they might be assigned to. Therefore: 

- The AllowedValues of a property are defined at the level of each property
- The PredefinedValues of a property is defined at the level of each classification (object)
- The relationship between a property and its property set is defined at the level of each classification (object)
- AllowedValues can be defined also at the level of each classification (object). When this happens, the AllowedValues defined at the level of the property are overwritten 


:pill: **Snippets**
<details><summary>bSDD property</summary>
    
```
{
	"Code": "text",
	"Uid": "text",
	"OwnedUri": "text",
	"Name": "text",
	"Definition": "text",
	"Status": "text",
	"ActivationDateUtc": "2022-05-12T00:00:00+02:00",
	"RevisionDateUtc": null,
	"VersionDateUtc": "2022-05-12T00:00:00+02:00",
	"DeActivationDateUtc": null,
	"VersionNumber": null,
	"RevisionNumber": null,
	"ReplacedObjectCodes": [],
	"ReplacingObjectCodes": [],
	"DeprecationExplanation": "text",
	"CreatorLanguageIsoCode": "text",
	"VisualRepresentationUri": "text",
	"CountriesOfUse": [],
	"SubdivisionsOfUse": [],
	"CountryOfOrigin": "text",
	"DocumentReference": "text",
	"Description": "text",
	"Example": "text",
	"ConnectedPropertyCodes": [],
	"PhysicalQuantity": "text",
	"Dimension": "text",
	"DimensionLength": null,
	"DimensionMass": null,
	"DimensionTime": null,
	"DimensionElectricCurrent": null,
	"DimensionThermodynamicTemperature": null,
	"DimensionAmountOfSubstance": null,
	"DimensionLuminousIntensity": null,
	"MethodOfMeasurement": "text",
	"DataType": "text",
	"PropertyValueKind": "text",
	"MinInclusive": null,
	"MaxInclusive": null,
	"MinExclusive": null,
	"MaxExclusive": null,
	"Pattern": "text",
	"IsDynamic": false,
	"DynamicParameterPropertyCodes": [],
	"Units": [],
	"AllowedValues": [
	  {
		"NamespaceUri": "text",
		"Code": "text",
		"Value": "text",
		"Description": "text",
		"SortNumber": null
	  }
	],
	"TextFormat": "text",
	"PropertyRelations": [
	  {
		"RelationType": "text",
		"RelatedPropertyUri": "text",
		"RelatedPropertyName": "text"
	  }
	]
}
```
</details>

<details><summary>IFC 2x3</summary>
    
    TBC

</details>

<details><summary>IDS</summary>

```
<ids:property minOccurs="1" measure="IfcText" uri="http://identifier.buildingsmart.org/uri/buildingsmart/ifc-4.3/prop/manufacturer"  instructions="One of the two manufacturers must be specified.">
    <ids:propertySet>
        <ids:simpleValue>Pset_ManufacturerTypeInformation</ids:simpleValue>
    </ids:propertySet>
    <ids:name>
        <ids:simpleValue>Manufacturer</ids:simpleValue>
    </ids:name>
    <ids:value>
        <xs:restriction>
            <xs:enumeration value="Manufacturer 1"/>
            <xs:enumeration value="Manufacturer 2"/>
        </xs:restriction>
    </ids:value>
</ids:property>
```
</details>

---

### 4. bSDD materials
**In bSDD**, a Material is similar to a Classification. Like a Classification, a Material can have multiple properties. Also, a Material can be associated to one or more Classifications (objects). A domain can contain both Materials and Classifications.
 
**In IFC**, materials information are captured using _IfcMaterial_, and (unfortunately) associated to objects in many possible ways.

**The main difference between bSDD Classifications and bSDD Materials is on the mapping rules for IFC models:**

- **bSDD Classifications (objects)** are linked to the IfcObject that they classify (e.g., to an IfcWall)
- **bSDD Materials** are linked to the IfcMaterial they classify, which is linked to the IfcObject (e.g., the IfcMaterial of the mentioned IfcWall)

:bulb: **IMPLEMENTATION OPTIONS** :bulb:
- The IFC concept template to be used for associating materials to objects is [Material Single](http://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/concepts/Object_Association/Material_Association/Material_Single/content.html)
- If there is the need to associate materials that are either homogenously mixed, or arbitrarily placed, then _IfcMaterialConstituent_ (and its _IfcMaterialConstituent.Fraction_) can be used - as indicated in the [Material Set](http://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/concepts/Object_Association/Material_Association/Material_Set/content.html) concept template


Below are the mapping rules, for different IFC versions.
:construction: :construction: :construction:
|                                                   | bSDD                                                               | IFC 4.3                 | IFC 4                   | IFC 2x3                 |IDS                 |
|---------------------------------------------------|--------------------------------------------------------------------|-------------------------|-------------------------|-------------------------|-----------|
| **Material identification** (option 1: Code)      | Code *(of material)* (E.g., MM34)                                  | IfcMaterial.Name        | IfcMaterial.Name        | IfcMaterial.Name        |❎*      |
| **Material identification** (option 2: Code_Name) | *Concatenate* "Code";"_";"Name" *(of material)* (E.g., MM34_Steel) | IfcMaterial.Name        | IfcMaterial.Name        | IfcMaterial.Name        |❎*      |
| **Material source**                               | *namespaceUri of the material*                                     | IfcMaterial.Description | IfcMaterial.Description | IfcMaterial.Description |uri      |

> \* IDS references bSDD using URI, instead of copying its content. Thanks to that, the information is still accessible by following the URI. 

:pill: **Snippets**
<details><summary>bSDD material</summary>
    
```
{
	"Code": "text",
	"Uid": "text",
	"OwnedUri": "text",
	"Name": "text",
	"Definition": "text",
	"Status": "text",
	"ActivationDateUtc": "2022-05-12T00:00:00+02:00",
	"RevisionDateUtc": null,
	"VersionDateUtc": "2022-05-12T00:00:00+02:00",
	"DeActivationDateUtc": null,
	"VersionNumber": null,
	"RevisionNumber": null,
	"ReplacedObjectCodes": [],
	"ReplacingObjectCodes": [],
	"DeprecationExplanation": "text",
	"CreatorLanguageIsoCode": "text",
	"VisualRepresentationUri": "text",
	"CountriesOfUse": [],
	"SubdivisionsOfUse": [],
	"CountryOfOrigin": "text",
	"DocumentReference": "text",
	"Synonyms": [],
	"ReferenceCode": "text",
	"ClassificationRelations": [
	  {
		"RelationType": "text",
		"RelatedClassificationUri": "text",
		"RelatedClassificationName": "text"
	  }
	],
	"ParentMaterialCode": "text",
	"MaterialProperties": [
	  {
		"AllowedValues": [
		  {
			"NamespaceUri": "text",
			"Code": "text",
			"Value": "text",
			"Description": "text",
			"SortNumber": null
		  }
		],
		"Code": "text",
		"Description": "text",
		"ExternalPropertyUri": "text",
		"IsRequired": null,
		"IsWritable": null,
		"MaxExclusive": null,
		"MaxInclusive": null,
		"MinExclusive": null,
		"MinInclusive": null,
		"Pattern": "text",
		"PredefinedValue": "text",
		"PropertyCode": "text",
		"PropertySet": "text",
		"PropertyType": "text",
		"SortNumber": null,
		"Symbol": "text",
		"Unit": "text"
	  }
	]
}
```
</details>

<details><summary>IFC 2x3</summary>
    
    TBC

</details>

<details><summary>IDS</summary>

```
<ids:material minOccurs="1" uri="
https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs-1.0/mat/fiber" instructions="The material should be fiber.">     
    <ids:value>
        <ids:simpleValue>fiber</ids:simpleValue>
    </ids:value>
</ids:material>
```
</details>
