using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenHaptics.UniHapticsConnect.Devices
{
    internal class DeviceCandidate
    {
        public DeviceCandidate(string uid) { }

        public static DeviceCandidate BLE(string macAddress, string localName)
        {
            return new DeviceCandidate(macAddress);
        }
    }
}
