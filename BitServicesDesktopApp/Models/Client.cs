using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using BitServicesDesktopApp.DAL;
using BitServicesDesktopApp.Views;

namespace BitServicesDesktopApp.Models
{
    public class Client : INotifyPropertyChanged, IDataErrorInfo
    {
        private int _clientId;
        private string _name;
        private string _email;
        private string _phone;
        private string _password;
        private bool _active;
        private SQLHelper _db;
        private LogHelper _log;
        public event PropertyChangedEventHandler PropertyChanged;
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string Error { get { return null; } }
        public string this[string propertyName]
        {
            get
            {
                string result = null;
                switch (propertyName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(this.Name))
                        {
                            result = "Client name cannot be left empty";
                        }
                        else if (this.Name.Length > 32)
                        {
                            result = "Client name cannot be more than 32 characters";
                        }
                        break;
                    case "Phone":
                        if (string.IsNullOrEmpty(Phone))
                        {
                            result = "Phone number cannot be empty";
                        }
                        else if (Phone.Length != 10)
                        {
                            result = "Phone numbers must be 10 digits";
                        }
                        break;

                    case "Email":
                        if (string.IsNullOrEmpty(Email))
                        {
                            result = "Email cannot be empty";
                        }
                        else if (this.Email.Length > 254)
                        {
                            result = "Email cannot be more than 254 characters";
                        }
                        break;
                }
                if (result != null && !ErrorCollection.ContainsKey(propertyName))
                {
                    ErrorCollection[propertyName] = result;
                    OnPropertyChanged("ErrorCollection");
                }
                return result;
            }
        }
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
            this._db = new SQLHelper();
            this._log = new LogHelper();
        }
        public Client(DataRow dr)
        {
            this._db = new SQLHelper();
            this._log = new LogHelper();
            this.ClientId = Convert.ToInt32(dr["client_id"].ToString());
            this.Name = dr["name"].ToString();
            this.Email = dr["email"].ToString();
            this.Phone = dr["phone"].ToString();
            this.Password = dr["password"].ToString();
            this.Active = Convert.ToBoolean(dr["active"]);
        }
        public Client(int clientId)
        {
            this._db = new SQLHelper();
            this._log = new LogHelper();
            _db = new SQLHelper();
            string sql = "SELECT client_id, client_name, email, phone, active" +
                         " FROM client" +
                         " WHERE client_id = @ClientId";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@ClientId", DbType.Int32);
            objParams[0].Value = clientId;
            DataRow dr = _db.ExecuteSQL(sql, objParams).Rows[0];
            this.ClientId = Convert.ToInt32(dr["client_id"]);
            this.Name = dr["client_name"].ToString();
            this.Email = dr["email"].ToString();
            this.Phone = dr["phone"].ToString();
            this.Active = Convert.ToBoolean(dr["active"]);
        }

        public int InsertClient()
        {
            GeneratePassword();
            string sql = "INSERT INTO client (client_name, email, phone, password, active)" +
                         " VALUES(@Name, @Email, @Phone, @Password, 1)";
            SqlParameter[] objParams = new SqlParameter[4];
            objParams[0] = new SqlParameter("@Name", DbType.String)
            {
                Value = this.Name
            };
            objParams[1] = new SqlParameter("@Email", DbType.String)
            {
                Value = this.Email
            };
            objParams[2] = new SqlParameter("@Phone", DbType.String)
            {
                Value = this.Phone
            };
            objParams[3] = new SqlParameter("@Password", DbType.String)
            {
                Value = this.Password
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams);
            
            if (rowsAffected > 0)
            {
                this._log.Log($"Client {this.Name} was successfully added by {MainWindow.LoggedInStaff.FullName} ({MainWindow.LoggedInStaff.StaffId}).", LogType.Info);
            }
            else
            {
                this._log.Log($"Error when adding Client {this.Name}.", LogType.Error);
            }
            return rowsAffected;
        }
        public int DeleteClient()
        {
            string sql = "UPDATE client" +
                         " SET active = 0" +
                         " WHERE client_id = @ClientId";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@ClientId", DbType.Int32)
            {
                Value = this.ClientId
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams);
            
            if (rowsAffected > 0)
            {
                this._log.Log($"Client {this.Name} ({this.ClientId}) was deleted by {MainWindow.LoggedInStaff.FullName} ({MainWindow.LoggedInStaff.StaffId}).", LogType.Info);
            }
            else
            {
                this._log.Log($"Error when deleting Client {this.Name} ({this.ClientId}).", LogType.Error);
            }
            return rowsAffected;
        }
        private void GeneratePassword()
        {
            this.Password = "Password";
        }

        public int UpdateClient()
        {
            string sql = "UPDATE client" +
                         " SET client_name = @Name, email = @Email, phone = @Phone" +
                         " WHERE client_id = @ClientId";
            SqlParameter[] objParams = new SqlParameter[4];
            objParams[0] = new SqlParameter("@Name", DbType.String)
            {
                Value = this.Name
            };
            objParams[1] = new SqlParameter("@Email", DbType.String)
            {
                Value = this.Email
            };
            objParams[2] = new SqlParameter("@Phone", DbType.String)
            {
                Value = this.Phone
            };
            objParams[3] = new SqlParameter("@ClientId", DbType.String)
            {
                Value = this.ClientId
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams);
            
            if (rowsAffected > 0)
            {
                this._log.Log($"Client {this.Name} ({this.ClientId}) was successfully updated by {MainWindow.LoggedInStaff.FullName} ({MainWindow.LoggedInStaff.StaffId}).", LogType.Info);
            }
            else
            {
                this._log.Log($"Error when updating Client {this.Name} ({this.ClientId}).", LogType.Error);
            }
            return rowsAffected;
        }
    }
}
