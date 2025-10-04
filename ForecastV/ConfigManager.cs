using System;
using System.IO;
using System.Reflection;

namespace ForecastV
{
    /// <summary>
    /// Handles loading, reading, and generating configuration values for ForecastV.
    /// The configuration file (<c>ForecastV.ini</c>) is generated automatically on first run.
    /// </summary>
    public static class ConfigManager
    {
        // Path to the configuration file in the GTA V scripts directory.
        private static readonly string configPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "ForecastV.ini");

        /// <summary>Enables developer-only keybinds (manual weather refresh, debug notifications).</summary>
        public static bool DeveloperOptions { get; private set; } = false;

        /// <summary>Determines if in-game notifications appear when weather changes.</summary>
        public static bool ShowNotifications { get; private set; } = true;

        /// <summary>Latitude coordinate used for weather retrieval.</summary>
        public static float Latitude { get; private set; } = 34.0983f;

        /// <summary>Longitude coordinate used for weather retrieval.</summary>
        public static float Longitude { get; private set; } = -118.3267f;

        /// <summary>Number of minutes between automatic weather updates.</summary>
        public static int UpdateIntervalMinutes { get; private set; } = 5;

        /// <summary>
        /// Loads configuration from <c>ForecastV.ini</c>.  
        /// If the file does not exist, creates one with default values.
        /// </summary>
        public static void Load()
        {
            // Generate default configuration file if it doesn't exist.
            if (!File.Exists(configPath))
            {
                File.WriteAllLines(configPath, new[]
                {
                    "[Settings]",
                    $"DeveloperOptions={DeveloperOptions}",
                    $"ShowNotifications={ShowNotifications}",
                    $"Latitude={Latitude}",
                    $"Longitude={Longitude}",
                    $"UpdateIntervalMinutes={UpdateIntervalMinutes}"
                });
                return;
            }

            // Read and parse key-value pairs.
            var lines = File.ReadAllLines(configPath);
            foreach (var line in lines)
            {
                if (line.StartsWith("DeveloperOptions="))
                {
                    if (bool.TryParse(line.Substring("DeveloperOptions=".Length), out bool tmpDev))
                        DeveloperOptions = tmpDev;
                }
                else if (line.StartsWith("ShowNotifications="))
                {
                    if (bool.TryParse(line.Substring("ShowNotifications=".Length), out bool tmpNot))
                        ShowNotifications = tmpNot;
                }
                else if (line.StartsWith("Latitude="))
                {
                    if (float.TryParse(line.Substring("Latitude=".Length), out float tmpLat))
                        Latitude = tmpLat;
                }
                else if (line.StartsWith("Longitude="))
                {
                    if (float.TryParse(line.Substring("Longitude=".Length), out float tmpLon))
                        Longitude = tmpLon;
                }
                else if (line.StartsWith("UpdateIntervalMinutes="))
                {
                    if (int.TryParse(line.Substring("UpdateIntervalMinutes=".Length), out int tmpInt))
                        UpdateIntervalMinutes = tmpInt;
                }
            }
        }
    }
}
