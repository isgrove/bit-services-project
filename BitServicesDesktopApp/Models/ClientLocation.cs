using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using BitServicesDesktopApp.Helpers;

namespace BitServicesDesktopApp.Models
{
    public class ClientLocation : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Private Properties
        private int _locationId;
        private int _clientId;
        private string _clientName;
        private string _email;
        private string _phone;
        private string _street;
        private string _suburb;
        private string _postcode;
        private string _state;
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
        public int LocationId
        {
            get { return _locationId; }
            set { _locationId = value; }
        }
        public int ClientId
        {
            get { return _clientId; }
            set { _clientId = value; }
        }

        public string ClientName
        {
            get { return _clientName; }
            set
            {
                _clientName = value;
                OnPropertyChanged("ClientName");
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
        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                OnPropertyChanged("Active");
            }
        }
        public string ClientLocationName
        {
            get { return $"{ClientName} {Suburb}"; }
        }
        #endregion

        #region Constructor
        public ClientLocation()
        {
            _db = new SQLHelper();
        }
        public ClientLocation(DataRow dr)
        {
            _db = new SQLHelper();
            this.LocationId = Convert.ToInt32(dr["location_id"].ToString());
            this.ClientId = Convert.ToInt32(dr["client_id"].ToString());
            this.ClientName = dr["client_name"].ToString();
            this.Email = dr["email"].ToString();
            this.Phone = dr["phone"].ToString();
            this.Street = dr["street"].ToString();
            this.Suburb = dr["suburb"].ToString();
            this.Postcode = dr["postcode"].ToString();
            this.State = dr["state"].ToString();
            this.Active = Convert.ToBoolean(dr["active"]);
        }
        public ClientLocation(int locationId)
        {
            _db = new SQLHelper();
            string sql = "usp_GetClientLocations";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@LocationId", DbType.Int32);
            objParams[0].Value = locationId;
            DataTable dt = _db.ExecuteSQL(sql, objParams);

            if (dt.Rows.Count >= 1)
            {
                DataRow dr = dt.Rows[0];
                this.LocationId = Convert.ToInt32(dr["location_id"].ToString());
                this.ClientId = Convert.ToInt32(dr["client_id"].ToString());
                this.ClientName = dr["client_name"].ToString();
                this.Email = dr["email"].ToString();
                this.Phone = dr["phone"].ToString();
                this.Street = dr["street"].ToString();
                this.Suburb = dr["suburb"].ToString();
                this.Postcode = dr["postcode"].ToString();
                this.State = dr["state"].ToString();
                this.Active = Convert.ToBoolean(dr["active"]);
            }
        }

        #endregion

        #region Public Methods
        public int Create()
        {
            if (this.ErrorCollection.Count > 0)
            {
                return -1;
            }
            string sql = "INSERT INTO client_location (client_id, email, phone, street, suburb, postcode, state, active)" +
                         " VALUES(@ClientId, @Email, @Phone, @Street, @Suburb, @Postcode, @State, 1)";
            SqlParameter[] objParams = new SqlParameter[7];
            objParams[0] = new SqlParameter("@ClientId", DbType.Int32)
            {
                Value = this.ClientId
            };
            objParams[1] = new SqlParameter("@Email", DbType.String)
            {
                Value = this.Email
            };
            objParams[2] = new SqlParameter("@Phone", DbType.String)
            {
                Value = this.Phone
            };
            objParams[3] = new SqlParameter("@Street", DbType.String)
            {
                Value = this.Street
            };
            objParams[4] = new SqlParameter("@Suburb", DbType.String)
            {
                Value = this.Street
            };
            objParams[5] = new SqlParameter("@Postcode", DbType.String)
            {
                Value = this.Postcode
            };
            objParams[6] = new SqlParameter("@State", DbType.String)
            {
                Value = this.State
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams);
            return rowsAffected;
        }
        public int Delete()
        {
            string sql = "UPDATE client_location" +
                         " SET active = 0" +
                         " WHERE location_id = @LocationId";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@LocationId", DbType.String)
            {
                Value = this.LocationId
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams);
            return rowsAffected;
        }

        public int Update()
        {
            if (this.ErrorCollection.Count > 0)
            {
                return -1;
            }
            string sql = "UPDATE client_location" +
                         " SET client_id = @ClientId, email = @Email, phone = @Phone, street = @Street, suburb = @Suburb, postcode = @Postcode, state = @State" +
                         " WHERE location_id = @LocationId";
            SqlParameter[] objParams = new SqlParameter[8];
            objParams[0] = new SqlParameter("@ClientId", DbType.Int32)
            {
                Value = this.ClientId
            };
            objParams[1] = new SqlParameter("@Email", DbType.String)
            {
                Value = this.Email
            };
            objParams[2] = new SqlParameter("@Phone", DbType.String)
            {
                Value = this.Phone
            };
            objParams[3] = new SqlParameter("@Street", DbType.String)
            {
                Value = this.Street
            };
            objParams[4] = new SqlParameter("@Suburb", DbType.String)
            {
                Value = this.Suburb
            };
            objParams[5] = new SqlParameter("@Postcode", DbType.String)
            {
                Value = this.Postcode
            };
            objParams[6] = new SqlParameter("@State", DbType.String)
            {
                Value = this.State
            };
            objParams[7] = new SqlParameter("@LocationId", DbType.String)
            {
                Value = this.LocationId
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams);
            return rowsAffected;
        }
        #endregion
    }
}
