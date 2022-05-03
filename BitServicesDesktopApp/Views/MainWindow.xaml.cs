using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace BitServicesDesktopApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            bool loginEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["LoginEnabled"]);
            InitializeComponent();

            if (loginEnabled)
            {
                contentFrame.Navigate(new LoginView());
            }
            else
            {
                contentFrame.Navigate(new JobManagementView());
                btnClientManagement.IsEnabled = true;
                btnContractorManagement.IsEnabled = true;
                btnJobManagement.IsEnabled = true;
                btnStaffManagement.IsEnabled = true;
            }
        }

        private void btnJobManagement_Click(object sender, RoutedEventArgs e)
        {
            ResetColours();
            btnJobManagement.Foreground = (Brush)new BrushConverter().ConvertFrom("#EA5D32");
            contentFrame.Navigate(new JobManagementView());
        }

        private void btnClientManagement_Click(object sender, RoutedEventArgs e)
        {
            ResetColours();
            btnClientManagement.Foreground = (Brush)new BrushConverter().ConvertFrom("#EA5D32");
            contentFrame.Navigate(new ClientManagementView());
        }

        private void btnContractorManagement_Click(object sender, RoutedEventArgs e)
        {
            ResetColours();
            btnContractorManagement.Foreground = (Brush)new BrushConverter().ConvertFrom("#EA5D32");
            contentFrame.Navigate(new ContractorManagementView());
        }

        private void btnStaffManagement_Click(object sender, RoutedEventArgs e)
        {
            ResetColours();
            btnStaffManagement.Foreground = (Brush)new BrushConverter().ConvertFrom("#EA5D32");
            contentFrame.Navigate(new StaffManagementView());
        }

        private void ResetColours()
        {
            btnJobManagement.Foreground = Brushes.Black;
            btnClientManagement.Foreground = Brushes.Black;
            btnContractorManagement.Foreground = Brushes.Black;
            btnStaffManagement.Foreground = Brushes.Black;
        }

        public void UpdateButtons(string userType)
        {
            if (userType == "admin")
            {
                AdminLogin();
            }
            else if (userType == "coordinator")
            {
                CoordinatorLogin();
            }
            else
            {
                MessageBox.Show("Invalid user type");
            }
        }

        private void AdminLogin()
        {
            btnClientManagement.IsEnabled = true;
            btnContractorManagement.IsEnabled = true;
            btnJobManagement.IsEnabled = true;
            btnStaffManagement.IsEnabled = true;
        }
        private void CoordinatorLogin()
        {
            btnClientManagement.IsEnabled = true;
            btnContractorManagement.IsEnabled = true;
            btnJobManagement.IsEnabled = true;
            btnStaffManagement.IsEnabled = false;
        }
    }
}
