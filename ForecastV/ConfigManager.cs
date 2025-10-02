using System.IO;
using System.Reflection;

namespace ForecastV
{
    public static class ConfigManager
    {
        private static readonly string configPath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "", "ForecastV.ini");

        public static bool DeveloperOptions { get; private set; } = false;
        public static bool ShowNotifications { get; private set; } = true;
        public static float Latitude { get; private set; } = 34.0983f;
        public static float Longitude { get; private set; } = -118.3267f;
        public static int UpdateIntervalMinutes { get; private set; } = 5;

        public static void Load()
        {
            // If config file doesn't exist, create it with defaults
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
                return; // use defaults
            }

            // Read and parse existing config
            var lines = File.ReadAllLines(configPath);
            foreach (var line in lines)
            {
                if (line.StartsWith("DeveloperOptions="))
                {
                    if (bool.TryParse(line.Substring("DeveloperOptions=".Length), out bool tmpDev))
                        DeveloperOptions = tmpDev;
                }
                else if (line.StartsWith("ShowNotifications="))
                    if (bool.TryParse(line.Substring("ShowNotifications=".Length), out bool tmpNot))
                        ShowNotifications = tmpNot;
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
