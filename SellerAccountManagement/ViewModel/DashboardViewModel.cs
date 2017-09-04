using SellerAccountManagement.Helpers;
using SellerAccountManagement.View;
using SellerAccountManagement.View.Shared;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SellerAccountManagement.ViewModel
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private AlertView _alertView;

        public DashboardViewModel()
        {
            AccountCommand = new RelayCommand(GotoAccounts);
            SettingsCommand = new RelayCommand(GotoSettings);
            LogoutCommand = new RelayCommand(Logout);
            ExitCommand = new RelayCommand(Exit);
            _alertView = new AlertView() { DataContext = new AlertViewModel() };
        }

        #region Commands

        public ICommand AccountCommand { get; }
        public ICommand SettingsCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand ExitCommand { get; }

        private void GotoAccounts(object obj)
        {
            //NavigationHelper.NavigationService.Navigate(dashboard);
        }

        private void GotoSettings(object obj)
        {
            //NavigationHelper.NavigationService.Navigate(dashboard);
        }

        private void Logout(object obj)
        {
            var laoginPage = new Login();
            NavigationHelper.NavigationService.Navigate(laoginPage);
        }

        private void Exit(object obj)
        {
            Application.Current.MainWindow.Close();
        }

        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string PropertyValue)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyValue));
        }

    }
}
