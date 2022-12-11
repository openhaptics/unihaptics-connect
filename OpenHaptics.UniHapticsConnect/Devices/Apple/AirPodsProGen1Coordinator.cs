using OpenHaptics.UniHapticsConnect.Model;

namespace OpenHaptics.UniHapticsConnect.Devices.Apple
{
    internal class AirPodsProGen1Coordinator : AbstractAirpodsCoordinator
    {
        public override string DeviceType() => Model.DeviceType.APPLE_AIRPODS_PRO_GEN1;

        protected override ushort DeviceModelValue() => AppleConstants.APPLE_CONTINUITY_AIRPODS_MODEL_AIRPODS_PRO_GEN1;
    }
}
