using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using BitServicesDesktopApp.Commands;
using BitServicesDesktopApp.Models;

namespace BitServicesDesktopApp.ViewModels
{
    public class AddContractorViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Skill> _allSkills;
        private Skill _newSkill;
        private Contractor _newContractor;
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
        public void AddMethod()
        {
            string message;
            int rowsAffected = NewContractor.InsertContractor();
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

        }

        public void ClearMethod()
        {
            this.NewContractor = new Contractor();
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
                MessageBox.Show(_newSkill.SkillName);
            }
        }
        public AddContractorViewModel()
        {
            NewContractor = new Contractor();
            Skills allSkills = new Skills();
            this.AllSkills = new ObservableCollection<Skill>(allSkills);
        }
    }
}
