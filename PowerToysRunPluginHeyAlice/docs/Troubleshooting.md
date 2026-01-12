# Troubleshooting Guide

This guide helps you resolve common issues with the Hey Alice plugin.

## General Troubleshooting Steps

1. **Restart PowerToys** - Many issues are resolved by restarting
2. **Check PowerToys Version** - Ensure you have version 0.76.0 or higher
3. **Verify Hey Alice Installation** - Make sure Hey Alice is installed and working
4. **Check Plugin Logs** - Review logs for error messages

## Common Issues

### Plugin Not Appearing

**Symptoms:**
- Plugin doesn't show in PowerToys Run
- Plugin not listed in PowerToys Settings

**Solutions:**

1. **Verify Installation Location:**
   - Check plugin is in: `%LOCALAPPDATA%\Microsoft\PowerToys\PowerToys Run\Plugins\Community.PowerToys.Run.Plugin.HeyAlice`
   - Verify all files are present

2. **Restart PowerToys:**
   - Completely close PowerToys (check system tray)
   - Reopen PowerToys
   - Check plugin list again

3. **Check PowerToys Version:**
   - Open PowerToys Settings
   - Verify version is 0.76.0 or higher
   - Update if necessary

4. **Check Plugin Logs:**
   - Navigate to: `%LOCALAPPDATA%\Microsoft\PowerToys\PowerToys Run\Logs`
   - Look for errors related to Hey Alice plugin

### Deep Links Not Working

**Symptoms:**
- Commands execute but Hey Alice doesn't open
- Error message appears
- Nothing happens when selecting plugin options

**Solutions:**

1. **Verify Hey Alice Installation:**
   - Ensure Hey Alice is installed
   - Test deep link manually:
     - Press `Win + R`
     - Type: `alice://chat/new`
     - Press Enter
     - Hey Alice should open

2. **Check Hey Alice is Running:**
   - Open Hey Alice manually
   - Try plugin commands again

3. **Verify Deep Link Registration:**
   - Hey Alice should register `alice://` protocol
   - If not, reinstall Hey Alice

4. **Check Windows Default Apps:**
   - Settings > Apps > Default apps
   - Verify protocol handlers are set correctly

### Configuration Not Working

**Symptoms:**
- Assistants/Skills not found
- Shortcuts don't work
- Configuration appears empty

**Solutions:**

1. **Validate JSON Format:**
   - Use a JSON validator (e.g., jsonlint.com)
   - Check for syntax errors:
     - Missing commas
     - Unclosed brackets
     - Incorrect quotes

2. **Verify UUIDs:**
   - Copy UUIDs directly from Hey Alice settings
   - Ensure no extra spaces or characters
   - UUIDs should be in format: `xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx`

3. **Check Shortcut Conflicts:**
   - Ensure shortcuts are unique
   - Avoid using reserved words
   - Use lowercase for consistency

4. **Restart After Configuration:**
   - Save configuration in PowerToys Settings
   - Restart PowerToys
   - Test commands again

5. **Check Plugin Logs:**
   - Look for JSON parsing errors
   - Check for UUID validation errors

### Icons Not Displaying

**Symptoms:**
- Plugin shows default icon
- Icons appear broken
- Dark/Light mode icons not switching

**Solutions:**

1. **Verify Images Folder:**
   - Check `Images` folder exists in plugin directory
   - Verify files exist:
     - `HeyAlice.dark.png`
     - `HeyAlice.light.png`

2. **Check File Permissions:**
   - Ensure images are readable
   - Check file paths in `plugin.json`

3. **Regenerate Icons:**
   ```powershell
   .\extract-icon.ps1
   ```
   - Reinstall plugin after regenerating

4. **Verify plugin.json:**
   - Check `IcoPathDark` and `IcoPathLight` paths
   - Ensure paths use backslashes: `Images\\HeyAlice.dark.png`

### Commands Not Working

**Symptoms:**
- Typing `al` shows no results
- Commands don't execute
- Wrong results appear

**Solutions:**

1. **Check Action Keyword:**
   - Default is `al`
   - Verify in PowerToys Settings > Plugins > Hey Alice
   - Can be changed if needed

2. **Verify Command Syntax:**
   - `al` - new chat
   - `alh` - history
   - `ala <shortcut>` - assistant
   - `als <shortcut>` - skill

3. **Check Plugin is Enabled:**
   - PowerToys Settings > PowerToys Run > Plugins
   - Ensure "Hey Alice" is enabled

4. **Clear PowerToys Cache:**
   - Close PowerToys
   - Delete cache folder (if exists)
   - Restart PowerToys

### Performance Issues

**Symptoms:**
- Plugin is slow to respond
- Commands take time to execute
- PowerToys Run becomes sluggish

**Solutions:**

1. **Check Configuration Size:**
   - Large configurations may slow down plugin
   - Consider reducing number of assistants/skills

2. **Verify System Resources:**
   - Check CPU/Memory usage
   - Close unnecessary applications

3. **Update PowerToys:**
   - Ensure latest version is installed
   - Check for performance updates

## Advanced Troubleshooting

### Enable Debug Logging

1. Open PowerToys Settings
2. Go to **General** > **Logging**
3. Enable **Debug** level logging
4. Reproduce the issue
5. Check logs in: `%LOCALAPPDATA%\Microsoft\PowerToys\PowerToys Run\Logs`

### Manual Plugin Test

1. **Test Deep Links Manually:**
   ```powershell
   # In PowerShell or Run dialog
   Start-Process "alice://chat/new"
   Start-Process "alice://chat/history"
   Start-Process "alice://snippet/your-uuid-here"
   ```

2. **Verify Plugin Files:**
   - Check all required files are present
   - Verify DLL is not corrupted
   - Ensure plugin.json is valid

3. **Test with Minimal Configuration:**
   ```json
   {
     "Assistants": [
       {
         "Name": "Alice",
         "Uuid": "alice",
         "Shortcut": "al"
       }
     ],
     "Skills": []
   }
   ```

### Reinstalling Plugin

If all else fails:

1. **Uninstall:**
   - Close PowerToys
   - Delete plugin folder: `%LOCALAPPDATA%\Microsoft\PowerToys\PowerToys Run\Plugins\Community.PowerToys.Run.Plugin.HeyAlice`

2. **Clean Install:**
   - Download fresh release
   - Follow [Installation Guide](Installation.md)
   - Configure from scratch

3. **Verify:**
   - Test basic commands
   - Check logs for errors

## Getting Help

If you're still experiencing issues:

1. **Check Logs:**
   - `%LOCALAPPDATA%\Microsoft\PowerToys\PowerToys Run\Logs`
   - Look for errors related to Hey Alice plugin

2. **Gather Information:**
   - PowerToys version
   - Hey Alice version
   - Windows version
   - Error messages from logs
   - Steps to reproduce

3. **Report Issue:**
   - [GitHub Issues](https://github.com/DawidAdamski/PowerToysRunPluginHeyAlice/issues)
   - Include all gathered information
   - Describe what you expected vs. what happened

## Prevention Tips

1. **Keep PowerToys Updated:**
   - Regular updates include bug fixes
   - Check for updates periodically

2. **Keep Hey Alice Updated:**
   - Ensure compatibility with latest features
   - Update when new versions are available

3. **Backup Configuration:**
   - Save your JSON configuration
   - Easy to restore if needed

4. **Test After Changes:**
   - Test commands after configuration changes
   - Verify everything works before relying on it

## Related Documentation

- [Installation Guide](Installation.md) - Reinstall if needed
- [Configuration Guide](Configuration.md) - Fix configuration issues
- [Building Guide](Building.md) - Build from source if needed
- [Main README](../README.md) - General usage and features
