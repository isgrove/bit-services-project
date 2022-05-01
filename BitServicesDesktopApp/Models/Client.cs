using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace BitServicesApp.Models
{
    public class Client : INotifyPropertyChanged
    {
        private int _clientId;
        private string _name;
        private string _email;
        private string _phone;
        private string _password;
        private bool _active;
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        public int ClientId
        {
            get { return _clientId; }
            set { _clientId = value; }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged("Phone");
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                OnPropertyChanged("Active");
            }
        }
        public Client()
        {

        }
        public Client(DataRow dr)
        {
            this.ClientId = Convert.ToInt32(dr["client_id"].ToString());
            this.Name = dr["name"].ToString();
            this.Email = dr["email"].ToString();
            this.Phone = dr["phone"].ToString();
            this.Password = dr["password"].ToString();
            this.Active = Convert.ToBoolean(dr["active"]);
        }
    }
}
