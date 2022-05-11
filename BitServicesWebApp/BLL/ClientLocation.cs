using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using BitServicesWebApp.DAL;

namespace BitServicesWebApp.BLL
{
    public class ClientLocation
    {
        private SQLHelper _db;
        public int LocationId { get; set; }

        public int ClientId { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Street { get; set; }

        public string Suburb { get; set; }

        public string Postcode { get; set; }

        public string State { get; set; }

        public bool Active { get; set; }

        public ClientLocation()
        {
            _db = new SQLHelper();
        }
        public ClientLocation(DataRow dr)
        {
            _db = new SQLHelper();
            this.LocationId = Convert.ToInt32(dr["location_id"].ToString());
            this.ClientId = Convert.ToInt32(dr["client_id"].ToString());
            this.Email = dr["email"].ToString();
            this.Phone = dr["phone"].ToString();
            this.Street = dr["street"].ToString();
            this.Suburb = dr["suburb"].ToString();
            this.Postcode = dr["postcode"].ToString();
            this.State = dr["state"].ToString();
            this.Active = Convert.ToBoolean(dr["active"]);
        }

        public ClientLocation(int locationId)
        {
            _db = new SQLHelper();
            string sql = "SELECT location_id, client_id, email, phone, street, suburb, postcode, state, active" +
                         " FROM client_location" +
                         " WHERE location_id = @LocationId";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@LocationId", DbType.Int32);
            objParams[0].Value = locationId;
            DataRow dr = _db.ExecuteSQL(sql, objParams).Rows[0];

            this.LocationId = Convert.ToInt32(dr["location_id"].ToString());
            this.ClientId = Convert.ToInt32(dr["client_id"].ToString());
            this.Email = dr["email"].ToString();
            this.Phone = dr["phone"].ToString();
            this.Street = dr["street"].ToString();
            this.Suburb = dr["suburb"].ToString();
            this.Postcode = dr["postcode"].ToString();
            this.State = dr["state"].ToString();
            this.Active = Convert.ToBoolean(dr["active"]);
        }
        public int InsertClientLocation()
        {
            string sql = "INSERT INTO client_location (client_id, email, phone, street, suburb, postcode, state, active)" +
                         " VALUES(@ClientId, @Email, @Phone, @Street, @Suburb, @Postcode, @State, 1)";
            SqlParameter[] objParams = new SqlParameter[7];
            objParams[0] = new SqlParameter("@ClientId", DbType.Int32)
            {
                Value = this.ClientId
            };
            objParams[1] = new SqlParameter("@Email", DbType.String)
            {
                Value = this.Email
            };
            objParams[2] = new SqlParameter("@Phone", DbType.String)
            {
                Value = this.Phone
            };
            objParams[3] = new SqlParameter("@Street", DbType.String)
            {
                Value = this.Street
            };
            objParams[4] = new SqlParameter("@Suburb", DbType.String)
            {
                Value = this.Street
            };
            objParams[5] = new SqlParameter("@Postcode", DbType.String)
            {
                Value = this.Postcode
            };
            objParams[6] = new SqlParameter("@State", DbType.String)
            {
                Value = this.State
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams);
            return rowsAffected;
        }
        public int DeleteClientLocation()
        {
            string sql = "UPDATE client_location" +
                         " SET active = 0" +
                         " WHERE location_id = @LocationId";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@LocationId", DbType.String)
            {
                Value = this.LocationId
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams);
            return rowsAffected;
        }

        public int UpdateClientLocation()
        {
            string sql = "UPDATE client_location" +
                         " SET client_id = @ClientId, email = @Email, phone = @Phone, street = @Street, suburb = @Suburb, postcode = @Postcode, state = @State" +
                         " WHERE location_id = @LocationId";
            SqlParameter[] objParams = new SqlParameter[8];
            objParams[0] = new SqlParameter("@ClientId", DbType.Int32)
            {
                Value = this.ClientId
            };
            objParams[1] = new SqlParameter("@Email", DbType.String)
            {
                Value = this.Email
            };
            objParams[2] = new SqlParameter("@Phone", DbType.String)
            {
                Value = this.Phone
            };
            objParams[3] = new SqlParameter("@Street", DbType.String)
            {
                Value = this.Street
            };
            objParams[4] = new SqlParameter("@Suburb", DbType.String)
            {
                Value = this.Suburb
            };
            objParams[5] = new SqlParameter("@Postcode", DbType.String)
            {
                Value = this.Postcode
            };
            objParams[6] = new SqlParameter("@State", DbType.String)
            {
                Value = this.State
            };
            objParams[7] = new SqlParameter("@LocationId", DbType.String)
            {
                Value = this.LocationId
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams);
            return rowsAffected;
        }
    }
}
