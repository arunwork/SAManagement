using SellerAccountManagement.Helpers;
using SellerAccountManagement.View;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace SellerAccountManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        ///     The app_ identifier
        /// </summary>
        private const string AppId = "Seller Account Management";

        private NavigationWindow navigationWindow;

        private ImageBrush _background;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var colorBrush = new SolidColorBrush(Color.FromRgb(89, 79, 142));
            navigationWindow = new NavigationWindow
            {
                Height = 500,
                Width = 750,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Title = "Seller Account Management",
                WindowState = WindowState.Maximized,
                Background = colorBrush,
                WindowStyle = WindowStyle.None,
                ResizeMode = ResizeMode.CanMinimize,
                ShowsNavigationUI = false
            };

            if (_background != null)
            {
                (_background.ImageSource as BitmapImage).StreamSource.Dispose();
                _background = null;
            }
            var page = new Login();
            navigationWindow.Navigate(page);
            navigationWindow.Show();
        }

        private void App_OnNavigated(object sender, NavigationEventArgs e)
        {
            var page = e.Content as Page;
            if (page != null)
            {
                NavigationHelper.NavigationService = null;
                NavigationHelper.NavigationService = page.NavigationService;
                RenderOptions.ProcessRenderMode = RenderMode.SoftwareOnly;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

    }
}
