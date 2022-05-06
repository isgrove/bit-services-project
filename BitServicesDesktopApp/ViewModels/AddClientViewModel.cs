using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Windows.Navigation;
using BitServicesDesktopApp.Commands;
using BitServicesDesktopApp.Models;

namespace BitServicesDesktopApp.ViewModels
{
    public class AddClientViewModel : INotifyPropertyChanged
    {
        private Client _newClient;
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
            int rowsAffected = NewClient.InsertClient();
            if (rowsAffected >= 1)
            {
                message = "You have successfully added " + NewClient.Name + "!";
            }
            else
            {
                message = "There was an issue when adding " + NewClient.Name + ", please try again!";
            }
            MessageBox.Show(message);
        }

        public void ClearMethod()
        {
            this.NewClient = new Client();
            MessageBox.Show("Cleared");
        }
        public Client NewClient
        {
            get { return _newClient; }
            set
            {
                _newClient = value; 
                OnPropertyChanged("NewClient");
            }
        }
        public AddClientViewModel()
        {
            NewClient = new Client();
        }
    }
}
