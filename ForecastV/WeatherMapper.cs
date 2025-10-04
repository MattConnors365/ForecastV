using GTA;

namespace ForecastV
{
    /// <summary>
    /// Maps WMO weather codes to GTA V weather enums.
    /// </summary>
    public static class WeatherMapper
    {
        /// <summary>
        /// Converts a WMO <paramref name="code"/> to a corresponding <see cref="Weather"/> type.
        /// </summary>
        /// <param name="code">The weather code received from the API.</param>
        /// <returns>The mapped <see cref="Weather"/> value, or <see cref="Weather.Clear"/> by default.</returns>
        public static Weather MapCodeToGtaWeather(int code)
        {
            switch (code)
            {
                case 0:
                case 1: return Weather.Clear;
                case 2: return Weather.Clouds;
                case 3: return Weather.Overcast;
                case 45:
                case 48: return Weather.Foggy;
                case 51:
                case 53:
                case 55:
                case 61:
                case 63:
                case 65: return Weather.Raining;
                case 71:
                case 73:
                case 75: return Weather.Snowlight;
                case 95:
                case 96:
                case 99: return Weather.ThunderStorm;
                default: return Weather.Clear;
            }
        }
    }
}
