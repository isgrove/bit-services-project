using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using BitServicesDesktopApp.DAL;

namespace BitServicesDesktopApp.Models
{
    public class Contractor : INotifyPropertyChanged, IDataErrorInfo
    {
        private int _contractorId;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phone;
        private string _password;
        private string _street;
        private string _suburb;
        private string _postcode;
        private string _state;
        private string _licenceNumber;
        private string _vehicleRegistration;
        private int _performanceRating;
        private bool _active;
        private SQLHelper _db;
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
                        else if (this.FirstName.Length > 254)
                        {
                            result = "First name cannot be more than 254 characters";
                        }
                        break;
                    case "Street":
                        if (string.IsNullOrEmpty(Street))
                        {
                            result = "Field cannot be left empty";
                        }
                        else if (this.Street.Length > 32)
                        {
                            result = "Street cannot be more than 32 characters!";
                        }
                        break;
                    case "Suburb":
                        if (string.IsNullOrEmpty(Suburb))
                        {
                            result = "Suburb cannot be empty";
                        }
                        else if (this.Suburb.Length > 32)
                        {
                            result = "Suburb cannot be more than 32 characters!";
                        }
                        break;
                    case "State":
                        if (string.IsNullOrEmpty(State))
                        {
                            result = "Field cannot be empty";
                        }
                        else if (this.State.Length > 3)
                        {
                            result = "State cannot be more than 3 characters!";
                        }
                        break;
                    case "Postcode":
                        if (string.IsNullOrEmpty(Postcode))
                        {
                            result = "Postcode cannot be empty";
                        }
                        else if (this.Postcode.Length > 4)
                        {
                            result = "Postcode cannot be more than 4 characters!";
                        }
                        break;
                    case "LicenceNumber":
                        if (string.IsNullOrEmpty(LicenceNumber))
                        {
                            result = "Licence Number cannot be empty";
                        }
                        else if (LicenceNumber.Length > 20)
                        {
                            result = "Licence Number cannot be more than 20 characters!";

                        }
                        break;
                    case "VehicleRegistration":
                        if (string.IsNullOrEmpty(LicenceNumber))
                        {
                            result = "Vehicle Registration cannot be empty";
                        }
                        else if (LicenceNumber.Length > 32)
                        {
                            result = "VehicleRegistration cannot be more than 20 characters!";

                        }
                        break;
                    case "PerformanceRating":
                        if (string.IsNullOrEmpty(this.PerformanceRating.ToString()))
                        {
                            result = "Performance Rating cannot be empty";
                        }
                        else if (PerformanceRating > 10)
                        {
                            result = "Licence Number cannot be more than 10!";

                        }
                        else if (PerformanceRating < 0)
                        {
                            result = "Licence Number cannot be less than 0!";

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
        public int ContractorId
        {
            get { return _contractorId; }
            set { _contractorId = value; }
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
        public string Street
        {
            get { return _street; }
            set
            {
                _street = value;
                OnPropertyChanged("Street");
            }
        }
        public string Suburb
        {
            get { return _suburb; }
            set
            {
                _suburb = value;
                OnPropertyChanged("Suburb");
            }
        }
        public string Postcode
        {
            get { return _postcode; }
            set
            {
                _postcode = value;
                OnPropertyChanged("Postcode");
            }
        }
        public string State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnPropertyChanged("State");
            }
        }
        public string LicenceNumber
        {
            get { return _licenceNumber; }
            set
            {
                _licenceNumber = value;
                OnPropertyChanged("LicenceNumber");
            }
        }
        public string VehicleRegistration
        {
            get { return _vehicleRegistration; }
            set
            {
                _vehicleRegistration = value;
                OnPropertyChanged("VehicleRegistration");
            }
        }
        public int PerformanceRating
        {
            get { return _performanceRating; }
            set
            {
                _performanceRating = value;
                OnPropertyChanged("PerformanceRating");
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
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public Contractor()
        {
            _db = new SQLHelper();
        }
        public Contractor(DataRow dr)
        {
            _db = new SQLHelper();
            this.ContractorId = Convert.ToInt32(dr["contractor_id"].ToString());
            this.FirstName = dr["first_name"].ToString();
            this.LastName = dr["last_name"].ToString();
            this.Email = dr["email"].ToString();
            this.Phone = dr["phone"].ToString();
            this.Street = dr["street"].ToString();
            this.Suburb = dr["suburb"].ToString();
            this.Postcode = dr["postcode"].ToString();
            this.State = dr["state"].ToString();
            this.LicenceNumber = dr["licence_number"].ToString();
            this.PerformanceRating = Convert.ToInt32(dr["performance_rating"]);
            this.VehicleRegistration = dr["vehicle_registration"].ToString();
            this.Active = Convert.ToBoolean(dr["active"]);

            if (dr.Table.Columns.Contains("password"))
            {
                this.Password = dr["password"].ToString();
            }
        }

        public Contractor(int contractorId)
        {
            _db = new SQLHelper();
            string sql = "SELECT contractor_id, first_name, last_name, email, phone, password, street, suburb, postcode, state," +
                         " suburb, postcode, state, licence_number, performance_rating, vehicle_registration, active" +
                         " FROM contractor" +
                         " WHERE contractor_id = @ContractorId";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@ContractorId", DbType.Int32);
            objParams[0].Value = contractorId;

            DataRow dr = _db.ExecuteSQL(sql, objParams).Rows[0];

            this.ContractorId = Convert.ToInt32(dr["contractor_id"].ToString());
            this.FirstName = dr["first_name"].ToString();
            this.LastName = dr["last_name"].ToString();
            this.Email = dr["email"].ToString();
            this.Phone = dr["phone"].ToString();
            this.Password = dr["password"].ToString();
            this.Street = dr["street"].ToString();
            this.Suburb = dr["suburb"].ToString();
            this.Postcode = dr["postcode"].ToString();
            this.State = dr["state"].ToString();
            this.LicenceNumber = dr["licence_number"].ToString();
            this.PerformanceRating = Convert.ToInt32(dr["performance_rating"].ToString());
            this.VehicleRegistration = dr["vehicle_registration"].ToString();
            this.Active = Convert.ToBoolean(dr["active"]);
        }
        // TODO: Generate a random password
        private void GeneratePassword()
        {
            this.Password = "Password";
        }


        public int InsertContractor(ObservableCollection<Skill> skills)
        {
            if (this.ErrorCollection.Count > 0)
            {
                return -1;
            } 
            this.GeneratePassword();
            DataTable skillsTable = new DataTable();
            skillsTable.Columns.Add("skill_name");

            foreach (Skill skill in skills)
            {
                skillsTable.Rows.Add(skill.SkillName);
            }

            _db = new SQLHelper();
            string sql = "usp_AddContractor";
            SqlParameter[] objParams = new SqlParameter[13];
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
            objParams[5] = new SqlParameter("@Street", DbType.String)
            {
                Value = this.Street
            };
            objParams[6] = new SqlParameter("@Suburb", DbType.String)
            {
                Value = this.Suburb
            };
            objParams[7] = new SqlParameter("@Postcode", DbType.String)
            {
                Value = this.Postcode
            };
            objParams[8] = new SqlParameter("@State", DbType.String)
            {
                Value = this.State
            };
            objParams[9] = new SqlParameter("@LicenceNumber", DbType.String)
            {
                Value = this.LicenceNumber
            };
            objParams[10] = new SqlParameter("@VehicleRegistration", DbType.String)
            {
                Value = this.VehicleRegistration
            };
            objParams[11] = new SqlParameter("@PerformanceRating", DbType.String)
            {
                Value = this.PerformanceRating
            };
            objParams[12] = new SqlParameter("@Skills", SqlDbType.Structured)
            {
                Value = skillsTable
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams, true);
            return rowsAffected;
        }

        public int DeleteContractor()
        {
            if (this.ErrorCollection.Count > 0)
            {
                return -1;
            }
            _db = new SQLHelper();
            string sql = "usp_DeleteContractor";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@ContractorId", DbType.Int32)
            {
                Value = this.ContractorId
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams, true);
            return rowsAffected;
        }

        public int UpdateContractor()
        {
            if (this.ErrorCollection.Count > 0)
            {
                return -1;
            }
            _db = new SQLHelper();
            string sql = "usp_UpdateContractor";
            SqlParameter[] objParams = new SqlParameter[13];
            objParams[0] = new SqlParameter("@ContractorId", DbType.Int32)
            {
                Value = this.ContractorId
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
            objParams[6] = new SqlParameter("@Street", DbType.String)
            {
                Value = this.Street
            };
            objParams[7] = new SqlParameter("@Suburb", DbType.String)
            {
                Value = this.Suburb
            };
            objParams[8] = new SqlParameter("@Postcode", DbType.String)
            {
                Value = this.Postcode
            };
            objParams[9] = new SqlParameter("@State", DbType.String)
            {
                Value = this.State
            };
            objParams[10] = new SqlParameter("@LicenceNumber", DbType.String)
            {
                Value = this.LicenceNumber
            };
            objParams[11] = new SqlParameter("@VehicleRegistration", DbType.String)
            {
                Value = this.VehicleRegistration
            };
            objParams[12] = new SqlParameter("@PerformanceRating", DbType.String)
            {
                Value = this.PerformanceRating
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams, true);
            return rowsAffected;
        }

        public int AddSkill(string skillName)
        {
            _db = new SQLHelper();
            string sql = "usp_AddSkill";
            SqlParameter[] objParams = new SqlParameter[2];
            objParams[0] = new SqlParameter("@SkillName", DbType.String)
            {
                Value = skillName
            };
            objParams[1] = new SqlParameter("@ContractorId", DbType.Int32)
            {
                Value = this.ContractorId
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams, true);
            return rowsAffected;
        }
    }
}
