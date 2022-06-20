using BitServicesDesktopApp.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BitServicesDesktopApp.Models
{

    //TODO: Convert SQL into stored procedures
    public class Staffs : List<Staff>
    {
        public Staffs()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "SELECT staff_id, type, first_name, last_name, email, phone, password, active" +
                " FROM staff" +
                " WHERE active = 1";
            DataTable staffTable = helper.ExecuteSQL(sql);
            foreach (DataRow dr in staffTable.Rows)
            {
                Staff newStaff = new Staff(dr);
                this.Add(newStaff);
            }
        }
        public Staffs(string searchText)
        {
            SQLHelper helper = new SQLHelper();
            string sql = "usp_SearchStaff";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@SearchQuery", DbType.String)
            {
                Value = searchText
            };
            DataTable staffTable = helper.ExecuteSQL(sql, objParams, true);
            foreach (DataRow dr in staffTable.Rows)
            {
                Staff newStaff = new Staff(dr);
                this.Add(newStaff);
            }
        }
    }
}
