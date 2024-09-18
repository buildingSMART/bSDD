"""
This is a script to convert a JSON file compiant with bSDD template to Excel. 

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


import pandas as pd
import json

def json_to_excel(json_file, excel_file):
    # Read the JSON data
    with open(json_file, 'r') as file:
        data = json.load(file)
    
    # Convert JSON data to DataFrame
    df = pd.json_normalize(data)
    
    # Write DataFrame to Excel file
    df.to_excel(excel_file, index=False)
    print(f"Data successfully written to {excel_file}")

if __name__ == "__main__":
    # EXCEL_PATH = sys.argv[1]
    # JSON_TEMPLATE_PATH = sys.argv[2]
    # JSON_OUTPUT_PATH = sys.argv[3]
    # WITHOUT_NULLS = sys.argv[4]
    # EXCEL_PATH = r"C:\Code\bSDD\Model\Import Model\spreadsheet-import\bSDD_Excel_Example_all_fields.xlsx"

    # Example usage
    json_file = 'data.json'  # Path to your JSON file
    excel_file = 'output.xlsx'  # Path to the output Excel file

    json_to_excel(json_file, excel_file)


    print(f"\nFile successfully saved to {JSON_OUTPUT_PATH}\n")