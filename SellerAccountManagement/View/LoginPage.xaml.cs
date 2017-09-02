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

namespace SellerAccountManagement.View
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private LoginViewModel _loginViewModel;
        public LoginPage()
        {
            InitializeComponent();
            this.Loaded += LoginPage_Loaded;
        }

        private void LoginPage_Loaded(object sender, RoutedEventArgs e)
        {
            _loginViewModel = new LoginViewModel();
            this.DataContext = _loginViewModel;
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUpPage signUpPage = new SignUpPage();
            this.NavigationService.Navigate(signUpPage);
        }

        async private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var hasLoggedIn = await _loginViewModel.Login();
            if (hasLoggedIn)
            {
                MainPage mainPage = new MainPage();
                this.NavigationService.Navigate(mainPage);
            }
        }
    }
}
