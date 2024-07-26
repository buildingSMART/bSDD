# Referencing bSDD in IFC and IDS

To associate a class from an external reference (such as bSDD) to objects in an IFC model, the following documentation shall be used.

The main IFC concept template to be used is: [Classification Association](https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/concepts/Object_Association/Classification_Association/content.html)

The main entities involved are:
- [_IfcClassification_](https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/lexical/IfcClassification.htm)
- [_IfcClassificationReference_](https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/lexical/IfcClassificationReference.htm)
- [_IfcRelAssociatesClassification_](https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/lexical/IfcRelAssociatesClassification.htm)

The next section indicates the mapping rules between the bSDD data model and IFC concepts used for class. To support this, snippets ( :pill:) of IFC files and bSDD dictionary files are reported as examples.


## openBIM workflows
The buildingSMART Data Dictionary is a key component in many openBIM workflows. Among other things, it allows to:
- access various kinds of standards to **enrich an openBIM model**
- check data for compliance
- provide concepts for Information Delivery Specifications (IDS)
- extend the IFC standard
- and much more, which you can read at: [bSDD website](https://www.buildingsmart.org/users/services/buildingsmart-data-dictionary/)

## bSDD - IFC mapping

Mapping rules are defined for the following concepts:

1. [bSDD dictionary](#1.-bSDD-dictionary)
2. [bSDD classes (objects)](#2.-bSDD-classes-(objects))
3. [bSDD materials](#3.-bSDD-materials)
4. [bSDD properties](#4.-bSDD-properties)

---

### 1. bSDD dictionary
**In bSDD**, a dictionary (a.k.a., class system) is a standardised collection of object definitions, properties, and materials owned and maintained by one organisation. One organisation can own one or more dictionaries.

**In IFC**, dictionary information are captured using [_IfcClassification_](https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/lexical/IfcClassification.htm). Below are the mapping rules for different IFC versions.

|                    | bSDD                      | IFC 4.3                      | IFC 4                      | IFC 2x3                    | IDS   |
|--------------------|------------------------------|---------------------------------|-------------------------------|-------------------------------|---------|
| **Dictionary name**    | DictionaryName                   | IfcClassification.Name          | IfcClassification.Name        | IfcClassification.Name        |❎*    |
| **Dictionary source**  | *uri of the dictionary* | IfcClassification.Specification | IfcClassification.Location    | ❌ (IfcClassification.Source can be used as a workaround)   |uri      |
| **Dictionary version** | DictionaryVersion                | IfcClassification.Edition       | IfcClassification.Edition     | IfcClassification.Edition     |uri**      |
| **Dictionary owner**   | OrganizationCode             | IfcClassification.Source        | IfcClassification.Source      | IfcClassification.Source      |uri**      |
| **Dictionary date**    | ReleaseDate                  | IfcClassification.EditionDate   | IfcClassification.EditionDate | IfcClassification.EditionDate |❎*      |

_\* IDS references bSDD using URI, instead of copying its content. Thanks to that, the information is still accessible by following the URI._
 
_\*\* The IDS doesn't support a direct reference to the bSDD dictionaries, but whenever a class or property is referenced by "uri" attribute, those include information about their dictionaries: uri="```http://identifier.buildingsmart.org/uri/<OrganizationCode>/<DictionaryCode>/<DictionaryVersion>/...```"_

:pill: **Snippets**
<details><summary>bSDD</summary>
    
```
{
    "OrganizationCode": "text",
    "DictionaryCode": "text",
    "DictionaryVersion": "text",
    "DictionaryName": "text",
    "ReleaseDate": null,
    "Status": "text",
    "MoreInfoUrl": "text",
    "UseOwnUri": false,
    "DictionaryUri": "text",
    "LanguageIsoCode": "text",
    "LanguageOnly": false,
    "License": "text",
    "LicenseUrl": "text",
    "QualityAssuranceProcedure": "text",
    "QualityAssuranceProcedureUrl": "text",
    "Classes": [], 
    "Properties": []
  }
```
</details>

<details><summary>IFC 4x3 <a href="https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/lexical/IfcClassification.htm">(IfcClassification)</a></summary>
	
    /* 			 Source,   Edition, EditionDate,  Name,                Description,            Specification,                                                      ReferenceTokens  */
    #1=IFCCLASSIFICATION('Molio',  '1.0',   '2023-08-27', 'CCI Construction',  'List of codes...',    'https://identifier.buildingsmart.org/uri/molio/cciconstruction/1.0', ('.'));
</details>

<details><summary>IFC 4 <a href="https://standards.buildingsmart.org/IFC/RELEASE/IFC4/ADD2_TC1/HTML/">(IfcClassification)</a></summary>

    /*  		 Source,   Edition, EditionDate,  Name,                Description,            Location,                                                      ReferenceTokens   */
    #1=IFCCLASSIFICATION('Molio',  '1.0',   '2023-08-27', 'CCI Construction',  'List of codes...',    'https://identifier.buildingsmart.org/uri/molio/cciconstruction/1.0', ('.'));
</details>

<details><summary>IFC 2x3 <a href="https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcexternalreferenceresource/lexical/ifcclassification.htm">(IfcClassification)</a></summary>

    /*  		 Source,   Edition, EditionDate,  Name    */
    #1=IFCCLASSIFICATION('Molio',  '1.0',   '2023-08-27', 'CCI Construction');
</details>
    

---

### 2. bSDD classes (objects)
**In bSDD**, a class can be any (abstract) object (e.g. [_IfcWall_](https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/lexical/IfcWall.htm)), abstract concept (e.g. _Costing_) or process (e.g. _Installation”_.

**In IFC**, class information is captured using [_IfcClassificationReference_](https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/lexical/IfcClassificationReference.htm). Below are the mapping rules, for different IFC versions.

|                    | bSDD                      | IFC 4.3 & IFC 4                      | IFC 2x3                    | IDS   |
|---------------------------|--------------------------------------|-------------------------------------------|------------------------------------------|-------|
| **Class name**   | name *of the class*           | IfcClassificationReference.Name           | IfcClassificationReference.Name          |❎*      |
| **Class code**   | code *of the class*           | IfcClassificationReference.Identification | IfcClassificationReference.ItemReference |uri**      |
| **Class identifier** | uri *of the class* | IfcClassificationReference.Location      | IfcClassificationReference.Location      |uri      |

_\* IDS references bSDD using URI instead of copying its content. Thanks to that, the information is still accessible by following the URI._ 

_\*\* Class code is a part of the "uri" attribute: uri="```http://identifier.buildingsmart.org/uri/<OrganizationCode>/<DictionaryCode>/<DictionaryVersion>/class/<code>```"_

:pill: **Snippets**
<details><summary>bSDD class</summary>
    
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
	"ClassRelations": [
	  {
		"RelationType": "text",
		"RelatedClassUri": "text",
		"RelatedClassName": "text",
		"Fraction": null
	  }
	],
	"ClassType": "text",
	"ParentClassCode": "text",
	"RelatedIfcEntityNamesList": [],
	"ClassProperties": [
	  {
		"AllowedValues": [
		  {
			"Uri": "text",
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
		"PropertyUri": "text",
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

<details><summary>IFC 4.3 & IFC 4</summary>
	
    /*  		 Source,   Edition, EditionDate,  Name,                Description,            Specification,                                                      ReferenceTokens   */
    #1=IFCCLASSIFICATION('Molio',  '1.0',   '2023-08-27', 'CCI Construction',  'List of codes...',    'https://identifier.buildingsmart.org/uri/molio/cciconstruction/1.0', ('.'));
    
    /*  			  Location,                                                                        Identification, Name,             ReferencedSource, Description,           Sort   */
    #2=IFCCLASSIFICATIONREFERENCE('https://identifier.buildingsmart.org/uri/molio/cciconstruction/1.0/class/L-BD', 'L-BD',         'Wall structure', #1,               'structural system...', $);

    /*         GlobalId,                 OwnerHistory, Name, Description, ObjectType, ObjectPlacement, Representation, Tag, PredefinedType   */
    #3=IFCWALL('3t3TDZl_D9NOIWB0BSjzJI', $,            $,    $,           $,          $,               $,              $,   $);
    
    /*  			      GlobalId,                 OwnerHistory, Name, Description, RelatedObjects, RelatingClassification  */
    #4=IFCRELASSOCIATESCLASSIFICATION('2t3TDZl_D9NOIWB0BSjzJI', $,            $,    $,           (#3),           #2);
</details>

<details><summary>IFC 2x3</summary>
    
	
    /*  		  Source,   Edition, EditionDate,  Name   */
    #1=IFCCLASSIFICATION('Molio',  '1.0',   '2023-08-27', 'CCI Construction');
    
    /*   			  Location,                                                                        ItemReference, Name,             ReferencedSource  */
    #2=IFCCLASSIFICATIONREFERENCE('https://identifier.buildingsmart.org/uri/molio/cciconstruction/1.0/class/L-BD', 'L-BD',        'Wall structure', #1);

    /*         GlobalId,                 OwnerHistory, Name, Description, ObjectType, ObjectPlacement, Representation, Tag   */
    #3=IFCWALL('3t3TDZl_D9NOIWB0BSjzJI', $,            $,    $,           $,          $,               $,              $);
    
    /* 				      GlobalId,                 OwnerHistory, Name, Description, RelatedObjects, RelatingClassification    */
    #4=IFCRELASSOCIATESCLASSIFICATION('2t3TDZl_D9NOIWB0BSjzJI', $,            $,    $,           (#3),           #2);
</details>

<details><summary>IDS</summary>
	
```
<ids:class minOccurs="1" uri="https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs-1.0/class/apple" instructions="Those objects must be classified as apples.">     
    <ids:value>
        <ids:simpleValue>apple</ids:simpleValue>
    </ids:value>
    <ids:system>
        <ids:simpleValue>fruitvegs</ids:simpleValue>
    </ids:system>
</ids:class>
```
</details>

---

### 3. bSDD materials
**In bSDD**, a material is defined with a class of type 'Material'. The main difference is in the mapping rules for IFC models. The **bSDD class of type 'Material'** should be linked to the [_IfcMaterial_](https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/lexical/IfcMaterial.htm), which is then linked to various [_IfcObject_](https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/lexical/IfcObject.htm).
 
**In IFC**, [_IfcMaterial_](https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/lexical/IfcMaterial.htm) are associated with objects through [_IfcRelAssociatesMaterial_](https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/lexical/IfcRelAssociatesMaterial.htm) relation. However, when more than one material is associated with an element, there are many possible ways to define this relation through layer sets, profiles or constituents (% of content). Read more about: [Material Association](https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/concepts/Object_Association/Material_Association/content.html), in particular: [Material Constituent Set](https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/concepts/Object_Association/Material_Association/Material_Constituent_Set/content.html), [Material Layer Set Usage](https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/concepts/Object_Association/Material_Association/Material_Constituent_Set/content.html), [Material Profile Set Usage](https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/concepts/Object_Association/Material_Association/Material_Profile_Set_Usage/content.html), [Material Set](https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/concepts/Object_Association/Material_Association/Material_Set/content.html), [Material Single](https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/concepts/Object_Association/Material_Association/Material_Single/content.html).

Below are the mapping rules for different IFC versions.

|                                                   | bSDD                                                               | IFC 4.3 & IFC 4         | IFC 2x3    |IDS   |
|---------------------------------------------------|--------------------------------------------------------------------|-------------------------|-----------|-----|
| **Material name**  | Class(Material).**Name**       | IfcMaterial.**Name** & IfcExternalReferenceRelationship.RelatingReference.IfcClassificationReference.**Name**        | IfcMaterial.**Name**        | ❎*      |
| **Material code**  | Class(Material).**Code**        | IfcExternalReferenceRelationship.RelatingReference.IfcClassificationReference.**Identification**        | IfcMaterialClassificationRelationship.IfcClassificationReference.**ItemReference**       | ❎*      |
| **Material identifier**  | Class(Material).**Uri**  | IfcExternalReferenceRelationship.RelatingReference.IfcClassificationReference.**Location** | IfcMaterialClassificationRelationship.IfcClassificationReference.**Location**  |uri      |

_\* IDS references bSDD using URI instead of copying its content. Thanks to that, the information is still accessible by following the URI._

:pill: **Snippets**

_For the bSDD snippet, look at the [bSDD classes (objects)](#2.-bSDD-classes-(objects))_

<details><summary>IFC 4.3 & IFC 4</summary>

    /*    		 Source, Edition, EditionDate,  Name,                Description,            Specification,                                                      ReferenceTokens   */
    #1=IFCCLASSIFICATION('SBE',  '1',     '2023-08-27', 'Swedish materials', 'List of materials...', 'https://identifier.buildingsmart.org/uri/molio/cciconstruction/1.0', ('.'));
    
    /*  			  Location,                                                                   Identification, Name,     ReferencedSource, Description,           Sort   */
    #2=IFCCLASSIFICATIONREFERENCE('https://identifier.buildingsmart.org/uri/sbe/swedishmaterials/1/mat/CE--', 'CE--',         'Betong', #1,               'kompositmaterial...', $);

    /*   	    Name,    Description,          Category   */
    #3=IFCMATERIAL('Betong','kompositmaterial...','concrete');
    
    /*  				 Name,     Description,          RelatingReference, RelatedResourceObjects  */
    #4=IFCEXTERNALREFERENCERELATIONSHIP('Betong', 'kompositmaterial...', #2,                (#3));

</details>

<details><summary>IFC 2x3</summary>
    
    /* 			 Source, Edition, EditionDate, Name 		    */
    #1=IFCCLASSIFICATION('SBE',  '1',    '2023-08-27', 'Swedish materials');
    
    /*  			  Location,                                                                   ItemReference, Name,     ReferencedSource */
    #2=IFCCLASSIFICATIONREFERENCE('https://identifier.buildingsmart.org/uri/sbe/swedishmaterials/1/mat/CE--', 'CE--',        'Betong', #1);
     IfcClassificationReference( $,**ItemReference**,$,$)
     
    /*              Name      */
    #3=IFCMATERIAL('Betong');
    
    /*  				     MaterialClassifications, ClassifiedMaterial  */
    #4=IFCMATERIALCLASSIFICATIONRELATIONSHIP(#2,                      #3);

</details>

<details><summary>IDS</summary>

```
<ids:material minOccurs="1" maxOccurs="unbounded" uri="
https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/1.0/mat/fiber" instructions="The material should be called fiber.">     
    <ids:value>
        <ids:simpleValue>fiber</ids:simpleValue>
    </ids:value>
</ids:material>
```
</details>

--- 

### 4. bSDD properties 
**In bSDD**, a class (object) can have multiple properties, and a property can be part of many classes (objects).

**In IFC**, properties information are captured using [_IfcProperty_](https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/lexical/IfcProperty.htm) (and grouped using [_IfcPropertySet_](https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/lexical/IfcPropertySet.htm)). [_IfcProperty_](https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/lexical/IfcProperty.htm) is an abstract definition, meaning it can not be instantiated. Instead, there are many forms it might take, with the most common being [_IfcPropertySingleValue_](https://ifc43-docs.standards.buildingsmart.org/IFC/RELEASE/IFC4x3/HTML/lexical/IfcPropertySingleValue.htm). 

Below are the mapping rules for different IFC versions.

|                                                | bSDD                                      | IFC 4.3                                      | IFC 4 & IFC 2x3                                      |IDS                                      |
|------------------------------------------------|-------------------------------------------|----------------------------------------------|----------------------------------------------|-----------------------|
| **Property name**                              | PropertyCode *(of ClassProperty)*                      | IfcPropertySingleValue.Name                             | IfcPropertySingleValue.Name                             |❎*      |
| **Property identifier**                            | uri *(of ClassProperty)*          | IfcPropertySingleValue.Specification                    | IfcPropertySingleValue.Description                      |uri      |
| **Property predefined value** (single value)              | PredefinedValue *(of ClassProperty)*   | IfcPropertySingleValue.NominalValue          | IfcPropertySingleValue.NominalValue          |❎*      |
| **Property unit** (single value or from enumeration)              | PredefinedValue *(of Property or ClassProperty)*   | IfcPropertySingleValue.Unit          | IfcPropertySingleValue.Unit          |❎*      |
| **Property allowed values** (from enumeration) | AllowedValues *(of Property or ClassProperty)*    | IfcPropertyEnumeratedValue .EnumerationValues | IfcPropertyEnumeratedValue .EnumerationValues |❎*      |
| **PropertySet name**                           | PropertySet *(of ClassProperty)* | IfcPropertySet.Name                          | IfcPropertySet.Name                          |❎*      |

_\* IDS references bSDD using URI instead of copying its content. Thanks to that, the information is still accessible by following the URI._

_\*\* Property code is a part of the "uri" attribute_

⚠️ **IMPORTANT**
In bSDD, properties exist independently of the class (object) they might be assigned to. Therefore: 

- The `AllowedValues` of a `Property` are defined at the level of each property.
- The `PredefinedValues` of a `Property` is defined at the level of each class (object).
- The relationship between a property and its property set is defined at the level of each class (object).
- `AllowedValue`s can be defined also at the level of each class (object). When this happens, the `AllowedValue` defined at the level of the `Property` is overwritten. 

The human-readable and translatable _Name_ that exists in bSDD has no reflection in IFC. That is why it is important to remember that the _Code_ will be used in IFC datasets, and _Name_ will only be displayed by software capable of reading the names from bSDD. The reason for this is that we need the datasets to follow consistent terms, regardless of the language displayed to the user. 
 
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
		"Uri": "text",
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
    
    /*                         Name,       Description, 							  NominalValue, Unit */
    #1=IFCPROPERTYSINGLEVALUE("EF021146", "https://identifier.buildingsmart.org/uri/etim/etim/9.0/prop/EF021146", $,            $);

</details>

</details>

<details><summary>IFC 4.3</summary>
    
    /*                         Name,       Specification, 							  NominalValue, Unit */
    #1=IFCPROPERTYSINGLEVALUE("EF021146", "https://identifier.buildingsmart.org/uri/etim/etim/9.0/prop/EF021146", $,            $);

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
