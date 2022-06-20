using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using BitServicesDesktopApp.Commands;
using BitServicesDesktopApp.Models;

namespace BitServicesDesktopApp.ViewModels
{
    class AddStaffViewModel : INotifyPropertyChanged
    {
        private Staff _newStaff;
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
            int rowsAffected = NewStaff.Create();
            string staffName = NewStaff.FullName;
            if (rowsAffected >= 1)
            {
                message = $"You have successfully added {staffName}!";
            }
            else
            {
                message = $"There was an issue when adding {staffName}, please try again!";
            }
            MessageBox.Show(message, $"Adding {staffName}");
        }

        public void ClearMethod()
        {
            this.NewStaff = new Staff()
            {
                StaffType = "Coordinator"
            };
            MessageBox.Show("Cleared all fields","Cleared");
        }
        public Staff NewStaff
        {
            get { return _newStaff; }
            set
            {
                _newStaff = value;
                OnPropertyChanged("NewStaff");
            }
        }
        public AddStaffViewModel()
        {
            NewStaff = new Staff()
            {
                StaffType = "Coordinator"
            };
        }
    }
}
