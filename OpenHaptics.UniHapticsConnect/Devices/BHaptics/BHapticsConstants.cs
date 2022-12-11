using System;

namespace OpenHaptics.UniHapticsConnect.Devices.BHaptics
{
    internal class BHapticsConstants
    {
        #region Haptic Feedback
        
        // Main service for communication, Nordic UART service
        public static readonly Guid UUID_SERVICE_MOTOR = new Guid("6e400001-b5a3-f393-e0a9-e50e24dcca9e");

        // Legacy Characteristic to create haptic feedback, Nordic UART RX
        public static readonly Guid UUID_CHAR_MOTOR = new Guid("6e400002-b5a3-f393-e0a9-e50e24dcca9e");

        // Characteristic for Device S/N, Nordic UART TX
        public static readonly Guid UUID_CHAR_SERIAL_KEY = new Guid("6e400003-b5a3-f393-e0a9-e50e24dcca9e");

        public static readonly Guid UUID_CHAR_CONFIG = new Guid("6e400005-b5a3-f393-e0a9-e50e24dcca9e");
        public static readonly Guid UUID_CHAR_FIRMWARE_VERSION = new Guid("6e400007-b5a3-f393-e0a9-e50e24dcca9e");
        public static readonly Guid UUID_CHAR_BATERY_LEVEL = new Guid("6e400008-b5a3-f393-e0a9-e50e24dcca9e");

        // New main Characteristic to create Haptic feedback
        public static readonly Guid UUID_CHAR_MOTOR_STABLE = new Guid("6e40000a-b5a3-f393-e0a9-e50e24dcca9e");

        public static readonly Guid UUID_CHAR_TACTSUIT_MONITOR = new Guid("6e40000b-b5a3-f393-e0a9-e50e24dcca9e");

        // Audio-to-haptic
        public static readonly Guid UUID_CHAR_ATH_GLOBAL_CONF = new Guid("6e40000c-b5a3-f393-e0a9-e50e24dcca9e");
        public static readonly Guid UUID_CHAR_ATH_THEME = new Guid("6e40000d-b5a3-f393-e0a9-e50e24dcca9e");

        public static readonly Guid UUID_CHAR_MOTTOR_MAPPING = new Guid("6e40000e-b5a3-f393-e0a9-e50e24dcca9e");
        public static readonly Guid UUID_CHAR_SIGNATURE_PATTERN = new Guid("6e40000f-b5a3-f393-e0a9-e50e24dcca9e");

        #endregion

        #region DFU, Firmware updates

        public static readonly Guid UUID_SERVICE_DFU = new Guid("0000fe59-0000-1000-8000-00805f9b34fb");
        public static readonly Guid UUID_CHAR_DFU_CONTROL = new Guid("8ec90003-f315-4f60-9fb8-838830daea50");

        #endregion
    }
}
