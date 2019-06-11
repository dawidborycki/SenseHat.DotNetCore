#region Using

using SenseHat.DotNetCore.Common.Services;

#endregion

namespace SenseHat.DotNetCore.Common.Helpers
{
    public static class SenseHatServiceHelper
    {
        #region Methods (Public)

        public static ISenseHatService GetService(bool emulationMode = false)
        {
            if (emulationMode)
            {
                return new SenseHatEmulationService();
            }
            else
            {
                return new SenseHatService();
            }
        }

        #endregion
    }
}
