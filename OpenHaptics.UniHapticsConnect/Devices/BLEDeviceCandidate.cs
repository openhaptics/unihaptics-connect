using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Storage.Streams;

namespace OpenHaptics.UniHapticsConnect.Devices
{
    internal class BLEDeviceCandidate : DeviceCandidate
    {
        public BluetoothLEAdvertisement Advertisement { get; }

        public short RSSI { get; }

        public BLEDeviceCandidate(string macAddress, string localName, BluetoothLEAdvertisement advertisement, short rssi) : base(macAddress, localName) =>
            (Advertisement, RSSI) = (advertisement, rssi);

        public short? Appearance()
        {
            var dataSections = Advertisement?.DataSections;
            if (dataSections == null || dataSections.Count <= 0)
                return null;

            var appearanceSection = dataSections.FirstOrDefault(section => section.DataType == 0x19);
            if (appearanceSection == null)
                return null;

            if (appearanceSection.Data.Length < 1)
                return null;

            var byteArray = new byte[(int)appearanceSection.Data.Length];
            DataReader dataReader = DataReader.FromBuffer(appearanceSection.Data);
            dataReader.ReadBytes(byteArray);

            return BitConverter.ToInt16(byteArray);
        }

        public Dictionary<ushort, short> ServiceData()
        {
            var dataSections = Advertisement.DataSections;
           
            var serviceData16Section = dataSections.FirstOrDefault(section => section.DataType == 0x16);
            if (serviceData16Section == null)
                return new Dictionary<ushort, short>();
            
            var serviceData16Bytes = new byte[(int)serviceData16Section.Data.Length];
            using (DataReader dataReader = DataReader.FromBuffer(serviceData16Section.Data))
                dataReader.ReadBytes(serviceData16Bytes);

            var serviceUUID = (ushort) BitConverter.ToInt16(new byte[2] { serviceData16Bytes[0], serviceData16Bytes[1] });
            var serviceValue = BitConverter.ToInt16(new byte[2] { serviceData16Bytes[2], serviceData16Bytes[3] });

            return new Dictionary<ushort, short>
            {
                [serviceUUID] = serviceValue,
            };
        }
    }
}
