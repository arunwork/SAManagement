using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SellerAccountManagement.Helpers
{
    /// <summary>
    ///     Represents a bindable password box
    /// </summary>
    public sealed class BindablePasswordHelper : Decorator
    {
        /// <summary>
        ///     The password dependency property.
        /// </summary>
        public static readonly DependencyProperty PasswordProperty;

        /// <summary>
        ///     The _saved callback
        /// </summary>
        private readonly RoutedEventHandler _savedCallback;

        /// <summary>
        ///     The _is prevent callback
        /// </summary>
        private bool _isPreventCallback;

        /// <summary>
        ///     Static constructor to initialize the dependency properties.
        /// </summary>
        static BindablePasswordHelper()
        {
            PasswordProperty = DependencyProperty.Register(
                "Password",
                typeof(string),
                typeof(BindablePasswordHelper),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnPasswordPropertyChanged)
                );
        }

        /// <summary>
        ///     Saves the password changed callback and sets the child element to the password box.
        /// </summary>
        public BindablePasswordHelper()
        {
            _savedCallback = HandlePasswordChanged;

            var passwordBox = new PasswordBox();
            passwordBox.PasswordChanged += _savedCallback;
            Child = passwordBox;
        }

        /// <summary>
        ///     The password dependency property.
        /// </summary>
        /// <value>
        ///     The password.
        /// </value>
        public string Password
        {
            get { return GetValue(PasswordProperty) as string; }
            set { SetValue(PasswordProperty, value); }
        }

        /// <summary>
        ///     Handles changes to the password dependency property.
        /// </summary>
        /// <param name="d">the dependency object</param>
        /// <param name="eventArgs">the event args</param>
        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs eventArgs)
        {
            var bindablePasswordBox = (BindablePasswordHelper)d;
            var passwordBox = (PasswordBox)bindablePasswordBox.Child;

            if (bindablePasswordBox._isPreventCallback)
            {
                return;
            }

            passwordBox.PasswordChanged -= bindablePasswordBox._savedCallback;
            passwordBox.Password = (eventArgs.NewValue != null) ? eventArgs.NewValue.ToString() : "";
            passwordBox.PasswordChanged += bindablePasswordBox._savedCallback;
        }

        /// <summary>
        ///     Handles the password changed event.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="eventArgs">the event args</param>
        private void HandlePasswordChanged(object sender, RoutedEventArgs eventArgs)
        {
            var passwordBox = (PasswordBox)sender;

            _isPreventCallback = true;
            Password = passwordBox.Password;
            _isPreventCallback = false;
        }
    }
}
