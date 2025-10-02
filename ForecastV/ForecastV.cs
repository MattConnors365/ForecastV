using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using GTA.NaturalMotion;
using GTA.UI;

namespace ForecastV
{
    public class ForecastV : Script
    {
        public ForecastV()
        {
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
                default:
                    break;
            }
        }
    }
}
