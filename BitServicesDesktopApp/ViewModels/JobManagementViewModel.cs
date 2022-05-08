using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using BitServicesDesktopApp.Models;

namespace BitServicesDesktopApp.ViewModels
{
    public class JobManagementViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Job> _jobs;
        private ObservableCollection<Job> _acceptedJobs;
        private ObservableCollection<Job> _awaitingPaymentJobs;
        private ObservableCollection<Job> _completedJobs;
        private ObservableCollection<Job> _pendingJobs;
        private ObservableCollection<Job> _rejectedJobs;
        private ObservableCollection<ClientLocation> _selectedClientLocations;
        private ObservableCollection<Client> _clients;
        private ObservableCollection<Contractor> _contractors;
        private ObservableCollection<JobStatus> _jobStatuses;
        private ObservableCollection<Skill> _skills;
        private Job _selectedJob;
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        public ObservableCollection<Job> Jobs
        {
            get { return _jobs; }
            set
            {
                _jobs = value;
                OnPropertyChanged("Jobs");
            }
        }
        public ObservableCollection<Job> AcceptedJobs
        {
            get { return _acceptedJobs; }
            set
            {
                _acceptedJobs = value;
                OnPropertyChanged("AcceptedJobs");
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
        public ObservableCollection<Job> RejectedJobs
        {
            get { return _rejectedJobs; }
            set
            {
                _rejectedJobs = value;
                OnPropertyChanged("RejectedJobs");
            }
        }

        public ObservableCollection<Job> AwaitingPaymentJobs
        {
            get { return _awaitingPaymentJobs; }
            set
            {
                _awaitingPaymentJobs = value;
                OnPropertyChanged("AwaitingPaymentJobs");
            }
        }
        public ObservableCollection<ClientLocation> SelectedClientLocations
        {
            get { return _selectedClientLocations; }
            set
            {
                _selectedClientLocations = value;
                OnPropertyChanged("SelectedClientLocations");
            }
        }
        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                OnPropertyChanged("Clients");
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
        public ObservableCollection<Skill> Skills
        {
            get { return _skills; }
            set
            {
                _skills = value;
                OnPropertyChanged("Skills");
            }
        }
        public Job SelectedJob
        {
            get { return _selectedJob; }
            set
            {
                _selectedJob = value;
                OnPropertyChanged("SelectedJob");
                ClientLocations alllClientLocations = new ClientLocations(SelectedJob.ClientId);
                // By updating the SelectedClientLocations variable, the binding on cmbClientLocation text breaks
                this.SelectedClientLocations = new ObservableCollection<ClientLocation>(alllClientLocations);
            }
        }
        /*
        Accepted
        Awaiting Payment
        Completed
        Pending
        Rejected
         */
        public void UpdateJobs()
        {
            Jobs allJobs = new Jobs();
            this.Jobs = new ObservableCollection<Job>(allJobs);

            Jobs allAcceptedJobs = new Jobs("Accepted");
            this.AcceptedJobs = new ObservableCollection<Job>(allAcceptedJobs);

            Jobs allAwaitingPaymentJobs = new Jobs("AwaitingPayment");
            this.AwaitingPaymentJobs = new ObservableCollection<Job>(allAwaitingPaymentJobs);

            Jobs allCompletedJobs = new Jobs("Completed");
            this.CompletedJobs = new ObservableCollection<Job>(allCompletedJobs);

            Jobs allPendingJobs = new Jobs("Pending");
            this.PendingJobs = new ObservableCollection<Job>(allPendingJobs);

            Jobs allRejectedJobs = new Jobs("Rejected");
            this.RejectedJobs = new ObservableCollection<Job>(allRejectedJobs);
        }
        public JobManagementViewModel()
        {
            UpdateJobs();
            Clients allClients = new Clients();
            this.Clients = new ObservableCollection<Client>(allClients);
            Contractors allContractors = new Contractors();
            this.Contractors = new ObservableCollection<Contractor>(allContractors);
            JobStatuses allJobStatues = new JobStatuses();
            this.JobStatuses = new ObservableCollection<JobStatus>(allJobStatues);
            Skills allSkills = new Skills();
            this.Skills = new ObservableCollection<Skill>(allSkills);
        }
    }
}
