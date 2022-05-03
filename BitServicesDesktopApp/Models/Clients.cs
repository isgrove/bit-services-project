using BitServicesDesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BitServicesDesktopApp.Models
{
    public class Clients : List<Client>
    {
        public Clients()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "SELECT client_id, client_name as name, email, phone, password, active" +
                " FROM client";
            DataTable clientsTable = helper.ExecuteSQL(sql);
            foreach (DataRow dr in clientsTable.Rows)
            {
                Client newClient = new Client(dr);
                this.Add(newClient);
            }
        }
    }
}
