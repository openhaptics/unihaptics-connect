using OpenHaptics.UniHapticsConnect.Model;
using System;

namespace OpenHaptics.UniHapticsConnect.Devices.BHaptics
{
    internal class BHapticsTactalCoordinator : AbstractBHapticsCoordinator
    {
        public override bool isSupportedDevice(DeviceCandidate candidate) => candidate.DeviceName.ToLower().StartsWith("tactal");

        public override string getDeviceType() => DeviceType.BHAPTICS_TACTAL;
    }
}
