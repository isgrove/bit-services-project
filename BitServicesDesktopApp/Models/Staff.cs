using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using BitServicesDesktopApp.DAL;

namespace BitServicesDesktopApp.Models
{
    public class Staff : INotifyPropertyChanged
    {
        private int _staffId;
        private string _staffType;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phone;
        private string _password;
        private bool _active;
        private SQLHelper _db;
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
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

        public int InsertStaff()
        {
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
        public int DeleteStaff()
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

        // TODO: Generate a random password
        private void GeneratePassword()
        {
            this.Password = "Password";
        }

        public int UpdateStaff()
        {
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
    }
}
