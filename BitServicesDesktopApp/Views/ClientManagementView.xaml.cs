using BitServicesDesktopApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for ClientManagementView.xaml
    /// </summary>
    public partial class ClientManagementView : Page
    {
        static ClientManagementViewModel vm;
        public ClientManagementView()
        {
            InitializeComponent();
            vm = new ClientManagementViewModel();
            this.DataContext = vm;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/AddClientView.xaml", UriKind.Relative));
        }

        private void btnAddLocation_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddClientLocationView(vm));
        }
    }
}
