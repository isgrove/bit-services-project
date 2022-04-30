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

namespace BitServicesApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            btnContractorManagement.Foreground = (Brush)new BrushConverter().ConvertFrom("#EA5D32");
            contentFrame.Navigate(new ContractorManagementView());
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
    }
}
