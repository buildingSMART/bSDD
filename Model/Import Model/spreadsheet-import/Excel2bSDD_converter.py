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
    excel['dictionary'] = pd.read_excel(excel_df, 'Dictionary', skiprows=6, usecols="C:Q", true_values="TRUE")
    excel['class'] = pd.read_excel(excel_df, 'Class', skiprows=6, usecols="C:AC", true_values="TRUE")
    excel['property'] = pd.read_excel(excel_df, 'Property', skiprows=6, usecols="C:AU", true_values="TRUE")
    excel['classproperty'] = pd.read_excel(excel_df, 'ClassProperty', usecols="C:T", skiprows=6, true_values="TRUE")
    excel['classrelation'] = pd.read_excel(excel_df, 'ClassRelation', usecols="C:H", skiprows=6, true_values="TRUE")
    excel['allowedvalue'] = pd.read_excel(excel_df, 'AllowedValue', skiprows=6, usecols="C:I", true_values="TRUE")
    excel['propertyrelation'] = pd.read_excel(excel_df, 'PropertyRelation', skiprows=6, usecols="C:G", true_values="TRUE")
    return excel

def map_data(excel_data, bsdd_part_template):
    """Transforms the input pandas dataframe to JSON only if a property exists in the template

    :param excel_data: Pandas dataframe with parsed Excel data
    :type excel_data: pd.DataFrame
    :param json_part: template dictinary from JSON_templates
    :type json_part: dict
    :return: Resultant list of dictionaries containing each row of the pandas table converted to appropriate dictionary
    :rtype: list
    """
    if isinstance(bsdd_part_template, list):
        template = deepcopy(bsdd_part_template[0])
    else:
        template = deepcopy(bsdd_part_template)
    
    for k, v in template.items():
        if isinstance(v, list):
            template[k] = []

    # replace NANs
    excel_data = excel_data.astype(object).replace(np.nan, None)
    new_objects = []
    for index, row in excel_data.iterrows():
        if not excel_data.dropna(how="all").empty:
            new_object = deepcopy(template)
            for column_name, column_data in row.items():
                if column_name in template:
                    # Convert date to: 2022-05-12T00:00:00+02:00
                    if type(column_data) == pd._libs.tslibs.timestamps.Timestamp:
                        column_data = column_data.isoformat()
                    elif type(column_data) == str:
                        if column_data.startswith("[") and column_data.endswith("]"):
                            content = literal_eval(column_data)
                            if isinstance(content, list):
                                column_data = content
                    new_object[column_name] = column_data
                    # new_part[column_name] = column_data
                elif column_name in ('(Origin Class Code)','(Origin Property Code)','(Origin ClassProperty Code)'):
                    new_object[column_name] = column_data
                else:
                    print(f"WARNING! No such property as '{column_name}' in the JSON template! It will NOT be added to the JSON file.")
                    # new_object[column_name] = column_data
            # bsdd_part_template.append(new_object)
            new_objects.append(new_object)
    return new_objects

def clean_nones(value):
    """Recursively remove all None values from dictionaries and lists, and returns
    the result as a new dictionary or list.
    """
    if isinstance(value, list):
        return [clean_nones(x) for x in value if not x in ("", [], None)] # is not None]
    elif isinstance(value, dict):
        return {
            key: clean_nones(val)
            for key, val in value.items()
            if not val in ("", [], None)
        }
    else:
        return value

def excel2bsdd(excel, bsdd_template):
    """Goes through all dataframes and appends data to the desired JSON structure

    :param excel: Dictionary of Pandas dataframes from load_excel
    :type excel: dict
    :return: Resultant JSON structure
    :rtype: dict
    """
    bsdd_data = bsdd_template
    bsdd_data = map_data(excel['dictionary'], bsdd_template)[0]
    # process basic concepts
    bsdd_data['Classes'] = map_data(excel['class'], bsdd_template['Classes'])
    bsdd_data['Properties'] = map_data(excel['property'], bsdd_template['Properties'])
    # process ClassProperty
    cls_props = map_data(excel['classproperty'], bsdd_template['Classes'][0]['ClassProperties'])
    for cls_prop in cls_props:
        related = cls_prop['(Origin Class Code)']
        cls_prop.pop("(Origin Class Code)")
        next(item for item in bsdd_data['Classes'] if item["Code"] == related)['ClassProperties'].append(cls_prop)
    # process ClassRelation
    cls_rels = map_data(excel['classrelation'], bsdd_template['Classes'][0]['ClassRelations'])
    for cls_rel in cls_rels:
        related = cls_rel['(Origin Class Code)']
        cls_rel.pop("(Origin Class Code)")
        next(item for item in bsdd_data['Classes'] if item["Code"] == related)['ClassRelations'].append(cls_rel)
    # process AllowedValue
    allowed_vals = map_data(excel['allowedvalue'], bsdd_template['Properties'][0]['AllowedValues'])
    for allowed_val in allowed_vals:
        # only one of the two Code columns is possible:
        if allowed_val['(Origin Property Code)']:
            relToProperty = True
            related = allowed_val['(Origin Property Code)']
        elif allowed_val['(Origin ClassProperty Code)']:
            relToProperty = False
            related = allowed_val['(Origin ClassProperty Code)']
        else:
            print("WARNING! Allowed value without origin property or classProperty code! It will NOT be added to the JSON file.")
        allowed_val.pop("(Origin Property Code)")
        allowed_val.pop("(Origin ClassProperty Code)")
        if relToProperty:
            # iterate all properties and add AllowedValue if present in spreadsheet
            next(item for item in bsdd_data['Properties'] if item["Code"] == related)['AllowedValues'].append(allowed_val)
        else: 
            # iterate all classes to find the one referenced by the property AllowedValue
            for c in bsdd_data['Classes']:
                # next(item for item in class['ClassProperties'] if item["Code"] == related)['AllowedValues'].append(allowed_val)
                for item in c['ClassProperties']:
                    if item["Code"] == related:
                        item['AllowedValues'].append(allowed_val)
    # process PropertyRelation
    prop_rels = map_data(excel['propertyrelation'], bsdd_template['Properties'][0]['PropertyRelations'])
    for prop_rel in prop_rels:
        related = prop_rel['(Origin Property Code)']
        prop_rel.pop("(Origin Property Code)")
        next(item for item in bsdd_data['Properties'] if item["Code"] == related)['PropertyRelations'].append(prop_rel)

    return bsdd_data


if __name__ == "__main__":
    EXCEL_PATH = sys.argv[1]
    JSON_TEMPLATE_PATH = sys.argv[2]
    JSON_OUTPUT_PATH = sys.argv[3]
    WITHOUT_NULLS = sys.argv[4]

    excel = load_excel(EXCEL_PATH)
    bsdd_template = json.load(open(JSON_TEMPLATE_PATH, encoding="utf-8"))
    bsdd_data = excel2bsdd(excel, bsdd_template)

    if str(WITHOUT_NULLS).lower() == 'true':
        bsdd_data = clean_nones(bsdd_data)
    resultant_file = open(JSON_OUTPUT_PATH, "w", encoding="utf-8")
    json.dump(bsdd_data, resultant_file, indent = 2)
    resultant_file.close()

    print(f"\nFile saved to {JSON_OUTPUT_PATH}\n")