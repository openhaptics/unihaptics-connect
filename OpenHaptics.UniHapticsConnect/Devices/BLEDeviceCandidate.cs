using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;

namespace OpenHaptics.UniHapticsConnect.Devices
{
    internal class BLEDeviceCandidate : DeviceCandidate
    {
        public BluetoothLEAdvertisement Advertisement { get; }

        public short RSSI { get; }

        public BLEDeviceCandidate(string macAddress, string localName, BluetoothLEAdvertisement advertisement, short rssi) : base(macAddress, localName) =>
            (Advertisement, RSSI) = (advertisement, rssi);
    }
}
