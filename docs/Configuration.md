# Configuration Guide

This guide explains how to configure assistants and skills in the Hey Alice plugin.

## Overview

The plugin allows you to configure custom assistants and skills that you can quickly access using shortcuts. Configuration is done through the PowerToys Settings UI using JSON format.

## Accessing Configuration

1. Open **PowerToys Settings**
2. Navigate to **PowerToys Run** > **Plugins** > **Hey Alice**
3. Find the **"Assistants and Skills Configuration"** field (multiline textbox)
4. Enter your configuration in JSON format

## Configuration Format

The configuration uses a JSON structure with two main sections: `Assistants` and `Skills`.

### Basic Structure

```json
{
  "Assistants": [
    {
      "Name": "Display Name",
      "Uuid": "assistant-uuid-here",
      "Shortcut": "shortcut"
    }
  ],
  "Skills": [
    {
      "Name": "Display Name",
      "Uuid": "skill-uuid-here",
      "Shortcut": "shortcut"
    }
  ]
}
```

### Default Configuration

When you first open the configuration, it will be pre-filled with an example:

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

## Finding UUIDs

### Assistant UUID

1. Open **Hey Alice** application
2. Go to **Settings** > **Assistants**
3. Click on the assistant you want to configure
4. The UUID is displayed below the assistant name
5. Copy the UUID (it looks like: `12345678-1234-1234-1234-123456789abc`)

### Skill UUID

1. Open **Hey Alice** application
2. Go to **Settings** > **Skills**
3. Click on the skill you want to configure
4. The UUID is displayed below the skill name
5. Copy the UUID (it looks like: `12345678-1234-1234-1234-123456789abc`)

## Configuration Fields

### Assistant Configuration

Each assistant requires three fields:

- **Name** (string, required): Display name for the assistant
  - Example: `"CommunicationGuru"`
  - Used in plugin results display
  
- **Uuid** (string, required): Unique identifier from Hey Alice
  - Example: `"6849705a-fbe4-45a0-a04f-8aa348d1a2c5"`
  - Must match the UUID shown in Hey Alice settings
  
- **Shortcut** (string, required): Short alias for quick access
  - Example: `"cg"` for CommunicationGuru
  - Used in commands like `ala cg`
  - Should be short and memorable
  - Avoid conflicts with other shortcuts

### Skill Configuration

Each skill requires three fields:

- **Name** (string, required): Display name for the skill
  - Example: `"Blog Writer"`
  - Used in plugin results display
  
- **Uuid** (string, required): Unique identifier from Hey Alice
  - Example: `"6849705a-fbe4-45a0-a04f-8aa348d1a2c5"`
  - Must match the UUID shown in Hey Alice settings
  
- **Shortcut** (string, required): Short alias for quick access
  - Example: `"blog"` for Blog Writer
  - Used in commands like `als blog`
  - Should be short and memorable
  - Avoid conflicts with other shortcuts

## Example Configurations

### Simple Configuration

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

### Multiple Assistants

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
      "Uuid": "6849705a-fbe4-45a0-a04f-8aa348d1a2c5",
      "Shortcut": "cg"
    },
    {
      "Name": "CodeHelper",
      "Uuid": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
      "Shortcut": "code"
    }
  ],
  "Skills": [
    {
      "Name": "Blog Writer",
      "Uuid": "12345678-1234-1234-1234-123456789abc",
      "Shortcut": "blog"
    }
  ]
}
```

### Real-World Example

```json
{
  "Assistants": [
    {
      "Name": "Alice",
      "Uuid": "alice",
      "Shortcut": "al"
    },
    {
      "Name": "Email Assistant",
      "Uuid": "email-assistant-uuid",
      "Shortcut": "email"
    },
    {
      "Name": "Code Reviewer",
      "Uuid": "code-reviewer-uuid",
      "Shortcut": "review"
    }
  ],
  "Skills": [
    {
      "Name": "Blog Post Generator",
      "Uuid": "blog-generator-uuid",
      "Shortcut": "blog"
    },
    {
      "Name": "Code Documentation",
      "Uuid": "code-docs-uuid",
      "Shortcut": "docs"
    },
    {
      "Name": "Meeting Summarizer",
      "Uuid": "meeting-summary-uuid",
      "Shortcut": "meeting"
    }
  ]
}
```

## Using Your Configuration

After saving your configuration:

1. **Access Assistants:**
   - Type `ala <shortcut>` to open a chat with that assistant
   - Example: `ala cg` opens CommunicationGuru
   - Example: `ala cg Write an email` opens CommunicationGuru with a prompt

2. **Access Skills:**
   - Type `als <shortcut>` to activate that skill
   - Example: `als blog` activates the Blog Writer skill
   - **Note:** Skills do not support prompts via deep links

## Tips for Configuration

### Choosing Shortcuts

- **Keep them short:** 2-4 characters work best
- **Make them memorable:** Use abbreviations or first letters
- **Avoid conflicts:** Don't use the same shortcut for multiple items
- **Use lowercase:** Shortcuts are case-insensitive, but lowercase is cleaner

### Organizing Your Configuration

- **Group by purpose:** Keep related assistants/skills together
- **Add comments in JSON:** While not standard, you can add comments for clarity
- **Keep it updated:** Remove assistants/skills you no longer use

### Validation

The plugin validates your JSON configuration. If there's an error:

- Invalid JSON will be logged
- The plugin will fall back to default configuration
- Check PowerToys logs for error details:
  - `%LOCALAPPDATA%\Microsoft\PowerToys\PowerToys Run\Logs`

## Troubleshooting Configuration

### Configuration Not Saving

- **Solution:** Click "Save" button in PowerToys Settings
- **Solution:** Ensure JSON is valid (use a JSON validator)
- **Solution:** Check for syntax errors (commas, quotes, brackets)

### Assistants/Skills Not Found

- **Solution:** Verify UUIDs are correct (copy from Hey Alice settings)
- **Solution:** Check shortcut spelling (case-insensitive but must match exactly)
- **Solution:** Ensure JSON structure is correct

### Plugin Not Using Configuration

- **Solution:** Restart PowerToys after saving configuration
- **Solution:** Check plugin logs for parsing errors
- **Solution:** Verify configuration field is not empty

## Next Steps

- Learn about [available commands](../README.md#quick-start)
- Read the [Troubleshooting Guide](Troubleshooting.md) for more help
- Check out [Building Guide](Building.md) if you want to customize the plugin
