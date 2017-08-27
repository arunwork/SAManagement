using SellerAccountManagement.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SellerAccountManagement.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        #region Properties

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyPropertyChanged("Email");
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyPropertyChanged("Password");
            }
        }

        #endregion

        private bool IsValidateFields()
        {
            bool isValid = false;
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(Email))
                errors.Add("Email is required.");
            else
            {
                var isValidEmail = Extensions.IsValidEmailAddress(Email);
                if (!isValidEmail)
                    errors.Add("Invalid email address");
            }

            if (string.IsNullOrEmpty(Password))
                errors.Add("Password is required.");

            if (errors.Count > 0)
            {
                isValid = false;
                MessageBox.Show(string.Join(Environment.NewLine, errors), "Login", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
                isValid = true;

            return isValid;
        }

        async public Task<bool> Login()
        {
            bool isLoggedIn = false;
            if (IsValidateFields())
            {
                var users = await LocalStorageHelper.GetUsers();
                var user = users.SingleOrDefault(p => p.Email == Email);
                if (user != null)
                {
                    if (user.Password != Password)
                    {
                        MessageBox.Show("Password is incorect,", "Login", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        isLoggedIn = false;
                    }
                    else
                        isLoggedIn = true;
                }
            }

            return isLoggedIn;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string PropertyValue)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyValue));
            }
        }
    }
}
