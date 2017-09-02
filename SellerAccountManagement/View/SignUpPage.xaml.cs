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
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {

        private SignUpViewModel _signUpViewModel;
        public SignUpPage()
        {
            InitializeComponent();
            this.Loaded += SignUpPage_Loaded;
        }

        private void SignUpPage_Loaded(object sender, RoutedEventArgs e)
        {
            _signUpViewModel = new SignUpViewModel();
            this.DataContext = _signUpViewModel;
        }

        async private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            var hasCreatedUser = await _signUpViewModel.CreateUser();
            if (hasCreatedUser)
            {
                MainPage mainPage = new MainPage();
                this.NavigationService.Navigate(mainPage);
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            NavigationService.Navigate(loginPage);
        }
    }
}
