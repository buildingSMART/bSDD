The desired input format to bSDD is JSON, and there are various ways and tools that allow you to create it. We added the Excel template for those of you that prefer this form. Since spreadhsheets are based on rows and columns, and JSON allows for multilevel hierarchies of data, we needed to add few fields that allow for conversion from Excel to JSON files.

Once you have your dictionary prepared in Excel, we offer this Python script to convert Excel files to JSON. This is an auxiliary helping tool on request of users, not a necesarry step to import anything to bSDD. If you're not familiar with basics of Python, we recommend to use a dedicated software instead. 

Please report if you experience any problems with using it. We advise to always review the output before uploading to bSDD.

## Using the Python converter

1. You need to have Python installed. If not, download and install python from here: https://www.python.org/downloads/.
2. You need the two Python libraries: Numpy and Pandas. If you don't, type in the console: `pip install numpy pandas openpyxl tqdm`
4. Copy the Excel template and fill in with your data. All the fields marked with * are mandatory if a row is in use.
5. Save and close the Excel file.
6. Run the Python script Excel_to_bSDDjson_converter.py with these arguments:
    *  EXCEL_PATH (path to your Excel file)
    *  JSON_TEMPLATE_PATH (path where the template JSON file is located. You can find this file in the bSDD/Model/Import Model)
    *  JSON_OUTPUT_PATH (path where you want to create the resultant JSON file, should end with ".JSON")
    *  WITHOUT_NULLS (False - produce minimal JSON only with fields filled in Excel, True - export also fields that are empty in Excel with 'null' assigned)  
   
    You can do that from the (Windows) console with:
    ```Python Excel_to_bSDDjson_converter.py "C:\...\Excel_file.xlsx" "C:\...\Template.json" "C:\...\Result.json" False```
