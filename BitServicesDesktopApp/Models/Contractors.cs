using BitServicesDesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BitServicesDesktopApp.Models
{
    public class Contractors : List<Contractor>
    {
        public Contractors()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "SELECT contractor_id, first_name, last_name, email, phone, street, suburb, postcode, state" +
                " suburb, postcode, state, licence_number, vehicle_registration, active" +
                " FROM contractor";
            DataTable contractorsTable = helper.ExecuteSQL(sql);
            foreach (DataRow dr in contractorsTable.Rows)
            {
                Contractor newContractor = new Contractor(dr);
                this.Add(newContractor);
            }
        }
        public Contractors(string requiredSkill, DateTime deadlineDate)
        {
            SQLHelper helper = new SQLHelper();
            string sql = "SELECT DISTINCT c.contractor_id, c.first_name, c.last_name, c.email, c.phone, c.street, c.suburb, c.postcode, c.state," +
                         " c.suburb, c.postcode, c.state, c.licence_number, c.vehicle_registration, c.active" +
                         " FROM contractor c, contractor_skill s, contractor_availability a" +
                         " WHERE c.contractor_id = s.contractor_id" +
                         " AND c.contractor_id = a.contractor_id" +
                         " AND s.skill_name = @RequiredSkill" +
                         " AND a.availability_date between CONVERT(char(10), GetDate(),126) and @DeadlineDate";
            SqlParameter[] objParams = new SqlParameter[2];
            objParams[0] = new SqlParameter("@RequiredSkill", DbType.String)
            {
                Value = requiredSkill
            };
            objParams[1] = new SqlParameter("@DeadlineDate", DbType.Date)
            {
                Value = deadlineDate
            };
            DataTable contractorsTable = helper.ExecuteSQL(sql, objParams);
            foreach (DataRow dr in contractorsTable.Rows)
            {
                Contractor newContractor = new Contractor(dr);
                this.Add(newContractor);
            }
        }
    }
}
