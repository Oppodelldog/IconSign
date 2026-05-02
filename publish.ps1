param(
    [Parameter(Mandatory)]
    [ValidateSet('Debug','Release')]
    [System.String]$Target,
    
    [Parameter(Mandatory)]
    [System.String]$TargetPath,
    
    [Parameter(Mandatory)]
    [System.String]$TargetAssembly,

    [Parameter(Mandatory)]
    [System.String]$ValheimPath,

    [Parameter(Mandatory)]
    [System.String]$ProjectPath
)

# Make sure Get-Location is the script path
Push-Location -Path (Split-Path -Parent $MyInvocation.MyCommand.Path)

# Test some preliminaries
("$TargetPath",
 "$ValheimPath",
 "$(Get-Location)\libraries"
) | % {
    if (!(Test-Path "$_")) {Write-Error -ErrorAction Stop -Message "$_ folder is missing"}
}

# Plugin name without ".dll"
$name = "$TargetAssembly" -Replace('.dll')

# Create the mdb file
$pdb = "$TargetPath\$name.pdb"
if (Test-Path -Path "$pdb") {
    Write-Host "Create mdb file for plugin $name"
    Invoke-Expression "& `"$(Get-Location)\libraries\Debug\pdb2mdb.exe`" `"$TargetPath\$TargetAssembly`""
}

# Main Script
$DeployPathDebug = "$ValheimPath\BepInEx\scripts\$name"
$DeployPathRelease = "$ValheimPath\BepInEx\plugins\$name"
$ReleasePackageRoot = "$TargetPath\$name"
$ReleasePackage = "$TargetPath\$name.zip"

Remove-Item -Path "$DeployPathDebug\$name.dll" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item -Path "$DeployPathDebug\$name.pdb" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item -Path "$DeployPathDebug\$name.dll.mdb" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item -Path "$DeployPathRelease" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item -Path "$ReleasePackageRoot" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item -Path "$ReleasePackage" -Recurse -Force -ErrorAction SilentlyContinue

if ($Target.Equals("Debug")) {
    Write-Host "Publishing for $Target from $TargetPath to $DeployPathDebug"
    
    $plug = New-Item -Type Directory -Path "$DeployPathDebug" -Force
    Write-Host "Copy $TargetAssembly to $plug"
    Copy-Item -Path "$TargetPath\$name.dll" -Destination "$plug" -Force
    Copy-Item -Path "$TargetPath\$name.pdb" -Destination "$plug" -Force
    Copy-Item -Path "$TargetPath\$name.dll.mdb" -Destination "$plug" -Force
}

if($Target.Equals("Release")) {
    Write-Host "Publishing for $Target from $TargetPath to $DeployPathRelease"
    
    $plug = New-Item -Type Directory -Path "$DeployPathRelease" -Force
    Write-Host "Copy $TargetAssembly to $plug"
    Copy-Item -Path "$TargetPath\$name.dll" -Destination "$plug" -Force

    $packageRoot = New-Item -Type Directory -Path "$ReleasePackageRoot" -Force
    Copy-Item -Path "$TargetPath\$name.dll" -Destination "$packageRoot" -Force
    Compress-Archive -Path "$ReleasePackageRoot" -DestinationPath "$ReleasePackage" -Force
    Write-Host "Created release package $ReleasePackage"
}

# Pop Location
Pop-Location
