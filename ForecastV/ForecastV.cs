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
        public ForecastV()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

            Tick += OnTick;
            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;
        }
        public void OnTick(object sender, EventArgs e) { }
        public void OnKeyDown(object sender, KeyEventArgs e) { }
        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.L:
                    Notification.Show(NotificationIcon.SocialClub, "ForecastV", "All Working", "The mod has sucessfully loaded", true, false);
                    break;
                case Keys.O:
                    FetchAndApplyWeather();
                    break;
                default:
                    break;
            }
        }

        private async void FetchAndApplyWeather()
        {
            int code = await DataRetrieval.GetWeatherCodeAsync();
            Weather gtaWeather = WeatherMapper.MapCodeToGtaWeather(code);

            World.Weather = gtaWeather;
            Notification.Show($"ForecastV: Applied {gtaWeather} (code {code})");
        }
    }
}
