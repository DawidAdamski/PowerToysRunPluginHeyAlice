# Contributing to Hey Alice Plugin

Thank you for your interest in contributing to the Hey Alice PowerToys Run plugin! This document provides guidelines and instructions for contributing.

## How to Contribute

There are many ways to contribute:

- 🐛 **Report bugs** - Help us identify and fix issues
- 💡 **Suggest features** - Share your ideas for improvements
- 📝 **Improve documentation** - Make the docs clearer and more helpful
- 🔧 **Submit code** - Fix bugs or add new features
- 🌍 **Translate** - Help translate the plugin to other languages

## Getting Started

1. **Fork the repository** on GitHub
2. **Clone your fork** locally:
   ```bash
   git clone https://github.com/your-username/PowerToysRunPluginHeyAlice.git
   ```
3. **Create a branch** for your changes:
   ```bash
   git checkout -b feature/your-feature-name
   ```

## Development Setup

1. **Install prerequisites:**
   - .NET 9.0 SDK or later
   - PowerToys installed
   - Visual Studio 2022 or VS Code (recommended)

2. **Build the project:**
   ```powershell
   .\build.ps1
   ```

3. **Install for testing:**
   ```powershell
   .\install.ps1
   ```

## Code Style

- Follow **C# coding conventions**
- Use **meaningful variable names**
- Add **comments** for complex logic
- Keep **methods focused** and small
- Follow **existing code style** in the project

## Submitting Changes

### Pull Request Process

1. **Update documentation** if needed
2. **Test your changes** thoroughly
3. **Commit your changes:**
   ```bash
   git commit -m "Description of changes"
   ```
4. **Push to your fork:**
   ```bash
   git push origin feature/your-feature-name
   ```
5. **Create a Pull Request** on GitHub

### Commit Messages

Use clear, descriptive commit messages:

- **Good:** `Fix deep link handling for skills without prompts`
- **Bad:** `fix bug` or `update`

### Pull Request Guidelines

- **Describe your changes** clearly
- **Reference related issues** if applicable
- **Include screenshots** for UI changes
- **Test on Windows 10/11** before submitting
- **Ensure code compiles** without errors

## Reporting Bugs

### Before Reporting

1. **Check existing issues** - Your bug might already be reported
2. **Test latest version** - Bug might be fixed already
3. **Gather information** - Logs, steps to reproduce, etc.

### Bug Report Template

```markdown
**Description:**
Clear description of the bug

**Steps to Reproduce:**
1. Step one
2. Step two
3. Step three

**Expected Behavior:**
What should happen

**Actual Behavior:**
What actually happens

**Environment:**
- Windows Version:
- PowerToys Version:
- Hey Alice Version:
- Plugin Version:

**Logs:**
Relevant log entries from PowerToys logs
```

## Suggesting Features

### Feature Request Template

```markdown
**Feature Description:**
Clear description of the feature

**Use Case:**
Why would this be useful?

**Proposed Solution:**
How should it work?

**Alternatives Considered:**
Other approaches you've thought about
```

## Code Contributions

### Areas for Contribution

- **Bug fixes** - Fix reported issues
- **Performance improvements** - Optimize code
- **New features** - Add requested functionality
- **Code cleanup** - Refactor and improve code quality
- **Tests** - Add unit tests (if test framework is added)

### Testing Your Changes

1. **Build the plugin:**
   ```powershell
   .\build.ps1
   ```

2. **Install and test:**
   ```powershell
   .\install.ps1
   ```

3. **Test all scenarios:**
   - Basic commands (`al`, `alh`, `ala`, `als`)
   - With prompts
   - With different assistants/skills
   - Edge cases

## Documentation Contributions

Documentation improvements are always welcome:

- **Fix typos** and grammar
- **Clarify instructions** that are unclear
- **Add examples** where helpful
- **Translate** to other languages
- **Add screenshots** for visual guides

## Translation Contributions

To contribute translations:

1. **Fork the repository**
2. **Create translation file:**
   - Location: `Properties\Resources.{locale}.resx`
   - Example: `Resources.pl-PL.resx` for Polish
3. **Translate strings** in the `.resx` file
4. **Test the translation** by building and running
5. **Submit a Pull Request**

## Questions?

- **GitHub Discussions** - Ask questions and discuss ideas
- **GitHub Issues** - Report bugs and request features
- **Pull Requests** - Submit your contributions

## Code of Conduct

- Be respectful and considerate
- Welcome newcomers and help them learn
- Focus on constructive feedback
- Respect different viewpoints and experiences

## License

By contributing, you agree that your contributions will be licensed under the MIT License.

## Recognition

Contributors will be recognized in:
- README.md (if significant contribution)
- Release notes (for major contributions)
- GitHub contributors page

Thank you for contributing! 🎉
