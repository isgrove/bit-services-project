using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using BitServicesDesktopApp.Commands;
using BitServicesDesktopApp.Models;
using BitServicesDesktopApp.Views;

namespace BitServicesDesktopApp.ViewModels
{
    public class AddJobViewModel : INotifyPropertyChanged
    {
        private Job _newJob;
        private Client _selectedClient;
        private ObservableCollection<Contractor> _availableContractors;
        private ObservableCollection<Skill> _skills;
        private ObservableCollection<Client> _clients;
        private ObservableCollection<ClientLocation> _clientLocations;
        private RelayCommand _findCommand;
        private RelayCommand _confirmCommand;
        private RelayCommand _clearCommand;
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public RelayCommand FindCommand
        {
            get
            {
                if (_findCommand == null)
                {
                    _findCommand = new RelayCommand(this.FindMethod, true);
                }

                return _findCommand;
            }
            set { _findCommand = value; }
        }

        public RelayCommand ConfirmCommand
        {
            get
            {
                if (_confirmCommand == null)
                {
                    _confirmCommand = new RelayCommand(this.ConfirmMethod, true);
                }

                return _confirmCommand;
            }
            set { _confirmCommand = value; }
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

        public Job NewJob
        {
            get { return _newJob; }
            set
            {
                _newJob = value;
                OnPropertyChanged("NewJob");
            }
        }

        public Client SelectedClient
        {
            get { return _selectedClient;  }
            set
            {
                _selectedClient = value;
                OnPropertyChanged("SelectedClient");
                if (_selectedClient != null)
                {
                    ClientLocations allClientsLocations = new ClientLocations(this.SelectedClient.ClientId);
                    this.ClientLocations = new ObservableCollection<ClientLocation>(allClientsLocations);
                }

            }
        }
        public ObservableCollection<Contractor> AvailableContractors
        {
            get { return _availableContractors; }
            set
            {
                _availableContractors = value;
                OnPropertyChanged("AvailableContractors");
            }
        }

        public ObservableCollection<Skill> Skills
        {
            get { return _skills; }
            set
            {
                _skills = value;
                OnPropertyChanged("Skills");
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
        private void FindMethod()
        {
            Contractors contractors = new Contractors(NewJob.RequiredSkill, NewJob.DeadlineDate);
            this.AvailableContractors = new ObservableCollection<Contractor>(contractors);
        }

        private void ConfirmMethod()
        {
            NewJob.JobStatus = "Pending";
            string message;
            // TODO: Get the id of the logged in staff member and pass that instead
            int rowsAffected = NewJob.InsertJob(MainWindow.LoggedInStaff.StaffId);
            if (rowsAffected >= 1)
            {
                message = $"You have successfully added a new job for {SelectedClient.Name} {NewJob.Location.Suburb}!";
            }
            else
            {
                message = $"There was an issue when adding {SelectedClient.Name} {NewJob.Location.Suburb}, please try again!";
            }
            MessageBox.Show(message);
        }
        public void ClearMethod()
        {
            this.NewJob = new Job();
            this.SelectedClient = new Client();
            MessageBox.Show("Cleared");
        }
        public AddJobViewModel()
        {
            this.NewJob = new Job()
            {
                DeadlineDate = DateTime.Now
            };

            Skills allSkills = new Skills();
            this.Skills = new ObservableCollection<Skill>(allSkills);

            Clients allClients = new Clients();
            this.Clients = new ObservableCollection<Client>(allClients);
        }
    }
}