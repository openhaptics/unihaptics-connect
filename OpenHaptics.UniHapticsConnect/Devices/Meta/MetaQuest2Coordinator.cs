using OpenHaptics.UniHapticsConnect.Model;
using Windows.Storage.Streams;

namespace OpenHaptics.UniHapticsConnect.Devices.Meta
{
    internal class MetaQuest2Coordinator : AbstractBLEDeviceCoordinator
    {
        public override string DeviceType() => Model.DeviceType.META_QUEST_2;

        public override bool isSupportedDevice(DeviceCandidate candidate)
        {
            if (candidate.GetType() != typeof(BLEDeviceCandidate))
                return false;

            var manufacturerData = (candidate as BLEDeviceCandidate).Advertisement.ManufacturerData;
            if (manufacturerData == null)
                return false;

            foreach(var data in manufacturerData)
            {
                if (data.CompanyId != MetaConstants.META_OCULUS_MANUFACTURER_ID)
                    continue;

                var byteArray = new byte[(int)data.Data.Length];
                DataReader dataReader = DataReader.FromBuffer(data.Data);
                dataReader.ReadBytes(byteArray);
            }

            return false;
        }
    }
}
