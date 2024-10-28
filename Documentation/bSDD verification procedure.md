# bSDD verification procedure

The content...
1. **Organisation review** - at the moment of registration, each organisation willing to publish in bSDD is reviewed to make sure their objective fits the mission of bSDD.
1. **Import validation** - automatic step triggered on every upload to bSDD. It checks the existence of necessary fields and their types. This step ensures the content is well structured and compliant with the service.
1. **bSDD verification** - an on-demand, paid service designed to increase the trustworthiness of data within the bSDD platform. Verified content undergoes a series of checks to ensure quality and consistency, marked by a verified icon in bSDD. While the verification list below is comprehensive, it is not exhaustive — additional aspects may be verified over time to maintain and enhance reliability further. The procedure is executed by buildingSMART International and/or trusted partners.

## Overview

| Code   | Item                                                         |
| ------ | ------------------------------------------------------------ |
| [GEN-01](#gen-01) | Required fields   |
| [GEN-02](#gen-02) | Must have English version   |
| [GEN-03](#gen-03) | Translations should be accurate              |
| [GEN-04](#gen-04) | Names should be clear and easy to interpret                  |
| [GEN-05](#gen-05) | Follow a consistent naming convention |
| [GEN-06](#gen-06) | Use of correct types                             |
| [GEN-07](#gen-07) | Governance of the data dictionary                             |
| [GEN-08](#gen-08) | Ownership verification                     |
| [DCT-01](#dct-01)   | Dictionary should be 'Active'                         |
| [DCT-02](#dct-02)   | Dictionary name should not be misleading |
| [CLS-01](#cls-01)   | Classes should be mapped to IFC correctly                    |
| [CLS-02](#cls-02)   | Avoid circular dependencies               |
| [CLS-03](#cls-03) | Hierarchy of classes |
| [CLS-04](#cls-04) | Avoid syncretic classes |
| [PRP-01](#prp-01)   | Numeric property metadata |
| [PRP-02](#prp-02)   | Avoid duplicating IFC properties                 |
| [PRP-03](#prp-03)   | PSET_ prefix is forbidden |
| [PRP-04](#prp-04)   | Property adequate data type            |
| [PRP-05](#prp-05) | Single aspect properties       |
| [PRP-06](#prp-06)   | Avoid unnecessary allowed values |
| [PRP-07](#prp-07) | Allowed values must be meaningful |
| [CPR-01](#cpr-01)  | Only use active bSDD Properties in ClassProperty      |
| [CPR-02](#cpr-02)  | ClassProperty should have a PropertySet name                  |
| [REL-01](#rel-01)  | Avoid circular relationships in RelationType                 |
| [REL-02](#rel-02)  | Avoid incorrect class types in RelationType                  |
| [REL-03](#rel-03) | Relations must be meaningful                                 |

**GEN (General), DCT (Dictionary), CLS (Class), PRP (Property), ALV (AllowedValue), CPR (ClassProperty), REL (Relations)*

## General

### GEN-01 
**Required fields**

While it is possible to publish in bSDD without some fields filled, the requirements for verification are set higher. The dictionary, classes and properties must at least provide the fields from both rows below correspondingly: 

|      |  Dictionary  |  Class  | Property | 
| ----- | ----- | ----- | ----- | 
| Required by bSDD | `OrganizationCode`, `DictionaryCode`, `DictionaryName`, `DictionaryVersion`, `LanguageIsoCode`  | `Code`, `Name`, `ClassType`  | `Code`, `Name`, `DataType` | 
| Additional requirements for the verification | `QualityAssuranceProcedure`, `ChangeRequestEmailAddress`, `License`, `LicenseUrl` | `Definition`, `RelatedIfcEntityNamesList`  | `Definition`, `Example`, `Dimension` (if numeric), `PropertyValueKind`  | 

Additionally, `ClassProperty` should have a value of its `PropertySet`.

### GEN-02
**Must have English version**

As per ISO 12006-3:2022, the dictionary should include an English version of all the relevant content for all translatable fields. See bSDD documentation for a list of translatable fields. The existence of other language translations is optional.

### GEN-03
**Translations should be accurate**

The translations are optional, but when they exist, all the translations should be precise and faithful to the original content. Translations can not extend the explanations or remove any part of the original sentences. 

Example:

|  ENG (original) | German (translation) |   |
| ---- | ----- | ----- |
| _The wall represents a vertical construction that may bound or subdivide spaces..._ | _Vertikale Konstruktion zur Abgrenzung oder Unterteilung von Räumen... Anmerkung: Nach ISO 6707-1 ist eine vertikale Konstruktion in der Regel aus Mauerwerk oder Beton, ..._ | ❌ FAIL: The German translation has additional sentence referring ISO - the two are therefore not consistent. |


### GEN-04
**Names should be clear and easy to interpret**

Unlike codes, the name of each item must be clear and help users understand the concept to enhance usability.

Notes:

- Avoid adding prefixes to each item in the dictionary, as this can complicate search and filtering.
- Try to avoid acronyms, as they may vary between languages and can have different meanings in different contexts.
- Avoid using abstract or generic placeholders like Class1, Class2, etc., which do not provide meaningful information.

Examples:

- ❌ Name: 'Class 20-18.7' - doesn't convey the actual meaning of the class.
- ❌ Name: 'FR-MR' - an acronym that could stand for many things.
- ❌ Name: 'ABC_Wall' - unnecessary prefix.

### GEN-05
**Follow a consistent naming convention**

Names and codes should follow consistent naming conventions. While no specific naming convention is required, using a consistent style for names and codes improves searchability and readability.

Notes:

- Common naming conventions include: Pascal case (_CustomClass_), sentence case (_Custom class_), title case (_Custom Class_), snake case (_custom_class_), and kebab case (_custom-class_).
- Whitespace, dots, dashes, and underscores are acceptable for use in both names and codes.
- It is recommended that the codes are also easily recognizable, as they are the pieces of information that get stored in the data and are displayed by most of the software without integration with the bSDD. The reason for having both is that names can be translated, unliked the codes, and some software doesn't allow special characters or whitespaces in the codes (e.g. 'Łączna Wysokość').

Examples:

- ❌ Codes: 'CLS03', 'CLS04', 'CLPRP-01' - last code with a dash separator, unlike the others
- ❌ Names: 'Load Capacity' (title case), 'Power zone' (sentence case), 'ZoneCategories' (pascal case) - not consistent naming convention.
- ❌ Code: '74ts8bifnc74e7toe8n' - hard to interpret or identify in IFC data
- ✔️ Code 1: 'IsExternal', Name 1: 'is external', Code 2: 'AirTerminal', Name 2: 'air terminal' - both codes and names follow consistent naming schemas, and the codes are also interpretable.

### GEN-06
**Use of correct types**

Ensure that each item is assigned the appropriate type.

Examples:

- ✔️ 'Cement' is a `Class` with ClassType: `Material`.
- ✔️ 'Volume' is a `Property` (it should also have adequate Dimension: 3 0 0 0 0 0 0, and DataType: Real) 


## Dictionary

### DCT-01 
**Dictionary should be 'Active'**

Ensure that the dictionary is in the status 'Active'. It is possible to apply for verification while in 'Preview', but it is only to be granted after a positive review and a change of status to 'Active'.

### DCT-02 
**Dictionary names should not be misleading**

The dictionary's name should be original and must clearly and accurately describe its content and purpose. It should not be misleading or suggest any association with other dictionaries or organizations. Please do not include names of other dictionaries or organizations.

Examples: 

- ❌ "Uniclass4Infra" - may mislead users into thinking the dictionary is published by NBS and is part of the official Uniclass.
- ❌ "IFC Something" - not allowed, as the IFC term is reserved for the official publications of the IFC standard by buildingSMART.
- ⚠️ "Revit Classification" - it is recommended first obtain permission from the rightful owner (in this case, the Autodesk company).

## Class

### CLS-01
**Classes should be mapped to IFC correctly**

Each class must be mapped to IFC appropriately using 'RelatedIfcEntities' ('RelatedIfcEntityNamesList' in the import file).

Notes:

- Do not map a class to an abstract class, type, relation, or measure.
- Avoid using 'USERDEFINED' and 'NOTDEFINED' types.
- Before mapping a class to a 'Proxy', ensure there are no existing appropriate IFC entities by thoroughly searching available options.

### CLS-02
**Avoid circular dependencies**

Parent-child class relationships, defined with 'ParentClassCode', must form a tree structure without circular dependencies.

Examples:
- ❌ Class A is the parent of B, B is the parent of C, and C is the parent of A - circular chain.

### CLS-03
**Hierarchy of classes**

When multiple hierarchical levels can be distinguished, they shouldn't be modelled as a flat list but be structured in a clear and logical hierarchy.

Examples: 

- ❌ 'Column', 'Round Column', 'Rectangular Column' being a flat list.
- ✔️ 'Column' is a parent of 'Round Column' and 'Rectangular Column'.

### CLS-04
**Avoid syncretic classes**

Do not create classes that combine multiple aspects, such as material, class, and property, into a single class.

Examples: 

- ❌ 'External Steel Door' - incorrect because it combines information about the class, material, and property into one definition
- ✔️ Class: 'Door', Material: 'Steel', IsExternal: 'True'.

## Property

### PRP-01
**Numeric property metadata**

When a property is numeric (its `DataType` is `Integer` or `Real`), it should specify a `Dimension`. The `Unit` is optional, always assumed to be an SI unit, but it must match the Dimension when present. 

Examples:
- ✔️ Dimension: '1 0 -1 0 0 0 0', Unit: 'm/s' - correct specification of a speed property.
- ✔️ Dimension: '0 0 0 0 0 0 0' - dimensionless property (still, the Dimension is specified).
- ❌ Dimension: '1 0 0 0 0 0 0', 'Unit: 'h' - mismatch between dimension (length) and unit (time).
- ❌ Unit: 'W' - no dimension.

Ensure that the assigned unit corresponds correctly with the dimension. A mismatch between the dimension and unit can lead to confusion and errors.

Notes:

- Units in Property should all match to the same Dimension. Unit in ClassProperty should match Property's Dimension.
- In case of a physical quantity, specify dimension according to [International_System_of_Quantities](https://en.wikipedia.org/wiki/International_System_of_Quantities), as defined in ISO 80000-1. The order is: `length`, `mass`, `time`, `electric current`, `thermodynamic temperature`, `amount of substance`, and `luminous intensity`. 

Examples: 

- A dimension value of '1 1 -2 0 0 0 0' in property `Units` could have 'kilonewton', 'newton' in the list, but should not have 'millimetre'.
- A dimension value of '1 0 0 0 0 0 0' should not be assigned to unit 'kilogram'. (correct dimension is '0 1 0 0 0 0 0', length = 0, mass = 1, time = 0, electric current = 0, thermodynamic temperature = 0, amount of substance = 0, luminous intensity = 0)
- Speed (m/s) would be denoted as '1 0 -1 0 0 0 0'.


### PRP-02
**Avoid duplicating IFC properties**

When a close match property already exists in the IFC dictionary, it should be referenced in a dictionary rather than recreated. This way, we increase the usage of consistent terms, limiting model variations. Do not be discouraged by the naming of the property set, as properties are independent objects. 

Only invent new properties if no close match exists in IFC. Reusing properties from other active and verified dictionaries is also recommended. When adding a new property that is a specialisation of an existing one (for example, 'Net Weight Dry' could be a specialisation of 'Net Weight'), provide a relation to the existing property in IFC. Use `IsSimilarTo` relation type.

### PRP-03
**PSET_ prefix is forbidden**

The prefix 'PSET_' is reserved for the IFC standard. All other forms are acceptable (for example: 'My cool set'). 

A common practice for naming new sets for future versions of IFC is to use the 'cPSET_' prefix ('c' for custom/created). To propose new properties to extend existing IFC sets, one could use 'ePSET_' ('e' for extend). 

### PRP-04
**Property adequate data type**

The data type of a property must follow its definition, ensuring clarity and restricting the value to the appropriate type. The data type of a property must be one of the following: `Boolean`, `Character`, `Integer`, `Real`, `String`, `Time`.

Examples: 

- If a property can only be 'True' or 'False', the data type should be `Boolean`, not `String`.

### PRP-05
**Single aspect properties**

Do not combine multiple aspects within a single property. Each property must clearly represent only one aspect to ensure clarity and proper classification.

Examples: 

- A property 'Type of Window' with an allowed value list like 'Steel frame single', 'Wood frame single', 'Steel frame double', and 'Wood frame double' should be split into two properties: one for the frame material and one for the type (single or double).

### PRP-06
**Avoid unnecessary allowed values**

Allowed values should only be used when a property has a defined and countable number of options. Do not use allowed values for `Boolean` or list all possible `Integers` within a specific range (use min/max inc/exclusive instead).

Examples: 

- ❌ Allowed values: 'Oui', 'Non'(Yes/No in French) - instead use the `Boolean` data type directly.
- ❌ Allowed values: '1', '2', '3' - instead use `Integer` data type with MinInclusive=1 and MaxInclusive=3

### PRP-07
**Allowed values must be meaningful**

The `AllowedValues` of a property should provide a clear and distinct value. Avoid including values that represent a combination or mix of alternatives.

Examples: 

- Allowed values of a property 'Color': ✔️ 'red', ✔️ 'green', ❌ 'gradient' (inappropriate because it represents a combination of colours).

## ClassProperty

### CPR-01
**Only use active bSDD Properties in ClassProperty**

Properties associated with a `ClassProperty` should be defined within the same dictionary or another dictionary found in bSDD with a status `Active`. Otherwise, it is hard to ensure immutability and findability.

### CPR-02
**ClassProperty should have a PropertySet name**

Property sets should be named appropriately and consistently.

## Relations

### REL-01 
**Avoid circular relationships in RelationType**

Class and Property relations must avoid circular dependencies, ensuring clarity in hierarchical structures.

Examples:

- ❌ A 'IsParentOf' B, B 'IsParentOf' C, C 'IsParentOf' A.
- ❌ A 'IsPartOf' B, and B 'IsPartOf' A.
- ❌ A 'IsPartOf' B, A 'HasPart' C, and C 'IsEqualTo' A.

### REL-02 
**Avoid incorrect class types in RelationType**

Ensure that relations between classes are logically consistent and correctly assigned to the appropriate class types.

Examples:

- ❌ Class 'A' 'HasMaterial' GroupOfProperties 'B'. (not a Material)
- ❌ Material 'A' 'IsParentOf' Class 'B'. (Material can only by a parent of other Material)

### REL-03
**Relations must be meaningful**

Relationships between classes or properties should be logical and purposeful.

Examples: 

- ❌ `IsChildOf` relation between a 'Column' and 'Concrete' class - illogical.
- ✔️ `IsChildOf` relation between a 'Round Column' and 'Column' class.
