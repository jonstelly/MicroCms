$ng = '.\.nuget\nuget.exe'

function Get-ScriptDirectory
{
  $Invocation = (Get-Variable MyInvocation -Scope 1).Value
  Split-Path $Invocation.MyCommand.Path
}

$scriptDir = Get-ScriptDirectory

function Get-Version()
{
	$version = [System.UInt16]0;
	$versionFile =  "$scriptDir\version.txt";

	if(Test-Path $versionFile)
	{
		$version = [System.UInt16](Get-Content $versionFile)
	}
	$version += 1
	Write-Host "Writing Version: $version"
	[IO.File]::WriteAllText($versionFile, $version)
	return $version
}

$version = Get-Version

$commonInfo = Get-Content "$scriptDir\CommonInfoTemplate.cs"
$commonInfo = $commonInfo -replace '%version%', $version
[IO.File]::WriteAllText("$scriptDir\Source\CommonInfo.cs", $commonInfo)

& 'msbuild'

$specs = Get-ChildItem -Path "$scriptDir\Source" -File -Recurse '*.nuspec'
foreach($spec in $specs)
{
	$path = Join-Path $spec.DirectoryName -ChildPath "$($spec.BaseName).csproj"
	
	Write-Host "Packing: $path - $version"
	& $ng @('pack', $path)# 'source\MicroCms\MicroCms.csproj')
}
