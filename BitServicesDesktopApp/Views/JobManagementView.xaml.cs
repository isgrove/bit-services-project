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

namespace BitServicesApp.Views
{
    /// <summary>
    /// Interaction logic for JobManagementView.xaml
    /// </summary>
    public partial class JobManagementView : Page
    {
        public JobManagementView()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/AddJobView.xaml", UriKind.Relative));
        }
    }
}
