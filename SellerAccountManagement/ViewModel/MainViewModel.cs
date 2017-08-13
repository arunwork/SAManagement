using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellerAccountManagement.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Properties



        #endregion Properties

        #region Methods



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
