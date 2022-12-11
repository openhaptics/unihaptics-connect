﻿using OpenHaptics.UniHapticsConnect.Model;

namespace OpenHaptics.UniHapticsConnect.Devices.Apple
{
    internal class AirPodsGen1Coordinator : AbstractAirpodsCoordinator
    {
        public override string getDeviceType()
        {
            return DeviceType.APPLE_AIRPODS_GEN1;
        }

        protected override ushort getDeviceModelValue()
        {
            return AppleConstants.APPLE_CONTINUITY_AIRPODS_MODEL_AIRPODS_GEN1;
        }
    }
}