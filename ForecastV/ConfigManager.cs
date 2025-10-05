using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ForecastV
{
    /// <summary>
    /// Represents all configurable settings for ForecastV.
    /// </summary>
    [DataContract]
    public class ConfigData
    {
        /// <summary>Enables developer-only keybinds (manual weather refresh, debug notifications).</summary>
        [DataMember] public bool DeveloperOptions { get; set; } = false;

        /// <summary>Determines if in-game notifications appear when weather changes.</summary>
        [DataMember] public bool ShowNotifications { get; set; } = true;

        /// <summary>Latitude coordinate used for weather retrieval.</summary>
        [DataMember] public float Latitude { get; set; } = 34.0983f;

        /// <summary>Longitude coordinate used for weather retrieval.</summary>
        [DataMember] public float Longitude { get; set; } = -118.3267f;

        /// <summary>Number of minutes between automatic weather updates.</summary>
        [DataMember] public int UpdateIntervalMinutes { get; set; } = 5;
        /// <summary>The unit for displaying temperatures. Must be c/f/k.</summary>
        [DataMember] public string TemperatureUnit { get; set; } = "c";

        public void Validate()
        {
            // Validate Temperature
            if (string.IsNullOrWhiteSpace(TemperatureUnit))
            {
                TemperatureUnit = "c";
            }
            else
            {
                string unit = TemperatureUnit.ToLower();
                switch (unit)
                {
                    case "c":
                    case "celsius":
                        TemperatureUnit = "c";
                        break;
                    case "f":
                    case "fahrenheit":
                        TemperatureUnit = "f";
                        break;
                    case "k":
                    case "kelvin":
                        TemperatureUnit = "k";
                        break;
                    default:
                        TemperatureUnit = "c";
                        break;
                }
            }
            // Validate Update Interval Minutes
            if (UpdateIntervalMinutes < 0)
            {
                UpdateIntervalMinutes = 5;
            }
        }
    }

    /// <summary>
    /// Handles loading, saving, and managing the ForecastV configuration file.
    /// </summary>
    public static class ConfigManager
    {
        /// <summary>Path to the configuration JSON file in the GTA V scripts directory.</summary>
        private static readonly string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ForecastV.json");

        private static ConfigData _config;

        /// <summary>Provides access to the current configuration data.</summary>
        public static ConfigData Config => _config;

        /// <summary>
        /// Loads the configuration from disk.  
        /// If the file is missing, creates it with default values.
        /// </summary>
        public static void Load()
        {
            if (File.Exists(configPath))
            {
                using (var stream = File.OpenRead(configPath))
                {
                    var serializer = new DataContractJsonSerializer(typeof(ConfigData));
                    _config = (ConfigData)serializer.ReadObject(stream);
                }
            }
            else
            {
                _config = new ConfigData();
                Save();
            }

            // Validate and save back fixed data if needed
            _config.Validate();
            Save();
        }

        /// <summary>
        /// Saves the current configuration to disk, formatted for readability.
        /// </summary>
        public static void Save()
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(ConfigData));
                serializer.WriteObject(stream, _config);

                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    string indentedJson = PrettyPrintJson(json);
                    File.WriteAllText(configPath, indentedJson);
                }
            }
        }

        /// <summary>
        /// Pretty-prints JSON text with indentation and new lines.
        /// </summary>
        /// <param name="json">Minified JSON string.</param>
        /// <returns>Indented, human-readable JSON.</returns>
        private static string PrettyPrintJson(string json)
        {
            const int spacesPerIndent = 4;
            var indent = 0;
            var quoted = false;
            var sb = new StringBuilder();

            foreach (var ch in json)
            {
                switch (ch)
                {
                    case '{':
                    case '[':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            indent++;
                            sb.Append(new string(' ', indent * spacesPerIndent));
                        }
                        break;
                    case '}':
                    case ']':
                        if (!quoted)
                        {
                            sb.AppendLine();
                            indent--;
                            sb.Append(new string(' ', indent * spacesPerIndent));
                        }
                        sb.Append(ch);
                        break;
                    case '"':
                        sb.Append(ch);
                        bool escaped = sb.Length > 1 && sb[sb.Length - 2] == '\\';
                        if (!escaped) quoted = !quoted;
                        break;
                    case ',':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            sb.Append(new string(' ', indent * spacesPerIndent));
                        }
                        break;
                    case ':':
                        sb.Append(ch);
                        if (!quoted) sb.Append(' ');
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Allows modifying configuration data and immediately saving it to disk.
        /// </summary>
        /// <param name="modify">Action that applies changes to the config object.</param>
        public static void Set(Action<ConfigData> modify)
        {
            modify(_config);
            Save();
        }
    }
}
