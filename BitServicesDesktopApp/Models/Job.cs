using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BitServicesDesktopApp.Helpers;

namespace BitServicesDesktopApp.Models
{
    public class Job : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Private Properties
        private int _jobId;
        private Contractor _assignedContractor;
        private Client _client;
        private ClientLocation _location;
        private string _requiredSkill;
        private string _jobStatus;
        private string _title;
        private string _description;
        private int _kilometers;
        private DateTime _deadlineDate;
        private DateTime _completionDate;
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
                    case "DeadlineDate":
                        if (DeadlineDate.Date < DateTime.Now.Date)
                        {
                            result = "Deadline date cannot be in the past";
                        }
                        break;
                    case "Title":
                        if (string.IsNullOrEmpty(this.Title))
                        {
                            result = "Job title cannot be left empty";
                        }
                        else if (this.Title.Length > 100)
                        {
                            result = "Job title cannot be more than 100 characters";
                        }
                        break;
                    case "Description":
                        if (string.IsNullOrEmpty(this.Description))
                        {
                            result = "Job description cannot be left empty";
                        }
                        else if (this.Title.Length > 1000)
                        {
                            result = "Job description cannot be more than 1000 characters";
                        }
                        break;
                    case "Client":
                        if (this.Client == null)
                        {
                            result = "Client cannot be left empty";
                        }
                        break;
                    case "ClientLocation":
                        if (this.Location == null)
                        {
                            result = "Client cannot be left empty";
                        }
                        break;
                    case "AssignedContractor":
                        if (this.AssignedContractor == null)
                        {
                            result = "Assigned contractor cannot be left empty";
                        }
                        break;
                    case "RequiredSkill":
                        if (string.IsNullOrEmpty(this.RequiredSkill))
                        {
                            result = "Required skill cannot be left empty";
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

        #region Public properties
        public int JobId
        {
            get { return _jobId; }
            set { _jobId = value; }
        }
        public string RequiredSkill
        {
            get { return _requiredSkill; }
            set
            {
                _requiredSkill = value;
                OnPropertyChanged("RequiredSkill");
            }
        }
        public string JobStatus
        {
            get { return _jobStatus; }
            set
            {
                _jobStatus = value;
                OnPropertyChanged("JobStatus");
            }
        }
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
        public int Kilometers
        {
            get { return _kilometers; }
            set
            {
                _kilometers = value;
                OnPropertyChanged("Kilometers");
            }
        }
        public DateTime DeadlineDate
        {
            get { return _deadlineDate; }
            set
            {
                _deadlineDate = value;
                OnPropertyChanged("DeadlineDate");
            }
        }
        public DateTime CompletionDate
        {
            get { return _completionDate; }
            set
            {
                _completionDate = value;
                OnPropertyChanged("CompletionDate");
            }
        }
        public Client Client
        {
            get { return _client; }
            set
            {
                _client = value;
                OnPropertyChanged("Client");
            }
        }
        public ClientLocation Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged("Location");
            }
        }
        public Contractor AssignedContractor
        {
            get { return _assignedContractor; }
            set
            {
                _assignedContractor = value;
                OnPropertyChanged("AssignedContractor");
            }
        }
        public string FormattedCompletionDate
        {
            get
            {
                if (this.CompletionDate != new DateTime())
                {
                    return this.CompletionDate.ToString("dddd, dd MMMM yyyy");
                }
                else
                {
                    return "";
                }
            }
        }
        #endregion

        #region Constructors
        public Job()
        {
            _db = new SQLHelper();
        }
        public Job(DataRow dr)
        {
            _db = new SQLHelper();
            this.JobId = Convert.ToInt32(dr["job_id"].ToString());
            this.RequiredSkill = dr["Required Skill"].ToString();
            this.JobStatus = dr["Status"].ToString();
            this.Title = dr["Title"].ToString();
            this.Description = dr["Description"].ToString();
            this.Kilometers = Convert.ToInt32(dr["kilometers"].ToString());
            this.DeadlineDate = Convert.ToDateTime(dr["Deadline Date"].ToString());

            this.Location = new ClientLocation(Convert.ToInt32(dr["location_id"]));
            this.Client = new Client(this.Location.ClientId);
            if (dr.Table.Columns.Contains("contractor_id") && dr["contractor_id"] != DBNull.Value)
            {
                this.AssignedContractor = new Contractor(Convert.ToInt32(dr["contractor_id"]));
            }

            if (dr.Table.Columns.Contains("Completion Date") && dr["Completion Date"] != DBNull.Value)
            {
                this.CompletionDate = Convert.ToDateTime(dr["Completion Date"]);
            }
            else
            {
                DateTime dt = new DateTime();
                this.CompletionDate = dt;
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
            string sql = "usp_InsertJob";
            SqlParameter[] objParams = new SqlParameter[7];
            objParams[0] = new SqlParameter("@LocationId", DbType.Int32)
            {
                Value = this.Location.LocationId
            };
            objParams[1] = new SqlParameter("@RequiredSkill", DbType.String)
            {
                Value = this.RequiredSkill
            };
            objParams[2] = new SqlParameter("@JobStatus", DbType.String)
            {
                Value = this.JobStatus
            };
            objParams[3] = new SqlParameter("@Kilometers", DbType.Int32)
            {
                Value = this.Kilometers
            };
            objParams[4] = new SqlParameter("@Title", DbType.String)
            {
                Value = this.Title
            };
            objParams[5] = new SqlParameter("@Description", DbType.String)
            {
                Value = this.Description
            };
            objParams[6] = new SqlParameter("@DeadlineDate", DbType.Date)
            {
                Value = this.DeadlineDate
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams, true);
            return rowsAffected;
        }
        public int Create(int staffId)
        {
            if (this.ErrorCollection.Count > 0)
            {
                return -1;
            }
            string sql = "usp_InsertJob";
            SqlParameter[] objParams = new SqlParameter[9];
            objParams[0] = new SqlParameter("@LocationId", DbType.Int32)
            {
                Value = this.Location.LocationId
            };
            objParams[1] = new SqlParameter("@RequiredSkill", DbType.String)
            {
                Value = this.RequiredSkill
            };
            objParams[2] = new SqlParameter("@JobStatus", DbType.String)
            {
                Value = this.JobStatus
            };
            objParams[3] = new SqlParameter("@Kilometers", DbType.Int32)
            {
                Value = this.Kilometers
            };
            objParams[4] = new SqlParameter("@Title", DbType.String)
            {
                Value = this.Title
            };
            objParams[5] = new SqlParameter("@Description", DbType.String)
            {
                Value = this.Description
            };
            objParams[6] = new SqlParameter("@DeadlineDate", DbType.Date)
            {
                Value = this.DeadlineDate
            };
            objParams[7] = new SqlParameter("@StaffId", DbType.Int32)
            {
                Value = staffId
            };
            objParams[8] = new SqlParameter("@ContractorId", DbType.Int32)
            {
                Value = this.AssignedContractor.ContractorId
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams, true);
            return rowsAffected;
        }
        public int Delete()
        {
            string sql = "UPDATE job" +
                         " SET job_status = 'Canceled'" +
                         " WHERE job_id = @JobId";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@JobId", DbType.Int32)
            {
                Value = this.JobId
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
            string sql = "UPDATE job" +
                         " SET location_id = @LocationId, required_skill = @RequiredSkill," +
                         " job_status = @JobStatus, kilometers = @Kilometers, title = @Title, description = @Description, deadline_date = @DeadlineDate" +
                         " WHERE job_id = @JobId";
            SqlParameter[] objParams = new SqlParameter[8];
            objParams[0] = new SqlParameter("@LocationId", DbType.Int32)
            {
                Value = this.Location.LocationId
            };
            objParams[1] = new SqlParameter("@RequiredSkill", DbType.String)
            {
                Value = this.RequiredSkill
            };
            objParams[2] = new SqlParameter("@JobStatus", DbType.String)
            {
                Value = this.JobStatus
            };
            objParams[3] = new SqlParameter("@Kilometers", DbType.Int32)
            {
                Value = this.Kilometers
            };
            objParams[4] = new SqlParameter("@Title", DbType.String)
            {
                Value = this.Title
            };
            objParams[5] = new SqlParameter("@Description", DbType.String)
            {
                Value = this.Description
            };
            objParams[6] = new SqlParameter("@DeadlineDate", DbType.Date)
            {
                Value = this.DeadlineDate
            };
            objParams[7] = new SqlParameter("@JobId", DbType.Int32)
            {
                Value = this.JobId
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams);
            return rowsAffected;
        }
        public int AssignContractor(int contractorId, int staffId)
        {
            string sql = "usp_AssignContractor";
            SqlParameter[] objParams = new SqlParameter[3];
            objParams[0] = new SqlParameter("@JobId", DbType.Int32)
            {
                Value = this.JobId
            };
            objParams[1] = new SqlParameter("@StaffId", DbType.Int32)
            {
                Value = staffId
            };
            objParams[2] = new SqlParameter("@ContractorId", DbType.Int32)
            {
                Value = contractorId
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams, true);
            return rowsAffected;
        }
        #endregion
    }
}
