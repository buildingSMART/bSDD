$BaseFolder = "C:\projects\bSDD\PSets"
$PSetManagerExe="$BaseFolder\Tools\PSetManager\PSetManager\bin\Release\PSetManager.exe"
$SourceFolderXml = "$BaseFolder\XML"
$TargetFolderYaml = "$BaseFolder\YAML"
$TargetFolderJson = "$BaseFolder\JSON"
$TargetFolderResx = "$BaseFolder\RESX"
$ExitCode=0
Write-Host "++++++++++++++++++++++++++++++++++++++"
Write-Host SourceFolderXml : $SourceFolderXml
Write-Host TargetFolderYaml : $TargetFolderYaml
Write-Host TargetFolderJson : $TargetFolderJson
Write-Host TargetFolderResx : $TargetFolderResx

& $PSetManagerExe --mode ConvertFromXml --folderXml $SourceFolderXml --folderYaml $TargetFolderYaml --folderJson $TargetFolderJson --folderResx $TargetFolderResx --checkBSDD true

if ($LastExitCode -eq '0') 
{ 
 Write-Host "OK: Transformation is successfull" -ForegroundColor Green
}
else 
{
 Write-Host "ERROR: Transformation is not successfull" -ForegroundColor Red
 Write-Host "Please check the errors and correct them. Thanks for your contribution!" -ForegroundColor Red
 $ExitCode=1
}
Exit $ExitCode