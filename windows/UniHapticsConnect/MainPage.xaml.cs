using Microsoft.ReactNative;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UniHapticsConnect
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private muxc.NavigationViewItem _lastItem;

        public MainPage()
        {
            this.InitializeComponent();

            // Workaround for VisualState issue that should be fixed
            // by https://github.com/microsoft/microsoft-ui-xaml/pull/2271
            NavigationViewControl.PaneDisplayMode = muxc.NavigationViewPaneDisplayMode.Left;
        }

        private void NavigationViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(var item in NavigationViewControl.MenuItems)
            {
                if (item is muxc.NavigationViewItem && (item as muxc.NavigationViewItem).Tag.ToString() == "HomeView")
                {
                    NavigationViewControl.SelectedItem = item;
                }
            }

            // NavigateToItem("HomeView");
        }

        private void NavigationViewControl_ItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
        {
            var item = args.InvokedItemContainer as muxc.NavigationViewItem;

            if (item == null || item == _lastItem)
            {
                return;
            }

            var clickedView = item.Tag?.ToString();

            if (string.IsNullOrWhiteSpace(clickedView))
            {
                return;
            }

            if (!NavigateToItem(clickedView))
            {
                return;
            }

            _lastItem = item;
        }

        private bool NavigateToItem(string clickedView)
        {
            var view = Assembly.GetExecutingAssembly().GetType($"UniHapticsConnect.Views.{clickedView}");

            if (string.IsNullOrWhiteSpace(clickedView) || view.Equals(null))
            {
                return false;
            }

            ContentFrame.Navigate(view, null, new EntranceNavigationTransitionInfo());
            return true;
        }

        private void NavigationViewControl_SelectionChanged(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs args)
        {

        }
    }
}
