#region Using

using SenseHat.DotNetCore.Common.Sensors;
using System.Drawing;

#endregion

namespace SenseHat.DotNetCore.Common.Services
{
    public interface ISenseHatService
    {
        public SensorReadings SensorReadings { get; }

        public void Fill(Color color);

        public bool EmulationMode { get; }
    }
}
