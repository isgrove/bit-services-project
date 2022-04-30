using BitServicesDesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BitServicesApp.Models
{
    public class ClientLocations : List<ClientLocation>
    {
        public ClientLocations()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "SELECT location_id, client_id, email, phone, street, suburb, postcode, state, active" +
                " FROM client_location";
            DataTable locationTable = helper.ExecuteSQL(sql);
            foreach (DataRow dr in locationTable.Rows)
            {
                ClientLocation newLocation = new ClientLocation(dr);
                this.Add(newLocation);
            }
        }
    }
}
