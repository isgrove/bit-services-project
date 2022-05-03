using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace BitServicesDesktopApp.Models
{
    public class Skill : INotifyPropertyChanged
    {
        private string _skillName;
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        public string SkillName
        {
            get { return _skillName; }
            set
            {
                _skillName = value;
                OnPropertyChanged("SkillName");
            }
        }
        public Skill()
        {

        }
        public Skill(DataRow dr)
        {
            this.SkillName = dr["skill_name"].ToString();
        }
    }
}
