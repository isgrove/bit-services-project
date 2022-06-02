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
        private int _jobId;
        // Aggregation relationship
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
            this.Client = new Client(Convert.ToInt32(dr["location_id"]));

            var contractorId = dr["contractor_id"];
            var completionDate = dr["Completion Date"];

            if (contractorId != DBNull.Value)
            {
                this.AssignedContractor = new Contractor(Convert.ToInt32(contractorId));
            }

            if (completionDate != DBNull.Value)
            {
                //MessageBox.Show("Setting completion date: " + completionDate);
                this.CompletionDate = Convert.ToDateTime(completionDate);
            }
            else
            {
                DateTime dt = new DateTime();
                this.CompletionDate = dt;
            }
        }
        public int InsertJob()
        {
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
        public int InsertJob(int staffId)
        {
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
        public int DeleteJob()
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
        // TODO: Let users update the assigned contractor of the job
        public int UpdateJob()
        {
            string sql = "UPDATE job" +
                         " SET location_id = @LocationId, required_skill = @RequiredSkill," +
                         " job_status = @JobStatus, kilometers = @Kilometers, title = @Title, description = @Description, deadline_date = @DeadlineDate" +
                         " WHERE job_id = @JobId";
            SqlParameter[] objParams = new SqlParameter[9];
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
            objParams[5] = new SqlParameter("@Title", DbType.String)
            {
                Value = this.Title
            };
            objParams[6] = new SqlParameter("@Description", DbType.String)
            {
                Value = this.Description
            };
            objParams[7] = new SqlParameter("@DeadlineDate", DbType.Date)
            {
                Value = this.DeadlineDate
            };
            objParams[8] = new SqlParameter("@JobId", DbType.Int32)
            {
                Value = this.JobId
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams);
            return rowsAffected;
        }
    }
}
