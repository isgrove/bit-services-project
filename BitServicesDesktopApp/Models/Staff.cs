using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using BitServicesDesktopApp.Helpers;

namespace BitServicesDesktopApp.Models
{
    public class Staff : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Private Properties
        private int _staffId;
        private string _staffType;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phone;
        private string _password;
        private bool _active;
        private SQLHelper _db;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        #region Validation
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string Error { get { return null; } }
        public string this[string propertyName]
        {
            get
            {
                string result = null;
                switch (propertyName)
                {
                    case "FirstName":
                        if (string.IsNullOrEmpty(this.FirstName))
                        {
                            result = "First name cannot be left empty";
                        }
                        else if (this.FirstName.Length > 32)
                        {
                            result = "First name cannot be more than 32 characters";
                        }
                        break;
                    case "LastName":
                        if (string.IsNullOrEmpty(LastName))
                        {
                            result = "Last name cannot be left empty";
                        }
                        else if (this.LastName.Length > 32)
                        {
                            result = "Last name cannot be more than 32 characters";
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
                if (result != null)
                {
                    ErrorCollection[propertyName] = result;
                }
                else if (ErrorCollection.ContainsKey(propertyName))
                {
                    ErrorCollection.Remove(propertyName);
                }

                OnPropertyChanged("ErrorCollection");
                return result;
            }
        }
        #endregion
        
        #region Public Properties
        public int StaffId
        {
            get { return _staffId; }
            set { _staffId = value; }
        }
        public string StaffType
        {
            get { return _staffType; }
            set
            {
                _staffType = value;
                OnPropertyChanged("StaffType");
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
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
        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
        }
        #endregion

        #region Constructors 
        public Staff()
        {
            _db = new SQLHelper();
        }
        public Staff(DataRow dr)
        {
            _db = new SQLHelper();
            this.StaffId = Convert.ToInt32(dr["staff_id"].ToString());
            this.StaffType = dr["type"].ToString();
            this.FirstName = dr["first_name"].ToString();
            this.LastName = dr["last_name"].ToString();
            this.Email = dr["email"].ToString();
            this.Phone = dr["phone"].ToString();
            this.Password = dr["password"].ToString();
            this.Active = Convert.ToBoolean(dr["active"]);
        }
        public Staff(int staffId)
        {
            _db = new SQLHelper();
            string sql = "usp_GetStaff";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@StaffId", DbType.Int32);
            objParams[0].Value = staffId;

            DataRow dr = _db.ExecuteSQL(sql, objParams).Rows[0];

            this.StaffId = Convert.ToInt32(dr["staff_id"].ToString());
            this.StaffType = dr["type"].ToString();
            this.FirstName = dr["first_name"].ToString();
            this.LastName = dr["last_name"].ToString();
            this.Email = dr["email"].ToString();
            this.Phone = dr["phone"].ToString();
            this.Password = dr["password"].ToString();
            this.Active = Convert.ToBoolean(dr["active"]);
        }
        #endregion

        private void GeneratePassword()
        {
            this.Password = "Password";
        }

        #region Public Methods
        public int Create()
        {
            if (this.ErrorCollection.Count > 0)
            {
                return -1;
            }
            GeneratePassword();
            string sql = "usp_InsertStaff";
            SqlParameter[] objParams = new SqlParameter[6];
            objParams[0] = new SqlParameter("@FirstName", DbType.String)
            {
                Value = this.FirstName
            };
            objParams[1] = new SqlParameter("@LastName", DbType.String)
            {
                Value = this.LastName
            };
            objParams[2] = new SqlParameter("@Email", DbType.String)
            {
                Value = this.Email
            };
            objParams[3] = new SqlParameter("@Phone", DbType.String)
            {
                Value = this.Phone
            };
            objParams[4] = new SqlParameter("@Password", DbType.String)
            {
                Value = this.Password
            };
            objParams[5] = new SqlParameter("@StaffType", DbType.String)
            {
                Value = this.StaffType
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams, true);
            return rowsAffected;
        }
        public int Delete()
        {
            string sql = "usp_DeleteStaff";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@StaffId", DbType.Int32)
            {
                Value = this.StaffId
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams, true);
            return rowsAffected;
        }
        public int Update()
        {
            if (this.ErrorCollection.Count > 0)
            {
                return -1;
            }
            string sql = "usp_UpdateStaff";
            SqlParameter[] objParams = new SqlParameter[7];
            objParams[0] = new SqlParameter("@StaffId", DbType.Int32)
            {
                Value = this.StaffId
            };
            objParams[1] = new SqlParameter("@FirstName", DbType.String)
            {
                Value = this.FirstName
            };
            objParams[2] = new SqlParameter("@LastName", DbType.String)
            {
                Value = this.LastName
            };
            objParams[3] = new SqlParameter("@Email", DbType.String)
            {
                Value = this.Email
            };
            objParams[4] = new SqlParameter("@Phone", DbType.String)
            {
                Value = this.Phone
            };
            objParams[5] = new SqlParameter("@Password", DbType.String)
            {
                Value = this.Password
            };
            objParams[6] = new SqlParameter("@StaffType", DbType.String)
            {
                Value = this.StaffType
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams, true);
            return rowsAffected;
        }
        #endregion
    }
}
