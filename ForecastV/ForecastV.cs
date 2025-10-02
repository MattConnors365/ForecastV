using GTA;
using GTA.Math;
using GTA.Native;
using GTA.NaturalMotion;
using GTA.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForecastV
{
    public class ForecastV : Script
    {
        private float timeSinceLastUpdate = 0f;
        private const float updateInterval = 300f;
        public ForecastV()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

            Tick += OnTick;
            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;
        }
        public async void OnTick(object sender, EventArgs e)
        {
            timeSinceLastUpdate += Game.LastFrameTime;

            if (timeSinceLastUpdate >= updateInterval)
            {
                timeSinceLastUpdate = 0f;
                await FetchAndApplyWeather(); // your async method
            }
        }
        public void OnKeyDown(object sender, KeyEventArgs e) { }
        public async void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.L:
                    Notification.Show(NotificationIcon.SocialClub, "ForecastV", "All Working", "The mod has sucessfully loaded", true, false);
                    break;
                case Keys.O:
                    await FetchAndApplyWeather();
                    break;
                default:
                    break;
            }
        }

        private async Task<bool> FetchAndApplyWeather()
        {
            try
            {
                int code = await DataRetrieval.GetWeatherCodeAsync();
                Weather gtaWeather = WeatherMapper.MapCodeToGtaWeather(code);
                World.Weather = gtaWeather;
                Notification.Show($"ForecastV: Applied {gtaWeather} (code {code})");
            }
            catch (Exception ex)
            {
                Notification.Show($"Error: {ex.Message}");
                return false;
            }
            return true;
        }
    }
}
