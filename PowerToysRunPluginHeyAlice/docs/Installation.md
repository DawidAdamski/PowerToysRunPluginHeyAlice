# Installation Guide

This guide will walk you through installing the Hey Alice plugin for PowerToys Run.

## Prerequisites

Before installing the plugin, ensure you have:

- **Windows 10/11** installed
- **PowerToys** version 0.76.0 or higher installed ([Download](https://github.com/microsoft/PowerToys))
- **Hey Alice** application installed ([Download](https://heyalice.app))

## Installation Methods

### Method 1: Pre-built Release (Recommended)

This is the easiest method for most users.

#### Step 1: Download the Release

1. Go to the [Releases](https://github.com/DawidAdamski/PowerToysRunPluginHeyAlice/releases) page
2. Download the latest release archive (`.zip` file)
3. Choose the appropriate architecture:
   - `x64` for 64-bit Windows (most common)
   - `ARM64` for ARM-based Windows devices

#### Step 2: Close PowerToys

**Important:** PowerToys must be completely closed before installation.

1. Right-click the PowerToys icon in the system tray
2. Select **Exit** or **Quit**
3. Verify PowerToys is closed (icon should disappear from system tray)

#### Step 3: Extract the Plugin

1. Extract the downloaded `.zip` file
2. You should see a folder named `Community.PowerToys.Run.Plugin.HeyAlice`

#### Step 4: Copy to PowerToys Directory

1. Press `Win + R` to open Run dialog
2. Type: `%LOCALAPPDATA%\Microsoft\PowerToys\PowerToys Run\Plugins`
3. Press Enter to open the folder
4. Copy the `Community.PowerToys.Run.Plugin.HeyAlice` folder into this directory

**Full path should be:**
```
%LOCALAPPDATA%\Microsoft\PowerToys\PowerToys Run\Plugins\Community.PowerToys.Run.Plugin.HeyAlice
```

#### Step 5: Verify Installation

1. Open PowerToys Settings
2. Navigate to **PowerToys Run** > **Plugins**
3. You should see **"Hey Alice"** in the plugins list
4. The plugin should be enabled by default

#### Step 6: Test the Plugin

1. Press `Alt + Space` (default PowerToys Run shortcut)
2. Type `al` and you should see the Hey Alice plugin options
3. If it works, installation is complete!

### Method 2: Build from Source

If you want to build the plugin yourself, see the [Building Guide](Building.md).

## Verifying Installation

After installation, you can verify the plugin is working:

1. **Check Plugin List:**
   - Open PowerToys Settings
   - Go to **PowerToys Run** > **Plugins**
   - Find "Hey Alice" in the list

2. **Test Basic Command:**
   - Press `Alt + Space`
   - Type `al`
   - You should see "Open a new chat in Hey Alice"

3. **Check Plugin Files:**
   - Navigate to: `%LOCALAPPDATA%\Microsoft\PowerToys\PowerToys Run\Plugins\Community.PowerToys.Run.Plugin.HeyAlice`
   - Verify these files exist:
     - `Community.PowerToys.Run.Plugin.HeyAlice.dll`
     - `plugin.json`
     - `Images\HeyAlice.dark.png`
     - `Images\HeyAlice.light.png`

## Uninstallation

To uninstall the plugin:

1. Close PowerToys completely
2. Navigate to: `%LOCALAPPDATA%\Microsoft\PowerToys\PowerToys Run\Plugins`
3. Delete the `Community.PowerToys.Run.Plugin.HeyAlice` folder
4. Restart PowerToys

## Troubleshooting Installation

### Plugin Not Appearing

- **Solution 1:** Ensure PowerToys is completely closed before installation
- **Solution 2:** Check that the folder is in the correct location
- **Solution 3:** Verify PowerToys version is 0.76.0 or higher
- **Solution 4:** Check PowerToys logs for errors:
  - `%LOCALAPPDATA%\Microsoft\PowerToys\PowerToys Run\Logs`

### Plugin Appears But Doesn't Work

- **Solution 1:** Verify Hey Alice is installed
- **Solution 2:** Check that deep links work manually:
  - Open Run dialog (`Win + R`)
  - Type: `alice://chat/new`
  - Press Enter - Hey Alice should open
- **Solution 3:** Check plugin logs for errors

### Icons Not Displaying

- **Solution 1:** Verify `Images` folder exists in plugin directory
- **Solution 2:** Check that `HeyAlice.dark.png` and `HeyAlice.light.png` exist
- **Solution 3:** Restart PowerToys

## Next Steps

After successful installation:

1. [Configure your assistants and skills](Configuration.md)
2. Learn about [available commands](../README.md#quick-start)
3. Read the [Troubleshooting Guide](Troubleshooting.md) if you encounter issues
