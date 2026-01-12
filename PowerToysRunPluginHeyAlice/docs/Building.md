# Building Guide

This guide explains how to build the Hey Alice plugin from source.

## Prerequisites

Before building, ensure you have:

- **.NET 9.0 SDK** or later ([Download](https://dotnet.microsoft.com/download))
- **PowerToys** installed (for required DLL references)
- **PowerShell 5.1** or later (for build scripts)
- **Git** (optional, for cloning the repository)

## Quick Build

The easiest way to build and install:

```powershell
.\build-and-install.ps1
```

This script will:
1. Build the plugin in Release configuration
2. Copy required DLLs from PowerToys installation
3. Copy plugin files (plugin.json, Images)
4. Install the plugin to PowerToys directory

## Build Methods

### Method 1: PowerShell Scripts (Recommended)

#### Build Only

```powershell
.\build.ps1
```

This will:
- Build the project in Release configuration
- Copy required DLLs from `C:\Program Files\PowerToys\`
- Copy plugin files to output directory
- Output location: `bin\Release\Community.PowerToys.Run.Plugin.HeyAlice\`

#### Install Only

After building, install the plugin:

```powershell
.\install.ps1
```

This will:
- Copy the built plugin to PowerToys plugins directory
- Location: `%LOCALAPPDATA%\Microsoft\PowerToys\PowerToys Run\Plugins\`

#### Build and Install

Combine both steps:

```powershell
.\build-and-install.ps1
```

### Method 2: Manual Build with dotnet CLI

#### Step 1: Build the Project

```powershell
cd PowerToysRunPluginHeyAlice
dotnet build -c Release
```

#### Step 2: Copy Required DLLs

The plugin requires DLLs from PowerToys installation. Copy these files from `C:\Program Files\PowerToys\` to `bin\Release\`:

- `Wox.Plugin.dll`
- `Wox.Infrastructure.dll`
- `PowerToys.ManagedCommon.dll`
- `PowerToys.Settings.UI.Lib.dll`
- `PowerToys.Common.UI.dll`

#### Step 3: Copy Plugin Files

Copy these files to `bin\Release\Community.PowerToys.Run.Plugin.HeyAlice\`:

- `plugin.json`
- `Images\HeyAlice.dark.png`
- `Images\HeyAlice.light.png`

#### Step 4: Install

Copy the entire `bin\Release\Community.PowerToys.Run.Plugin.HeyAlice\` folder to:
```
%LOCALAPPDATA%\Microsoft\PowerToys\PowerToys Run\Plugins\
```

### Method 3: Visual Studio

1. Open `Community.PowerToys.Run.Plugin.HeyAlice.sln` in Visual Studio
2. Set configuration to **Release**
3. Build the solution (`Ctrl + Shift + B`)
4. Follow steps 2-4 from Method 2

## Build Scripts Explained

### build.ps1

The build script performs these steps:

1. **Clean previous builds** (optional)
2. **Build with dotnet CLI:**
   ```powershell
   dotnet build -c Release
   ```
3. **Copy required DLLs** from PowerToys installation
4. **Copy plugin files** (plugin.json, Images)
5. **Verify build output**

### install.ps1

The install script:

1. **Locates PowerToys plugins directory**
2. **Creates directory if needed**
3. **Copies built plugin files**
4. **Verifies installation**

### build-and-install.ps1

Combines both scripts for convenience.

## Build Output Structure

After building, the output structure should be:

```
bin\Release\Community.PowerToys.Run.Plugin.HeyAlice\
├── Community.PowerToys.Run.Plugin.HeyAlice.dll
├── Community.PowerToys.Run.Plugin.HeyAlice.pdb
├── plugin.json
├── Images\
│   ├── HeyAlice.dark.png
│   └── HeyAlice.light.png
└── [Required DLLs from PowerToys]
```

## Extracting Icons

To update the plugin icons from Hey Alice application:

```powershell
.\extract-icon.ps1
```

This script:
1. Extracts icon from `C:\Program Files\Alice\Alice.exe`
2. Creates light version (original)
3. Creates dark version (darkened by 15%)
4. Saves to `Images\` directory

## Troubleshooting Build

### Build Errors

#### .NET Version Mismatch

**Error:** `error CS1705: Assembly 'Wox.Plugin' uses 'System.Runtime, Version=9.0.0.0'`

**Solution:**
- Ensure .NET 9.0 SDK is installed
- Check `global.json` specifies correct SDK version
- Verify `TargetFramework` in `.csproj` is `net9.0-windows`

#### Missing DLLs

**Error:** `Could not find file 'Wox.Plugin.dll'`

**Solution:**
- Verify PowerToys is installed
- Check DLLs exist in `C:\Program Files\PowerToys\`
- Run `build.ps1` which automatically copies DLLs

#### Path Issues

**Error:** `Cannot find path`

**Solution:**
- Run scripts from plugin root directory
- Use absolute paths if needed
- Check PowerShell execution policy: `Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser`

### Installation Errors

#### Permission Denied

**Error:** `Access to the path is denied`

**Solution:**
- Run PowerShell as Administrator
- Close PowerToys before installing
- Check folder permissions

#### Plugin Not Loading

**Error:** Plugin appears but doesn't work

**Solution:**
- Verify all files are copied
- Check `plugin.json` is valid
- Verify Images folder exists
- Check PowerToys logs for errors

## Development Setup

### Recommended IDE

- **Visual Studio 2022** (Community edition is free)
- **Visual Studio Code** with C# extension
- **JetBrains Rider**

### Project Structure

```
PowerToysRunPluginHeyAlice/
├── Main.cs                    # Main plugin logic
├── plugin.json                # Plugin metadata
├── Community.PowerToys.Run.Plugin.HeyAlice.csproj
├── Properties/
│   ├── Resources.resx        # Localized strings
│   └── Resources.Designer.cs
├── Images/                    # Plugin icons
├── docs/                      # Documentation
└── build scripts
```

### Debugging

1. Build in **Debug** configuration
2. Attach debugger to PowerToys process
3. Set breakpoints in `Main.cs`
4. Use PowerToys Run to trigger plugin

## Creating Releases

To create a release package:

1. **Build in Release:**
   ```powershell
   .\build.ps1
   ```

2. **Create archive:**
   - Zip the `bin\Release\Community.PowerToys.Run.Plugin.HeyAlice\` folder
   - Name it: `Community.PowerToys.Run.Plugin.HeyAlice-v1.0.0-x64.zip`

3. **Verify contents:**
   - DLL file
   - plugin.json
   - Images folder
   - Required DLLs

## Next Steps

- Read the [Configuration Guide](Configuration.md) to customize settings
- Check the [Troubleshooting Guide](Troubleshooting.md) for common issues
- Review the [main README](../README.md) for usage instructions
