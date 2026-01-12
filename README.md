# PowerToys Run: Hey Alice Plugin

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![PowerToys](https://img.shields.io/badge/PowerToys-0.76.0+-blue.svg)](https://github.com/microsoft/PowerToys)

**A powerful PowerToys Run plugin that enables quick access to Hey Alice AI assistant directly from your launcher.**

Open new chats, access your assistants, activate skills, and view chat history instantly with PowerToys Run - no more manually opening Hey Alice and navigating through menus.

## 🚀 Features

- **💬 Quick Chat Access**: Launch new chats in Hey Alice with a single command
- **🤖 Assistant Management**: Quickly switch between your configured assistants
- **⚡ Skill Activation**: Instantly activate your favorite skills
- **📜 History Access**: Open chat history with one command
- **⌨️ Prompt Pre-filling**: Start conversations with pre-filled prompts
- **⚡ Lightning Fast**: Integrated with PowerToys Run for optimal performance
- **🎨 Native Icons**: Uses Hey Alice application icons for authentic look
- **⚙️ Easy Configuration**: Manage assistants and skills through PowerToys Settings UI

## 📸 Screenshots

*Coming soon - screenshots will be added here*

## 🎯 Use Cases

- **Rapid AI Access**: Start conversations with Hey Alice without opening the app
- **Assistant Switching**: Quickly switch between different AI assistants
- **Skill Activation**: Instantly activate specific skills for specialized tasks
- **Workflow Integration**: Integrate Hey Alice into your daily workflow
- **Productivity Boost**: Reduce context switching and improve efficiency

## 📋 Requirements

- **Windows 10/11**: Compatible with modern Windows versions
- **PowerToys**: Minimum version 0.76.0 or higher ([Download](https://github.com/microsoft/PowerToys))
- **Hey Alice**: Latest version of Hey Alice application installed ([Download](https://heyalice.app))

## 🔧 Installation

### Method 1: Manual Installation (Recommended)

1. **Download**: Get the latest release from [Releases](https://github.com/DawidAdamski/PowerToysRunPluginHeyAlice/releases)
2. **Close PowerToys**: Ensure PowerToys is completely closed
3. **Extract**: Unzip the archive to:
   ```
   %LOCALAPPDATA%\Microsoft\PowerToys\PowerToys Run\Plugins\Community.PowerToys.Run.Plugin.HeyAlice
   ```
4. **Restart**: Open PowerToys and the plugin will be automatically loaded

For detailed installation instructions, see [Installation Guide](PowerToysRunPluginHeyAlice/docs/Installation.md).

## 🚀 Quick Start

1. **Activate PowerToys Run**: Press `Alt + Space` (default shortcut)
2. **Type your command**: Start with `al` followed by your action
3. **Select & Execute**: Choose your option and press `Enter`

### Basic Commands

| Command | Action |
|---------|--------|
| `al` | Open a new chat |
| `al <text>` | Open new chat with pre-filled prompt |
| `alh` or `al h` | Open chat history |
| `ala` | Open a new chat |
| `ala <shortcut>` | Open new chat with specific assistant |
| `ala <shortcut> <prompt>` | Open new chat with assistant and prompt |
| `als <shortcut>` | Open/activate a specific skill |

**Example:**
```
al How can I improve my code?
ala cg Write a professional email
als blog Create a blog post about AI
```

## ⚙️ Configuration

Configure your assistants and skills through PowerToys Settings:

1. Open **PowerToys Settings**
2. Navigate to **PowerToys Run** > **Plugins** > **Hey Alice**
3. Find **"Assistants and Skills Configuration"** field
4. Enter your configuration in JSON format

### Example Configuration

```json
{
  "Assistants": [
    {
      "Name": "Alice",
      "Uuid": "alice",
      "Shortcut": "al"
    },
    {
      "Name": "CommunicationGuru",
      "Uuid": "your-assistant-uuid-here",
      "Shortcut": "cg"
    }
  ],
  "Skills": [
    {
      "Name": "Blog Writer",
      "Uuid": "your-skill-uuid-here",
      "Shortcut": "blog"
    }
  ]
}
```

For detailed configuration instructions, see [Configuration Guide](PowerToysRunPluginHeyAlice/docs/Configuration.md).

## 🔗 Deep Links

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

**Note:** Skills do not support prompts via deep links.

## 🛠️ Building from Source

### Requirements

- .NET 9.0 SDK or later ([Download](https://dotnet.microsoft.com/download))
- PowerToys installed (for required DLL references)
- PowerShell 5.1 or later

### Quick Build

```powershell
# Build and install in one command
.\build-and-install.ps1
```

For detailed build instructions, see [Building Guide](PowerToysRunPluginHeyAlice/docs/Building.md).

## 🐛 Troubleshooting

### Common Issues

- **Plugin not appearing**: Ensure PowerToys is restarted after installation
- **Deep links not working**: Verify Hey Alice is installed and running
- **Assistants/Skills not found**: Check your configuration JSON format
- **Icons not displaying**: Ensure Images folder is in the plugin directory

For more troubleshooting help, see [Troubleshooting Guide](PowerToysRunPluginHeyAlice/docs/Troubleshooting.md).

## 📚 Documentation

- [Installation Guide](PowerToysRunPluginHeyAlice/docs/Installation.md) - Detailed installation instructions
- [Configuration Guide](PowerToysRunPluginHeyAlice/docs/Configuration.md) - How to configure assistants and skills
- [Building Guide](PowerToysRunPluginHeyAlice/docs/Building.md) - Building from source
- [Troubleshooting Guide](PowerToysRunPluginHeyAlice/docs/Troubleshooting.md) - Common issues and solutions

## 🤝 Contributing

We welcome contributions! Please see our [Contributing Guidelines](CONTRIBUTING.md) for details.

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## 📝 License

This project is licensed under the [MIT License](LICENSE) - see the LICENSE file for details.

## 🙏 Attribution

This plugin uses deep links provided by [Hey Alice](https://heyalice.app). Special thanks to the PowerToys community and contributors.

## 🏷️ Keywords

`powertoys` `powertoys-run` `powertoys-plugin` `hey-alice` `alice-ai` `assistant` `ai-chat` `deep-links` `productivity` `windows` `dotnet` `csharp` `plugin-development` `launcher` `quick-access`

---

⭐ **Star this repository** if you find it useful!

🐛 **Report issues** on our [GitHub Issues](https://github.com/DawidAdamski/PowerToysRunPluginHeyAlice/issues) page

💬 **Join the discussion** in our [GitHub Discussions](https://github.com/DawidAdamski/PowerToysRunPluginHeyAlice/discussions)
