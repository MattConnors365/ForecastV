using GTA;

namespace ForecastV
{
    public static class WeatherMapper
    {
        public static Weather MapCodeToGtaWeather(int code)
        {
            switch (code)
            {
                case 0: // Clear sky
                case 1: // Mainly clear
                    return Weather.Clear;

                case 2: // Partly cloudy
                    return Weather.Clouds;

                case 3: // Overcast
                    return Weather.Overcast;

                case 45: // Fog
                case 48:
                    return Weather.Foggy;

                case 51: // Drizzle
                case 53:
                case 55:
                case 61: // Rain
                case 63:
                case 65:
                    return Weather.Raining;

                case 71: // Snow
                case 73:
                case 75:
                    return Weather.Snowlight;

                case 95: // Thunderstorm
                case 96:
                case 99:
                    return Weather.ThunderStorm;

                default:
                    return Weather.Clear; // fallback
            }
        }
    }
}
