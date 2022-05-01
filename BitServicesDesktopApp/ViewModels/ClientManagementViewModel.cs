using BitServicesApp.Models;
using BitServicesDesktopApp.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace BitServicesApp.ViewModels
{
    class ClientManagementViewModel : INotifyPropertyChanged
    {
        // TODO: Add functionality to all butonns
        private ObservableCollection<Client> _clients;
        private ObservableCollection<ClientLocation> _clientLocations;
        private Client _selectedClient;
        private ClientLocation _selectedLocation;
        private string _searchText;
        private RelayCommand _searchCommand;
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
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
        public RelayCommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(this.SearchMethod, true);
                }
                return _searchCommand;
            }
            set { _searchCommand = value; }
        }
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
                OnPropertyChanged("SelectedClient");
                ClientLocations allLocations = new ClientLocations(SelectedClient.ClientId);
                this.ClientLocations = new ObservableCollection<ClientLocation>(allLocations);
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
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
            }
        }
        public void SearchMethod()
        {
            //Clients allCustomers = new Client(SearchText);
            //this.Customers = new ObservableCollection<Customer>(allCustomers);
        }
        public ClientManagementViewModel()
        {
            Clients allClients = new Clients();
            this.Clients = new ObservableCollection<Client>(allClients);
        }
    }
}
