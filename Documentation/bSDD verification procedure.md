# bSDD verification procedure

## Overview

| Code   | Item                                                         |
| ------ | ------------------------------------------------------------ |
| [GEN-01](#gen-01) | Must have English version   |
| [GEN-02](#gen-02) | Translations should be accurate              |
| [GEN-03](#gen-03) | Names should be clear and easy to interpret                  |
| [GEN-04](#gen-04) | Follow a consistent naming convention |
| [DCT-01](#dct-01)   | Dictionary should be 'Active'                         |
| [DCT-02](#dct-02)   | Dictionary name should not be misleading |
| [DCT-03](#dct-03)   | Dictionary metadata should be complete |
| [CLS-01](#cls-01)   | Classes should be mapped to IFC correctly                    |
| [CLS-02](#cls-02)   | Avoid circular dependencies               |
| [PRP-01](#prp-01)   | Numeric property metadata |
| [PRP-02](#prp-02)   | Avoid duplicating IFC properties                 |
| [PRP-03](#prp-03)   | PSET_ prefix is forbidden |
| [PRP-04](#prp-04)   | Data type of property should match the definition            |
| [PRP-05](#prp-05)   | Avoid unnecessary allowed values |
| [CPR-01](#cpr-01)  | External properties should not be used in ClassProperty      |
| [CPR-02](#cpr-02)  | ClassProperty should have PropertySet named                  |
| [CRE-01](#cre-01)  | Avoid circular relationships in RelationType                 |
| [CRE-02](#cre-02)  | Avoid incorrect class types in RelationType                  |
| [PRE-01](#pre-01)  | Avoid unclear circular relationships in RelationType         |
| [SEM-01](#sem-01) | Correct types should be assigned                             |
| [SEM-02](#sem-02) | Dimension-Unit match must be accurate                        |
| [SEM-03](#sem-03) | Classes with multiple levels should be structured hierarchically |
| [SEM-04](#sem-04) | Relations must be meaningful                                 |
| [SEM-05](#sem-05) | Avoid creating syncretic classes that combine multiple aspects |
| [SEM-06](#sem-06) | Avoid mixing multiple aspects within a single property       |
| [SEM-07](#sem-07) | AllowedValues must be distinct and represent alternative options |

**Verification item code naming convention: GEN (General), DCT (Dictionary), CLS (Class), PRP (Property), ALV (AllowedValue), CPR (ClassProperty), CRE (ClassRelation), PRE (PropertyRelation), SEM (Semantics) with two digit number using dash separator.*

## General

### GEN-01 
**Must have English version**

As per ISO 12006-3:2022, the dictionary should include an English version of all the relevant content for all translatable fields, such as properties, classes, and allowed values. These fields include name, definition, description, and examples. The existence of other language translations is optional.

### GEN-02
**Translations should be accurate**

The translations are optional, but when they exist, all the translations should be precise and faithful to the original content. Translations can not extend the explanations or remove any part of the original sentences. 

Example:

|  ENG (original) | German (translation) |   |
| ---- | ----- | ----- |
| _The wall represents a vertical construction that may bound or subdivide spaces..._ | _Vertikale Konstruktion zur Abgrenzung oder Unterteilung von Räumen... Anmerkung: Nach ISO 6707-1 ist eine vertikale Konstruktion in der Regel aus Mauerwerk oder Beton, ..._ | ❌ FAIL: The German translation has additional sentence referring ISO - the two are therefore not consistent. |


### GEN-03
**Names should be clear and easy to interpret**

Unlike codes, the name of each item must be clear and help users understand the concept to enhance usability.

Notes:

- Avoid adding prefixes to each item in the dictionary, as this can complicate search and filtering.
- Try to avoid acronyms, as they may vary between languages and can have different meanings in different contexts.
- Avoid using abstract or generic placeholders like Class1, Class2, etc., which do not provide meaningful information.

Examples:

- ❌ Name: `Class 20-18.7` - doesn't convey the actual meaning of the class.
- ❌ Name: `FR-MR` - an acronym that could stand for many things.
- ❌ Name: `ABC_Wall` - unnecessary prefix.

### GEN-04
**Follow a consistent naming convention**

Names and codes should follow consistent naming conventions. While no specific naming convention is required, using a consistent style for names and codes improves searchability and readability.

Notes:

- Common naming conventions include: Pascal case (_CustomClass_), sentence case (_Custom class_), title case (_Custom Class_), snake case (_custom_class_), and kebab case (_custom-class_).
- Whitespace, dots, dashes, and underscores are acceptable for use in both names and codes.
- It is recommended that the codes are also easily recognizable, as they are the pieces of information that get stored in the data and are displayed by most of the software without integration with the bSDD. The reason for having both is that names can be translated, unliked the codes, and some software doesn't allow special characters or whitespaces in the codes (e.g. 'Łączna Wysokość').

Examples:

- ❌ Codes: `CLS03`, `CLS04`, `CLPRP-01` - last code with a dash separator, unlike the others
- ❌ Names: `Load Capacity` (title case), `Power zone` (sentence case), `ZoneCategories` (pascal case) - not consistent naming convention.
- ❌ Code: `74ts8bifnc74e7toe8n` - hard to interpret or identify in IFC data
- ✔️ Code 1: `IsExternal`, Name 1: `is external`, Code 2: `AirTerminal`, Name 2: `air terminal` - both codes and names follow consistent naming schemas, and the codes are also interpretable.

## Dictionary

### DCT-01 
**Dictionary should be 'Active'**

Ensure that the dictionary is in the status `Active`. It is possible to apply for verification while in 'Preview', but it is only to be granted after a positive review and a change of status to 'Active'.

### DCT-02 
**Dictionary names should not be misleading**

The dictionary's name should be original and must clearly and accurately describe its content and purpose. It should not be misleading or suggest any association with other dictionaries or organizations. Please do not include names of other dictionaries or organizations.

Examples: 

- ❌ "Uniclass4Infra" - may mislead users into thinking the dictionary is published by NBS and is part of the official Uniclass.
- ❌ "IFC Something" - not allowed, as the IFC term is reserved for the official publications of the IFC standard by buildingSMART.
- ⚠️ "Revit Classification" - it is recommended first obtain permission from the rightful owner (in this case, the Autodesk company).

### DCT-03
**Dictionary metadata should be complete**

While the minimum information for publishing in bSDD is low, the requirements for verification are set higher. The dictionary must provide complete and transparent information, including its license, official website, quality assurance procedures, and a contact email for change requests. 🚧

The required Dictionary fields are:
- QualityAssuranceProcedure
- ChangeRequestEmailAddress
- License
- LicenseUrl
- 🚧

## Class

### CLS-01
**Classes should be mapped to IFC correctly**

Each class must be mapped to IFC appropriately using `RelatedIfcEntities` (`RelatedIfcEntityNamesList` in the import file).

Notes:

- Do not map a class to an abstract class, type, relation, or measure.
- Avoid using `USERDEFINED` and `NOTDEFINED` types.
- Before mapping a class to a `Proxy`, ensure there are no existing appropriate IFC entities by thoroughly searching available options.

### CLS-02
**Avoid circular dependencies**

Parent-child class relationships, defined with `ParentClassCode`, must form a tree structure without circular dependencies.

Examples:
- ❌ Class A is the parent of B, B is the parent of C, and C is the parent of A - circular chain.

## Property

### PRP-01
**Numeric property metadata**

When a property is numeric (its `DataType` is `Integer` or `Real`), it should specify a `Dimension`. The `Unit` is optional, always assumed to be an SI unit, but it must match the Dimension when present. 

Examples:
- ✔️ Dimension: '1 0 -1 0 0 0 0', Unit: 'm/s' - correct specification of a speed property.
- ✔️ Dimension: '0 0 0 0 0 0 0' - dimensionless property (still, the Dimension is specified).
- ❌ Dimension: '1 0 0 0 0 0 0', 'Unit: 'h' - mismatch between dimension (length) and unit (time).
- ❌ Unit: 'W' - no dimension. 
  
### PRP-02
**Avoid duplicating IFC properties**

When a close match property already exists in the IFC dictionary, it should be referenced in a dictionary rather than recreated. This way, we increase the usage of consistent terms, limiting model variations. Do not be discouraged by the naming of the property set, as properties are independent objects. 

Only invent new properties if no close match exists in IFC. Reusing properties from other active and verified dictionaries is also recommended. When adding a new property that is a specialisation of an existing one (for example, 'Net Weight Dry' could be a specialisation of 'Net Weight'), provide a relation to the existing property in IFC. Use `IsSimilarTo` relation type.

### PRP-03
**PSET_ prefix is forbidden**

The prefix 'PSET_' is reserved for the IFC standard. All other forms are acceptable (example: 'My cool set'). 

A common practice for naming new sets that are to be proposed for being part of the future version of IFC is to use 'cPSET_' prefix ('c' for custom/created). For proposing new properties to extend existing property sets, one could use 'ePSET_'. Those are allowed, and not mandatory. 

### PRP-04
**Data type of property should match the definition**

The data type of a property must follow its definition, ensuring clarity and restricting the value to the appropriate type. The data type of a property must be one of the following: `Boolean`, `Character`, `Integer`, `Real`, `String`, `Time`.

Examples: 

- If a property has a `String` type but its definition specifies only `True` or `False`, the data type should be changed to `Boolean`.

### PRP-05
**Avoid unnecessary allowed values**

Allowed values should be used only when a property has a defined and countable number of options. Do not use allowed values to localize basic data types such as `Boolean` or `Integer`.

Examples: 

- Instead of creating an allowed value list for options like `True/False` or `Yes/No` or `Oui/Non`(Yes/No in French), use the `Boolean` data type directly.


## ClassProperty

### CPR-01
**External properties should not be used in ClassProperty**

Properties associated with a ClassProperty must be defined within the same dictionary or in another existing internal dictionary.

### CPR-02
**ClassProperty should have PropertySet named**

Property sets should be properly named and consistently defined within the appropriate context.

## Relations

### CRE-01 
**Avoid circular relationships in RelationType**

Class relations must avoid circular dependencies, ensuring clarity in hierarchical structures.

Examples:

- Circular case 1: A `IsPartOf` B, and B `IsPartOf` A.
- Circular case 2: A `IsPartOf` B, A `HasPart` C, and C `IsEqualTo` A.

### CRE-02 
**Avoid incorrect class types in RelationType**

Ensure that relations between classes are logically consistent and correctly assigned to the appropriate class types.

Examples:

- Incorrect relation 1: ClassA `HasMaterial` GroupOfPropertiesB.
- Incorrect relation 2: MaterialA `IsParentOf` ClassB.

### PRE-01
**Avoid unclear circular relationships in RelationType**

Relationships between properties should be clearly defined, avoiding circular dependencies that can obscure meaning.

Examples:

- Circular case: A `IsSimilarTo` B, B `IsEqualTo` C, and A `IsEqualTo` C.


## Semantics

### SEM-01
**Correct types should be assigned**

Ensure that each item is assigned the appropriate type. This helps maintain consistency and clarity across the data structure.

Examples:

- `Cement` should be classified as a `Class` with ClassType: `Material`.
- `Volume` should be classified as a `Property`.

### SEM-02
**Dimension-Unit match must be accurate**

Ensure that the assigned unit corresponds correctly with the dimension. A mismatch between the dimension and unit can lead to confusion and errors.

Notes:

- Units in Property should all match to the same Dimension. Unit in ClassProperty should match Property's Dimension.
- In case of a physical quantity, specify dimension according to [International_System_of_Quantities](https://en.wikipedia.org/wiki/International_System_of_Quantities), as defined in ISO 80000-1. The order is: `length`, `mass`, `time`, `electric current`, `thermodynamic temperature`, `amount of substance`, and `luminous intensity`. 

Examples: 

- A dimension value of `1 1 -2 0 0 0 0` in property `Units` could have `kilonewton`, `newton` in the list, but should not have `millimetre`.
- A dimension value of `1 0 0 0 0 0 0` should not be assigned to unit `kilogram`. (correct dimension is `0 1 0 0 0 0 0`, length = 0, mass = 1, time = 0, electric current = 0, thermodynamic temperature = 0, amount of substance = 0, luminous intensity = 0)
- Speed (m/s) would be denoted as `1 0 -1 0 0 0 0`.

### SEM-03
**Classes with multiple levels should be structured hierarchically**

When multiple hierarchical levels exist within a class, they should be structured in a clear and logical hierarchy.

Notes: 

- A flat, unrelated list should not be approved without considering potential hierarchical relationships.

Examples: 

- If there are `Columns` with subtypes such as `Round Columns` and `Rectangular Columns`, these subtypes should be organized as children of the `Columns` class.

### SEM-04
**Relations must be meaningful**

Relationships between classes or properties should be logical and purposeful. Avoid creating relationships that do not make sense or serve a clear function.

Examples: 

- An `IsChildOf` relation between `Column` and `Concrete` is illogical and should be avoided.

### SEM-05
**Avoid creating syncretic classes that combine multiple aspects**

Do not create classes that merge multiple dimensions, such as material, class, and property, into a single entity.

Examples: 

- A class like `External Steel Door` is incorrect because it combines information about the class, material, and property into one definition, which complicates classification.

### SEM-06
**Avoid mixing multiple aspects within a single property**

Do not combine multiple aspects within a single property. Each property must clearly represent only one aspect to ensure clarity and proper classification.

Examples: 

- A `Type of Window` property with an allowed value list like `Steel frame single`, `Wood frame single`, `Steel frame double`, and `Wood frame double` should be split into two properties: one for the frame material and one for the type (single or double).

### SEM-07
**AllowedValues must be distinct and represent alternative options**

The `AllowedValues` of a property should provide clear and distinct options. Avoid including values that represent a combination or mix of alternatives.

Examples: 

- For a `Color` property that allows values like `red` and `green`, a value like `gradient` would be inappropriate because it represents a combination rather than a clear, distinct alternative.

