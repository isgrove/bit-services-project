using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using BitServicesDesktopApp.Commands;
using BitServicesDesktopApp.Models;
using BitServicesDesktopApp.Views;

namespace BitServicesDesktopApp.ViewModels
{
    public class JobManagementViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Job> _jobs;
        private ObservableCollection<Job> _inProgressJobs;
        private ObservableCollection<Job> _verifiedJobs;
        private ObservableCollection<Job> _completedJobs;
        private ObservableCollection<Job> _pendingJobs;
        private ObservableCollection<Job> _canceledJobs;
        private ObservableCollection<Job> _rejectedJobs;
        private ObservableCollection<Contractor> _contractors;
        private ObservableCollection<JobStatus> _jobStatuses;
        private Job _selectedJob;
        private RelayCommand _deleteCommand;
        private RelayCommand _saveCommand;
        private bool _isRejectedTabSelected;
        public event PropertyChangedEventHandler PropertyChanged;
        
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        #region Commands
        public RelayCommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(this.DeleteMethod, true);
                }
                return _deleteCommand;
            }
            set { _deleteCommand = value; }
        }
        public RelayCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(this.SaveMethod, true);
                }
                return _saveCommand;
            }
            set { _saveCommand = value; }
        }
        #endregion Commands
        #region Command Methods
        public void DeleteMethod()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure that you want to delete this job for {SelectedJob.Client.Name} {SelectedJob.Location.Suburb}?", $"Delete Job", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string message;
                int rowsAffected = SelectedJob.DeleteJob();
                if (rowsAffected >= 1)
                {
                    message = $"You have successfully deleted this job for {SelectedJob.Client.Name} {SelectedJob.Location.Suburb}!";
                }
                else
                {
                    message = $"There was an issue when deleting this job for {SelectedJob.Client.Name} {SelectedJob.Location.Suburb}, please try again!";
                }
                UpdateJobs();
                MessageBox.Show(message, $"Delete Job");
            }

        }
        public void SaveMethod()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure that you want to update this job for {SelectedJob.Client.Name} {SelectedJob.Location.Suburb}?", $"Update Job", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string message;
                int rowsAffected;
                if (IsRejectedTabSelected)
                {
                    int contractorId = SelectedJob.AssignedContractor.ContractorId;
                    int staffId = MainWindow.LoggedInStaff.StaffId;
                    rowsAffected = SelectedJob.AssignContractor(contractorId, staffId);
                }
                else
                {
                    rowsAffected = SelectedJob.UpdateJob();
                }
                if (rowsAffected >= 1)
                {
                    message = $"You have successfully saved this job for {SelectedJob.Client.Name} {SelectedJob.Location.Suburb}!";
                }
                else
                {
                    message = $"There was an issue when saving this job for {SelectedJob.Client.Name} {SelectedJob.Location.Suburb}, please try again!";
                }
                MessageBox.Show(message, $"Update Job");
            }

        }
        #endregion Command Methods
        #region Public Properties
        public ObservableCollection<Job> Jobs
        {
            get { return _jobs; }
            set
            {
                _jobs = value;
                OnPropertyChanged("Jobs");
            }
        }
        public ObservableCollection<Job> InProgressJobs
        {
            get { return _inProgressJobs; }
            set
            {
                _inProgressJobs = value;
                OnPropertyChanged("InProgressJobs");
            }
        }
        public ObservableCollection<Job> CompletedJobs
        {
            get { return _completedJobs; }
            set
            {
                _completedJobs = value;
                OnPropertyChanged("CompletedJobs");
            }
        }
        public ObservableCollection<Job> PendingJobs
        {
            get { return _pendingJobs; }
            set
            {
                _pendingJobs = value;
                OnPropertyChanged("PendingJobs");
            }
        }
        public ObservableCollection<Job> CanceledJobs
        {
            get { return _canceledJobs; }
            set
            {
                _canceledJobs = value;
                OnPropertyChanged("CanceledJobs");
            }
        }

        public ObservableCollection<Job> VerifiedJobs
        {
            get { return _verifiedJobs; }
            set
            {
                _verifiedJobs = value;
                OnPropertyChanged("VerifiedJobs");
            }
        }
        public ObservableCollection<Job> RejectedJobs
        {
            get { return _rejectedJobs; }
            set
            {
                _rejectedJobs = value;
                OnPropertyChanged("RejectedJobs");
            }
        }
        public ObservableCollection<Contractor> Contractors
        {
            get { return _contractors; }
            set
            {
                _contractors = value;
                OnPropertyChanged("Contractors");
            }
        }
        public ObservableCollection<JobStatus> JobStatuses
        {
            get { return _jobStatuses; }
            set
            {
                _jobStatuses = value;
                OnPropertyChanged("JobStatuses");
            }
        }
        public Job SelectedJob
        {
            get { return _selectedJob; }
            set
            {
                _selectedJob = value;
                OnPropertyChanged("SelectedJob");
                if (IsRejectedTabSelected)
                {
                    Contractors allContractors = new Contractors(SelectedJob.JobId);
                    Contractors = new ObservableCollection<Contractor>(allContractors);
                }
            }
        }
        public bool IsRejectedTabSelected
        {
            get
            {
                return _isRejectedTabSelected;
            }
            set
            {
                _isRejectedTabSelected = value;
                OnPropertyChanged("IsRejectedTabSelected");
            }
        }
        #endregion Public Properties
        public void UpdateJobs()
        {
            Jobs allJobs = new Jobs();
            this.Jobs = new ObservableCollection<Job>(allJobs);

            Jobs allInProgressJobs = new Jobs("In Progress");
            this.InProgressJobs = new ObservableCollection<Job>(allInProgressJobs);

            Jobs allVerifiedJobs = new Jobs("Verified");
            this.VerifiedJobs = new ObservableCollection<Job>(allVerifiedJobs);

            Jobs allCompletedJobs = new Jobs("Completed");
            this.CompletedJobs = new ObservableCollection<Job>(allCompletedJobs);

            Jobs allPendingJobs = new Jobs("Pending");
            this.PendingJobs = new ObservableCollection<Job>(allPendingJobs);

            Jobs allCanceledJobs = new Jobs("Canceled");
            this.CanceledJobs = new ObservableCollection<Job>(allCanceledJobs);

            Jobs allRejectedJobs = new Jobs("Rejected");
            this.RejectedJobs = new ObservableCollection<Job>(allRejectedJobs);
        }
        public JobManagementViewModel()
        {
            UpdateJobs();
            Contractors allContractors = new Contractors();
            this.Contractors = new ObservableCollection<Contractor>(allContractors);
            JobStatuses allJobStatues = new JobStatuses();
            this.JobStatuses = new ObservableCollection<JobStatus>(allJobStatues);
        }
    }
}
