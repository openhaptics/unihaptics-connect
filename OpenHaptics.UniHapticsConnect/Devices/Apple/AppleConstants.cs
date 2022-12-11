using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenHaptics.UniHapticsConnect.Devices.Apple
{
    // See: https://github.com/furiousMAC/continuity/blob/master/messages/proximity_pairing.md
    internal class AppleConstants
    {
        public static readonly int AIRPODS_MANUFACTURER = 76;

        public static readonly byte APPLE_CONTINUITY_TYPE_PROXIMITY_PAIRING = 0x07;
        public static readonly byte APPLE_CONTINUITY_AIRPODS_PREFIX = 0x01;

        public static readonly ushort APPLE_CONTINUITY_AIRPODS_MODEL_AIRPODS_GEN1 = 0x0220;
        public static readonly ushort APPLE_CONTINUITY_AIRPODS_MODEL_AIRPODS_GEN2 = 0x0f20;
        public static readonly ushort APPLE_CONTINUITY_AIRPODS_MODEL_AIRPODS_GEN3 = 0x1320;
        public static readonly ushort APPLE_CONTINUITY_AIRPODS_MODEL_AIRPODS_PRO_GEN1 = 0x0e20;
        public static readonly ushort APPLE_CONTINUITY_AIRPODS_MODEL_AIRPODS_PRO_GEN2 = 0x1420;
        public static readonly ushort APPLE_CONTINUITY_AIRPODS_MODEL_AIRPODS_MAX = 0x0a20;
    }
}
