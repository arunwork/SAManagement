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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        private DashboardViewModel _dashboardViewModel;
        public Dashboard()
        {
            InitializeComponent();
            _dashboardViewModel = new DashboardViewModel();
            DataContext = _dashboardViewModel;
        }
    }
}
