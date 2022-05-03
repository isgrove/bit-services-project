using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using BitServicesDesktopApp.Models;

namespace BitServicesDesktopApp.ViewModels
{
    public class StaffManagementViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Staff> _staffs;
        private ObservableCollection<StaffType> _staffTypes;
        private Staff _selectedStaff;
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

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
       
        public StaffManagementViewModel()
        {
            Staffs allStaff = new Staffs();
            this.Staffs = new ObservableCollection<Staff>(allStaff);
            StaffTypes allStaffTypes = new StaffTypes();
            this.StaffTypes = new ObservableCollection<StaffType>(allStaffTypes);
        }
    }
}
