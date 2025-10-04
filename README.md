# 🌤️ ForecastV

**ForecastV** is a lightweight GTA V mod that synchronizes the in-game weather with real-world conditions — using live data from the **[Open Meteo API](https://open-meteo.com/)**.

---

## 🌍 Credits + A Small Explanation

- **Weather data powered by [Open Meteo](https://open-meteo.com/)** — a free, no-auth weather API.

- **WMO Weather Codes:** ForecastV uses standardized [WMO (World Meteorological Organization)](https://community.wmo.int/en/activity-areas/wmo-codes) weather codes to determine conditions such as clear skies, rain, snow, and storms.  
  These codes are integers (e.g. `0` = clear, `63` = rain, `95` = thunderstorm) that map directly to GTA V’s built-in `Weather` enums via `WeatherMapper.cs`.

---

## 🧭 Overview

ForecastV automatically fetches real-world weather for a specified location and applies it in GTA V.

It supports configurable update intervals, notifications, and developer tools for debugging.

---

## ⚙️ Configuration

Configuration is stored in a human-readable JSON file: `scripts/ForecastV.json`
Example:
```json
{
    "DeveloperOptions": true,
    "ShowNotifications": true,
    "Latitude": 44.4375,
    "Longitude": 26.1250,
    "UpdateIntervalMinutes": 5
}
```
## Options

- DeveloperOptions-(bool)-[false]: Enables developer keybinds.

- ShowNotifications-(bool)-[true]: Toggles on-screen notifications for weather updates.

- Latitude-(float)-[34.0983]: Latitude of your desired location.

- Longitude-(float)-[-118.3267]: Longitude of your desired location.

- UpdateIntervalMinutes-(int)-[5]: Minutes between automatic weather updates.

You can edit this file manually — it’s automatically created on first launch.

All values persist between sessions.

---

## ⌨️ Developer Keybinds
Developer keybinds only work when DeveloperOptions is set to true.
The two current keybinds are:
1. 'L' - Display debug notification (“All Working”).
2. 'O' - Force immediate weather refresh.

---

## 🧩 Installation

- Install ScriptHookV and ScriptHookVDotNet3 if you haven’t already.

- Copy the compiled ForecastV.dll into your GTA V scripts/ directory.

- Launch GTA V — the file ForecastV.json will be created automatically in the same directory.

- Adjust latitude, longitude, or other settings as needed.

---

## 🔧 How It Works

- On game start, the mod loads settings from ForecastV.json.

- Every X minutes (configurable, defaults to 5), it queries the Open Meteo API for your location (also configurable, defaults to Hollywood):

- The returned WMO weather code is mapped to a GTA V Weather enum via WeatherMapper.cs.

- The result is applied in-game using World.Weather.

---

## 🧠 Technical Notes

- Uses only built-in .NET Framework libraries (no external JSON dependencies).

- Weather is fetched asynchronously to avoid frame stutter.

- Configuration is prettified for readability on save.

- Uses modern TLS 1.2/1.3 for secure HTTPS connections.

---

## 💬 Example Mapped Weather Codes

- 0–1: Clear sky
- 2: Partly cloudy
- 3: Overcast
- 45–48: Fog
- 51–65: Drizzle/Rain
- 71–75: Snow
- 95–99: Thunderstorms

---

## 🧑‍💻 Developer Info

If you’re extending or modifying the mod:

- All settings are defined once in ConfigData.cs — no duplicated property declarations.

- Adding or removing options only requires editing ConfigData (the config file is automatically updated).

- Weather API responses are modeled in Models.cs with DataContract attributes.

---

## 📜 License
This project is licensed under the [MIT License](./LICENSE)

Weather data © Open-Meteo and licensed under the Creative Commons Attribution 4.0 International (CC BY 4.0) license.

Author: Matt Connors | Updated: October 4th, 2025
