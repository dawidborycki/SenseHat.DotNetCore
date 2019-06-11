#region Using

using Iot.Units;
using System;

#endregion

namespace SenseHat.DotNetCore.Common.Sensors
{
    public class SensorReadings
    {
        #region Properties

        public Temperature Temperature { get; set; }

        public Temperature Temperature2 { get; set; }

        public float Humidity { get; set; }

        public float Pressure { get; set; }

        public DateTime TimeStamp { get; } = DateTime.UtcNow;

        #endregion

        #region Methods 

        public override string ToString()
        {
            return $"Temperature: {Temperature.Celsius,5:F2} °C"
                + $" Temperature2: {Temperature2.Celsius,5:F2} °C"
                + $" Humidity: {Humidity,4:F1} %"
                + $" Pressure: {Pressure,6:F1} hPa";
        }

        #endregion

    }
}
