# Build and install script for Hey Alice PowerToys Run Plugin
# This script builds the plugin and installs it automatically

param(
    [string]$Configuration = "Release"
)

Write-Host "Building and installing Hey Alice PowerToys Run Plugin..." -ForegroundColor Green
Write-Host ""

# Build
& "$PSScriptRoot\build.ps1" -Configuration $Configuration

if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed. Installation aborted." -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "Installing plugin..." -ForegroundColor Cyan
Write-Host ""

# Install
& "$PSScriptRoot\install.ps1" -Configuration $Configuration

if ($LASTEXITCODE -ne 0) {
    Write-Host "Installation failed." -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "Done! Please restart PowerToys to use the plugin." -ForegroundColor Green
Write-Host ""
