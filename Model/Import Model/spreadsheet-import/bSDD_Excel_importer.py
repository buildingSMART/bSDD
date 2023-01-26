import pandas as pd
import sys
from pprint import pprint
import json


EXCEL_PATH = sys.argv[0]
JSON_PATH = sys.argv[1]

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

excel_domain                    = pd.read_excel(xls, 'Domain', skiprows=5, usecols="C:Q", true_values="TRUE", false_values="FALSE")
excel_classification            = pd.read_excel(xls, 'Classification', skiprows=5, usecols="C:AA", true_values="TRUE", false_values="FALSE")
excel_property                  = pd.read_excel(xls, 'Property', skiprows=5, usecols="C:AT", true_values="TRUE", false_values="FALSE")
excel_classificationproperty    = pd.read_excel(xls, 'ClassificationProperty', usecols="C:T", skiprows=5, true_values="TRUE", false_values="FALSE")
excel_classificationrelation    = pd.read_excel(xls, 'ClassificationRelation', usecols="C:G", skiprows=5, true_values="TRUE", false_values="FALSE")

# Load JSON template
file = open(JSON_PATH)
json_data = json.load(file)

""" MAP """

fill_fields(excel_domain, json_data)

""" EXPORT """

json_data = excel_domain.to_json()
print(json_data)