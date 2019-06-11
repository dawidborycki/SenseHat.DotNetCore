#region Using

using SenseHat.DotNetCore.Common.Helpers;
using SenseHat.DotNetCore.Common.Services;
using System;
using System.Drawing;
using System.Threading.Tasks;

#endregion

namespace SenseHat.DotNetCore.ConsoleApp
{
    class Program
    {
        #region Fields

        private static readonly Color[] ledColors = { Color.Red, Color.Blue, Color.Green };
        private static readonly int msDelayTime = 1000;

        private static int ledColorIndex = 0;

        #endregion

        #region Main

        static void Main(string[] args)
        {
            // Parse input arguments, and set emulation mode accordingly
            var emulationMode = ParseInputArgumentsToSetEmulationMode(args);

            // Instantiate service
            var senseHatService = SenseHatServiceHelper.GetService(emulationMode);

            // Display the mode
            Console.WriteLine($"Emulation mode: {senseHatService.EmulationMode}");

            // Infinite loop
            while (true)
            {
                // Display sensor readings
                Console.WriteLine(senseHatService.SensorReadings);

                // Change the LED array color
                ChangeFillColor(senseHatService);

                // Delay
                Task.Delay(msDelayTime).Wait();
            }
        }

        #endregion

        #region Methods (Private)

        private static bool ParseInputArgumentsToSetEmulationMode(string[] args)
        {
            return args.Length == 1 && string.Equals(
                args[0], "Y", StringComparison.OrdinalIgnoreCase);
        }

        private static void ChangeFillColor(ISenseHatService senseHatService)
        {
            if (!senseHatService.EmulationMode)
            {
                senseHatService.Fill(ledColors[ledColorIndex]);

                ledColorIndex = ++ledColorIndex % ledColors.Length;
            }
        }

        #endregion
    }
}
