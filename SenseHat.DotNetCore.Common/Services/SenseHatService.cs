#region Using

using Iot.Device.SenseHat;
using SenseHat.DotNetCore.Common.Sensors;
using System.Device.I2c;
using System.Device.I2c.Drivers;
using System.Drawing;
using System.Runtime.InteropServices;

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
            ledMatrix = new SenseHatLedMatrixI2c(
                GetI2cDevice(SenseHatLedMatrixI2c.I2cAddress));

            pressureAndTemperatureSensor = new SenseHatPressureAndTemperature(
                GetI2cDevice(SenseHatPressureAndTemperature.I2cAddress));

            temperatureAndHumiditySensor = new SenseHatTemperatureAndHumidity(
                GetI2cDevice(SenseHatTemperatureAndHumidity.I2cAddress));
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

        private I2cDevice GetI2cDevice(int deviceAddress)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var settings = new I2cConnectionSettings(1, deviceAddress);

                return new Windows10I2cDevice(settings);
            }
            else
            {
                // The default, UnixI2cDevice will be used internally by 
                // IoT.Device.Bindings
                return null;
            }
        }

        #endregion
    }
}
