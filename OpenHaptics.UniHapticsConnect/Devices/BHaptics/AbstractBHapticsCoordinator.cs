using System.Linq;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Storage.Streams;

namespace OpenHaptics.UniHapticsConnect.Devices.BHaptics
{
    internal abstract class AbstractBHapticsCoordinator : AbstractBLEDeviceCoordinator
    {
        public override bool isSupportedDevice(DeviceCandidate candidate)
        {
            if (candidate.GetType() != typeof(BLEDeviceCandidate))
                return false;

            var appearance = (candidate as BLEDeviceCandidate).Appearance();

            if (!(appearance == 508 || appearance == 509 || appearance == 510))
                return false;

            return candidate.DeviceName.ToLower().StartsWith(NameStartsWith());
        }

        protected abstract string NameStartsWith();
    }
}
