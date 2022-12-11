using Microsoft.Extensions.Logging;
using OpenHaptics.UniHapticsConnect.Devices.BHaptics;
using OpenHaptics.UniHapticsConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenHaptics.UniHapticsConnect.Devices
{
    internal class DeviceManager
    {
        private readonly ILogger<DeviceManager> _logger;

        private readonly List<IDeviceCoordinator> _coordinatorList;

        public DeviceManager(ILogger<DeviceManager> logger)
        {
            _logger = logger;

            _coordinatorList = new List<IDeviceCoordinator>
            {
                new BHapticsTactalCoordinator(),
            };
        }

        public string getSupportedDeviceType(DeviceCandidate deviceCandidate)
        {
            foreach (var coordinator in _coordinatorList)
            {
                if (coordinator.isSupportedDevice(deviceCandidate))
                {
                    return coordinator.getDeviceType();
                }
            }

            return DeviceType.UNKNOWN;
        }

        public bool handleDeviceFound(DeviceCandidate deviceCandidate)
        {
            var deviceType = getSupportedDeviceType(deviceCandidate);

            if (deviceType == DeviceType.UNKNOWN)
            {
                return false;
            }

            _logger.LogDebug(string.Format("Recognized device: {0}", deviceType));

            return false;
        }
    }
}
