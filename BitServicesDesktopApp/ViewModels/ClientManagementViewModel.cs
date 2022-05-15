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
        private ObservableCollection<Client> _clients;
        private ObservableCollection<ClientLocation> _clientLocations;
        private Client _selectedClient;
        private ClientLocation _selectedLocation;
        private RelayCommand _deleteCommand;
        private RelayCommand _saveCommand;
        private RelayCommand _deleteLocationCommand;
        private RelayCommand _saveLocationCommand;
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
        public RelayCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(this.SaveMethod, true);
                }
                return _saveCommand;
            }
            set { _saveCommand = value; }
        }
        public RelayCommand DeleteLocationCommand
        {
            get
            {
                if (_deleteLocationCommand == null)
                {
                    _deleteLocationCommand = new RelayCommand(this.DeleteLocationMethod, true);
                }
                return _deleteLocationCommand;
            }
            set { _deleteLocationCommand = value; }
        }
        public RelayCommand SaveLocationCommand
        {
            get
            {
                if (_saveLocationCommand == null)
                {
                    _saveLocationCommand = new RelayCommand(this.SaveLocationMethod, true);
                }
                return _saveLocationCommand;
            }
            set { _saveLocationCommand = value; }
        }
        public void DeleteMethod()
        {
            string ClientName = SelectedClient.Name;
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
                MessageBox.Show(message, $"Delete {ClientName}");
            }

        }
        public void SaveMethod()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure that you would like to update {SelectedClient.Name}?", $"Update {SelectedClient.Name}", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string message;
                int rowsAffected = SelectedClient.UpdateClient();
                if (rowsAffected >= 1)
                {
                    message = "You have successfully saved " + SelectedClient.Name + "!";
                }
                else
                {
                    message = "There was an issue when saving " + SelectedClient.Name + ", please try again!";
                }
                MessageBox.Show(message, $"Update {SelectedClient.Name}");
            }

        }
        public void DeleteLocationMethod()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure that you want to delete {SelectedClient.Name} {SelectedLocation.Suburb}?", $"Delete {SelectedClient.Name} {SelectedLocation.Suburb}", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string message;
                int rowsAffected = SelectedLocation.DeleteClientLocation();
                if (rowsAffected >= 1)
                {
                    message = $"You have successfully deleted {SelectedClient.Name} {SelectedLocation.Suburb}!";
                }
                else
                {
                    message = $"There was an issue when deleting {SelectedClient.Name} {SelectedLocation.Suburb}, please try again!";
                }
                MessageBox.Show(message, $"Delete {SelectedClient.Name} {SelectedLocation.Suburb}");
                UpdateClients();
            }

        }

        public void SaveLocationMethod()
        {
            MessageBoxResult messageBoxResult =
                MessageBox.Show(
                    $"Are you sure that you would like to update {SelectedClient.Name} {SelectedLocation.Suburb}?",
                    $"Update {SelectedClient.Name} {SelectedLocation.Suburb}", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string message;
                int rowsAffected = SelectedLocation.UpdateClientLocation();
                if (rowsAffected >= 1)
                {
                    message = $"You have successfully saved {SelectedClient.Name} {SelectedLocation.Suburb}!";
                }
                else
                {
                    message =
                        $"There was an issue when saving {SelectedClient.Name} {SelectedLocation.Suburb}, please try again!";
                }

                MessageBox.Show(message, $"Update {SelectedClient.Name} {SelectedLocation.Suburb}");
                UpdateClientLocations();
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
                    UpdateClientLocations();
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
        private void UpdateClientLocations()
        {
            if (SelectedClient != null)
            {
                ClientLocations allClientLocations = new ClientLocations(SelectedClient.ClientId);
                this.ClientLocations = new ObservableCollection<ClientLocation>(allClientLocations);
            }
        }
        public ClientManagementViewModel()
        {
            UpdateClients();
        }
    }
}
