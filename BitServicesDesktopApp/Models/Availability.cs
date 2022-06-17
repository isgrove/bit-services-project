using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BitServicesDesktopApp.Helpers;

namespace BitServicesDesktopApp.Models
{
    public class Availability : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Private Properties
        private DateTime _availabilityDate;
        private int _contractorId;
        private SQLHelper _db;
        #endregion Private Properties

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

                    case "AvailabilityDate":
                        if (AvailabilityDate.Date < DateTime.Now.Date)
                        {
                            result = "Date for a new availability cannot be in the past";
                        }
                        else if (AvailabilityDate.Date == DateTime.Now.Date)
                        {
                            result = "Date for a new availability cannot be today";
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
        #endregion Validation        

        #region Public Properties
        public DateTime AvailabilityDate
        {
            get { return _availabilityDate; }
            set
            {
                _availabilityDate = value;
                OnPropertyChanged("AvailabilityDate");
            }
        }
        public int ContractorId
        {
            get { return _contractorId; }
            set
            {
                _contractorId = value;
                OnPropertyChanged("ContractorId");
            }
        }
        #endregion Public Properties

        #region Constructors
        public Availability()
        {
            this._db = new SQLHelper();
        }

        public Availability(DataRow dr)
        {
            this._db = new SQLHelper();
            this.ContractorId = Convert.ToInt32(dr["contractor_id"].ToString());
            this.AvailabilityDate = Convert.ToDateTime(dr["availability_date"].ToString());
        }
        #endregion Constructors

        #region Public Methods
        public int AddAvailability()
        {
            if (this.ErrorCollection.Count > 0)
            {
                return -1;
            }
            _db = new SQLHelper();
            string sql = "usp_AddContractorAvailability";
            SqlParameter[] objParams = new SqlParameter[2];
            objParams[0] = new SqlParameter("@AvailabilityDate", DbType.Date)
            {
                Value = this.AvailabilityDate

            };
            objParams[1] = new SqlParameter("@ContractorId", DbType.Int32)
            {
                Value = this.ContractorId

            };
            try
            {
                int rowsAffected = _db.ExecuteNonQuery(sql, objParams, true);
                return rowsAffected;
            }
            catch (Exception ex)
            {
                StringBuilder errorMessage = new StringBuilder();
                errorMessage.Append("Add Availability caused previous error:\n" +
                                    "Exception: " + ex.Message + "\n" +
                                    "SqlParameters:\n");
                foreach (SqlParameter sqlParameter in objParams)
                {
                    errorMessage.Append($"- {sqlParameter.ParameterName} {sqlParameter.DbType}: {sqlParameter.Value}\n");
                }
                new LogHelper().Log(errorMessage.ToString(), LogType.Error);
                return -1;
            }
        }

        public int DeleteAvailability()
        {
            if (this.ErrorCollection.Count > 0)
            {
                return -1;
            }
            _db = new SQLHelper();
            string sql = "usp_DeleteContractorAvailability";
            SqlParameter[] objParams = new SqlParameter[2];
            objParams[0] = new SqlParameter("@AvailabilityDate", DbType.Date)
            {
                Value = this.AvailabilityDate
            };
            objParams[1] = new SqlParameter("@ContractorId", DbType.Int32)
            {
                Value = this.ContractorId
            };
            try
            {
                int rowsAffected = _db.ExecuteNonQuery(sql, objParams, true);
                return rowsAffected;
            }
            catch (Exception ex)
            {
                StringBuilder errorMessage = new StringBuilder();
                errorMessage.Append("Delete Availability caused previous error:\n" +
                                    "Exception: " + ex.Message + "\n" +
                                    "SqlParameters:\n");
                foreach (SqlParameter sqlParameter in objParams)
                {
                    errorMessage.Append($"- {sqlParameter.ParameterName} {sqlParameter.DbType}: {sqlParameter.Value}\n");
                }
                new LogHelper().Log(errorMessage.ToString(), LogType.Error);
                return -1;
            }
        }
        #endregion Public Methods
    }
}
