using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenHaptics.UniHapticsConnect.Devices
{
    internal abstract class AbstractBLEDeviceCoordinator : IDeviceCoordinator
    {
        public abstract bool isSupportedDevice(DeviceCandidate candidate);

        public abstract string DeviceType();

        public int getBatteryCount() => 1;
    }
}
