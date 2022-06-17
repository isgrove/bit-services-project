using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitServicesDesktopApp.Helpers;

namespace BitServicesDesktopApp.Models
{
    public class StaffTypes : List<StaffType>
    {
        public StaffTypes()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "SELECT type" +
                         " FROM staff_type";
            DataTable staffTable = helper.ExecuteSQL(sql);
            foreach (DataRow dr in staffTable.Rows)
            {
                StaffType newStaffType = new StaffType(dr);
                this.Add(newStaffType);
            }
        }
    }
}
