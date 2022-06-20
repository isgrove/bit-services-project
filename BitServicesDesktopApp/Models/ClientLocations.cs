using BitServicesDesktopApp.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BitServicesDesktopApp.Models
{

    //TODO: Convert SQL into stored procedures
    public class ClientLocations : List<ClientLocation>
    {
        public ClientLocations()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "usp_GetClientLocations";
            DataTable locationTable = helper.ExecuteSQL(sql, null, true);
            foreach (DataRow dr in locationTable.Rows)
            {
                ClientLocation newLocation = new ClientLocation(dr);
                this.Add(newLocation);
            }
        }
        public ClientLocations(int clientId)
        {
            SQLHelper db = new SQLHelper();
            string sql = "usp_GetClientLocations";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@ClientId", DbType.Int32);
            objParams[0].Value = clientId;
            DataTable locationTable = db.ExecuteSQL(sql, objParams, true);
            foreach (DataRow dr in locationTable.Rows)
            {
                ClientLocation newLocation = new ClientLocation(dr);
                this.Add(newLocation);
            }
        }
        public ClientLocations(int clientId, string searchText)
        {
            SQLHelper db = new SQLHelper();
            string sql = "usp_SearchClientLocations";
            SqlParameter[] objParams = new SqlParameter[2];
            objParams[0] = new SqlParameter("@SearchQuery", DbType.String)
            {
                Value = searchText
            };
            objParams[0] = new SqlParameter("@ClientId", DbType.Int32)
            {
                Value = clientId
            };
            DataTable locationTable = db.ExecuteSQL(sql, objParams, true);
            foreach (DataRow dr in locationTable.Rows)
            {
                ClientLocation newLocation = new ClientLocation(dr);
                this.Add(newLocation);
            }
        }
    }
}
