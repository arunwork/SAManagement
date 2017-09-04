using MaterialDesignThemes.Wpf;
using SellerAccountManagement.Enums;
using SellerAccountManagement.Helpers;
using SellerAccountManagement.Model.Domain;
using SellerAccountManagement.View;
using SellerAccountManagement.View.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SellerAccountManagement.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private AlertView _alertView;
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            SignupCommand = new RelayCommand(Signup);
            _alertView = new AlertView() { DataContext = new AlertViewModel() };
        }

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


        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyPropertyChanged("LastName");
            }
        }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        private string _userEmail;
        public string UserEmail
        {
            get { return _userEmail; }
            set
            {
                _userEmail = value;
                NotifyPropertyChanged("Email");
            }
        }

        /// <summary>
        /// Gets or sets the new password
        /// </summary>
        private string _newPassword;
        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                NotifyPropertyChanged("Password");
            }
        }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                NotifyPropertyChanged("ConfirmPassword");
            }
        }

        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                NotifyPropertyChanged("PhoneNumber");
            }
        }

        /// <summary>
        /// Gets or sets the user type
        /// </summary>
        private UserType _userType;
        public UserType UserType
        {
            get { return _userType; }
            set
            {
                _userType = value;
                NotifyPropertyChanged("UserType");
            }
        }
        #endregion

        #region Commands

        public ICommand LoginCommand { get; }
        public ICommand SignupCommand { get; }

        private async void Login(object obj)
        {
            var hasLoggedIn = await Login();
            if (!hasLoggedIn) return;

            var dashboard = new Dashboard();
            NavigationHelper.NavigationService.Navigate(dashboard);
        }

        private async void Signup(object obj)
        {
            var isUserCreated = await CreateUser();
            if (!isUserCreated) return;

            var dashboard = new Dashboard();
            NavigationHelper.NavigationService.Navigate(dashboard);
        }

        #endregion

        #region Login Methods
        
        private async Task<bool> IsValidateLoginFields()
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
                await _alertView.Show(string.Join(Environment.NewLine, errors));
            }
            else
                isValid = true;

            return isValid;
        }
        
        async public Task<bool> Login()
        {
            bool isLoggedIn = false;
            if (await IsValidateLoginFields())
            {
                var users = await LocalStorageHelper.GetUsers();
                var user = users.SingleOrDefault(p => p.Email == Email);
                if (user != null)
                {
                    if (user.Password != Password)
                    {
                        await _alertView.Show("Password is incorect");
                        isLoggedIn = false;
                    }
                    else
                        isLoggedIn = true;
                }
                else {
                    await _alertView.Show("Inavlid user credentials!");
                    isLoggedIn = false;
                }
            }

            return isLoggedIn;
        }

        #endregion

        #region Signup Methods

        private async Task<bool> IsValidateSignupFields()
        {
            bool isValid = false;
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(FirstName))
                errors.Add("Firstname is required.");

            if (string.IsNullOrEmpty(LastName))
                errors.Add("Lastname is required.");

            if (string.IsNullOrEmpty(UserEmail))
                errors.Add("Email is required.");
            else
            {
                var isValidEmail = Extensions.IsValidEmailAddress(UserEmail);
                if (!isValidEmail)
                    errors.Add("Invalid email address");
            }

            if (string.IsNullOrEmpty(NewPassword))
                errors.Add("Password is required.");

            if (string.IsNullOrEmpty(ConfirmPassword))
                errors.Add("ConfirmPassword is required.");

            if (!string.IsNullOrEmpty(NewPassword) && !string.IsNullOrEmpty(ConfirmPassword) && NewPassword != ConfirmPassword)
                errors.Add("Confirmation password doesn't match with password.");

            if (errors.Count > 0)
            {
                isValid = false;
                await _alertView.Show(string.Join(Environment.NewLine, errors));
            }
            else
            {
                isValid = true;
            }

            return isValid;
        }

        async private Task<bool> IsEmailExists(string email)
        {
            var users = await LocalStorageHelper.GetUsers();
            return users.Any(p => p.Email == email);
        }

        async public Task<bool> CreateUser()
        {
            if (await IsValidateSignupFields())
            {
                var existsEmail = await IsEmailExists(Email);
                if (existsEmail)
                {
                    await _alertView.Show("This email id already exists!");
                    return false;
                }
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = UserEmail,
                    Password = NewPassword,
                    PhoneNumber = PhoneNumber,
                    UserType = UserType.User
                };

                await LocalStorageHelper.InsertUser(user);
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Methods

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string PropertyValue)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyValue));
        }
    }
}
