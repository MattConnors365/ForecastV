using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static ForecastV.Models;

namespace ForecastV
{
    /// <summary>
    /// Handles retrieval of real-world weather data from the Open-Meteo API.
    /// </summary>
    public static class DataRetrieval
    {
        private static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Retrieves the current weather code from the Open-Meteo API based on coordinates.
        /// </summary>
        /// <param name="latitude">Latitude for the forecast location.</param>
        /// <param name="longitude">Longitude for the forecast location.</param>
        /// <returns>
        /// The Open-Meteo weather code, or -1 if retrieval or deserialization fails.
        /// </returns>
        public static async Task<int> GetWeatherCodeAsync(float latitude, float longitude)
        {
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,weather_code&timezone=auto";

            try
            {
                string json = await client.GetStringAsync(url);
                var response = JsonConvert.DeserializeObject<WeatherResponse>(json);
                return response?.Current?.WeatherCode ?? -1;
            }
            catch (Exception ex)
            {
                // Log to console if debug logging is enabled in future versions.
                Console.WriteLine($"Weather fetch error: {ex.Message}");
                return -1;
            }
        }
    }
}
