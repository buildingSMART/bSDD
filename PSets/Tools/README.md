## Introduction

This repository contains tools. They help, to read and write the content of the PSets and to manage the connection to the buildingSMAT Data Dictionary (bSDD).

### PSetManager

The PSetManager is a .NET commandline application. You can build the PSetManager on you own or you can download a prebuilded executable.

#### Commandline Reference 

The PSetManager can run with the follwowing different modes

#### ConvertFromXml  
Convert the existing PSets from the XML files into the new YAML files          

```console 
PSetManager.exe --mode ConvertFromXml --folderXml "YourLocalPath\bSDD\PSets\XML" --folderYaml "YourLocalPath\bSDD\PSets\YAML" --folderJson "YourLocalPath\bSDD\PSets\JSON" --folderResx "YourLocalPath\bSDD\PSets\RESX" --checkBSDD true
```
							
#### LoadTranslation 
Load a translation for a specific language from an Excel sheet into the the new YAML files

```console 
PSetManager.exe --mode LoadTranslation --translationSourceFile "YourLocalPathToExcel\YourExcelTranslationSource.xlsx --folderYaml "YourLocalPath\bSDD\PSets\YAML" --folderJson "YourLocalPath\bSDD\PSets\JSON" --folderResx "YourLocalPath\bSDD\PSets\RESX"
```

#### PublishToBSDD
Check and publish the PSets from the new YAML files into the buildingSMART Data Dictionary

```console 
PSetManager.exe --mode UploadToBSDD --folderYaml "YourLocalPath\bSDD\PSets\YAML" --bsddUrl "http://test.bsdd.buildingsmart.org" --bsddUser "Your bSDD Username" --bsddPassword "Your bSDD Password"
```
