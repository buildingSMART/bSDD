$BaseFolder = "C:\projects\psets"
$PSet2YamlConverterExe="$BaseFolder\Tools\PSet2YamlConverter\PSet2YamlConverter\bin\Release\PSet2YamlConverter.exe"
$SourceFolderXml = "C:\projects\psets\XML"
$TargetFolderYaml = "C:\projects\psets\YAML"
$TargetFolderJson = "C:\projects\psets\JSON"
$ExitCode=0
Write-Host "++++++++++++++++++++++++++++++++++++++"
Write-Host Transforming now...
& $PSet2YamlConverterExe "$SourceFolderXml" "$TargetFolderYaml" "$TargetFolderJson"
if ($LastExitCode -eq '0') 
{ 
 Write-Host "OK: Tranformation is successfull" -ForegroundColor Green
}
else 
{
 Write-Host "ERROR: Tranformation is not successfull" -ForegroundColor Red
 Write-Host "Please check the errors and correct them. Thanks for your contribution!" -ForegroundColor Red
 $ExitCode=1
}
Exit $ExitCode