﻿using System;
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
using BitServicesDesktopApp.ViewModels;

namespace BitServicesDesktopApp.Views
{
    /// <summary>
    /// Interaction logic for StaffManagementView.xaml
    /// </summary>
    public partial class StaffManagementView : Page
    {
        public StaffManagementView()
        {
            InitializeComponent();
            this.DataContext = new StaffManagementViewModel();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/AddStaffView.xaml", UriKind.Relative));
        }
    }
}
