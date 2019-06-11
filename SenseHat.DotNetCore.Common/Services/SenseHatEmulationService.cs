#region Using

using Iot.Units;
using SenseHat.DotNetCore.Common.Sensors;
using System;
using System.Drawing;

#endregion

namespace SenseHat.DotNetCore.Common.Services
{
    public class SenseHatEmulationService : ISenseHatService
    {
        #region Methods (Public)

        public SensorReadings SensorReadings => GetSensorReadings();

        public void Fill(Color color) {/* Intentionally do nothing*/}

        #endregion

        #region Properties

        public bool EmulationMode => true;

        #endregion

        #region Fields

        private readonly Random randomNumberGenerator = new Random();

        private readonly SensorReadingRange temperatureRange = 
            new SensorReadingRange { Min = 20, Max = 40 };
        private readonly SensorReadingRange humidityRange = 
            new SensorReadingRange { Min = 0, Max = 100 };
        private readonly SensorReadingRange pressureRange = 
            new SensorReadingRange { Min = 1000, Max = 1050 };

        #endregion

        #region Methods (Private)

        private SensorReadings GetSensorReadings()
        {
            return new SensorReadings
            {
                Temperature = Temperature.FromCelsius(GetRandomValue(temperatureRange)),
                Humidity = (float)GetRandomValue(humidityRange),
                Temperature2 = Temperature.FromCelsius(GetRandomValue(temperatureRange)),
                Pressure = (float)GetRandomValue(pressureRange)
            };
        }

        private double GetRandomValue(SensorReadingRange sensorReadingRange)
        {
            var randomValueRescaled = randomNumberGenerator.NextDouble()
                * sensorReadingRange.ValueRange();

            return sensorReadingRange.Min + randomValueRescaled;
        }

        #endregion
    }
}
