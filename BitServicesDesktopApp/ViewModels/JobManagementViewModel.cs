using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using BitServicesDesktopApp.Models;

namespace BitServicesDesktopApp.ViewModels
{
    public class JobManagementViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Job> _jobs;
        private ObservableCollection<ClientLocation> _selectedClientLocations;
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
        public ObservableCollection<ClientLocation> SelectedClientLocations
        {
            get { return _selectedClientLocations; }
            set
            {
                _selectedClientLocations = value;
                OnPropertyChanged("SelectedClientLocations");
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
        public JobManagementViewModel()
        {
            Jobs allJobs = new Jobs();
            this.Jobs = new ObservableCollection<Job>(allJobs);
            Contractors allContractors = new Contractors();
            this.Contractors = new ObservableCollection<Contractor>(allContractors);
            JobStatuses allJobStatues = new JobStatuses();
            this.JobStatuses = new ObservableCollection<JobStatus>(allJobStatues);
            Skills allSkills = new Skills();
            this.Skills = new ObservableCollection<Skill>(allSkills);
        }
    }
}
