using MaterialDesignThemes.Wpf;
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

namespace SellerAccountManagement.View.Shared
{
    /// <summary>
    /// Interaction logic for AlertDialog.xaml
    /// </summary>
    public partial class AlertView : UserControl
    {
        private AlertViewModel _alertViewModel;
        public AlertView()
        {
            InitializeComponent();
        }
        
        public async Task Show(string message)
        {
            _alertViewModel = new AlertViewModel();
            _alertViewModel.Message = message;
            DataContext = _alertViewModel;
            
            //show the dialog
            var result = await DialogHost.Show(this, "RootDialog", ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("You can intercept the closing event, and cancel here.");

            //if ((bool)eventArgs.Parameter == false) return;

            ////OK, lets cancel the close...
            //eventArgs.Cancel();

            ////...now, lets update the "session" with some new content!
            //eventArgs.Session.UpdateContent(new SampleProgressDialog());
            //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

            ////lets run a fake operation for 3 seconds then close this baby.
            //Task.Delay(TimeSpan.FromSeconds(3))
            //    .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
            //        TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
