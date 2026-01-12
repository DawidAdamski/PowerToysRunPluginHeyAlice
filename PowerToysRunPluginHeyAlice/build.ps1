# Build script for Hey Alice PowerToys Run Plugin
# This script builds the plugin using dotnet CLI and copies required DLLs

param(
    [string]$Configuration = "Release",
    [string]$PowerToysPath = "C:\Program Files\PowerToys"
)

Write-Host "Building Hey Alice PowerToys Run Plugin..." -ForegroundColor Green

# Check if dotnet is available
if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    Write-Host "Error: .NET SDK not found. Please install .NET 8.0 SDK or later." -ForegroundColor Red
    exit 1
}

# Check if PowerToys DLLs exist
$requiredDlls = @(
    "$PowerToysPath\Wox.Plugin.dll",
    "$PowerToysPath\Wox.Infrastructure.dll",
    "$PowerToysPath\PowerToys.ManagedCommon.dll",
    "$PowerToysPath\PowerToys.Settings.UI.Lib.dll",
    "$PowerToysPath\PowerToys.Common.UI.dll"
)

foreach ($dll in $requiredDlls) {
    if (-not (Test-Path $dll)) {
        Write-Host "Error: Required DLL not found: $dll" -ForegroundColor Red
        Write-Host "Please make sure PowerToys is installed." -ForegroundColor Yellow
        exit 1
    }
}

# Build the project
Write-Host "Building project..." -ForegroundColor Cyan
$buildOutput = dotnet build -c $Configuration 2>&1

if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed!" -ForegroundColor Red
    $buildOutput | Write-Host
    exit 1
}

Write-Host "Build successful!" -ForegroundColor Green

# Get output directory
$outputDir = Join-Path $PSScriptRoot "bin\$Configuration"
$pluginOutputDir = Join-Path $outputDir "Community.PowerToys.Run.Plugin.HeyAlice"

# Create plugin output directory if it doesn't exist
if (-not (Test-Path $pluginOutputDir)) {
    New-Item -ItemType Directory -Path $pluginOutputDir -Force | Out-Null
}

# Copy required DLLs
Write-Host "Copying required DLLs..." -ForegroundColor Cyan
$dllsToCopy = @(
    @{ Source = "$PowerToysPath\Wox.Plugin.dll"; Dest = "Wox.Plugin.dll" },
    @{ Source = "$PowerToysPath\Wox.Plugin.pdb"; Dest = "Wox.Plugin.pdb" },
    @{ Source = "$PowerToysPath\Wox.Infrastructure.dll"; Dest = "Wox.Infrastructure.dll" },
    @{ Source = "$PowerToysPath\Wox.Infrastructure.pdb"; Dest = "Wox.Infrastructure.pdb" },
    @{ Source = "$PowerToysPath\PowerToys.ManagedCommon.dll"; Dest = "PowerToys.ManagedCommon.dll" },
    @{ Source = "$PowerToysPath\PowerToys.ManagedCommon.pdb"; Dest = "PowerToys.ManagedCommon.pdb" },
    @{ Source = "$PowerToysPath\PowerToys.Settings.UI.Lib.dll"; Dest = "PowerToys.Settings.UI.Lib.dll" },
    @{ Source = "$PowerToysPath\PowerToys.Settings.UI.Lib.pdb"; Dest = "PowerToys.Settings.UI.Lib.pdb" },
    @{ Source = "$PowerToysPath\PowerToys.Common.UI.dll"; Dest = "PowerToys.Common.UI.dll" },
    @{ Source = "$PowerToysPath\PowerToys.Common.UI.pdb"; Dest = "PowerToys.Common.UI.pdb" }
)

foreach ($dll in $dllsToCopy) {
    if (Test-Path $dll.Source) {
        Copy-Item $dll.Source -Destination (Join-Path $pluginOutputDir $dll.Dest) -Force
        Write-Host "  Copied: $($dll.Dest)" -ForegroundColor Gray
    }
}

# Copy plugin files
Write-Host "Copying plugin files..." -ForegroundColor Cyan
$pluginFiles = @(
    "plugin.json",
    "Images"
)

foreach ($file in $pluginFiles) {
    $sourcePath = Join-Path $PSScriptRoot $file
    if (Test-Path $sourcePath) {
        if (Test-Path $sourcePath -PathType Container) {
            Copy-Item $sourcePath -Destination $pluginOutputDir -Recurse -Force
        } else {
            Copy-Item $sourcePath -Destination $pluginOutputDir -Force
        }
        Write-Host "  Copied: $file" -ForegroundColor Gray
    }
}

# Copy built DLL and PDB
$builtDll = Join-Path $outputDir "Community.PowerToys.Run.Plugin.HeyAlice.dll"
$builtPdb = Join-Path $outputDir "Community.PowerToys.Run.Plugin.HeyAlice.pdb"
$builtDeps = Join-Path $outputDir "Community.PowerToys.Run.Plugin.HeyAlice.deps.json"

if (Test-Path $builtDll) {
    Copy-Item $builtDll -Destination $pluginOutputDir -Force
    Write-Host "  Copied: Community.PowerToys.Run.Plugin.HeyAlice.dll" -ForegroundColor Gray
}

if (Test-Path $builtPdb) {
    Copy-Item $builtPdb -Destination $pluginOutputDir -Force
    Write-Host "  Copied: Community.PowerToys.Run.Plugin.HeyAlice.pdb" -ForegroundColor Gray
}

if (Test-Path $builtDeps) {
    Copy-Item $builtDeps -Destination $pluginOutputDir -Force
    Write-Host "  Copied: Community.PowerToys.Run.Plugin.HeyAlice.deps.json" -ForegroundColor Gray
}

Write-Host ""
Write-Host "Plugin built successfully!" -ForegroundColor Green
Write-Host "Output directory: $pluginOutputDir" -ForegroundColor Cyan
Write-Host ""
Write-Host "To install, copy the contents of the output directory to:" -ForegroundColor Yellow
Write-Host "  %LOCALAPPDATA%\Microsoft\PowerToys\PowerToys Run\Plugins\Community.PowerToys.Run.Plugin.HeyAlice" -ForegroundColor Cyan
Write-Host ""
