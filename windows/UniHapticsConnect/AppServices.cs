using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace UniHapticsConnect
{
    class AppServices
    {
        private static AppServices _instance;
        private static readonly object _instanceLock = new object();

        public static AppServices Instance => _instance ?? GetInstance();

        public IServiceProvider ServiceProvider { get; }

        public AppServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<Device.Watcher.BLEDeviceWatcherService>();

            services.AddLogging(builder =>
            {
#if DEBUG
                builder.AddDebug().SetMinimumLevel(LogLevel.Trace);
#endif
            });

            ServiceProvider = services.BuildServiceProvider();
        }

        private static AppServices GetInstance()
        {
            lock (_instanceLock)
            {
                return _instance ?? (_instance = new AppServices());
            }
        }
    }
}
