using BitServicesDesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitServicesDesktopApp.Models
{
    public class LoginHelper
    {

        public static bool IsCoordinator(string username, string password)
        {
            return UserCheck(username, password, "Coordinator");
        }

        public static bool IsAdmin(string username, string password)
        {
            return UserCheck(username, password, "Admin");
        }

        public static bool UserCheck(string username, string password, string staffType)
        {
            string sql = $"SELECT * FROM staff WHERE email = @Username AND password = @Password AND staff_type = @StaffType";

            SQLHelper db = new SQLHelper();
            SqlParameter[] objParams = new SqlParameter[3];
            objParams[0] = new SqlParameter("@Username", DbType.String);
            objParams[0].Value = username;
            objParams[1] = new SqlParameter("@Password", DbType.String);
            objParams[1].Value = password;
            objParams[2] = new SqlParameter("@StaffType", DbType.String);
            objParams[2].Value = staffType;

            DataTable dt = db.ExecuteSQL(sql, objParams);
            return dt.Rows.Count == 1;
        }

    }
}
