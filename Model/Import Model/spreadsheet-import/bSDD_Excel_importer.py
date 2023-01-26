import pandas as pd
import sys
from pprint import pprint
import json


EXCEL_PATH = sys.argv[0]
# EXCEL_PATH = r"C:\Code\bSDD\ignore\bSDD Finnish Granlund\bSDD_Excel_MEP-Fin.xlsx"
JSON_PATH = sys.argv[1]
# JSON_PATH = r"C:\Code\bSDD\Model\Import Model\spreadsheet-import\bsdd-import-model.json"

""" METHODS """

def fill_fields(dataframe, json_file):
    for index, row in dataframe.iterrows():
        for column_name, column_data in dataframe.iteritems():
            try:
                # Find such column in JSON and replace with Excel data
                json_file[column_name] = column_data[0]
            except KeyError:
                print("No such property in the JSON")
    return json_file

""" IMPORT """

# xls = pd.ExcelFile(sys.argv[0])
xls = pd.ExcelFile(EXCEL_PATH)

excel_domain                    = pd.read_excel(xls, 'Domain', skiprows=5, usecols="C:Q", true_values="TRUE", False_values="False")
excel_classification            = pd.read_excel(xls, 'Classification', skiprows=5, usecols="C:AA", true_values="TRUE", False_values="False")
excel_property                  = pd.read_excel(xls, 'Property', skiprows=5, usecols="C:AT", true_values="TRUE", False_values="False")
excel_classificationproperty    = pd.read_excel(xls, 'ClassificationProperty', usecols="C:T", skiprows=5, true_values="TRUE", False_values="False")
excel_classificationrelation    = pd.read_excel(xls, 'ClassificationRelation', usecols="C:G", skiprows=5, true_values="TRUE", False_values="False")

# Load JSON template
file = open(JSON_PATH)
json_data = json.load(file)

""" PARTS """

Domain = {
    "OrganizationCode": None,
    "DomainCode": None,
    "DomainVersion": None,
    "DomainName": None,
    "ReleaseDate": None,
    "Status": None,
    "MoreInfoUrl": None,
    "UseOwnUri": False,
    "DomainNamespaceUri": None,
    "LanguageIsoCode": None,
    "LanguageOnly": False,
    "License": None,
    "LicenseUrl": None,
    "QualityAssuranceProcedure": None,
    "QualityAssuranceProcedureUrl": None,
    "Classifications": [],
    "Properties": [],
    "Materials": []
}

Classification = {
    "Code": None,
    "Uid": None,
    "OwnedUri": None,
    "Name": None,
    "Definition": None,
    "Status": None,
    "ActivationDateUtc": "2022-05-12T00:00:00+02:00",
    "RevisionDateUtc": None,
    "VersionDateUtc": "2022-05-12T00:00:00+02:00",
    "DeActivationDateUtc": None,
    "VersionNumber": None,
    "RevisionNumber": None,
    "ReplacedObjectCodes": [],
    "ReplacingObjectCodes": [],
    "DeprecationExplanation": None,
    "CreatorLanguageIsoCode": None,
    "VisualRepresentationUri": None,
    "CountriesOfUse": [],
    "SubdivisionsOfUse": [],
    "CountryOfOrigin": None,
    "DocumentReference": None,
    "Synonyms": [],
    "ReferenceCode": None,
    "ClassificationRelations": [],
    "ClassificationType": None,
    "ParentClassificationCode": None,
    "RelatedIfcEntityNamesList": [],
    "ClassificationProperties": []
    }

ClassificationRelation = {
    "RelationType": None,
    "RelatedClassificationUri": None,
    "RelatedClassificationName": None,
    "Fraction": None
    }

AllowedValue = {
    "NamespaceUri": None,
    "Code": None,
    "Value": None,
    "Description": None,
    "SortNumber": None
    }

ClassificationProperty = {
    "AllowedValues": [],
    "Code": None,
    "Description": None,
    "IsRequired": None,
    "IsWritable": None,
    "MaxExclusive": None,
    "MaxInclusive": None,
    "MinExclusive": None,
    "MinInclusive": None,
    "Pattern": None,
    "PredefinedValue": None,
    "PropertyCode": None,
    "PropertyNamespaceUri": None,
    "PropertySet": None,
    "PropertyType": None,
    "SortNumber": None,
    "Symbol": None,
    "Unit": None
    }

Property = {
    "Code": None,
    "Uid": None,
    "OwnedUri": None,
    "Name": None,
    "Definition": None,
    "Status": None,
    "ActivationDateUtc": "2022-05-12T00:00:00+02:00",
    "RevisionDateUtc": None,
    "VersionDateUtc": "2022-05-12T00:00:00+02:00",
    "DeActivationDateUtc": None,
    "VersionNumber": None,
    "RevisionNumber": None,
    "ReplacedObjectCodes": [],
    "ReplacingObjectCodes": [],
    "DeprecationExplanation": None,
    "CreatorLanguageIsoCode": None,
    "VisualRepresentationUri": None,
    "CountriesOfUse": [],
    "SubdivisionsOfUse": [],
    "CountryOfOrigin": None,
    "DocumentReference": None,
    "Description": None,
    "Example": None,
    "ConnectedPropertyCodes": [],
    "PhysicalQuantity": None,
    "Dimension": None,
    "DimensionLength": None,
    "DimensionMass": None,
    "DimensionTime": None,
    "DimensionElectricCurrent": None,
    "DimensionThermodynamicTemperature": None,
    "DimensionAmountOfSubstance": None,
    "DimensionLuminousIntensity": None,
    "MethodOfMeasurement": None,
    "DataType": None,
    "PropertyValueKind": None,
    "MinInclusive": None,
    "MaxInclusive": None,
    "MinExclusive": None,
    "MaxExclusive": None,
    "Pattern": None,
    "IsDynamic": False,
    "DynamicParameterPropertyCodes": [],
    "Units": [],
    "AllowedValues": [],
    "TextFormat": None,
    "PropertyRelations": []
    }

PropertyRelation = {
    "RelationType": None,
    "RelatedPropertyUri": None,
    "RelatedPropertyName": None
    }

Material = {
    "Code": None,
    "Uid": None,
    "OwnedUri": None,
    "Name": None,
    "Definition": None,
    "Status": None,
    "ActivationDateUtc": "2022-05-12T00:00:00+02:00",
    "RevisionDateUtc": None,
    "VersionDateUtc": "2022-05-12T00:00:00+02:00",
    "DeActivationDateUtc": None,
    "VersionNumber": None,
    "RevisionNumber": None,
    "ReplacedObjectCodes": [],
    "ReplacingObjectCodes": [],
    "DeprecationExplanation": None,
    "CreatorLanguageIsoCode": None,
    "VisualRepresentationUri": None,
    "CountriesOfUse": [],
    "SubdivisionsOfUse": [],
    "CountryOfOrigin": None,
    "DocumentReference": None,
    "Synonyms": [],
    "ReferenceCode": None,
    "ClassificationRelations": [],
    "ParentMaterialCode": None,
    "MaterialProperties": []
    }

""" MAP """

fill_fields(excel_domain, json_data)

""" EXPORT """

json_data = excel_domain.to_json()
print(json_data)