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
	[IO.File]::WriteAllText($versionFile, $version)
	return $version
}

$version = Get-Version

$commonInfo = Get-Content "$scriptDir\CommonInfoTemplate.cs"
$commonInfo = $commonInfo -replace '%version%', $version
[IO.File]::WriteAllText("$scriptDir\Source\CommonInfo.cs", $commonInfo)

Write-Host "Building"
$output = & 'msbuild'

if($LastExitCode -ne 0)
{
	Write-Error "Error building: $output"
	return "Error building"
}

Get-ChildItem '*.nupkg' | Remove-Item

$specs = Get-ChildItem -Path "$scriptDir\Source" -File -Recurse '*.nuspec'
foreach($spec in $specs)
{
	Write-Host "Packaging: $spec"
	$path = Join-Path $spec.DirectoryName -ChildPath "$($spec.BaseName).csproj"
	
	$output = & $ng @('pack', '-NonInteractive', '-IncludeReferencedProjects', $path)
	if($LastExitCode -ne 0)
	{
		Write-Error "Error packaging: $path`r`n$output"
		return "Error packaging: $path"
	}
}

$packages = Get-ChildItem '*.nupkg'

foreach($package in $packages)
{
	Write-Host "Pushing: $package"
	$output = & $ng @('push', '-NonInteractive', $package)
	if($LastExitCode -ne 0)
	{
		Write-Error "Error packaging: $package`r`n$output"
		return "Error pushing: $package"
	}
}