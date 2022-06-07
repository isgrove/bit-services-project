using BitServicesDesktopApp.Models;
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
using BitServicesDesktopApp.DAL;

namespace BitServicesDesktopApp.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Page
    {
        private LogHelper _log;

        public LoginView()
        {
            _log = new LogHelper();
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string userName = txtUserName.Text;
            string password = txtPassword.Text;

            var mainWindow = (MainWindow)Window.GetWindow(this);
            if (LoginHelper.IsCoordinator(userName, password))
            {
                this._log.Log($"Coordinator ({userName}) has logged in.", LogType.Info);
                mainWindow.UpdateButtons("coordinator");
            }
            else if (LoginHelper.IsAdmin(userName, password))
            {
                this._log.Log($"Admin ({userName}) has logged in.", LogType.Info);
                mainWindow.UpdateButtons("admin");
            }
            else
            {
                this._log.Log($"Someone tried to log in as {userName}.", LogType.Info);
                MessageBox.Show("Invalid login");
                return;
            }
            NavigationService.Navigate(new JobManagementView());
            mainWindow.btnContractorManagement.Foreground = (Brush)new BrushConverter().ConvertFrom("#EA5D32");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
