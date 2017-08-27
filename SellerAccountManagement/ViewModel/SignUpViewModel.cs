using SellerAccountManagement.Enums;
using SellerAccountManagement.Helpers;
using SellerAccountManagement.Model.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SellerAccountManagement.ViewModel
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        #region Properties

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

        /// <summary>
        /// Gets or sets the password
        /// </summary>
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

        private bool IsValidateFields()
        {
            bool isValid = false;
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(FirstName))
                errors.Add("Firstname is required.");

            if (string.IsNullOrEmpty(LastName))
                errors.Add("Lastname is required.");

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

            if (string.IsNullOrEmpty(ConfirmPassword))
                errors.Add("ConfirmPassword is required.");

            if (!string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword) && Password != ConfirmPassword)
                errors.Add("Confirmation password doesn't match with password.");

            if (errors.Count > 0)
            {
                isValid = false;
                MessageBox.Show(string.Join(Environment.NewLine, errors), "SignUp", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                isValid = true;
            }

            return isValid;

        }

        #region Methods

        async private Task<bool> IsEmailExists(string email)
        {
            var users = await LocalStorageHelper.GetUsers();
            return users.Any(p => p.Email == email);
        }

        async public Task<bool> CreateUser()
        {
            if (IsValidateFields())
            {
                var existsEmail = await IsEmailExists(Email);
                if (existsEmail)
                {
                    MessageBox.Show("This email id already exists!", "SignUp", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return false;
                }
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    Password = Password,
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyValue));
            }
        }
    }
}
