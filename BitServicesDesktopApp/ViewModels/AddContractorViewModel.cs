using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BitServicesDesktopApp.Commands;
using BitServicesDesktopApp.Models;

namespace BitServicesDesktopApp.ViewModels
{
    public class AddContractorViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Skill> _allSkills;
        private ObservableCollection<Skill> _addedSkills;
        private Skill _newSkill;
        private Skill _selectedSkill;
        private Contractor _newContractor;
        private RelayCommand _addCommand;
        private RelayCommand _addSkillCommand;
        private RelayCommand _removeSkillCommand;
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

        public RelayCommand RemoveSkillCommand
        {
            get
            {
                if (_removeSkillCommand == null)
                {
                    _removeSkillCommand = new RelayCommand(this.RemoveSkillMethod, true);
                }

                return _removeSkillCommand;
            }
            set { _removeSkillCommand = value; }
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
        public void AddMethod()
        {
            string message;
            int rowsAffected = NewContractor.Create(AddedSkills);
            if (rowsAffected >= 1)
            {
                message = $"You have successfully added {NewContractor.FirstName} {NewContractor.LastName}!";
            }
            else
            {
                message = $"There was an issue when adding {NewContractor.FirstName} {NewContractor.LastName}, please try again!";
            }
            MessageBox.Show(message);
        }

        public void AddSkillMethod()
        {
            if (NewSkill != null)
            {
                if (AddedSkills.Contains(NewSkill))
                {
                    MessageBox.Show("You have already added this skill!");
                }
                else
                {
                    AddedSkills.Add(NewSkill);
                }
            }
        }
        private void RemoveSkillMethod()
        {
            if (SelectedSkill != null)
            {
                AddedSkills.Remove(SelectedSkill);
            }
        }

        public void ClearMethod()
        {
            this.NewContractor = new Contractor();
            this.AddedSkills = new ObservableCollection<Skill>();
            MessageBox.Show("Cleared");
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
        public ObservableCollection<Skill> AddedSkills
        {
            get { return _addedSkills; }
            set
            {
                _addedSkills = value;
                OnPropertyChanged("AddedSkills");
            }
        }
        public Contractor NewContractor
        {
            get { return _newContractor; }
            set
            {
                _newContractor = value;
                OnPropertyChanged("NewContractor");
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
        public Skill SelectedSkill
        {
            get { return _selectedSkill; }
            set
            {
                _selectedSkill = value;
                OnPropertyChanged("SelectedSkill");
            }
        }
        public AddContractorViewModel()
        {
            NewContractor = new Contractor();
            Skills allSkills = new Skills();
            this.AllSkills = new ObservableCollection<Skill>(allSkills);
            this.AddedSkills = new ObservableCollection<Skill>();
        }
    }
}
