The desired input format to bSDD is JSON, and there are various wasys and tools that allow you to create it. We added the Excel template for those of you that prefer this form. Since spreadhsheets are based on rows and columns, and JSON allows for multilevel hierarchies of data, we needed to add few fields that allow for conversion from Excel to JSON files.

Once you have your domain prepared in Excel, we offer this Python script to convert Excel files to JSON. This is an auxiliary helping tool on request of users, not a necesarry step to import anything to bSDD. If you're not familiar with basics of Python, we recommend to use a dedicated software instead. 

Please report if you experience any problems with using it. We advise to always review the output before uploading to bSDD.

## Using the Python converter

1. Copy the Excel template and fill in with your data. All the fields marked with * are mandatory if a row is in use.
2. Save and close the Excel file.
3. Run the Python script Excel2bSDD_converter.py with the three arguments:
    *  EXCEL_PATH (path to your Excel file)
    *  JSON_PATH (path where you want to create the resultant JSON file, should end with ".JSON")
    *  WITH_NULLS (False - produce minimal JSON only with fields filled in Excel, True - export also fields that are empty in Excel with 'null' assigned)  
  
    You can do that from the (Windows) console with:
    ```Python Excel2bSDD_converter.py '...\Excel_file.xlsx' '...\Result.json' True```
