# Hackathon - Use case walkthrough

The Python console app provides the API calls needed to implement the use case presented in the Hackathon on 2021 3th of March.

see: https://github.com/buildingSMART/bSDD/blob/master/2021%20Hackathon/tutorial.md

1- Get the list of available domains in bSDD

2- Get the classes linked to IfcWall from NL-SfB 2005 domain 

3- Get the properties available in (16.21) funderingsconstructies; keerwanden, grondkerende wanden classification

For ease of use, the results of step 1 and 2 are saved to csv files to copy/paste the URIs as parameters of the following calls 


# 1- List of available domains

Call Get_Domains() 

## Ressource

 /api/Domain/v2
 
## Result

```json
[
  {
    "namespaceUri": "http://identifier.buildingsmart.org/uri/buildingsmart/ifc-4.3",
    "name": "IFC",
    "version": "4.3",
    "organizationNameOwner": "buildingSMART",
    "defaultLanguageCode": "EN",
    "license": "Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International",
    "licenseUrl": "https://creativecommons.org/licenses/by-nc-nd/4.0/",
    "qualityAssuranceProcedure": "bSI process",
    "qualityAssuranceProcedureUrl": "https://www.buildingsmart.org/about/bsi-process"
  },
  
   {
    "namespaceUri": "http://identifier.buildingsmart.org/uri/nlsfb/nlsfb2005-2.2",
    "name": "NL-SfB 2005",
    "version": "2.2",
    "organizationNameOwner": "NL-SfB",
    "defaultLanguageCode": "nl-NL"
  }
  ]
```

 ## Results saved to 
 bSDD_Domains.csv

# 2- Get the classes linked to IfcWall from a domain 

Call Get_Classes_Linked_To_IFC()
 
## Ressource

/api/SearchListOpen/v2

## Parameters

Need to input the parameters into the console
	- DomainNamespaceUri : http://identifier.buildingsmart.org/uri/nlsfb/nlsfb2005-2.2
	- RelatedIfcEntity: IfcWall
	
## Result

The call will return 22 classification linked to IfcWall

```json
{
  "numberOfClassificationsFound": 22,
  "domains": [
    {
      "name": "NL-SfB 2005",
      "namespaceUri": "http://identifier.buildingsmart.org/uri/nlsfb/nlsfb2005-2.2",
      "classifications": [
        {
          "name": "(16.21) funderingsconstructies; keerwanden, grondkerende wanden",
          "namespaceUri": "http://identifier.buildingsmart.org/uri/nlsfb/nlsfb2005-2.2/class/16.21"
        },
        {
          "name": "(16.22) funderingsconstructies; keerwanden, waterkerende wanden",
          "namespaceUri": "http://identifier.buildingsmart.org/uri/nlsfb/nlsfb2005-2.2/class/16.22"
        }
        ...
      ]
    }
  ]
}
Response header
```

 ## Results saved to 
NL-SfB 2005_Classes.csv

# 3- Get the properties available for a classification

Call Get_Classification_Properties()
 
## Ressource

/api/Classification/v2

## Parameters

Need to input the parameters into the console
	- ClassificationUri : http://identifier.buildingsmart.org/uri/nlsfb/nlsfb2005-2.2/class/16.21
	- ClassificationName: 16.21 (we just use it for a nice filename, nothing else)
	
## Result

2 properties found, each from IFC domain
```json
{
  "relatedIfcEntityNames": [
    "IfcWall"
  ],
  "parentClassificationReference": {
    "namespaceUri": "http://identifier.buildingsmart.org/uri/nlsfb/nlsfb2005-2.2/class/16.2",
    "name": "(16.2) funderingsconstructies; keerwanden",
    "code": "16.2"
  },
  "classificationProperties": [
    {
      "propertyDomainName": "IFC",
      "propertyNamespaceUri": "http://identifier.buildingsmart.org/uri/buildingsmart/ifc-4.3/prop/LoadBearing",
      "name": "LoadBearing",
      "description": "Indicates whether the object is intended to carry loads TRUE or not FALSE .",
      "propertySet": "Pset_WallCommon",
      "predefinedValue": "",
      "dataType": "boolean"
    },
    {
      "propertyDomainName": "IFC",
      "propertyNamespaceUri": "http://identifier.buildingsmart.org/uri/buildingsmart/ifc-4.3/prop/IsExternal",
      "name": "IsExternal",
      "description": "Indication of whether the junction box type is allowed for exposure to outdoor elements set TRUE where external exposure is allowed .",
      "propertySet": "Pset_WallCommon",
      "predefinedValue": "TRUE",
      "dataType": "boolean"
    }
  ],
  "code": "16.21",
  "namespaceUri": "http://identifier.buildingsmart.org/uri/nlsfb/nlsfb2005-2.2/class/16.21",
  "name": "(16.21) funderingsconstructies; keerwanden, grondkerende wanden",
  "status": "Preview",
  "activationDateUtc": "0001-01-01T00:00:00",
  "versionDateUtc": "0001-01-01T00:00:00"
}
```

## Results saved to 

16.21_Properties.csv
