# Hey Alice Plugin for PowerToys Run

This is a plugin for [PowerToys Run](https://github.com/microsoft/PowerToys/wiki/PowerToys-Run-Overview) that allows you to quickly access the [Hey Alice](https://heyalice.app) application via deep links.

## Features

- **`al`** - Open a new chat in Hey Alice
- **`al <text>`** - Open a new chat with pre-filled prompt text
- **`alh`** or **`al h`** - Open chat history
- **`ala`** - Open a new chat in Hey Alice
- **`ala <shortcut/name/uuid>`** - Open new chat with a specific assistant (e.g., `ala cg`)
- **`ala <shortcut/name/uuid> <prompt>`** - Open new chat with assistant and prompt (e.g., `ala cg odpisz Andrzejowi`)
- **`als <shortcut/name/uuid>`** - Open a specific skill (e.g., `als blog`)

**Note:** Skills do not support prompts via deep links, so `als <skill> <prompt>` is not available.

## Installation

### Manual Installation

1. Build the plugin (see Build section below)
2. Copy the plugin folder to your PowerToys modules directory:
   - Usually: `%LOCALAPPDATA%\Microsoft\PowerToys\PowerToys Run\Plugins\Community.PowerToys.Run.Plugin.HeyAlice`
3. Restart PowerToys

### Icons

The plugin uses icons extracted from the Hey Alice application. To update the icons:

```powershell
.\extract-icon.ps1
```

This script extracts the icon from `C:\Program Files\Alice\Alice.exe` and creates both light and dark versions.

## Build from Source

### Requirements
- .NET 9.0 SDK or later ([Download](https://dotnet.microsoft.com/download))
- PowerToys installed (for required DLL references)

### Build using PowerShell Scripts (Recommended)

The easiest way to build and install:

```powershell
# Build and install in one command
.\build-and-install.ps1
```

Or build and install separately:

```powershell
# Build only
.\build.ps1

# Install only (after building)
.\install.ps1
```

### Build using dotnet CLI manually

1. Navigate to the plugin directory
2. Build the project:
   ```powershell
   dotnet build -c Release
   ```
3. Copy required DLLs from `C:\Program Files\PowerToys\` to the output directory
4. Copy plugin files (plugin.json, Images) to the output directory
5. Copy the built DLL to the PowerToys plugins directory

### Build using Visual Studio (Alternative)

1. Clone the [PowerToys repo](https://github.com/microsoft/PowerToys) (if you haven't already)
2. Navigate to the `PowerToys` directory
3. Initialize submodules: `git submodule update --init --recursive`
4. Copy this plugin folder to `PowerToys/src/modules/launcher/Plugins/Community.PowerToys.Run.Plugin.HeyAlice`
5. Open `PowerToys.slnx` in Visual Studio
6. Add this project to the solution
7. Build the solution

## Usage

1. Open PowerToys Run (default shortcut is `Alt+Space`)
2. Type `al` followed by your command:
   - `al` - Opens a new chat
   - `al Hello, how are you?` - Opens a new chat with "Hello, how are you?" as the prompt
   - `alh` or `al h` - Opens chat history
   - `ala` - Opens a new chat
   - `ala cg` - Opens new chat with assistant with shortcut "cg" (configured in settings)
   - `ala cg odpisz Andrzejowi` - Opens new chat with assistant "cg" and prompt "odpisz Andrzejowi"
   - `als blog` - Opens skill with shortcut "blog" (configured in settings)

**Note:** Skills do not support prompts via deep links.

## Configuration

The plugin configuration is managed through PowerToys Settings UI.

### Setting up Assistants and Skills

1. Open PowerToys Settings
2. Go to **PowerToys Run** > **Plugins** > **Hey Alice**
3. Find the **"Assistants and Skills Configuration"** field
4. Enter your configuration in JSON format:

```json
{
  "Assistants": [
    {
      "Name": "Alice",
      "Uuid": "alice",
      "Shortcut": "al"
    },
    {
      "Name": "Example Assistant",
      "Uuid": "your-assistant-uuid-here",
      "Shortcut": "exa"
    }
  ],
  "Skills": [
    {
      "Name": "Example Skill",
      "Uuid": "your-skill-uuid-here",
      "Shortcut": "exs"
    }
  ]
}
```

**Note:** Alice is a special assistant with UUID "alice" and is included by default in the configuration.

5. Click **Save** - the configuration will be applied immediately

### Finding UUIDs

- **Assistant UUID**: Go to Hey Alice Settings > Assistants, click on an assistant, and find the UUID below the assistant name
- **Skill UUID**: Go to Hey Alice Settings > Skills, click on a skill, and find the UUID below the skill name

### Configuration Format

Each assistant and skill requires:
- **Name**: Display name (e.g., "CommunicationGuru")
- **Uuid**: The UUID from Hey Alice settings
- **Shortcut**: Short alias to use in commands (e.g., "cg" for CommunicationGuru)

## Deep Links

This plugin uses Hey Alice deep links as documented at:
https://heyalice.app/academy/deep-links-(x-scheme-url)

### Command to Deep Link Mapping

| Command | Deep Link |
|---------|-----------|
| `al` | `alice://chat/new` |
| `al <prompt>` | `alice://newchat?prompt=<prompt>` |
| `alh` | `alice://chat/history` |
| `ala` | `alice://chat/new` |
| `ala <shortcut>` | `alice://newchat?assistant=<uuid>` |
| `ala <shortcut> <prompt>` | `alice://newchat?assistant=<uuid>&prompt=<prompt>` |
| `als <shortcut>` | `alice://snippet/<uuid>` |

**Note:** Skills do not support prompts via deep links (`alice://snippet/<uuid>?prompt=<prompt>` does not work).

## Requirements

- Windows 10/11
- PowerToys installed
- Hey Alice application installed

## License

This project is licensed under the [MIT License](LICENSE).
