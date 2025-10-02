using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastV
{
    public class Models
    {
        public class CurrentWeather
        {
            [JsonProperty("time")]
            public string Time { get; set; }

            [JsonProperty("temperature_2m")]
            public double Temperature { get; set; }

            [JsonProperty("weather_code")]
            public int WeatherCode { get; set; }
        }

        public class WeatherResponse
        {
            [JsonProperty("current")]
            public CurrentWeather Current { get; set; }
        }
    }
}
