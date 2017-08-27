using SellerAccountManagement.View;
using SellerAccountManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SellerAccountManagement
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private MainViewModel _mainViewModel;
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            _mainViewModel = new MainViewModel();
            this.DataContext = _mainViewModel;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnMessages_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            SignUpPage signUpPage = new SignUpPage();
            this.NavigationService.Navigate(signUpPage);
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
