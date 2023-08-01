"""
This is a script to convert an Excel file made using the attached bSDD template to a properly structured bSDD JSON file. 

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

__license__ = "MIT"
__author__ = "Artur Tomczak"
__copyright__ = "Copyright (c) 2023 buildingSMART International Ltd."
__version__ = "1.0.0"
__email__ = "bsdd_support@buildingsmart.org"
"""

import sys
import json
from ast import literal_eval 
from copy import deepcopy
from JSON_templates import Domain, Classification, ClassificationRelation, AllowedValue
from JSON_templates import ClassificationProperty, Property, PropertyRelation, Material
import numpy as np
import pandas as pd


def load_excel(EXCEL_PATH):
    """Parses an excel file from path. Note: only works on provided template file.

    :param EXCEL_PATH: Path to an excel file 
    :type EXCEL_PATH: str
    :return: Dictionary of Pandas dataframes with parsed Excel data
    :rtype: dict
    """
        
    excel_df = pd.ExcelFile(EXCEL_PATH)

    excel={}
    excel['domain'] = pd.read_excel(excel_df, 'Domain', skiprows=5, usecols="C:Q", true_values="TRUE")
    excel['classification'] = pd.read_excel(excel_df, 'Classification', skiprows=5, usecols="C:AB", true_values="TRUE")
    excel['material'] = pd.read_excel(excel_df, 'Material', skiprows=5, usecols="C:Y", true_values="TRUE")
    excel['property'] = pd.read_excel(excel_df, 'Property', skiprows=5, usecols="C:AU", true_values="TRUE")
    excel['classificationproperty'] = pd.read_excel(excel_df, 'ClassificationProperty', usecols="C:T", skiprows=5, true_values="TRUE")
    excel['classificationrelation'] = pd.read_excel(excel_df, 'ClassificationRelation', usecols="C:G", skiprows=5, true_values="TRUE")
    excel['allowedvalue'] = pd.read_excel(excel_df, 'AllowedValue', skiprows=5, usecols="C:I", true_values="TRUE")
    excel['propertyrelation'] = pd.read_excel(excel_df, 'PropertyRelation', skiprows=5, usecols="C:F", true_values="TRUE")
    return excel

def jsonify(dataframe, json_part):
    """Transforms the input pandas dataframe to JSON only if a property exists in the template

    :param dataframe: Pandas dataframe with parsed Excel data
    :type dataframe: pd.DataFrame
    :param json_part: template dictinary from JSON_templates
    :type json_part: dict
    :return: Resultant list of dictionaries containing each row of the pandas table converted to appropriate dictionary
    :rtype: list
    """
    jsons = []
    # replace NANs
    dataframe = dataframe.astype(object).replace(np.nan, None)
    for index, row in dataframe.iterrows():
        new_part = deepcopy(json_part)
        for column_name, column_data in row.items():
            if column_name in new_part:
                # Convert date to: 2022-05-12T00:00:00+02:00
                if type(column_data) == pd._libs.tslibs.timestamps.Timestamp:
                    column_data = column_data.isoformat()
                elif type(column_data) == str:
                    if column_data.startswith("[") and column_data.endswith("]"):
                        content = literal_eval(column_data)
                        if isinstance(content, list):
                            column_data = content
                new_part[column_name] = column_data
            else:
                print(f"No such property as '{column_name}' in the JSON")
        jsons.append(new_part)
    return jsons

def clean_nones(value):
    """Recursively remove all None values from dictionaries and lists, and returns
    the result as a new dictionary or list.
    """
    if isinstance(value, list):
        return [clean_nones(x) for x in value if x is not None]
    elif isinstance(value, dict):
        return {
            key: clean_nones(val)
            for key, val in value.items()
            if val is not None
        }
    else:
        return value

def excel2bsdd(excel):
    """Goes through all dataframes and appends data to the desired JSON structure

    :param excel: Dictionary of Pandas dataframes from load_excel
    :type excel: dict
    :return: Resultant JSON structure
    :rtype: dict
    """
    bsdd_data = {}
    bsdd_data = jsonify(excel['domain'], Domain)[0]
    # process basic concepts
    bsdd_data['Classifications'] = jsonify(excel['classification'], Classification)
    bsdd_data['Materials'] = jsonify(excel['material'], Material)
    bsdd_data['Properties'] = jsonify(excel['property'], Property)
    # process ClassificationProperty
    cls_props = jsonify(excel['classificationproperty'], ClassificationProperty)
    for cls_prop in cls_props:
        related = cls_prop['(Origin Classification Code)']
        cls_prop.pop("(Origin Classification Code)")
        next(item for item in bsdd_data['Classifications'] if item["Code"] == related)['ClassificationProperties'].append(cls_prop)
    # process ClassificationRelation
    cls_rels = jsonify(excel['classificationrelation'], ClassificationRelation)
    for cls_rel in cls_rels:
        related = cls_rel['(Origin Classification Code)']
        cls_rel.pop("(Origin Classification Code)")
        next(item for item in bsdd_data['Classifications'] if item["Code"] == related)['ClassificationRelations'].append(cls_rel)
    # process AllowedValue
    allowed_vals = jsonify(excel['allowedvalue'], AllowedValue)
    for allowed_val in allowed_vals:
        # only one of the two Code columns is possible:
        if allowed_val['(Origin Property Code)']:
            relToProperty = True
            related = allowed_val['(Origin Property Code)']
        else:
            relToProperty = False
            related = allowed_val['(ClassificationProperty Code)']
        allowed_val.pop("(Origin Property Code)")
        allowed_val.pop("(ClassificationProperty Code)")
        if relToProperty:
            # iterate all properties and add AllowedValue if present in spreadsheet
            next(item for item in bsdd_data['Properties'] if item["Code"] == related)['AllowedValues'].append(allowed_val)
        else: 
            # iterate all classifications to find the one referenced by the property AllowedValue
            for classification in bsdd_data['Classifications']:
                # next(item for item in classification['ClassificationProperties'] if item["Code"] == related)['AllowedValues'].append(allowed_val)
                for item in classification['ClassificationProperties']:
                    if item["Code"] == related:
                        item['AllowedValues'].append(allowed_val)
    # process PropertyRelation
    prop_rels = jsonify(excel['propertyrelation'], PropertyRelation)
    for prop_rel in prop_rels:
        related = prop_rel['(Origin Property)']
        prop_rel.pop("(Origin Property)")
        next(item for item in bsdd_data['Properties'] if item["Code"] == related)['PropertyRelations'].append(prop_rel)

    return bsdd_data


if __name__ == "__main__":
    EXCEL_PATH = sys.argv[1]
    JSON_PATH = sys.argv[2]
    WITHOUT_NULLS = sys.argv[3]

    excel = load_excel(EXCEL_PATH)
    bsdd_data = excel2bsdd(excel)

    if str(WITHOUT_NULLS).lower() == 'true':
        bsdd_data = clean_nones(bsdd_data)
    resultant_file = open(JSON_PATH, "w")
    json.dump(bsdd_data, resultant_file, indent = 2)
    resultant_file.close()

    print(f"\nFile saved to {JSON_PATH}\n")