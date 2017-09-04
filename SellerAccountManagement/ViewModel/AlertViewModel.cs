using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellerAccountManagement.ViewModel
{
    public class AlertViewModel : INotifyPropertyChanged
    {
        #region Properties

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyPropertyChanged("Message");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string PropertyValue)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyValue));
        }
    }
}
#endregion