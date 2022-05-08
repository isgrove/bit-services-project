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
using BitServicesDesktopApp.DAL;

namespace BitServicesDesktopApp.Models
{
    public class Job : INotifyPropertyChanged
    {
        //TODO: Add required time (days) and job title
        private int _jobId;
        private int _locationId;
        private int _clientId;
        private Contractor _assignedContractor;
        private Client _client;
        private ClientLocation _location;
        private int _assignedContractorId;
        private string _requiredSkill;
        private string _jobStatus;
        private string _description;
        private int _kilometers;
        private DateTime _deadlineDate;
        private SQLHelper _db;
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        public int JobId
        {
            get { return _jobId; }
            set { _jobId = value; }
        }
        public int LocationId
        {
            get { return _locationId; }
            set
            {
                _locationId = value;
                OnPropertyChanged("AssignedContractorId");
            }
        }
        public int ClientId
        {
            get { return _clientId; }
            set
            {
                _clientId = value;
                OnPropertyChanged("ClientId");
            }
        }
        public int AssignedContractorId
        {
            get { return _assignedContractorId; }
            set
            {
                _assignedContractorId = value;
                OnPropertyChanged("AssignedContractorId");
            }
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
        public Job()
        {
            _db = new SQLHelper();
        }
        public Job(DataRow dr)
        {
            _db = new SQLHelper();
            this.JobId = Convert.ToInt32(dr["job_id"].ToString());
            this.LocationId = Convert.ToInt32(dr["location_id"].ToString());
            this.ClientId = Convert.ToInt32(dr["client_id"].ToString());
            this.AssignedContractorId = Convert.ToInt32(dr["assigned_contractor_id"].ToString());
            this.RequiredSkill = dr["required_skill"].ToString();
            this.JobStatus = dr["job_status"].ToString();
            this.Description = dr["description"].ToString();
            this.Kilometers = Convert.ToInt32(dr["kilometers"].ToString());
            this.DeadlineDate = Convert.ToDateTime(dr["deadline_date"].ToString());
            this.Location = new ClientLocation(this.LocationId);
            this.AssignedContractor = new Contractor(this.AssignedContractorId);
            this.Client = new Client(this.Location.ClientId);
        }
        public int InsertJob()
        {
            string sql = "INSERT INTO job (location_id, assigned_contractor_id, required_skill_name, job_status, kilometers, description, deadline_date)" +
                         " VALUES(@LocationId, @AssignedContractorId, @RequiredSkill, @JobStatus, @Kilometers, @Description, @DeadlineDate)";
            SqlParameter[] objParams = new SqlParameter[7];
            objParams[0] = new SqlParameter("@LocationId", DbType.Int32)
            {
                Value = this.Location.LocationId
            };
            objParams[1] = new SqlParameter("@AssignedContractorId", DbType.Int32)
            {
                Value = this.AssignedContractor.ContractorId
            };
            objParams[2] = new SqlParameter("@RequiredSkill", DbType.String)
            {
                Value = this.RequiredSkill
            };
            objParams[3] = new SqlParameter("@JobStatus", DbType.String)
            {
                Value = this.JobStatus
            };
            objParams[4] = new SqlParameter("@Kilometers", DbType.Int32)
            {
                Value = this.Kilometers
            };
            objParams[5] = new SqlParameter("@Description", DbType.String)
            {
                Value = this.Description
            };
            objParams[6] = new SqlParameter("@DeadlineDate", DbType.Date)
            {
                Value = this.DeadlineDate
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams);
            return rowsAffected;
        }
        public int DeleteJob()
        {
            string sql = "DELETE FROM job" +
                         " WHERE job_id = @JobId";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@JobId", DbType.Int32)
            {
                Value = this.JobId
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams);
            return rowsAffected;
        }
        public int UpdateJob()
        {
            string sql = "UPDATE job" +
                         " SET location_id = @LocationId, assigned_contractor_id = @AssignedContractorId, required_skill_name = @RequiredSkill," +
                         " job_status = @JobStatus, kilometers = @Kilometers, description = @Description, deadline_date = @DeadlineDate" +
                         " WHERE job_id = @JobId";
            SqlParameter[] objParams = new SqlParameter[8];
            objParams[0] = new SqlParameter("@LocationId", DbType.Int32)
            {
                Value = this.Location.LocationId
            };
            objParams[1] = new SqlParameter("@AssignedContractorId", DbType.Int32)
            {
                Value = this.AssignedContractor.ContractorId
            };
            objParams[2] = new SqlParameter("@RequiredSkill", DbType.String)
            {
                Value = this.RequiredSkill
            };
            objParams[3] = new SqlParameter("@JobStatus", DbType.String)
            {
                Value = this.JobStatus
            };
            objParams[4] = new SqlParameter("@Kilometers", DbType.Int32)
            {
                Value = this.Kilometers
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
    }
}
