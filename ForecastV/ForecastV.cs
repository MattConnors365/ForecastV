using GTA;
using GTA.UI;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ForecastV.ConfigData;
namespace ForecastV
{
    /// <summary>
    /// Main script class controlling ForecastV runtime behavior.
    /// Handles timed weather updates, developer keybinds, and notification display.
    /// </summary>
    public class ForecastV : Script
    {
        private ConfigData cfg;
        private float timeSinceLastUpdate = 0f;

        /// <summary>
        /// Initializes the ForecastV script, loads configuration,
        /// and starts periodic weather synchronization.
        /// </summary>
        public ForecastV()
        {
            ConfigManager.Load();
            cfg = ConfigManager.Config;

            // Ensure modern TLS support.
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

            Tick += OnTick;
            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;

            // Immediately fetch weather once on game load.
            _ = FetchAndApplyWeather();
        }

        /// <summary>
        /// Triggered each game tick. Handles the update interval timer.
        /// </summary>
        public async void OnTick(object sender, EventArgs e)
        {
            timeSinceLastUpdate += Game.LastFrameTime;

            if (timeSinceLastUpdate >= cfg.UpdateIntervalMinutes * 60f)
            {
                timeSinceLastUpdate = 0f;
                await FetchAndApplyWeather();
            }
        }

        public void OnKeyDown(object sender, KeyEventArgs e) { }

        /// <summary>
        /// Handles key input when developer options are enabled.
        /// </summary>
        public async void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (!cfg.DeveloperOptions) return;

            switch (e.KeyCode)
            {
                case Keys.L:
                    Notification.Show(NotificationIcon.SocialClub, "ForecastV", "All Working",
                        "The mod has successfully loaded", true, false);
                    break;
                case Keys.O:
                    await FetchAndApplyWeather();
                    break;
            }
        }

        /// <summary>
        /// Fetches the current weather from the API and applies it in-game.
        /// </summary>
        private async Task<bool> FetchAndApplyWeather()
        {
            try
            {
                int code = await DataRetrieval.GetWeatherCodeAsync(cfg.Latitude, cfg.Longitude);
                Weather gtaWeather = WeatherMapper.MapCodeToGtaWeather(code);
                World.Weather = gtaWeather;

                if (cfg.ShowNotifications)
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
