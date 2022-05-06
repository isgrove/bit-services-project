using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BitServicesDesktopApp.Commands;
using BitServicesDesktopApp.Models;

namespace BitServicesDesktopApp.ViewModels
{
    public class AddClientLocationViewModel : INotifyPropertyChanged
    {

        private Client _selectedClient;
        private ClientLocation _newClientLocation;
        private ObservableCollection<Client> _clients;
        private RelayCommand _addCommand;
        private RelayCommand _clearCommand;
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        public RelayCommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(this.AddMethod, true);
                }
                return _addCommand;
            }
            set { _addCommand = value; }
        }
        public RelayCommand ClearCommand
        {
            get
            {
                if (_clearCommand == null)
                {
                    _clearCommand = new RelayCommand(this.ClearMethod, true);
                }
                return _clearCommand;
            }
            set { _clearCommand = value; }
        }
        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
            }
        }

        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value; 
                OnPropertyChanged("SelectedClient");
            }
        }
        public ClientLocation NewClientLocation
        {
            get { return _newClientLocation; }
            set
            {
                _newClientLocation = value;
                OnPropertyChanged("NewClientLocation");
            }
        }
        private void AddMethod()
        {
            string message;
            NewClientLocation.ClientId = SelectedClient.ClientId;
            int rowsAffected =  NewClientLocation.InsertClientLocation();
            if (rowsAffected >= 1)
            {
                message = $"You have successfully added {SelectedClient.Name} {NewClientLocation.Suburb}!";
            }
            else
            {
                message = $"There was an issue when adding {SelectedClient.Name} {NewClientLocation.Suburb}, please try again!";
            }
            MessageBox.Show(message);
        }
        public void ClearMethod()
        {
            this.NewClientLocation = new ClientLocation();
        }
        public AddClientLocationViewModel(ClientManagementViewModel vm)
        {
            NewClientLocation = new ClientLocation();
            SelectedClient = new Client();
            if (vm.SelectedClient != null)
            {
                SelectedClient = vm.SelectedClient;
            }
            Clients allClients = new Clients();
            this.Clients = new ObservableCollection<Client>(allClients);
        }
    }
}
