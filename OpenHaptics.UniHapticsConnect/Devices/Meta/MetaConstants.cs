using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenHaptics.UniHapticsConnect.Devices.Meta
{
    // See: https://github.com/QuestEscape/research
    // See: https://github.com/KonradIT/oculus_jailbreak (decompiled hasm bytecode)
    internal class MetaConstants
    {
        public static readonly ushort META_OCULUS_MANUFACTURER_ID = 0x058E;

        public static readonly Guid UUID_SERVICE_PACIFIC = new Guid("0000FEB8-0000-1000-8000-00805F9B34FB");
        public static readonly Guid UUID_CHAR_CCS = new Guid("7A442881-509C-47FA-AC02-B06A37D9EB76");
        public static readonly Guid UUID_CHAR_STATUS = new Guid("7A442666-509C-47FA-AC02-B06A37D9EB76");
        public static readonly Guid UUID_CHAR_TEST = new Guid("2F007180-10A4-4CA4-8D04-F0C3981ACAE3");
        public static readonly Guid UUID_CHAR_TEST_CHUNKED = new Guid("2F007180-10A4-4CA4-8D04-F0C3981ACAE3");
        public static readonly Guid UUID_CHAR_WIFI_NETWORKS = new Guid("6A54E109-F661-4ACA-9D6A-36A735ABF638");
    }
}
