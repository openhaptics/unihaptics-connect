namespace OpenHaptics.UniHapticsConnect.Devices.BHaptics
{
    internal class BHapticsTactalCoordinator : AbstractBHapticsCoordinator
    {

        public override string DeviceType() => Model.DeviceType.BHAPTICS_TACTAL;

        protected override string NameStartsWith() => "tactal";
    }
}
