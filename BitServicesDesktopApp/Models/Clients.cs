using BitServicesDesktopApp.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BitServicesDesktopApp.Models
{
    public class Clients : List<Client>
    {
        public Clients()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "SELECT client_id, client_name as name, email, phone, password, active" +
                " FROM client" +
                " WHERE active = 1";
            DataTable clientsTable = helper.ExecuteSQL(sql);
            foreach (DataRow dr in clientsTable.Rows)
            {
                Client newClient = new Client(dr);
                this.Add(newClient);
            }
        }
        public Clients(string searchText)
        {
            SQLHelper helper = new SQLHelper();
            string sql = "usp_SearchClients";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@SearchQuery", DbType.String)
            {
                Value = searchText
            };
            DataTable clientsTable = helper.ExecuteSQL(sql, objParams, true);
            foreach (DataRow dr in clientsTable.Rows)
            {
                Client newClient = new Client(dr);
                this.Add(newClient);
            }
        }
    }
}
