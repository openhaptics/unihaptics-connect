using Microsoft.Extensions.Logging;
using OpenHaptics.UniHapticsConnect.Devices.Apple;
using OpenHaptics.UniHapticsConnect.Devices.BHaptics;
using OpenHaptics.UniHapticsConnect.Devices.Meta;
using OpenHaptics.UniHapticsConnect.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenHaptics.UniHapticsConnect.Devices
{
    internal sealed class DeviceManager
    {
        private readonly ILogger<DeviceManager> _logger;

        private readonly List<IDeviceCoordinator> _coordinatorList;

        public ObservableCollection<DeviceCandidate> Candidates { get; private set; } = new ObservableCollection<DeviceCandidate>();

        public DeviceManager(ILogger<DeviceManager> logger)
        {
            _logger = logger;

            _coordinatorList = new List<IDeviceCoordinator>
            {
                new BHapticsTactalCoordinator(),

                // I made it here just for testing to distinguish multiple deevices at the same time
                new AirPodsGen1Coordinator(),
                new AirPodsProGen1Coordinator(),

                new MetaQuest2Coordinator(),
            };
        }

        public string GuessDeviceType(DeviceCandidate deviceCandidate)
        {
            foreach (var coordinator in _coordinatorList)
            {
                if (coordinator.isSupportedDevice(deviceCandidate))
                {
                    return coordinator.DeviceType();
                }
            }

            return DeviceType.UNKNOWN;
        }

        public bool HandleDeviceFound(DeviceCandidate deviceCandidate)
        {
            var deviceType = GuessDeviceType(deviceCandidate);

            if (deviceType == DeviceType.UNKNOWN)
            {
                return false;
            }

            _logger.LogDebug(string.Format("Recognized device: {0} ({1})", deviceType, deviceCandidate));

            return false;
        }

        public DeviceCandidate FindDevice(string uid)
        {
            foreach (var candidate in Candidates)
            {
                if (candidate.UID == uid)
                {
                    return candidate;
                }
            }

            return null;
        }
    }
}
