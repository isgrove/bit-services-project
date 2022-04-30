using BitServicesDesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BitServicesApp.Models
{
    public class Staffs : List<Staff>
    {
        public Staffs()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "SELECT staff_id, staff_type, first_name, last_name, email, phone, password, active" +
                " FROM Staff";
            DataTable staffTable = helper.ExecuteSQL(sql);
            foreach (DataRow dr in staffTable.Rows)
            {
                Staff newStaff = new Staff(dr);
                this.Add(newStaff);
            }
        }
    }
}
