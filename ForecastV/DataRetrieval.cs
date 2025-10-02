using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ForecastV.Models;

namespace ForecastV
{
    public static class DataRetrieval
    {
        private static readonly HttpClient client = new HttpClient();
        public static async Task<int> GetWeatherCodeAsync()
        {
            string url = "https://api.open-meteo.com/v1/forecast?latitude=34.0983&longitude=-118.3267&current=temperature_2m,weather_code&timezone=auto";

            try
            {
                string json = await client.GetStringAsync(url);

                var response = JsonConvert.DeserializeObject<WeatherResponse>(json);

                return response?.Current?.WeatherCode ?? -1; // -1 if something goes wrong
            }
            catch (Exception ex)
            {
                // For debugging, log it somewhere or notify in-game
                Console.WriteLine($"Weather fetch error: {ex.Message}");
                return -1;
            }
        }
    }
}
