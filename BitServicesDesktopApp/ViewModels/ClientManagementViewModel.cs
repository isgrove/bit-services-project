using BitServicesDesktopApp.Models;
using BitServicesDesktopApp.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace BitServicesDesktopApp.ViewModels
{
    public class ClientManagementViewModel : INotifyPropertyChanged
    {
        // TODO: Add functionality to all butonns
        private ObservableCollection<Client> _clients;
        private ObservableCollection<ClientLocation> _clientLocations;
        private Client _selectedClient;
        private ClientLocation _selectedLocation;
        private RelayCommand _deleteCommand;
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        public RelayCommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(this.DeleteMethod, true);
                }
                return _deleteCommand;
            }
            set { _deleteCommand = value; }
        }
        public void DeleteMethod()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure that you want to delete {SelectedClient.Name}?", $"Delete {SelectedClient.Name}", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string message;
                int rowsAffected = SelectedClient.DeleteClient();
                if (rowsAffected >= 1)
                {
                    message = "You have successfully deleted " + SelectedClient.Name + "!";
                }
                else
                {
                    message = "There was an issue when deleting " + SelectedClient.Name + ", please try again!";
                }
                UpdateClients();
                MessageBox.Show(message);
            }

        }
        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                OnPropertyChanged("Clients");
            }
        }
        public ObservableCollection<ClientLocation> ClientLocations
        {
            get { return _clientLocations; }
            set
            {
                _clientLocations = value;
                OnPropertyChanged("ClientLocations");
            }
        }
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
                OnPropertyChanged("SelectedClient");
                if (_selectedClient != null)
                {
                    ClientLocations allLocations = new ClientLocations(SelectedClient.ClientId);
                    this.ClientLocations = new ObservableCollection<ClientLocation>(allLocations);
                }
            }
        }
        public ClientLocation SelectedLocation
        {
            get { return _selectedLocation; }
            set
            {
                _selectedLocation = value;
                OnPropertyChanged("SelectedLocation");
            }
        }

        private void UpdateClients()
        {
            Clients allClients = new Clients();
            this.Clients = new ObservableCollection<Client>(allClients);
        }
        public ClientManagementViewModel()
        {
            UpdateClients();
        }
    }
}
