using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;

namespace OpenHaptics.UniHapticsConnect.Services
{
    internal class BLEDeviceWatcherService : IHostedService
    {
        private readonly ILogger _logger;

        private readonly BluetoothLEAdvertisementWatcher _bleWatcher;

        public BLEDeviceWatcherService(ILogger<BLEDeviceWatcherService> logger) 
        {
            _logger = logger;

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

            _logger.LogTrace("Received: " + advertisement.LocalName, advertisement);
        }
    }
}
