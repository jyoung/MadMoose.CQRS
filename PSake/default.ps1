#parameter properties
properties {
    $versionOverride
}

#base properties
properties {
    $baseDirectory = $PSScriptRoot
    $sourceDirectory = Join-Path -path $baseDirectory -childPath "Source"
}

#nuget properties
properties {
    $nugetDirectory = Join-Path -path $sourceDirectory -childPath ".nuget"
    $packageDirectory = Join-Path -path $baseDirectory -childPath "NuGet"
    $nuget = Join-Path -path $nugetDirectory -childPath "nuget.exe"
}

#project properties
properties {
    $solution = Join-Path -path $sourceDirectory -childPath "MySolution.sln"
}

#import the default build tasks
. .\PSake\build.ps1

#add custom tasks below

task Do-It {
    Write-Output $baseDirectory
}

#-------------------------------------------------------------------------------
# Get the version to use
#-------------------------------------------------------------------------------
function Get-VersionFn([string] $spec_path, [string] $versionOverride) {
	Write-Host "Version Override: $versionOverride"
	Write-Host "Spec Path: $spec_path"
			
	if (($versionOverride -eq "") -or ($versionOverride -eq $null)) {
		Write-Host "VersionOverride not supplied, getting version from NuSpec"
		[xml] $nuspec = Get-Content($spec_path)
		return $nuspec.package.metadata.version
	} else {
		Write-Host "Overriding version with VersionOverride"
		return $versionOverride
	}
}