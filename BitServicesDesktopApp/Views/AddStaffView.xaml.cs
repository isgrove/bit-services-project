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
    /// Interaction logic for AddStaffView.xaml
    /// </summary>
    public partial class AddStaffView : Page
    {
        public AddStaffView()
        {
            InitializeComponent();
            this.DataContext = new AddStaffViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/StaffManagementView.xaml", UriKind.Relative));
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/StaffManagementView.xaml", UriKind.Relative));
        }
    }
}
