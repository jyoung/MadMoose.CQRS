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
	$nuspec = Join-Path -path $sourceDirectory -childPath (Join-Path -path "MadMoose.CQRS" -childPath "MadMoose.CQRS.nuspec")
}

#nunit properties 
properties {
    $nunitDirectory = Join-Path -path $sourceDirectory -childPath "packages\NUnit.Runners.2.6.4\tools"
    $nunit = Join-Path -path $nunitDirectory -childPath "nunit-console.exe"
	$reportDirectory = Join-Path -path $baseDirectory -childPath "Reports"
    $nunitReportFile = Join-Path -path $reportDirectory -childPath "TestResults.xml"
}

#project properties
properties {
    $solution = Join-Path -path $sourceDirectory -childPath "MadMoose.CQRS.sln"
}

#import the default build tasks
. .\PSake\build.ps1

#add custom tasks below

task Do-It {
    Write-Output $baseDirectory
}

task Run-Specs -Description "Runs all specifications" -Depends Build-Debug {

	if (-not(Test-Path $reportDirectory -PathType Container)) 
	{
		New-Item -ItemType Directory -Path $reportDirectory
	}

    $path = Join-Path -path $sourceDirectory -childPath ("**\bin\Debug\MadMoose.CQRS.Specifications.dll")
    
    $assemblies = Get-ChildItem $path

    if ($assemblies.count -gt 0 ) {
        # need to use the call operator "&" to call out to nunit, this is unlike msbuild which is built into psake
        exec { & $nunit $assemblies /xml=$nunitReportFile /framework:net-4.5 /nologo}
    } else {
        Write-Output "No assemblies found to execute tests"
    }
}

# The pack (and release) tasks require a version parameter, you can pass the version on the command line
# like  invoke-psake pack-release -parameters @{"version"="1.2.3.4"}
task Pack-Release -Description "Creates a NuGet package based on the release build" -depends Build-Init {
           
	$version = Get-VersionFn $nuspec $versionOverride
    
	Write-Host "Release Version: $version"
	
    Build-ReleaseFn $version
   
    #create the package
    exec {nuget pack $nuspec -Version $version -OutputDirectory $packageDirectory}
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