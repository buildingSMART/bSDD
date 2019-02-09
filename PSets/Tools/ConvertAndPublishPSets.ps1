param(
[string]$bsddUrl,
[string]$bsddUser,
[string]$bsddPassword
)


$BaseFolder = "C:\projects\bSDD\PSets"
$PSetManagerExe="$BaseFolder\Tools\PSetManager\PSetManager\bin\Release\PSetManager.exe"
$FolderXml = "$BaseFolder\XML"
$FolderYaml = "$BaseFolder\YAML"
$FolderJson = "$BaseFolder\JSON"
$FolderResx = "$BaseFolder\RESX"
$ExitCode=0
Write-Host "++++++++++++++++++++++++++++++++++++++"
Write-Host SourceFolderXml  : $SourceFolderXml
Write-Host TargetFolderYaml : $TargetFolderYaml
Write-Host TargetFolderJson : $TargetFolderJson
Write-Host bsddUrl          : $bsddUrl

## Run the PSetManager in the mode to convert the XML-Psets to the new YAML format
## This mode was only created for the initial conversion of the PSets
# & $PSetManagerExe --mode ConvertFromXml --folderXml $FolderXml --folderYaml $FolderYaml --folderJson $tFolderJson --folderResx $FolderResx --checkBSDD true

## Run the PSetManager in the mode to publish them from the new YAML format to the target bSDD server
& $PSetManagerExe --mode PublishToBSDD --folderYaml $FolderYaml --bsddLanguageCode "de-DE" --bsddUrl $bsddUrl --bsddUser $bsddUser --bsddPassword $bsddPassword 


if ($LastExitCode -eq '0') 
{ 
 Write-Host "OK: Publication was successfull" -ForegroundColor Green
}
else 
{
 Write-Host "ERROR: Publication was not successfull" -ForegroundColor Red
 Write-Host "Please check the errors and correct them. Thanks for your contribution!" -ForegroundColor Red
 $ExitCode=1
}
Exit $ExitCode