The desired input format to bSDD is JSON, and there are various ways and tools that allow you to create it. We added the Excel template for those of you that prefer this form. Since spreadhsheets are based on rows and columns, and JSON allows for multilevel hierarchies of data, we needed to add few fields that allow for conversion from Excel to JSON files.

Once you have your dictionary prepared in Excel, we offer this Python script to convert Excel files to JSON. This is an auxiliary helping tool on request of users, not a necessary step to import anything to bSDD. If you're not familiar with basics of Python, we recommend to use a dedicated software instead. 

Please report if you experience any problems with using it. We advise to always review the output before uploading to bSDD.

## Using the Python converter

1. You need to have Python installed. If not, download and install python from here: https://www.python.org/downloads/.
2. You need to install a few Python libraries. If you don't have them already, type in the console: `pip install numpy pandas openpyxl tqdm`
   (note: system console, not a Python console. To open on Windows, find 'Command Prompt' app)
4. Copy the Excel template and fill in with your data. All the fields marked with * are mandatory if a row is in use.
5. Save and close the Excel file.
6. Run the Python script Excel2bSDD_converter.py with these arguments:
    *  EXCEL_PATH (path to your Excel file)
    *  JSON_TEMPLATE_PATH (path where the template JSON file is located. You can find this file in the bSDD/Model/Import Model)
    *  JSON_OUTPUT_PATH (path where you want to create the resultant JSON file, should end with ".JSON")
    *  WITHOUT_NULLS (set to 'True' to produce a minimal JSON only with fields filled in Excel, 'False' to also export all empty fields with 'null' assigned)  
   
    You can do that from the (Windows) console with:
    ```Python Excel2bSDD_converter.py "C:\...\Excel_file.xlsx" "C:\...\Template.json" "C:\...\Result.json" False```
