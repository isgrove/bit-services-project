using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using BitServicesDesktopApp.Commands;
using BitServicesDesktopApp.Models;

namespace BitServicesDesktopApp.ViewModels
{
    public class StaffManagementViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Staff> _staffs;
        private ObservableCollection<StaffType> _staffTypes;
        private Staff _selectedStaff;
        private RelayCommand _deleteCommand;
        private RelayCommand _saveCommand;
        private RelayCommand _searchCommand;
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
        public void DeleteMethod()
        {
            if (SelectedStaff.StaffType == "Admin")
            {
                MessageBox.Show(
                    "You can only delete the details of coordinators! Please contact the database admin if you would like to delete a staff admin.", "Cannot Delete Admin");
                return;
            }
            string StaffName = SelectedStaff.FullName;
            MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure that you want to delete {StaffName}?",
                $"Delete {StaffName}", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string message;
                int rowsAffected = SelectedStaff.Delete();
                if (rowsAffected >= 1)
                {
                    message = $"You have successfully deleted {StaffName}!";
                }

                else
                {
                    message = $"There was an issue when deleting {StaffName}, please try again!";
                }
                MessageBox.Show(message, $"Delete {StaffName}");
                UpdateStaff();
            }
        }
        public void SaveMethod()
        {
            if (SelectedStaff == null) return;
            if (SelectedStaff.StaffType == "Admin")
            {
                MessageBox.Show(
                    "You can only update the details of coordinators! Please contact the database admin if you would like to make changes to a staff admin.", "Cannot Update Admin");
                return;
            }
            string StaffName = SelectedStaff.FullName;
            MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure that you would like to update {StaffName}?", $"Update {StaffName}", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string message;
                int rowsAffected = SelectedStaff.Update();
                if (rowsAffected >= 1)
                {
                    message = $"You have successfully saved {StaffName}!";
                }
                else
                {
                    message = $"There was an issue when saving {StaffName}, please try again!";
                }
                MessageBox.Show(message, $"Update {StaffName}");
                UpdateStaff();
            }

        }
        public void SearchMethod()
        {
            Staffs allStaff = new Staffs(SearchText);
            this.Staffs = new ObservableCollection<Staff>(allStaff);
        }
        #endregion
        #region Public Properties
        public ObservableCollection<Staff> Staffs
        {
            get { return _staffs; }
            set
            {
                _staffs = value;
                OnPropertyChanged("Staffs");
            }
        }
        public ObservableCollection<StaffType> StaffTypes
        {
            get { return _staffTypes; }
            set
            {
                _staffTypes = value;
                OnPropertyChanged("StaffTypes");
            }
        }
        public Staff SelectedStaff
        {
            get { return _selectedStaff; }
            set
            {
                _selectedStaff = value;
                OnPropertyChanged("SelectedStaff");
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
        private void UpdateStaff()
        {
            Staffs allStaff = new Staffs();
            this.Staffs = new ObservableCollection<Staff>(allStaff);
        }

        public StaffManagementViewModel()
        {
            UpdateStaff();
            StaffTypes allStaffTypes = new StaffTypes();
            this.StaffTypes = new ObservableCollection<StaffType>(allStaffTypes);
        }
        public virtual ObservableCollection<Staff> GetStaff()
        {
            List<Staff> allStaff = new List<Staff>()
            {
                new Staff(1),
                new Staff(2),
                new Staff(3),
            };
            return new ObservableCollection<Staff>(allStaff);
        }        
    }
}
