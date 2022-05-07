using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitServicesDesktopApp.Models
{
    public class Job : INotifyPropertyChanged
    {
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

        }
        public Job(DataRow dr)
        {
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
            this.Client = new Client(Location.ClientId);
        }
    }
}
