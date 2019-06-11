namespace SenseHat.DotNetCore.Common.Sensors
{
    public class SensorReadingRange
    {
        #region Properties

        public double Min { get; set; }

        public double Max { get; set; }

        #endregion

        #region Methods

        public double ValueRange()
        {
            return Max - Min;
        }

        #endregion
    }
}
