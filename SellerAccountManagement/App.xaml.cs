using SellerAccountManagement.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace SellerAccountManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private NavigationWindow navigationWindow;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            navigationWindow = new NavigationWindow();
            navigationWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //  navigationWindow.Icon = new BitmapImage(new Uri("pack://application:,,,/AppName;component/Assets/Images/ApplicationIcon.png"));
            navigationWindow.Title = "Seller Account Management";
            var page = new LoginPage();
            navigationWindow.Navigate(page);
            navigationWindow.Show();
        }
    }
}
