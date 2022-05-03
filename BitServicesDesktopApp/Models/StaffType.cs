using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitServicesDesktopApp.Models
{
    public class StaffType : INotifyPropertyChanged
    {
        private string _type;   
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }

        public StaffType()
        {

        }
        public StaffType(DataRow dr)
        {
            this.Type = dr["type"].ToString();
        }
    }
}
