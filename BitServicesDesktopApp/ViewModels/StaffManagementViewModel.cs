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
        #region Private Properties
        private ObservableCollection<Staff> _staffs;
        private ObservableCollection<StaffType> _staffTypes;
        private Staff _selectedStaff;
        private RelayCommand _deleteCommand;
        private RelayCommand _saveCommand;
        private RelayCommand _searchCommand;
        private string _searchText;
        #endregion

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
            string caption = "Delete Coordinator";
            if (SelectedStaff == null)
            {
                MessageBox.Show(
                    "You must select a coordinator to delete!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Warning
                    );
                return;
            }
            if (SelectedStaff.StaffType == "Admin")
            {
                MessageBox.Show(
                    "You can only delete the details of coordinators! Please contact the database admin if you would like to delete a staff admin.",
                    caption, MessageBoxButton.OK, MessageBoxImage.Warning
                    );
                return;
            }
            MessageBoxResult messageBoxResult = MessageBox.Show(
                $"Are you sure that you want to delete {SelectedStaff.FullName}?",
                caption, MessageBoxButton.YesNo, MessageBoxImage.Information
            );

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                int rowsAffected;
                try
                {
                    rowsAffected = SelectedStaff.Delete();
                }
                catch
                {
                    rowsAffected = -1;
                }

                if (rowsAffected >= 1)
                {
                    MessageBox.Show(
                        $"You have successfully deleted {SelectedStaff.FullName}!",
                        caption, MessageBoxButton.OK, MessageBoxImage.Information
                        );
                    UpdateStaff();
                }
                else
                {
                    MessageBox.Show(
                        $"There was an issue when deleting {SelectedStaff.FullName}, please try again!",
                        caption, MessageBoxButton.OK, MessageBoxImage.Error
                        );
                }
            }
        }
        public void SaveMethod()
        {
            string caption = "Save Coordinator";
            if (SelectedStaff == null)
            {
                MessageBox.Show(
                    "You must select a coordinator to save!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Warning
                    );
                return;
            }
            if (SelectedStaff.StaffType == "Admin")
            {
                MessageBox.Show(
                    "You can only update the details of coordinators! Please contact the database admin if you would like to make changes to a staff admin.",
                    caption, MessageBoxButton.OK, MessageBoxImage.Warning
                    );
                return;
            }

            int rowsAffected;
            try
            {
                rowsAffected = SelectedStaff.Update();
            }
            catch
            {
                rowsAffected = -1;
            }

            if (rowsAffected >= 1)
            {
                MessageBox.Show(
                    $"You have successfully saved {SelectedStaff.FullName}!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Information
                );
                UpdateStaff();
            }
            else
            {
                MessageBox.Show(
                    $"There was an issue when saving {SelectedStaff.FullName}, please try again!",
                    caption, MessageBoxButton.OK, MessageBoxImage.Error
                );
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
