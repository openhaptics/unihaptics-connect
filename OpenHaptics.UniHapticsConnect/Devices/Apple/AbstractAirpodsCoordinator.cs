using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace OpenHaptics.UniHapticsConnect.Devices.Apple
{
    // See: https://github.com/furiousMAC/continuity/blob/master/messages/proximity_pairing.md
    internal abstract class AbstractAirpodsCoordinator : AbstractBLEDeviceCoordinator
    {
        public override bool isSupportedDevice(DeviceCandidate candidate)
        {
            if (candidate.GetType() != typeof(BLEDeviceCandidate))
                return false;

            var manufacturerData = (candidate as BLEDeviceCandidate).Advertisement.ManufacturerData;
            if (manufacturerData.Count < 1)
                return false;

            var byteArray = new byte[(int)manufacturerData[0].Data.Length];
            using (DataReader dataReader = DataReader.FromBuffer(manufacturerData[0].Data))
            {
                dataReader.ReadBytes(byteArray);

                var Type = byteArray[0];

                if (Type != AppleConstants.APPLE_CONTINUITY_TYPE_PROXIMITY_PAIRING)
                    return false;

                var Length = byteArray[1];
                var Prefix = byteArray[2];

                if (Prefix != AppleConstants.APPLE_CONTINUITY_AIRPODS_PREFIX)
                    return false;

                var DeviceModel = (ushort)((byteArray[3] << 8) | byteArray[4]);
                if (DeviceModel == getDeviceModelValue())
                    return true;
            }

            return false;
        }

        protected abstract ushort getDeviceModelValue();
    }
}
