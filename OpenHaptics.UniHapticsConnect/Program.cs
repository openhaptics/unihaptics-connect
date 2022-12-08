using CommunityToolkit.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Dispatching;
using Microsoft.Windows.AppLifecycle;
using OpenHaptics.UniHapticsConnect.UI;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace OpenHaptics.UniHapticsConnect
{
    class Program
    {
        [STAThread]
        public static Task Main(string[] args)
        {
            WinRT.ComWrappersSupport.InitializeComWrappers();
            bool isRedirect = DecideRedirection();
            if (!isRedirect)
            {
                var builder = new WindowsAppSdkHostBuilder<App>();

                builder.ConfigureServices((_, collection) =>
                {
                    // If your main Window is named differently, change it here.
                    collection.AddSingleton<MainWindow>();
                });

                var app = builder.Build();

                return app.StartAsync();

                // Microsoft.UI.Xaml.Application.Start((p) =>
                // {
                //     var context = new DispatcherQueueSynchronizationContext(
                //         DispatcherQueue.GetForCurrentThread());
                //     SynchronizationContext.SetSynchronizationContext(context);
                //     new App();
                // });
            }

            return Task.Run(() => { });
        }

        private static bool DecideRedirection()
        {
            bool isRedirect = false;

            AppActivationArguments args = AppInstance.GetCurrent().GetActivatedEventArgs();
            ExtendedActivationKind kind = args.Kind;

            try
            {
                AppInstance keyInstance = AppInstance.FindOrRegisterForKey("randomKey");

                if (keyInstance.IsCurrent)
                {
                    keyInstance.Activated += OnActivated;
                }
                else
                {
                    isRedirect = true;
                    RedirectActivationTo(args, keyInstance);
                }
            }
            catch (Exception ex)
            {

            }

            return isRedirect;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr CreateEvent(IntPtr lpEventAttributes, bool bManualReset, bool bInitialState, string lpName);

        [DllImport("kernel32.dll")]
        private static extern bool SetEvent(IntPtr hEvent);

        [DllImport("ole32.dll")]
        private static extern uint CoWaitForMultipleObjects(uint dwFlags, uint dwMilliseconds, ulong nHandles, IntPtr[] pHandles, out uint dwIndex);

        private static IntPtr redirectEventHandle = IntPtr.Zero;

        // Do the redirection on another thread, and use a non-blocking
        // wait method to wait for the redirection to complete.
        public static void RedirectActivationTo(AppActivationArguments args, AppInstance keyInstance)
        {
            redirectEventHandle = CreateEvent(IntPtr.Zero, true, false, null);
            Task.Run(() =>
            {
                keyInstance.RedirectActivationToAsync(args).AsTask().Wait();
                SetEvent(redirectEventHandle);
            });
            uint CWMO_DEFAULT = 0;
            uint INFINITE = 0xFFFFFFFF;
            _ = CoWaitForMultipleObjects(CWMO_DEFAULT, INFINITE, 1, new IntPtr[] { redirectEventHandle }, out uint handleIndex);
        }

        private static void OnActivated(object sender, AppActivationArguments args)
        {
            ExtendedActivationKind kind = args.Kind;
        }
    }
}
