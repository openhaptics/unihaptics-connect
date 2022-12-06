using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace UniHapticsConnect.UI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellView : Page
    {
        private List<(string Tag, Type Page)> _pages = new List<(string Tage, Type Page)>
        {
            ("HomeView", typeof(HomeView)),
            ("DevicesView", typeof(DevicesView)),
        };

        public ShellView()
        {
            this.InitializeComponent();

            // Workaround for VisualState issue that should be fixed
            // by https://github.com/microsoft/microsoft-ui-xaml/pull/2271
            NavigationViewControl.PaneDisplayMode = muxc.NavigationViewPaneDisplayMode.Left;
        }

        private void NavigationViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigated += ContentFrame_Navigated;

            // NavView doesn't load any page by default, so load home page.
            NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems[0];

            // NavigationViewControl_Navigate("HomeView", new Windows.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());

            Window.Current.CoreWindow.PointerPressed += CoreWindow_PointerPressed;
            SystemNavigationManager.GetForCurrentView().BackRequested += System_BackRequested;
        }

        private void NavigationViewControl_ItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == true)
            {
                NavigationViewControl_Navigate("settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer != null)
            {
                NavigationViewControl_Navigate(args.InvokedItemContainer.Tag.ToString(), args.RecommendedNavigationTransitionInfo);
            }
        }

        private void NavigationViewControl_Navigate(string viewTag, Windows.UI.Xaml.Media.Animation.NavigationTransitionInfo transitionInfo)
        {
            Type _page = viewTag == "settings" ? null : _pages.FirstOrDefault(p => p.Tag.Equals(viewTag)).Page;

            // Get the page type before navigation so you can prevent duplicate
            // entries in the backstack.
            var preNavPageType = ContentFrame.CurrentSourcePageType;
            if (!(_page is null) && !Type.Equals(preNavPageType, _page))
            {
                ContentFrame.Navigate(_page, null, transitionInfo);
            }
        }

        private void NavigationViewControl_BackRequested(muxc.NavigationView sender, muxc.NavigationViewBackRequestedEventArgs args)
        {
            TryGoBack();
        }

        private void System_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = TryGoBack();
            }
        }

        private void CoreWindow_PointerPressed(CoreWindow sender, PointerEventArgs e)
        {
            // Handle mouse back button.
            if (e.CurrentPoint.Properties.IsXButton1Pressed)
            {
                e.Handled = TryGoBack();
            }
        }

        private bool TryGoBack()
        {
            if (!ContentFrame.CanGoBack)
            {
                return false;
            }

            ContentFrame.GoBack();

            return true;
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            NavigationViewControl.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType != null)
            {
                var item = _pages.FirstOrDefault(p => p.Page == e.SourcePageType);
                
                NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems
                    .OfType<muxc.NavigationViewItem>()
                    .First(n => n.Tag.Equals(item.Tag));

                // NavigationViewControl.Header = ((muxc.NavigationViewItem)NavigationViewControl.SelectedItem)?.Content?.ToString();
            }
        }
    }
}
