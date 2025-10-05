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
        /// <param name="latitude">Latitude for the forecast location.</param>
        /// <param name="longitude">Longitude for the forecast location.</param>
        public static async Task<int> GetWeatherCodeAsync(HttpClient client, float latitude, float longitude)
        {
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,weather_code&timezone=auto";

            try
            {
                string json = await client.GetStringAsync(url).ConfigureAwait(false);

                // Use DataContractJsonSerializer (part of .NET) to avoid external dependencies.
                var serializer = new DataContractJsonSerializer(typeof(Models.WeatherResponse));
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    var response = serializer.ReadObject(ms) as Models.WeatherResponse;
                    return response?.Current?.WeatherCode ?? -1;
                }
            }
            catch (Exception ex)
            {
                // Show a small notification so the user can see issues in-game (developer mode doesn't matter).
                Notification.Show($"ForecastV JSON error: {ex.Message}");
                return -1;
            }
        }
    }
}
