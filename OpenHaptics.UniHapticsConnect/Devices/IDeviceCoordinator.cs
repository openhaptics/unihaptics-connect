using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenHaptics.UniHapticsConnect.Devices
{
    internal interface IDeviceCoordinator
    {
        public bool isSupportedDevice(DeviceCandidate candidate);

        public string getDeviceType();

        public int getBatteryCount();
    }
}
