#region Using

using Iot.Device.SenseHat;
using SenseHat.DotNetCore.Common.Sensors;
using System.Drawing;

#endregion

namespace SenseHat.DotNetCore.Common.Services
{
    public class SenseHatService : ISenseHatService
    {
        #region Properties

        public SensorReadings SensorReadings => GetReadings();

        public bool EmulationMode => false;

        #endregion

        #region Methods (Public)

        public void Fill(Color color) => ledMatrix.Fill(color);

        #endregion

        #region Fields

        private readonly SenseHatLedMatrix ledMatrix;

        private readonly SenseHatPressureAndTemperature pressureAndTemperatureSensor;

        private readonly SenseHatTemperatureAndHumidity temperatureAndHumiditySensor;

        #endregion

        #region Constructor

        public SenseHatService()
        {
            ledMatrix = new SenseHatLedMatrixI2c();

            pressureAndTemperatureSensor = new SenseHatPressureAndTemperature();

            temperatureAndHumiditySensor = new SenseHatTemperatureAndHumidity();
        }

        #endregion

        #region Methods (Private)

        private SensorReadings GetReadings()
        {
            return new SensorReadings
            {
                Temperature = temperatureAndHumiditySensor.Temperature,
                Humidity = temperatureAndHumiditySensor.Humidity,
                Temperature2 = pressureAndTemperatureSensor.Temperature,
                Pressure = pressureAndTemperatureSensor.Pressure
            };
        }        

        #endregion
    }
}
