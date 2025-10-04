using System.Runtime.Serialization;

namespace ForecastV
{
    /// <summary>
    /// JSON models used for deserializing the Open-Meteo response with DataContractJsonSerializer.
    /// </summary>
    public static class Models
    {
        /// <summary>
        /// Represents the "current" object from Open-Meteo.
        /// </summary>
        [DataContract]
        public class CurrentWeather
        {
            /// <summary>ISO time string for the current reading.</summary>
            [DataMember(Name = "time")]
            public string Time { get; set; }

            /// <summary>Current temperature in degrees (from Open-Meteo).</summary>
            [DataMember(Name = "temperature_2m")]
            public double Temperature { get; set; }

            /// <summary>WMO weather code.</summary>
            [DataMember(Name = "weather_code")]
            public int WeatherCode { get; set; }
        }

        /// <summary>
        /// Root object returned by the Open-Meteo API (contains a "current" property).
        /// </summary>
        [DataContract]
        public class WeatherResponse
        {
            /// <summary>The "current" weather block.</summary>
            [DataMember(Name = "current")]
            public CurrentWeather Current { get; set; }
        }
    }
}
