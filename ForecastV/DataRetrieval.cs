using System;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using GTA.UI;

namespace ForecastV
{
    /// <summary>
    /// Handles retrieval of real-world weather data from the Open-Meteo API
    /// using only built-in .NET serializers (no external JSON libs).
    /// </summary>
    public static class DataRetrieval
    {
        /// <summary>
        /// Retrieves the current weather code from the Open-Meteo API for the specified coordinates.
        /// Returns -1 on error.
        /// </summary>
        /// <param name="client">The HTTP client</param>
        /// <param name="cfg">User configurations</param>
        public static async Task<Models.CurrentWeather> GetWeatherDataAsync(HttpClient client, ConfigData cfg)
        {
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={cfg.Latitude}&longitude={cfg.Longitude}&current=temperature_2m,weather_code&timezone=auto";

            try
            {
                string json = await client.GetStringAsync(url).ConfigureAwait(false);

                // Use DataContractJsonSerializer (part of .NET) to avoid external dependencies.
                var serializer = new DataContractJsonSerializer(typeof(Models.WeatherResponse));
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    var response = serializer.ReadObject(ms) as Models.WeatherResponse;
                    return response?.Current;
                }
            }
            catch (Exception ex)
            {
                // Show a small notification so the user can see issues in-game (developer mode doesn't matter).
                Notification.Show($"ForecastV JSON error: {ex.Message}");
                throw new Exception("An error appeared while trying to parse received JSON data.");
            }
        }
    }
}
