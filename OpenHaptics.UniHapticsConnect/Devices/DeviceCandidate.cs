namespace OpenHaptics.UniHapticsConnect.Devices
{
    internal class DeviceCandidate
    {
        public string UID { get; }
        public string DeviceName { get; }

        public DeviceCandidate(string uid, string deviceName) => (UID, DeviceName) = (uid, deviceName);

        public static DeviceCandidate BLE(string macAddress, string? localName) => new DeviceCandidate(macAddress, localName);

        public override string ToString()
        {
            var logName = string.IsNullOrWhiteSpace(DeviceName) ? "<empty>" : DeviceName;
            return string.Format("{0}: {1}", logName, UID);
        }
    }
}
