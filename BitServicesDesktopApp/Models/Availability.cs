using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BitServicesDesktopApp.DAL;

namespace BitServicesDesktopApp.Models
{
    public class Availability : INotifyPropertyChanged, IDataErrorInfo
    {
        private DateTime _availabilityDate;
        private int _contractorId;
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
                    case "AvailabilityDate":
                        if (AvailabilityDate.Date < DateTime.Now.Date)
                        {
                            result = "Date cannot be in the past";
                        }
                        else if (AvailabilityDate.Date == DateTime.Now.Date)
                        {
                            result = "Date cannot be today";
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

        public int InsertAvailability()
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
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams, true);
            return rowsAffected;
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
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams, true);
            return rowsAffected;
        }
    }
}
