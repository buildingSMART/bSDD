# bSDD verification procedure

## Overview

| Code   | Item                                                         |
| ------ | ------------------------------------------------------------ |
| [GEN-01](#gen-01) | English version should be provided for all relevant fields   |
| [GEN-02](#gen-02) | Translations should be accurate without changes              |
| [GEN-03](#gen-03) | Names should be clear and easy to interpret                  |
| [GEN-04](#gen-04) | Names and codes should follow a consistent naming convention |
| [DCT-01](#dct-01)   | Dictionary should have the status 'Active'                         |
| [DCT-02](#dct-02)   | Dictionary name should not be misleading or suggest they represent something else |
| [DCT-03](#dct-03)   | Dictionary procedure information should be complete and transparent |
| [PRP-01](#prp-01)   | Numeric property should have Unit, Dimension, and correct data type |
| [PRP-02](#prp-02)   | Duplicated IFC properties should be avoided                  |
| [PRP-03](#prp-03)   | Using PSET_ in property or property set names is not allowed |
| [PRP-04](#prp-04)   | Data type of property should match the definition            |
| [PRP-05](#prp-05)   | Avoid unnecessary allowed values when simple data types can represent the value |
| [CLS-01](#cls-01)   | Classes should be mapped to IFC correctly                    |
| [CLS-02](#cls-02)   | Avoid circular dependencies in ParentClassCode               |
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
**English version should be provided for all relevant fields**

The dictionary should include English translations for all translatable fields across items such as properties, classes, and allowed values. These fields include name, definition, description, examples, and synonyms.

### GEN-02
**Translations should be accurate without changes**

Translations should be precise and faithful to the original content without adding additional explanations or removing any part of the original sentences.

### GEN-03
**Names should be clear and easy to interpret**

The name of each item should be clear and help users understand the concept, which enhances usability and readability in software.

Notes:

- Avoid adding prefixes to each item in the dictionary, as this can complicate search and filtering for others.
- Try to avoid acronyms, as they may vary between languages and can have different meanings in different contexts.
- Avoid using abstract or generic placeholders like Class1, Class2, etc., which do not provide meaningful information.

Examples:

- A class name like `Class 20-18.7` doesn't convey the actual meaning of the class.
- The property name using the acronym `FR-MR` could stand for "Fire Rating Minimal Requirement" or "Mechanical Resistance with country code FR," leading to confusion.
- Prefixing all classes with `MAT_` for "My Architect Technologies" could mislead users searching for material-related items.

### GEN-04
**Names and codes should follow a consistent naming convention**

While no specific naming convention is required, using a consistent style for names and codes improves searchability and readability.

Notes:

- Pascal case(CustomClass), sentence case(Custom class), title case(Custom Class), snake case(custom_class), and kebab case(custom-class) are all common naming conventions. Whitespace, dots, dashes, and underscores are acceptable for use in both names and codes.

Examples:

- The names `CLS03 Load Capacity` (title case), `CLS04 Power zone` (sentence case), and `CLPRP-01 ZoneCategories` (pascal case, code with a dash separator) would be clearer if the same convention were used across the dictionary.

## Dictionary

### DCT-01 
**Dictionary should have the status 'Active'**

Ensure that the dictionary is in the status `Active`. Only active dictionaries can be included in API requests and displayed in the bSDD search interface.

### DCT-02 
**Dictionary name should not be misleading or suggest they represent something else**

The dictionary's name must clearly and accurately describe its content and purpose. It should not be misleading or suggest any association with other dictionaries or organizations. Please do not include names of other dictionaries or organizations.

Examples: 

- Dictionary name "Uniclass for Infra" may mislead users into thinking the dictionary is published by NBS and is part of the Uniclass.
- Dictionary name "IFC Something" is not allowed, as no dictionary may include `IFC` in its title.

### DCT-03
**Dictionary procedure information should be complete and transparent**

The dictionary must provide complete and transparent information, including its license, official website, quality assurance procedures, and a contact email for change requests. 

## Property

### PRP-01
**Numeric property should have Unit, Dimension, and correct data type**

For numeric properties, both Unit and Dimension must be provided. The correct data type should be either `Real` or `Integer`.

Notes:

- For properties without units, use Unit: `{"Code":"unitless","Name":"unitless","Symbol":"-"}` and Dimension: `0000000`

### PRP-02
**Duplicated IFC properties should be avoided**

Avoid replicating IFC properties with a 1:1 correspondence. When necessary, use a relation such as `IsEqualTo` or `IsSimilarTo` instead of direct duplication. This approach ensures better data integrity and reduces redundancy.

### PRP-03
**Using PSET_ in property or property set names is not allowed**

The prefix `PSET_` is reserved by IFC. Property sets should be named differently to avoid conflicts. This helps implementers to correctly group properties and display them in user interfaces.

### PRP-04
**Data type of property should match the definition**

The data type of a property must follow its definition, ensuring clarity and restricting the value to the appropriate type. The data type of a property must be one of the following: `Boolean`, `Character`, `Integer`, `Real`, `String`, `Time`.

Examples: 

- If a property has a `String` type but its definition specifies only `True` or `False`, the data type should be changed to `Boolean`.

### PRP-05
**Avoid unnecessary allowed values when simple data types can represent the value**

Allowed values should be used only when a property has a defined and countable number of options. Do not use allowed values to localize basic data types such as `Boolean` or `Integer`.

Examples: 

- Instead of creating an allowed value list for options like `True/False` or `Yes/No` or `Oui/Non`(Yes/No in French), use the `Boolean` data type directly.

## Class

### CLS-01
**Classes should be mapped to IFC correctly**

Each class must be mapped to IFC appropriately using `RelatedIfcEntities`.

Notes:

- Do not map a class to an abstract class, type, relation, or measure.
- Avoid using `USERDEFINED` and `NOTDEFINED` types.
- Before mapping a class to a `Proxy`, ensure there are no existing appropriate IFC entities by thoroughly searching available options.

### CLS-02
**Avoid circular dependencies in ParentClassCode**

Parent-child class relationships must be clearly defined, with no circular dependencies.

Examples:

- Circular case: A's parent is B, B's parent is C, and C's parent is A.

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

