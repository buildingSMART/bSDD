# bSDD-IFC documentation


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

|                    | in bSDD                      | in IFC 4.3                      | in IFC 4                      | in IFC 2x3                    |
|--------------------|------------------------------|---------------------------------|-------------------------------|-------------------------------|
| **Domain name**    | DomainName                   | IfcClassification.Name          | IfcClassification.Name        | IfcClassification.Name        |
| **Domain source**  | *namespaceUri of the domain* | IfcClassification.Specification | IfcClassification.Location    | n.a.                          |
| **Domain version** | DomainVersion                | IfcClassification.Edition       | IfcClassification.Edition     | IfcClassification.Edition     |
| **Domain owner**   | OrganizationCode             | IfcClassification.Source        | IfcClassification.Source      | IfcClassification.Source      |
| **Domain date**    | ReleaseDate                  | IfcClassification.EditionDate   | IfcClassification.EditionDate | IfcClassification.EditionDate |

:pill: **Snippets**
<details><summary>bSDD domain</summary>
    
```
{
    "OrganizationCode": "buildingsmart",
    "DomainCode": "bSI-wd",
    "DomainVersion": "0.1",
    "DomainName": "Workshop Dictionary",
    "ReleaseDate": "2020-06-01T00:00:00",
    "Status": "Active",
    "LanguageIsoCode": "en-GB",
    "LanguageOnly": false,
  
  
  
    "Classifications": [], 
    
    "Properties": [],
  
    "Materials": []
  }
```
</details>

<details><summary>IFC 4.3</summary>

```    
    #45 = IFCCLASSIFICATION('buildingSMART','4.3',$,'IFC',$,'https://identifier.buildingsmart.org/uri/buildingsmart/ifc-4.3',$);
```
</details>

<details><summary>IFC 2x3</summary>

```
    #39115 = IFCCLASSIFICATION('buildingSMART', '0.1', $, 'Workshop Dictionary');
```
</details>

---

### 2. bSDD classifications (objects)
**In bSDD**, a classification can be any (abstract) object (e.g. _IfcWall_), abstract concept (e.g. _Costing_) or process (e.g. _Installation”_.

**In IFC**, classification information are captured using _IfcClassificationReference_. Below are the mapping rules, for different IFC versions.

|                           | in bSDD                              | in IFC 4.3                                | in IFC 4                                  | in IFC 2x3                               |
|---------------------------|--------------------------------------|-------------------------------------------|-------------------------------------------|------------------------------------------|
| **Classification name**   | Name *(of classification)*           | IfcClassificationReference.Name           | IfcClassificationReference.Name           | IfcClassificationReference.Name          |
| **Classification code**   | Code *(of classification)*           | IfcClassificationReference.Identification | IfcClassificationReference.Identification | IfcClassificationReference.ItemReference |
| **Classification source** | *namespaceUri of the classification* | IfcClassificationReference.Location       | IfcClassificationReference.Location       | IfcClassificationReference.Location      |

:pill: **Snippets**
<details><summary>bSDD classification</summary>
    
```
    "Classifications": [
        {
            "Code": "BAR-WI",
            "Name": "MyWindow",
      
            "ClassificationType": "Class",
            "Synonyms": [],
            "Definition": "Cambridge: a space, usually filled with glass, in the wall of a building, to allow light and air in, and to allow people inside the building to see out.",
      
            "RelatedIfcEntityNamesList": [
              "IfcWindow"
            ],
            
            "ClassificationRelations": [
              {
                "RelatedClassificationUri": "http://identifier.buildingsmart.org/uri/buildingsmart/ifc-4.3/class/IfcWindow",
                "RelationType": "HasReference"
              }
            ],
            "ClassificationProperties": [
              {
                "PropertyCode": "ws-ISB",
                "PropertySet": "WS_MyWindow",
                "PropertyType": "Property",
                "AllowedValues": [
                  {
                    "Code": "IsBeautiful_true",
                    "Value": "true"
                  }
                ]
              },
              {
                "ExternalPropertyUri": "http://identifier.buildingsmart.org/uri/buildingsmart/ifc-4.3/prop/FireExit",
                "PropertySet": "WS_MyWindow",
                "PropertyType": "Property"
              },
              {
                "ExternalPropertyUri": "http://identifier.buildingsmart.org/uri/buildingsmart/ifc-4.3/prop/AssemblyPlace",
                "PropertySet": "WS_MyWindow",
                "PropertyType": "Property"
              }
            ]
          }
    ], 
```
</details>

<details><summary>IFC 4.3</summary>
    
    #46 = IFCCLASSIFICATIONREFERENCE('http://identifier.buildingsmart.org/uri/buildingsmart/ifc-4.3/class/ifctrackelementsleeper','ifctrackelementsleeper','IfcTrackElement.SLEEPER',#45,$,$)

</details>

<details><summary>IFC 2x3</summary>
    
    #39116 = IFCCLASSIFICATIONREFERENCE('https://identifier.buildingsmart.org/uri/buildingsmart/bSI-wd-0.1/class/BAR-WI', 'BAR-WI', 'MyWindow', #39115);

</details>

---

### 3. bSDD properties
**In bSDD**, a classification (object) can have multiple properties and a property can be part of many classifications (objects).

**In IFC**, properties information are captured using _IfcProperty_ (and grouped using _IfcPropertySet_). Below are the mapping rules, for different IFC versions.
:construction: :construction: :construction:
|                                                | bSDD                                      | IFC 4.3                                      | IFC 4                                        | IFC 2x3                                      |
|------------------------------------------------|-------------------------------------------|----------------------------------------------|----------------------------------------------|----------------------------------------------|
| **Property name**                              | Name *(of property)*                      | IfcProperty.Name                             | IfcProperty.Name                             | IfcProperty.Name                             |
| **Property source**                            | *namespaceUri of the property*            | IfcProperty.Specification                    | IfcProperty.Description                      | IfcProperty.Description                      |
| **Property predefined value** (single value)              | PredefinedValue                           | IfcPropertySingleValue.NominalValue          | IfcPropertySingleValue.NominalValue          | IfcPropertySingleValue.NominalValue          |
| **Property allowed values** (from enumeration) | AllowedValues (which ones?)               | IfcPropertyEnumeratedValue.EnumerationValues | IfcPropertyEnumeratedValue.EnumerationValues | IfcPropertyEnumeratedValue.EnumerationValues |
| **PropertySet name**                           | PropertySet *(of ClassificationProperty)* | IfcPropertySet.Name                          | IfcPropertySet.Name                          | IfcPropertySet.Name                          |

:o: **IMPORTANT** :o:
In bSDD, properties exist independently form the classification (object) they might be assigned to. Therefore: 

- The AllowedValues of a property are defined at the level of each property
- The PredefinedValues of a property is defined at the level of each classification (object)
- The relationship between a property and its property set is defined at the level of each classification (object)
- AllowedValues can be defined also at the level of each classification (object). When this happens, the AllowedValues defined at the level of the property are overwritten 


:pill: **Snippets**
<details><summary>bSDD property</summary>
    
```
    TBC
```
</details>

<details><summary>IFC 2x3</summary>
    
    TBC

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
|                                                   | bSDD                                                               | IFC 4.3                 | IFC 4                   | IFC 2x3                 |
|---------------------------------------------------|--------------------------------------------------------------------|-------------------------|-------------------------|-------------------------|
| **Material identification** (option 1: Code)      | Code *(of material)* (E.g., MM34)                                  | IfcMaterial.Name        | IfcMaterial.Name        | IfcMaterial.Name        |
| **Material identification** (option 2: Code_Name) | *Concatenate* "Code";"_";"Name" *(of material)* (E.g., MM34_Steel) | IfcMaterial.Name        | IfcMaterial.Name        | IfcMaterial.Name        |
| **Material source**                               | *namespaceUri of the material*                                     | IfcMaterial.Description | IfcMaterial.Description | IfcMaterial.Description |


:pill: **Snippets**
<details><summary>bSDD material</summary>
    
```
    TBC
```
</details>

<details><summary>IFC 2x3</summary>
    
    TBC

</details>
