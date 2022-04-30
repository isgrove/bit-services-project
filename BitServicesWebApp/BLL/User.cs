using BitServicesWebApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BitServicesWebApp.BLL
{
    public class User
    {

        public string Email { get; set; }
        public string Password { get; set; }
        private SQLHelper _db;

        public User()
        {
            _db = new SQLHelper();
        }
        public int CheckClient()
        {
            string sql = "SELECT client_id" +
                " FROM client" +
                "WHERE email = @Email and password = @Password";
            SqlParameter[] objParams = new SqlParameter[2];
            objParams[0] = new SqlParameter("@Email", DbType.String);
            objParams[0].Value = this.Email;
            objParams[1] = new SqlParameter("@Password", DbType.String);
            objParams[1].Value = this.Password;
            DataTable dataTable = _db.ExecuteSQL(sql, objParams);
            int id = -1;
            if (dataTable.Rows.Count > 0)
            {
                id = Convert.ToInt32(dataTable.Rows[0][0]);
            }
            return id;
        }
        public int CheckContractor()
        {
            string sql = "SELECT contractor_id" +
                " FROM contractor" +
                " WHERE email = @Email and password = @Password";
            SqlParameter[] objParams = new SqlParameter[2];
            objParams[0] = new SqlParameter("@Email", DbType.String);
            objParams[0].Value = this.Email;
            objParams[1] = new SqlParameter("@Password", DbType.String);
            objParams[1].Value = this.Password;
            DataTable dataTable = _db.ExecuteSQL(sql, objParams);
            int id = -1;
            if (dataTable.Rows.Count > 0)
            {
                id = Convert.ToInt32(dataTable.Rows[0][0]);
            }
            return id;
        }
        public int CheckStaff()
        {
            string sql = "SELECT staff_id" +
                " FROM staff" +
                "WHERE email = @Email and password = @Password";
            SqlParameter[] objParams = new SqlParameter[2];
            objParams[0] = new SqlParameter("@Email", DbType.String);
            objParams[0].Value = this.Email;
            objParams[1] = new SqlParameter("@Password", DbType.String);
            objParams[1].Value = this.Password;
            DataTable dataTable = _db.ExecuteSQL(sql, objParams);
            int id = -1;
            if (dataTable.Rows.Count > 0)
            {
                id = Convert.ToInt32(dataTable.Rows[0][0]);
            }
            return id;
        }
    }
}