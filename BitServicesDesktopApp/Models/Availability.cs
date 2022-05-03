using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitServicesDesktopApp.Models
{
    public class Availability
    {
        private DateTime _availabilityDate;
        private int _clientId;

        public DateTime AvailabilityDate
        {
            get { return _availabilityDate; }
            set { _availabilityDate = value; }
        }

        public int ClientId
        {
            get { return _clientId; }
            set { _clientId = value; }
        }

        public Availability()
        {

        }
        public Availability(DataRow dr)
        {
            this.ClientId = Convert.ToInt32(dr["contractor_id"].ToString());
            this.AvailabilityDate = Convert.ToDateTime(dr["availability_date"].ToString());
        }
    }
}
