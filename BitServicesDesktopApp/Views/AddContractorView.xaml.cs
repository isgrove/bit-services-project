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
    /// Interaction logic for AddContractorView.xaml
    /// </summary>
    public partial class AddContractorView : Page
    {
        public AddContractorView()
        {
            InitializeComponent();
            this.DataContext = new AddContractorViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/ContractorManagementView.xaml", UriKind.Relative));
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/ContractorManagementView.xaml", UriKind.Relative));
        }
    }
}
