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
            (_logger, _deviceManager) = (logger, deviceManager);

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

            var advertisement = eventArgs.Advertisement;
            var manufacturerData = advertisement.ManufacturerData;
            var dataSections = advertisement.DataSections;
            var rssi = eventArgs.RawSignalStrengthInDBm;

            var macAddress = string.Join(":", BitConverter.GetBytes(eventArgs.BluetoothAddress).Reverse().Select(b => b.ToString("X2"))).Substring(6);
            var logName = string.IsNullOrWhiteSpace(advertisement.LocalName) ? "<empty>" : advertisement.LocalName;

            _logger.LogDebug(string.Format("Found device: {0} ({1}), RSSI: {2}", logName, macAddress, rssi));

            var deviceCandidate = new BLEDeviceCandidate(macAddress, advertisement.LocalName, advertisement, rssi);
            _deviceManager.HandleDeviceFound(deviceCandidate);
        }
    }
}
