using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.Extensions.Logging;
using Windows.Devices.Bluetooth.Advertisement;

namespace UniHapticsConnect.Device.Watcher
{
    internal class BLEDeviceWatcherService : IDeviceWatcherService
    {
        private readonly ILogger<BLEDeviceWatcherService> logger;
        private readonly BluetoothLEAdvertisementWatcher _bleWatcher;

        public BLEDeviceWatcherService(ILogger<BLEDeviceWatcherService> logger)
        {
            this.logger = logger;

            try
            {
                this._bleWatcher = new BluetoothLEAdvertisementWatcher()
                {
                    ScanningMode = BluetoothLEScanningMode.Active,
                };
                this._bleWatcher.Received += BLEWatcher_Received;
                this._bleWatcher.Start();
            }
            catch (Exception ex)
            {
                this.logger.LogError("Failed to start: " + ex.Message, ex);
                this._bleWatcher = (BluetoothLEAdvertisementWatcher)null;
            }
        }

        private async void BLEWatcher_Received(BluetoothLEAdvertisementWatcher watcher, BluetoothLEAdvertisementReceivedEventArgs eventArgs)
        {
            if (watcher != this._bleWatcher)
            {
                return;
            }

            var advertisement = eventArgs.Advertisement;

            this.logger.LogInformation("Received: " + advertisement.LocalName, advertisement);
        }

        public void Dispose()
        {
            if (this._bleWatcher == null)
            {
                return;
            }

            this._bleWatcher.Received -= BLEWatcher_Received;
            this._bleWatcher.Stop();
        }
    }
}