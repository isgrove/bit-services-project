using BitServicesDesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public ClientLocations(int clientId)
        {
            SQLHelper db = new SQLHelper();
            string sql = "SELECT location_id, client_id, email, phone, street, suburb, postcode, state, active" +
                 " FROM client_location" +
                 " WHERE client_id = @ClientId";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@ClientId", DbType.Int32);
            objParams[0].Value = clientId;
            DataTable locationTable = db.ExecuteSQL(sql, objParams);
            foreach (DataRow dr in locationTable.Rows)
            {
                ClientLocation newLocation = new ClientLocation(dr);
                this.Add(newLocation);
            }
        }
    }
}
