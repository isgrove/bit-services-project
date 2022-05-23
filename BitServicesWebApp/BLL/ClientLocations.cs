using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using BitServicesWebApp.Helpers;

namespace BitServicesWebApp.BLL
{
    public class ClientLocations
    {
        public DataTable AllClientLocations(int clientId)
        {
            SQLHelper db = new SQLHelper();
            string sql = "SELECT location_id, client_id, email, phone, street, suburb, postcode, state, active" +
                 " FROM client_location" +
                 " WHERE client_id = @ClientId" +
                 " AND active = 1";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@ClientId", DbType.Int32);
            objParams[0].Value = clientId;
            DataTable locationTable = db.ExecuteSQL(sql, objParams);
            return locationTable;
        }
    }
}
