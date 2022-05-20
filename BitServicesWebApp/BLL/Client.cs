using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using BitServicesWebApp.DAL;

namespace BitServicesWebApp.BLL
{
    public class Client
    {
        private SQLHelper _db;
        public int ClientId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public bool Active { get; set; }

        public Client()
        {
            this._db = new SQLHelper();
        }
        public Client(DataRow dr)
        {
            this._db = new SQLHelper();
            this.ClientId = Convert.ToInt32(dr["client_id"].ToString());
            this.Name = dr["name"].ToString();
            this.Email = dr["email"].ToString();
            this.Phone = dr["phone"].ToString();
            this.Password = dr["password"].ToString();
            this.Active = Convert.ToBoolean(dr["active"]);
        }
        public Client(int clientId)
        {
            this._db = new SQLHelper();
            _db = new SQLHelper();
            string sql = "SELECT client_id, client_name, email, phone, active" +
                         " FROM client" +
                         " WHERE client_id = @ClientId";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@ClientId", DbType.Int32);
            objParams[0].Value = clientId;
            DataRow dr = _db.ExecuteSQL(sql, objParams).Rows[0];
            this.ClientId = Convert.ToInt32(dr["client_id"]);
            this.Name = dr["client_name"].ToString();
            this.Email = dr["email"].ToString();
            this.Phone = dr["phone"].ToString();
            this.Active = Convert.ToBoolean(dr["active"]);
        }

        public int InsertClient()
        {
            GeneratePassword();
            string sql = "INSERT INTO client (client_name, email, phone, password, active)" +
                         " VALUES(@Name, @Email, @Phone, @Password, 1)";
            SqlParameter[] objParams = new SqlParameter[4];
            objParams[0] = new SqlParameter("@Name", DbType.String)
            {
                Value = this.Name
            };
            objParams[1] = new SqlParameter("@Email", DbType.String)
            {
                Value = this.Email
            };
            objParams[2] = new SqlParameter("@Phone", DbType.String)
            {
                Value = this.Phone
            };
            objParams[3] = new SqlParameter("@Password", DbType.String)
            {
                Value = this.Password
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams);
            return rowsAffected;
        }
        public int DeleteClient()
        {
            string sql = "UPDATE client" +
                         " SET active = 0" +
                         " WHERE client_id = @ClientId";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@ClientId", DbType.String)
            {
                Value = this.ClientId
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams);
            return rowsAffected;
        }

        // TODO: Generate a random password
        private void GeneratePassword()
        {
            this.Password = "Password";
        }

        public int UpdateClient()
        {
            string sql = "UPDATE client" +
                         " SET client_name = @Name, email = @Email, phone = @Phone" +
                         " WHERE client_id = @ClientId";
            SqlParameter[] objParams = new SqlParameter[4];
            objParams[0] = new SqlParameter("@Name", DbType.String)
            {
                Value = this.Name
            };
            objParams[1] = new SqlParameter("@Email", DbType.String)
            {
                Value = this.Email
            };
            objParams[2] = new SqlParameter("@Phone", DbType.String)
            {
                Value = this.Phone
            };
            objParams[3] = new SqlParameter("@ClientId", DbType.String)
            {
                Value = this.ClientId
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams);
            return rowsAffected;
        }

        public DataTable AllJobs()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "usp_GetAllJobs";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@ClientId", DbType.Int32)
            {
                Value = this.ClientId
            };
            DataTable jobsTable = helper.ExecuteSQL(sql, objParams, true);
            return jobsTable;
        }
    }
}
