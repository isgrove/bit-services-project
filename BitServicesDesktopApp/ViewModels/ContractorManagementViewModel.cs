using BitServicesDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using BitServicesDesktopApp.Commands;

namespace BitServicesDesktopApp.ViewModels
{
    public class ContractorManagementViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Contractor> _contractors;
        private ObservableCollection<Skill> _contractorSkills;
        private ObservableCollection<Availability> _contractorAvailability;
        private Contractor _selectedContractor;
        private RelayCommand _deleteCommand;
        private RelayCommand _saveCommand;
        private Skill _selectedSkill;
        private Availability _selectedAvailability;
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

        public void DeleteMethod()
        {
            string ContractorName = SelectedContractor.FullName;
            MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure that you want to delete {ContractorName}?",
                $"Delete {ContractorName}", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string message;
                int rowsAffected = SelectedContractor.DeleteContractor();
                if (rowsAffected >= 1)
                {
                    message = $"You have successfully deleted {ContractorName}!";
                }

                else
                {
                    message = $"There was an issue when deleting {ContractorName}, please try again!";
                }
                MessageBox.Show(message, $"Delete {ContractorName}");
                UpdateContractors();
            }
        }
        public void SaveMethod()
        {
            string ContractorName = SelectedContractor.FullName;
            MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure that you would like to update {ContractorName}?", $"Update {ContractorName}", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string message;
                int rowsAffected = SelectedContractor.UpdateContractor();
                if (rowsAffected >= 1)
                {
                    message = $"You have successfully saved {ContractorName}!";
                }
                else
                {
                    message = $"There was an issue when saving {ContractorName}, please try again!";
                }
                MessageBox.Show(message, $"Update {ContractorName}");
                UpdateContractors();
            }

        }

        public ObservableCollection<Contractor> Contractors
        {
            get { return _contractors; }
            set
            {
                _contractors = value;
                OnPropertyChanged("Contractors");
            }
        }
        public ObservableCollection<Skill> ContractorSkills
        {
            get { return _contractorSkills; }
            set
            {
                _contractorSkills = value;
                OnPropertyChanged("ContractorSkills");
            }
        }
        public ObservableCollection<Availability> ContractorAvailability
        {
            get { return _contractorAvailability; }
            set
            {
                _contractorAvailability = value;
                OnPropertyChanged("ContractorAvailability");
            }
        }
        public Contractor SelectedContractor
        {
            get { return _selectedContractor; }
            set
            {
                _selectedContractor = value;
                OnPropertyChanged("SelectedContractor");
                if (SelectedContractor != null)
                {
                    Skills allSkills = new Skills(SelectedContractor.ContractorId);
                    this.ContractorSkills = new ObservableCollection<Skill>(allSkills);
                    Availabilities allAvailability = new Availabilities(SelectedContractor.ContractorId);
                    this.ContractorAvailability = new ObservableCollection<Availability>(allAvailability);
                }
            }
        }
        public Skill SelectedSkill
        {
            get { return _selectedSkill; }
            set
            {
                _selectedSkill = value;
                OnPropertyChanged("SelectedSkill");
            }
        }

        public Availability SelectedAvailability
        {
            get { return _selectedAvailability; }
            set
            {
                _selectedAvailability = value;
                OnPropertyChanged("SelectedAvailability");
            }
        }
        public void UpdateContractors()
        {

            Contractors allContractors = new Contractors();
            this.Contractors = new ObservableCollection<Contractor>(allContractors);
        }

        public ContractorManagementViewModel()
        {
            UpdateContractors();
        }
    }
}
