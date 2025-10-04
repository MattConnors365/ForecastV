using Newtonsoft.Json;

namespace ForecastV
{
    /// <summary>
    /// Contains model definitions for JSON responses returned by the Open-Meteo API.
    /// </summary>
    public class Models
    {
        /// <summary>
        /// Represents current weather conditions in the API response.
        /// </summary>
        public class CurrentWeather
        {
            [JsonProperty("time")]
            public string Time { get; set; }

            [JsonProperty("temperature_2m")]
            public double Temperature { get; set; }

            [JsonProperty("weather_code")]
            public int WeatherCode { get; set; }
        }

        /// <summary>
        /// Root object returned by the Open-Meteo API.
        /// </summary>
        public class WeatherResponse
        {
            [JsonProperty("current")]
            public CurrentWeather Current { get; set; }
        }
    }
}
