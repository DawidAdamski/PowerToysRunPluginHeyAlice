// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Controls;
using ManagedCommon;
using Microsoft.PowerToys.Settings.UI.Library;
using Wox.Plugin;
using Wox.Plugin.Logger;

namespace Community.PowerToys.Run.Plugin.HeyAlice
{
    public class AssistantConfig
    {
        public string Name { get; set; } = string.Empty;
        public string Uuid { get; set; } = string.Empty;
        public string Shortcut { get; set; } = string.Empty;
    }

    public class SkillConfig
    {
        public string Name { get; set; } = string.Empty;
        public string Uuid { get; set; } = string.Empty;
        public string Shortcut { get; set; } = string.Empty;
    }

    public class PluginConfig
    {
        public List<AssistantConfig> Assistants { get; set; } = new List<AssistantConfig>();
        public List<SkillConfig> Skills { get; set; } = new List<SkillConfig>();
    }

    public class Main : IPlugin, IPluginI18n, ISettingProvider, IReloadable, IDisposable
    {
        private PluginInitContext _context;
        private string _iconPath;
        private bool _disposed;
        private PluginConfig _config = new PluginConfig();

        private const string ConfigJsonKey = "AssistantsAndSkillsConfig";

        public string Name => Properties.Resources.plugin_name;

        public string Description => Properties.Resources.plugin_description;

        public static string PluginID => "7D94A00B6F58454F9C88552157298B5C";

        // Constructor
        public Main()
        {
        }

        public List<Result> Query(Query query)
        {
            if (query is null)
            {
                return new List<Result>();
            }

            var results = new List<Result>();
            string search = query.Search?.Trim() ?? string.Empty;

            // Empty query - show new chat option
            if (string.IsNullOrEmpty(search))
            {
                results.Add(new Result
                {
                    Title = Properties.Resources.plugin_new_chat,
                    SubTitle = "Open a new chat in Hey Alice",
                    QueryTextDisplay = string.Empty,
                    IcoPath = _iconPath,
                    Action = _ =>
                    {
                        OpenDeepLink("alice://chat/new");
                        return true;
                    },
                });
                return results;
            }

            var searchLower = search.ToLower(CultureInfo.CurrentCulture);

            // Check for history command: "h" or "history"
            if (searchLower.Equals("h", StringComparison.OrdinalIgnoreCase) || 
                searchLower.Equals("history", StringComparison.OrdinalIgnoreCase))
            {
                results.Add(new Result
                {
                    Title = Properties.Resources.plugin_chat_history,
                    SubTitle = "Open chat history panel",
                    QueryTextDisplay = search,
                    IcoPath = _iconPath,
                    Action = _ =>
                    {
                        OpenDeepLink("alice://chat/history");
                        return true;
                    },
                });
                return results;
            }

            // Check for assistant command: "a" (empty) or "a <shortcut/name/uuid> [prompt]"
            if (searchLower.Equals("a", StringComparison.OrdinalIgnoreCase))
            {
                // "ala" without parameters - same as "al"
                results.Add(new Result
                {
                    Title = Properties.Resources.plugin_new_chat,
                    SubTitle = "Open a new chat in Hey Alice",
                    QueryTextDisplay = search,
                    IcoPath = _iconPath,
                    Action = _ =>
                    {
                        OpenDeepLink("alice://chat/new");
                        return true;
                    },
                });
                return results;
            }

            if (searchLower.StartsWith("a ", StringComparison.OrdinalIgnoreCase))
            {
                string assistantQuery = search.Substring(2).Trim();
                return HandleAssistantQuery(assistantQuery);
            }

            // Check for skill command: "s" (empty) or "s <shortcut/name/uuid>"
            if (searchLower.Equals("s", StringComparison.OrdinalIgnoreCase))
            {
                // "als" without parameters - show placeholder
                results.Add(new Result
                {
                    Title = "Open skill",
                    SubTitle = "Enter skill shortcut, name, or UUID",
                    QueryTextDisplay = "als ",
                    IcoPath = _iconPath,
                    Action = _ => true,
                });
                return results;
            }

            if (searchLower.StartsWith("s ", StringComparison.OrdinalIgnoreCase))
            {
                string skillQuery = search.Substring(2).Trim();
                return HandleSkillQuery(skillQuery);
            }

            // Default: treat as prompt text for new chat
            if (!string.IsNullOrEmpty(search))
            {
                string promptText = Uri.EscapeDataString(search);
                results.Add(new Result
                {
                    Title = string.Format(CultureInfo.CurrentCulture, Properties.Resources.plugin_chat_with_prompt, search),
                    SubTitle = "New chat with prompt",
                    QueryTextDisplay = search,
                    IcoPath = _iconPath,
                    Action = _ =>
                    {
                        OpenDeepLink($"alice://newchat?prompt={promptText}");
                        return true;
                    },
                });
            }

            return results;
        }

        private List<Result> HandleAssistantQuery(string query)
        {
            var results = new List<Result>();

            if (string.IsNullOrEmpty(query))
            {
                results.Add(new Result
                {
                    Title = Properties.Resources.plugin_new_chat,
                    SubTitle = "Open a new chat in Hey Alice",
                    QueryTextDisplay = "ala ",
                    IcoPath = _iconPath,
                    Action = _ =>
                    {
                        OpenDeepLink("alice://chat/new");
                        return true;
                    },
                });
                return results;
            }

            // Check if query contains a prompt (has spaces after assistant identifier)
            string[] parts = query.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            string assistantIdentifier = parts[0];
            string prompt = parts.Length > 1 ? parts[1] : string.Empty;

            // Find assistant by shortcut, name, or UUID
            AssistantConfig assistant = FindAssistant(assistantIdentifier);

            if (assistant != null)
            {
                string title;
                string subtitle;
                string deepLink;

                if (!string.IsNullOrEmpty(prompt))
                {
                    string promptText = Uri.EscapeDataString(prompt);
                    title = string.Format(CultureInfo.CurrentCulture, "New chat with {0}: {1}", assistant.Name, prompt);
                    subtitle = "Assistant: " + assistant.Name;
                    deepLink = $"alice://newchat?assistant={assistant.Uuid}&prompt={promptText}";
                }
                else
                {
                    title = string.Format(CultureInfo.CurrentCulture, Properties.Resources.plugin_open_assistant, assistant.Name);
                    subtitle = "Open assistant";
                    deepLink = $"alice://newchat?assistant={assistant.Uuid}";
                }

                results.Add(new Result
                {
                    Title = title,
                    SubTitle = subtitle,
                    QueryTextDisplay = query,
                    IcoPath = _iconPath,
                    Action = _ =>
                    {
                        OpenDeepLink(deepLink);
                        return true;
                    },
                });
            }
            else
            {
                // Not found in config, try as UUID or name directly
                string assistantId = IsValidGuid(assistantIdentifier) ? assistantIdentifier : assistantIdentifier;
                string title = !string.IsNullOrEmpty(prompt)
                    ? string.Format(CultureInfo.CurrentCulture, "New chat with {0}: {1}", assistantIdentifier, prompt)
                    : string.Format(CultureInfo.CurrentCulture, Properties.Resources.plugin_open_assistant, assistantIdentifier);
                string subtitle = !string.IsNullOrEmpty(prompt) ? "Assistant: " + assistantIdentifier : "Open assistant";
                string promptText = !string.IsNullOrEmpty(prompt) ? Uri.EscapeDataString(prompt) : string.Empty;
                string deepLink = !string.IsNullOrEmpty(prompt)
                    ? $"alice://newchat?assistant={assistantId}&prompt={promptText}"
                    : $"alice://newchat?assistant={assistantId}";

                results.Add(new Result
                {
                    Title = title,
                    SubTitle = subtitle,
                    QueryTextDisplay = query,
                    IcoPath = _iconPath,
                    Action = _ =>
                    {
                        OpenDeepLink(deepLink);
                        return true;
                    },
                });
            }

            return results;
        }

        private List<Result> HandleSkillQuery(string query)
        {
            var results = new List<Result>();

            if (string.IsNullOrEmpty(query))
            {
                results.Add(new Result
                {
                    Title = "Open skill",
                    SubTitle = "Enter skill shortcut, name, or UUID",
                    QueryTextDisplay = "als ",
                    IcoPath = _iconPath,
                    Action = _ => true,
                });
                return results;
            }

            // Skills don't support prompts, so we ignore any text after the skill identifier
            string skillIdentifier = query.Trim();

            // Find skill by shortcut, name, or UUID
            SkillConfig skill = FindSkill(skillIdentifier);

            if (skill != null)
            {
                Log.Info($"Found skill: {skill.Name}, UUID: {skill.Uuid}", GetType());
                string title = string.Format(CultureInfo.CurrentCulture, Properties.Resources.plugin_open_skill, skill.Name);
                string subtitle = "Open skill";
                string deepLink = $"alice://snippet/{skill.Uuid}";
                
                Log.Info($"Generated deep link for skill: {deepLink}", GetType());

                results.Add(new Result
                {
                    Title = title,
                    SubTitle = subtitle,
                    QueryTextDisplay = query,
                    IcoPath = _iconPath,
                    Action = _ =>
                    {
                        OpenDeepLink(deepLink);
                        return true;
                    },
                });
            }
            else
            {
                // Not found in config, try as UUID or name directly
                Log.Info($"Skill not found in config, using identifier directly: {skillIdentifier}", GetType());
                string skillId = IsValidGuid(skillIdentifier) ? skillIdentifier : skillIdentifier;
                string title = string.Format(CultureInfo.CurrentCulture, Properties.Resources.plugin_open_skill, skillIdentifier);
                string subtitle = "Open skill";
                string deepLink = $"alice://snippet/{skillId}";
                
                Log.Info($"Generated deep link for skill (not in config): {deepLink}", GetType());

                results.Add(new Result
                {
                    Title = title,
                    SubTitle = subtitle,
                    QueryTextDisplay = query,
                    IcoPath = _iconPath,
                    Action = _ =>
                    {
                        OpenDeepLink(deepLink);
                        return true;
                    },
                });
            }

            return results;
        }

        private AssistantConfig FindAssistant(string identifier)
        {
            if (string.IsNullOrEmpty(identifier))
                return null;

            string identifierLower = identifier.ToLower(CultureInfo.CurrentCulture);
            return _config.Assistants.FirstOrDefault(a =>
                a.Shortcut.Equals(identifierLower, StringComparison.OrdinalIgnoreCase) ||
                a.Name.Equals(identifier, StringComparison.OrdinalIgnoreCase) ||
                a.Uuid.Equals(identifier, StringComparison.OrdinalIgnoreCase));
        }

        private SkillConfig FindSkill(string identifier)
        {
            if (string.IsNullOrEmpty(identifier))
                return null;

            string identifierLower = identifier.ToLower(CultureInfo.CurrentCulture);
            return _config.Skills.FirstOrDefault(s =>
                s.Shortcut.Equals(identifierLower, StringComparison.OrdinalIgnoreCase) ||
                s.Name.Equals(identifier, StringComparison.OrdinalIgnoreCase) ||
                s.Uuid.Equals(identifier, StringComparison.OrdinalIgnoreCase));
        }

        public void Init(PluginInitContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _context.API.ThemeChanged += OnThemeChanged;
            UpdateIconPath(_context.API.GetCurrentTheme());
            
            // Initialize with default configuration
            LoadConfigurationFromJson(GetDefaultConfigJson());
        }

        private void LoadConfigurationFromJson(string json)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(json))
                {
                    _config = JsonSerializer.Deserialize<PluginConfig>(json) ?? new PluginConfig();
                }
                else
                {
                    _config = new PluginConfig();
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to parse configuration JSON: {ex.Message}", GetType());
                _config = new PluginConfig();
            }
        }

        private string GetDefaultConfigJson()
        {
            var defaultConfig = new PluginConfig
            {
                Assistants = new List<AssistantConfig>
                {
                    new AssistantConfig
                    {
                        Name = "Alice",
                        Uuid = "alice",
                        Shortcut = "al"
                    },
                    new AssistantConfig
                    {
                        Name = "Example Assistant",
                        Uuid = "your-assistant-uuid-here",
                        Shortcut = "exa"
                    }
                },
                Skills = new List<SkillConfig>
                {
                    new SkillConfig
                    {
                        Name = "Example Skill",
                        Uuid = "your-skill-uuid-here",
                        Shortcut = "exs"
                    }
                }
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(defaultConfig, options);
        }

        private void OpenDeepLink(string url)
        {
            try
            {
                // Log the URL being opened for debugging
                Log.Info($"Opening Hey Alice deep link: {url}", GetType());
                
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true,
                });
                Log.Info($"Successfully opened Hey Alice deep link: {url}", GetType());
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to open Hey Alice: {ex.Message}. URL was: {url}", GetType());
                _context?.API.ShowMsg(
                    $"Plugin: {Properties.Resources.plugin_name}",
                    $"{Properties.Resources.plugin_failed_to_open}\nURL: {url}");
            }
        }

        private static bool IsValidGuid(string input)
        {
            return Guid.TryParse(input, out _);
        }

        public string GetTranslatedPluginTitle()
        {
            return Properties.Resources.plugin_name;
        }

        public string GetTranslatedPluginDescription()
        {
            return Properties.Resources.plugin_description;
        }

        public Control CreateSettingPanel()
        {
            throw new NotImplementedException();
        }

        public void UpdateSettings(PowerLauncherPluginSettings settings)
        {
            // Load configuration from settings
            var configOption = settings?.AdditionalOptions?.FirstOrDefault(x => x.Key == ConfigJsonKey);
            if (configOption != null && !string.IsNullOrWhiteSpace(configOption.TextValue))
            {
                LoadConfigurationFromJson(configOption.TextValue);
            }
            else
            {
                // If no configuration provided, use empty config
                _config = new PluginConfig();
            }
        }

        public IEnumerable<PluginAdditionalOption> AdditionalOptions => new List<PluginAdditionalOption>()
        {
            new PluginAdditionalOption()
            {
                Key = ConfigJsonKey,
                DisplayLabel = "Assistants and Skills Configuration",
                DisplayDescription = "Configure your assistants and skills in JSON format. Each assistant/skill needs: Name, Uuid, and Shortcut. See placeholder for example.",
                PluginOptionType = PluginAdditionalOption.AdditionalOptionType.MultilineTextbox,
                PlaceholderText = GetDefaultConfigJson(),
                TextValue = string.Empty,
            },
        };

        private void OnThemeChanged(Theme oldTheme, Theme newTheme)
        {
            UpdateIconPath(newTheme);
        }

        private void UpdateIconPath(Theme theme)
        {
            if (theme == Theme.Light || theme == Theme.HighContrastWhite)
            {
                _iconPath = "Images/HeyAlice.light.png";
            }
            else
            {
                _iconPath = "Images/HeyAlice.dark.png";
            }
        }

        public void ReloadData()
        {
            if (_context is null)
            {
                return;
            }

            UpdateIconPath(_context.API.GetCurrentTheme());
            // Configuration will be reloaded when UpdateSettings is called
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                if (_context != null && _context.API != null)
                {
                    _context.API.ThemeChanged -= OnThemeChanged;
                }

                _disposed = true;
            }
        }
    }
}
