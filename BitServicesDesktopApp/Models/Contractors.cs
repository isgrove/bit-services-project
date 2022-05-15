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
                " suburb, postcode, state, licence_number, vehicle_registration, performance_rating, active" +
                " FROM contractor" +
                " WHERE active = 1";
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
            string sql = "usp_GetContractorsForJob";
            SqlParameter[] objParams = new SqlParameter[2];
            objParams[0] = new SqlParameter("@JobSkillName", DbType.String)
            {
                Value = requiredSkill
            };
            objParams[1] = new SqlParameter("@DeadlineDate", DbType.Date)
            {
                Value = deadlineDate
            };
            DataTable contractorsTable = helper.ExecuteSQL(sql, objParams, true);
            foreach (DataRow dr in contractorsTable.Rows)
            {
                Contractor newContractor = new Contractor(dr);
                this.Add(newContractor);
            }
        }
    }
}
