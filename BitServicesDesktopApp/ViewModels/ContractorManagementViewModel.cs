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
        private ObservableCollection<Skill> _allSkills;
        private Contractor _selectedContractor;
        private RelayCommand _deleteCommand;
        private RelayCommand _saveCommand;
        private RelayCommand _addNewSkillCommand;
        private RelayCommand _addSkillCommand;
        private RelayCommand _deleteAvailabilityCommand;
        private RelayCommand _addAvailabilityCommand;
        private Skill _selectedSkill;
        private Skill _newSkill;
        private Availability _selectedAvailability;
        private Availability _newAvailability;
        private bool _isDetailsTabSelected;
        private bool _isSkillsTabSelected;
        private bool _isAvailabilityTabSelected;
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        #region Commands
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
        public RelayCommand AddSkillCommand
        {
            get
            {
                if (_addSkillCommand == null)
                {
                    _addSkillCommand = new RelayCommand(this.AddSkillMethod, true);
                }
                return _addSkillCommand;
            }
            set { _addSkillCommand = value; }
        }
        public RelayCommand AddNewSkillCommand
        {
            get
            {
                if (_addNewSkillCommand == null)
                {
                    _addNewSkillCommand = new RelayCommand(this.AddNewSkill, true);
                }
                return _addNewSkillCommand;
            }
            set { _addNewSkillCommand = value; }
        }

        public RelayCommand AddAvailabilityCommand
        {
            get
            {
                if (_addAvailabilityCommand == null)
                {
                    _addAvailabilityCommand = new RelayCommand(this.AddAvailabilityMethod, true);
                }

                return _addAvailabilityCommand;
            }
            set { _addAvailabilityCommand = value; }
        }

        public RelayCommand DeleteAvailabilityCommand
        {
            get
            {
                if (_deleteAvailabilityCommand == null)
                {
                    _deleteAvailabilityCommand = new RelayCommand(this.DeleteAvailabilityMethod, true);
                }

                return _deleteAvailabilityCommand;
            }
            set { _deleteAvailabilityCommand = value; }
        }
        #endregion

        #region Command Methods
        public void DeleteMethod()
        {
            string contractorName = SelectedContractor.FullName;
            MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure that you want to delete {contractorName}?",
                $"Delete {contractorName}", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string message;
                int rowsAffected = SelectedContractor.DeleteContractor();
                if (rowsAffected >= 1)
                {
                    message = $"You have successfully deleted {contractorName}!";
                }

                else
                {
                    message = $"There was an issue when deleting {contractorName}, please try again!";
                }
                MessageBox.Show(message, $"Delete {contractorName}");
                UpdateContractors();
            }
        }
        public void SaveMethod()
        {
            string contractorName = SelectedContractor.FullName;
            MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure that you would like to update {contractorName}?", $"Update {contractorName}", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string message;
                int rowsAffected = SelectedContractor.UpdateContractor();
                if (rowsAffected >= 1)
                {
                    message = $"You have successfully saved {contractorName}!";
                }
                else
                {
                    message = $"There was an issue when saving {contractorName}, please try again!";
                }
                MessageBox.Show(message, $"Update {contractorName}");
                UpdateContractors();
            }

        }
        public void AddSkillMethod()
        {
            string contractorName = SelectedContractor.FullName;
            string skillName = SelectedSkill.SkillName;
            foreach (Skill skill in ContractorSkills)
            {
                if (skill.SkillName == SelectedSkill.SkillName)
                {
                    MessageBox.Show($"{contractorName} already already has the {skillName} skill!", $"Add {SelectedSkill.SkillName}");
                    return;
                }
            }
            // I was originally going to use the below method to check if a contractor had a skill but it did not work.
            // My guess is this is due to the ErrorCollection being the same
            //if (ContractorSkills.Contains(SelectedSkill))
            //{
            //    MessageBox.Show($"{contractorName} already has {skillName}!", $"Add {SelectedSkill.SkillName}");
            //    return;
            //}
            string message;
            int rowsAffected = SelectedContractor.AddSkill(skillName);
            if (rowsAffected >= 1)
            {
                message = $"You have successfully added {skillName} to {contractorName}!";
            }
            else
            {
                message = $"There was an issue when adding {skillName} to {contractorName}, please try again!";
            }
            MessageBox.Show(message, $"Add {SelectedSkill.SkillName}");
            UpdateSkills();
        }
        public void AddNewSkill()
        {
            string skillName = NewSkill.SkillName;
            foreach (Skill skill in AllSkills)
            {
                if (skill.SkillName == skillName)
                {
                    MessageBox.Show($"{skillName} already has exists!", $"Add {SelectedSkill.SkillName}");
                    return;
                }
            }
            string message;
            int rowsAffected = NewSkill.InsertSkill();
            if (rowsAffected >= 1)
            {
                message = $"You have successfully added {skillName}!";
            }
            else
            {
                message = $"There was an issue when adding {skillName}, please try again!";
            }
            MessageBox.Show(message, $"Add {skillName}");
            UpdateSkills();
        }
        public void AddAvailabilityMethod()
        {
            if (SelectedContractor == null)
            {
                MessageBox.Show($"You must select a contractor to add availability for!", "Add Availability");
                return;
            }
            string availabilityDate = NewAvailability.AvailabilityDate.ToString("MM/dd/yyyy");
            foreach (Availability availability in ContractorAvailability)
            {
                if (availability.AvailabilityDate.Date == NewAvailability.AvailabilityDate.Date)
                {
                    MessageBox.Show($"{SelectedContractor.FullName} already has the availability for {availabilityDate}!", "Add Availability");
                    return;
                }
            }
            string message;
            int rowsAffected = NewAvailability.InsertAvailability();
            if (rowsAffected >= 1)
            {
                message = $"You have successfully added availability for {availabilityDate}!";
            }
            else
            {
                message = $"There was an issue when adding availability for {availabilityDate}, please try again!";
            }
            MessageBox.Show(message, $"Add Availability");
            UpdateAvailabilities();
        }
        public void DeleteAvailabilityMethod()
        {
            if (SelectedAvailability == null)
            {
                MessageBox.Show("You must select an availability to delete!", "Delete Availability");
                return;
            }
            string availabilityDate = SelectedAvailability.AvailabilityDate.ToString("MM/dd/yyyy");
            string message;
            int rowsAffected = NewAvailability.DeleteAvailability();
            if (rowsAffected >= 1)
            {
                message = $"You have successfully deleted availability for {availabilityDate}!";
            }
            else
            {
                message = $"There was an issue when deleting availability for {availabilityDate}, please try again!";
            }
            MessageBox.Show(message, $"Delete Availability");
            UpdateAvailabilities();

        }
        #endregion

        #region Public Properties
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
        public ObservableCollection<Skill> AllSkills
        {
            get { return _allSkills; }
            set
            {
                _allSkills = value;
                OnPropertyChanged("AllSkills");
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
                    UpdateSkills();
                    UpdateAvailabilities();
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
        public Skill NewSkill
        {
            get { return _newSkill; }
            set
            {
                _newSkill = value;
                OnPropertyChanged("NewSkill");
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
        public Availability NewAvailability
        {
            get { return _newAvailability; }
            set
            {
                _newAvailability = value;
                OnPropertyChanged("NewAvailability");
            }
        }

        #endregion
        public bool IsSkillsTabSelected
        {
            get { return _isSkillsTabSelected; }
            set
            {
                if (value && this.NewSkill == null)
                {
                    this.NewSkill = new Skill()
                    {
                        SkillName = ""
                    };
                }
                _isSkillsTabSelected = value;
                OnPropertyChanged("IsSkillsTabSelected");
            }
        }
        public bool IsAvailabilityTabSelected
        {
            get { return _isSkillsTabSelected; }
            set
            {

                if (value && SelectedContractor == null)
                {
                    _isSkillsTabSelected = false;
                    MessageBox.Show("You must select a contractor before looking at their availability");
                }
                else
                {
                    if (value && this.NewAvailability == null)
                    {
                        this.NewAvailability = new Availability()
                        {
                            ContractorId = SelectedContractor.ContractorId,
                            AvailabilityDate = DateTime.Now
                        };
                    }
                    _isSkillsTabSelected = value;
                }
                OnPropertyChanged("IsAvailabilityTabSelected");
            }
        }
        public void UpdateContractors()
        {
            Contractors allContractors = new Contractors();
            this.Contractors = new ObservableCollection<Contractor>(allContractors);
        }

        public void UpdateSkills()
        {
            if (SelectedContractor != null)
            {
                Skills allContractorSkills = new Skills(SelectedContractor.ContractorId);
                this.ContractorSkills = new ObservableCollection<Skill>(allContractorSkills);
            }
            Skills allSkills = new Skills();
            this.AllSkills = new ObservableCollection<Skill>(allSkills);
        }

        public void UpdateAvailabilities()
        {
            Availabilities allAvailability = new Availabilities(SelectedContractor.ContractorId);
            this.ContractorAvailability = new ObservableCollection<Availability>(allAvailability);
        }
        public ContractorManagementViewModel()
        {
            UpdateContractors();
            UpdateSkills();
        }
        public virtual ObservableCollection<Contractor> GetContractors()
        {
            List<Contractor> allContractors = new List<Contractor>()
                {
                    new Contractor(1),
                    new Contractor(2),
                    new Contractor(3),
                };
            return new ObservableCollection<Contractor>(allContractors);
        }
    }
}
