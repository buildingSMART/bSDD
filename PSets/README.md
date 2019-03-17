[![Build status](https://ci.appveyor.com/api/projects/status/n8fwfxte8uvw8pl7/branch/master?svg=true)](https://ci.appveyor.com/project/klacol/bsdd/branch/master)


# PSets - Property Set Extension of the IFC schema

Just for testing, Do not use. This is an repo, where we test, how the PSets could be presentend and maintainted with Git/GitHub.

# Schema Specifications
Official releases of the PSets are listed here. PSets releases add semantics to the [IFC schema](https://github.com/buildingSMART/IFC).

Linked released indicate those that are officially released and actively supported.

IFC-Release	| Date |	Identifier  |	Documentation	 | ISO Standard |	Release Notes 
------------|------|--------------|----------------|--------------|---------------
4.0.2.1 | 2017 |IFC 4 Add2 TC1 |               |              |[Download](http://www.buildingsmart-tech.org/ifc/IFC4/Add2TC1/html/link/listing-ifc4_add2.htm)|
4.0.2.0 | 2016 | IFC 4 Add2   |                |              |[Download](http://www.buildingsmart-tech.org/ifc/IFC4/Add2/html/link/listing-ifc4_add2.htm)|
4.0.0.5	| 2013 | IFC4	final   | ISO 16739:2013 |	            | [Download](http://www.buildingsmart-tech.org/ifc/IFC4/final/html) (A. Computer interpretable listings)
2.3.0.1	| 2007 | IFC2X3_FINAL | IFC 2x3 TC1	   |   |[Download](http://www.buildingsmart-tech.org/specifications/pset-releases/Psets%20for%20IFC2x3%20TC1)
2.3.0.0	| 2006 | IFC2X3_FINAL	| IFC 2x3	       ||

# Version Notation
IFC versions are identified using the sematic versioning notation "Major.Minor.Addendum.Corrigendum".

- Major versions consist of scope expansions or deletions and may have changes that break compatibility.
- Minor versions consist of feature extensions, where compatibility is guaranteed for the "core" schema, but not for other definitions.
- Addendums consist of improvements to existing features, where the schema may change but upward compatibility is guaranteed.
- Corrigendums consist of improvements to documentation, where the schema does not change though deprecation is possible.

# Which version do I use?
The latest version, IFC 4.1 is recommended for all current developments, which is fully backward compatible with IFC 4.0. Core definitions within IFC 4.1 and 4.0 are backward compatible with IFC 2.3.

# Serialization formats

The PSets are provides in different serialization formats. The PSetManager runs on every commit and creates and commits semanticall identical PSet in the other serialization formats.

The build chain is as following:

1. PSets in YAML folder
2. JSON : Javascript object notation
3. RESX : Ressource files
4. XML  : Exentable Markup Language
5. bSDD : buildingSMART Data Dictionary (online)

The origin are the PSets in the subfolder YAML. All changes, translations go in there. PullRequests for other folders will be ignored.


