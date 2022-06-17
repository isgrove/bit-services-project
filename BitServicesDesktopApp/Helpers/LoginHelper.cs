using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitServicesDesktopApp.Models;

namespace BitServicesDesktopApp.Helpers
{
    public class LoginHelper
    {

        public static Staff IsCoordinator(string username, string password)
        {
            return UserCheck(username, password, "Coordinator");
        }
        
        public static Staff IsAdmin(string username, string password)
        {
            return UserCheck(username, password, "Admin");
        }

        public static Staff UserCheck(string username, string password, string staffType)
        {
            string sql = $"SELECT * FROM staff WHERE email = @Username AND password = @Password AND type = @StaffType";

            SQLHelper db = new SQLHelper();
            SqlParameter[] objParams = new SqlParameter[3];
            objParams[0] = new SqlParameter("@Username", DbType.String)
            {
                Value = username
            };
            objParams[1] = new SqlParameter("@Password", DbType.String)
            {
                Value = password
            };
            objParams[2] = new SqlParameter("@StaffType", DbType.String)
            {
                Value = staffType
            };

            DataTable dt = db.ExecuteSQL(sql, objParams);
            return dt.Rows.Count == 1 ? new Staff(dt.Rows[0]) : null;
        }

    }
}
