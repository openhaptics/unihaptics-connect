using OpenHaptics.UniHapticsConnect.Model;
using System;

namespace OpenHaptics.UniHapticsConnect.Devices.BHaptics
{
    internal class BHapticsTactalCoordinator : AbstractBHapticsCoordinator
    {
        public override bool isSupportedDevice(DeviceCandidate candidate)
        {
            if (candidate.GetType() != typeof(BLEDeviceCandidate))
                return false;

            var appearance = (candidate as BLEDeviceCandidate).Appearance();

            if (!(appearance == 508 || appearance == 509 || appearance == 510))
                return false;

            if (!candidate.DeviceName.ToLower().StartsWith("tactal"))
                return false;

            return true;
        }

        public override string getDeviceType() => DeviceType.BHAPTICS_TACTAL;
    }
}
