# Install script for Hey Alice PowerToys Run Plugin
# This script copies the built plugin to the PowerToys plugins directory

param(
    [string]$Configuration = "Release"
)

$pluginName = "Community.PowerToys.Run.Plugin.HeyAlice"
$sourceDir = Join-Path $PSScriptRoot "bin\$Configuration\$pluginName"
$targetDir = Join-Path $env:LOCALAPPDATA "Microsoft\PowerToys\PowerToys Run\Plugins\$pluginName"

Write-Host "Installing Hey Alice PowerToys Run Plugin..." -ForegroundColor Green

# Check if source directory exists
if (-not (Test-Path $sourceDir)) {
    Write-Host "Error: Plugin not found in $sourceDir" -ForegroundColor Red
    Write-Host "Please run build.ps1 first to build the plugin." -ForegroundColor Yellow
    exit 1
}

# Create target directory if it doesn't exist
if (-not (Test-Path $targetDir)) {
    New-Item -ItemType Directory -Path $targetDir -Force | Out-Null
    Write-Host "Created directory: $targetDir" -ForegroundColor Cyan
}

# Copy all files
Write-Host "Copying files..." -ForegroundColor Cyan
Copy-Item "$sourceDir\*" -Destination $targetDir -Recurse -Force

Write-Host ""
Write-Host "Plugin installed successfully!" -ForegroundColor Green
Write-Host "Please restart PowerToys to load the plugin." -ForegroundColor Yellow
Write-Host ""
