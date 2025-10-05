using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastV
{
    public class Utilities
    {
        /// <summary>
        /// Converts a temperature value from Celsius to the specified unit.
        /// </summary>
        /// <param name="celsius">The temperature in degrees Celsius to be converted.</param>
        /// <param name="unit">The target unit for the conversion. Supported values are  <see langword="fahrenheit"/> (or <see
        /// langword="f"/>) and  <see langword="kelvin"/> (or <see langword="k"/>).</param>
        /// <returns>The converted temperature value in the specified unit.</returns>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="unit"/> is not a supported value.</exception>
        public static double ConvertCelsius(double celsius, string unit)
        {
            switch (unit.ToLower())
            {
                case "celcius":
                case "c":
                    return celsius;
                case "fahrenheit":
                case "f":
                    return (celsius * 9) / 5 + 32;
                case "kelvin":
                case "k":
                    return celsius + 273.15;
                default:
                    throw new ArgumentException($"Unit '{unit}' is not supported.");
            }
        }

    }
}
