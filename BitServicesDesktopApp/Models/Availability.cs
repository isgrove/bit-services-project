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

        public DateTime AvilabilityDate
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
            this.ClientId = Convert.ToInt32(dr["client_id"].ToString());
            this.AvilabilityDate = Convert.ToDateTime(dr["availability_date"].ToString());
        }
    }
}
