using BitServicesDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using BitServicesDesktopApp.Commands;
using MessageBox = System.Windows.MessageBox;

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
        private RelayCommand _deletedSkillCommand;
        private RelayCommand _addSkillCommand;
        private RelayCommand _deleteAvailabilityCommand;
        private RelayCommand _addAvailabilityCommand;
        private RelayCommand _searchCommand;
        private Skill _selectedSkill;
        private Skill _newSkill;
        private Availability _selectedAvailability;
        private Availability _newAvailability;
        private bool _isSkillsTabSelected;
        private bool _isAvailabilityTabSelected;
        private string _searchText;

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
                    _addSkillCommand = new RelayCommand(this.AddContractorSkillMethod, true);
                }
                return _addSkillCommand;
            }
            set { _addSkillCommand = value; }
        }
        public RelayCommand DeleteSkillCommand
        {
            get
            {
                if (_deletedSkillCommand == null)
                {
                    _deletedSkillCommand = new RelayCommand(this.DeleteContractorSkillMethod, true);
                }
                return _deletedSkillCommand;
            }
            set { _deletedSkillCommand = value; }
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
        #endregion

        #region Command Methods

        #region Contractor Command Methods
        public void DeleteMethod()
        {
            string caption = "Delete Contractor";
            if (SelectedContractor == null)
            {
                MessageBox.Show(
                    "You must select a contractor to delete!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Warning
                );
                return;
            }

            string contractorName = SelectedContractor.FullName;
            MessageBoxResult messageBoxResult = MessageBox.Show(
                $"Are you sure that you want to delete {contractorName}?",
                caption, MessageBoxButton.YesNo, MessageBoxImage.Information
                );

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                int rowsAffected;
                try
                {
                    rowsAffected = SelectedContractor.Delete();
                }
                catch
                {
                    rowsAffected = -1;
                }

                if (rowsAffected >= 1)
                {
                    MessageBox.Show(
                        $"You have successfully deleted {contractorName}!",
                        caption, MessageBoxButton.OK, MessageBoxImage.Information
                        );
                    UpdateContractors();
                }
                else
                {
                    MessageBox.Show(
                        $"There was an issue when deleting {contractorName}, please try again!",
                        caption, MessageBoxButton.OK, MessageBoxImage.Error
                        );
                }
            }
        }
        public void SaveMethod()
        {
            string caption = "Save Contractor";
            if (SelectedContractor == null)
            {
                MessageBox.Show(
                    "You must select a contractor to save!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Warning
                );
                return;
            }
            int rowsAffected;
            try
            {
                rowsAffected = SelectedContractor.Update();
            }
            catch
            {
                rowsAffected = -1;
            }
            if (rowsAffected >= 1)
            {
                MessageBox.Show(
                    $"You have successfully saved {SelectedContractor.FullName}!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Information
                );
                UpdateContractors();
            }
            else
            {
                MessageBox.Show(
                    $"There was an issue when saving {SelectedContractor.FullName}, please try again!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Error
                    );
            }
        }
        #endregion

        #region Skill Command Methods
        public void AddContractorSkillMethod()
        {
            string caption = "Add Skill";
            if (SelectedContractor == null)
            {
                MessageBox.Show(
                    "You must select a contractor to add a skill to!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Warning
                );
                return;
            }
            if (SelectedSkill == null)
            {
                MessageBox.Show(
                    "You must select a skill to add!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Warning
                );
                return;
            }
            if (ContractorSkills.Any(skill => skill.SkillName == NewSkill.SkillName))
            {
                MessageBox.Show(
                    $"{SelectedContractor.FullName} already has the {NewSkill.SkillName} skill!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Warning
                );
                return;
            }

            int rowsAffected;
            try
            {
                rowsAffected = SelectedContractor.AddSkill(SelectedSkill.SkillName);
            }
            catch
            {
                rowsAffected = -1;
            }

            if (rowsAffected >= 1)
            {
                MessageBox.Show(
                    $"You have added the {SelectedSkill.SkillName} skill to {SelectedContractor.FullName}!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Information
                );
                UpdateSkills();
            }
            else
            {
                MessageBox.Show(
                    $"There was an issue when adding the {SelectedSkill.SkillName} skill to {SelectedContractor.FullName}, please try again!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteContractorSkillMethod()
        {
            string caption = "Delete Skill";
            if (SelectedContractor == null)
            {
                MessageBox.Show(
                    "You must select a contractor to delete a skill from!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Warning
                );
                return;
            }
            if (SelectedSkill == null)
            {
                MessageBox.Show(
                    "You must select a skill to delete!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Warning
                );
                return;
            }
            if (ContractorSkills.All(skill => skill.SkillName != SelectedSkill.SkillName))
            {
                MessageBox.Show(
                    $"{SelectedContractor.FullName} does not have the {SelectedSkill.SkillName} skill!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Warning
                );
                return;
            }

            int rowsAffected;
            try
            {

                rowsAffected = SelectedContractor.DeleteSkill(SelectedSkill.SkillName);
            }
            catch
            {
                rowsAffected = -1;
            }

            if (rowsAffected >= 1)
            {
                MessageBox.Show(
                    $"You have successfully deleted {SelectedSkill.SkillName} from {SelectedContractor.FullName}!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Information
                    );
                UpdateSkills();
            }
            else
            {
                MessageBox.Show(
                    $"There was an issue when deleting {SelectedSkill.SkillName} from {SelectedContractor.FullName}, please try again!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Information
                    );
            }
        }
        public void AddNewSkill()
        {
            string caption = "Create Skill";
            if (NewSkill.ErrorCollection.Count > 0)
            {
                MessageBox.Show(
                    "Skill data isn't valid!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Warning
                );
                return;
            }
            if (AllSkills.All(skill => skill.SkillName != NewSkill.SkillName))
            {
                MessageBox.Show(
                    $"A skill called {NewSkill.SkillName} already exists!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Warning
                    );
                return;
            }

            int rowsAffected;
            try
            {

                rowsAffected = NewSkill.Create();
            }
            catch
            {
                rowsAffected = -1;
            }

            if (rowsAffected >= 1)
            {
                MessageBox.Show(
                    $"You have successfully added a new skill called {NewSkill.SkillName}!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Information
                    );
                UpdateSkills();
            }
            else
            {
                MessageBox.Show(
                    $"There was an issue when adding a new skill called {NewSkill.SkillName}, please try again!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Error
                    );
            }
        }
        #endregion

        #region Availability Command Methods
        public void AddAvailabilityMethod()
        {
            string caption = "Add Availability";
            if (SelectedContractor == null)
            {
                MessageBox.Show(
                    "You must select a contractor to add availability for!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Warning
                    );
                return;
            }
            if (NewAvailability.ErrorCollection.Count > 0)
            {
                MessageBox.Show(
                    "Availability data isn't valid!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Warning
                    );
                return;
            }
            if (ContractorAvailability.Any(availability => availability.AvailabilityDate == NewAvailability.AvailabilityDate))
            {
                MessageBox.Show(
                    $"{SelectedContractor.FullName} already has the availability for {NewAvailability.FormattedAvailability}!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Warning
                    );
                return;
            }

            int rowsAffected;
            try
            {
                rowsAffected = NewAvailability.Create();
            }
            catch
            {
                rowsAffected = -1;
            }

            if (rowsAffected >= 1)
            {
                MessageBox.Show(
                    $"You have added availability for {SelectedContractor.FullName} on {NewAvailability.FormattedAvailability}!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Information
                    );
                UpdateAvailabilities();
            }
            else
            {
                MessageBox.Show(
                    $"There was an issue when adding availability for {SelectedContractor.FullName} on {NewAvailability.FormattedAvailability}, please try again!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteAvailabilityMethod()
        {
            string caption = "Delete Availability";

            if (SelectedAvailability == null)
            {
                MessageBox.Show(
                    "You must select an availability to delete!",
                    caption,
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                    );
                return;
            }

            int rowsAffected;
            try
            {
                rowsAffected = SelectedAvailability.Delete();
            }
            catch
            {
                rowsAffected = -1;
            }

            if (rowsAffected >= 1)
            {
                MessageBox.Show(
                    $"You have successfully deleted availability for {SelectedAvailability.FormattedAvailability}!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateAvailabilities();
            }
            else
            {
                MessageBox.Show(
                    $"There was an issue when deleting availability for {SelectedAvailability.FormattedAvailability}, please try again!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
        #endregion

        public void SearchMethod()
        {
            Contractors allContractors = new Contractors(SearchText);
            this.Contractors = new ObservableCollection<Contractor>(allContractors);
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
            get { return _isAvailabilityTabSelected; }
            set
            {
                if (value && SelectedContractor == null)
                {
                    _isAvailabilityTabSelected = false;
                    MessageBox.Show("You must select a contractor before looking at their availability");
                }
                else
                {
                    if (value && this.NewAvailability == null)
                    {
                        this.NewAvailability = new Availability()
                        {
                            ContractorId = SelectedContractor.ContractorId,
                            AvailabilityDate = DateTime.Now.Add(new TimeSpan(1, 0, 0, 0, 0))
                        };
                    }
                    _isAvailabilityTabSelected = value;
                }
                OnPropertyChanged("IsAvailabilityTabSelected");
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
        #endregion

        #region Public Methods
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
        #endregion

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