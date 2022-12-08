using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenHaptics.UniHapticsConnect.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;

namespace OpenHaptics.UniHapticsConnect.Services
{
    internal class BLEDeviceWatcherService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly DeviceManager _deviceManager;

        private readonly BluetoothLEAdvertisementWatcher _bleWatcher;

        public BLEDeviceWatcherService(
            ILogger<BLEDeviceWatcherService> logger,
            DeviceManager deviceManager
        ) {
            _logger = logger;
           _deviceManager = deviceManager;

            try
            {
                _bleWatcher = new BluetoothLEAdvertisementWatcher()
                {
                    ScanningMode = BluetoothLEScanningMode.Active,
                };
                _bleWatcher.Received += BLEWatcher_Received;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to start: " + ex.Message, ex);
                _bleWatcher = null;
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (_bleWatcher != null)
            {
                _logger.LogDebug("Starting Hosted Service");

                _bleWatcher?.Start();
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if ( _bleWatcher != null)
            {
                _logger.LogDebug("Stopping Hosted Service");

                _bleWatcher?.Stop();
            }

            return Task.CompletedTask;
        }

        private void BLEWatcher_Received(BluetoothLEAdvertisementWatcher watcher, BluetoothLEAdvertisementReceivedEventArgs eventArgs)
        {
            if (watcher != _bleWatcher)
            {
                return;
            }

            var macAddress = string.Join(":", BitConverter.GetBytes(eventArgs.BluetoothAddress).Reverse().Select(b => b.ToString("X2"))).Substring(6);
            var advertisement = eventArgs.Advertisement;
            var appearance = getAppearance(advertisement);
            var appearanceHex = appearance != null ? ("0x" + appearance?.ToString("X")) : null;

            _logger.LogInformation(string.Format("{0} ({1}), appearance: {2}", macAddress, advertisement.LocalName, appearanceHex));
            var deviceCandidate = DeviceCandidate.BLE(macAddress, advertisement.LocalName);
        }

        private int? getAppearance(BluetoothLEAdvertisement advertisement)
        {
            var dataSections = advertisement.DataSections;

            if (dataSections == null || dataSections.Count <= 0)
                return null;

            byte[] array = dataSections[0].Data.ToArray();
            if (array.Length != 2)
                return null;

            return array[0] + array[1] * 256;
        }
    }
}
