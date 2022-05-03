using BitServicesDesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BitServicesDesktopApp.Models
{
    public class Skills : List<Skill>
    {
        public Skills()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "SELECT skill_name" +
                " FROM skill";
            DataTable skillTable = helper.ExecuteSQL(sql);
            foreach (DataRow dr in skillTable.Rows)
            {
                Skill newSkill = new Skill(dr);
                this.Add(newSkill);
            }
        }
        public Skills(int contractorId)
        {
            SQLHelper db = new SQLHelper();
            string sql = "SELECT skill_name, contractor_id" +
                 " FROM contractor_skill" +
                 " WHERE contractor_id = @ContractorId";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@ContractorId", DbType.Int32);
            objParams[0].Value = contractorId;
            DataTable skillTable = db.ExecuteSQL(sql, objParams);
            foreach (DataRow dr in skillTable.Rows)
            {
                Skill newSkill = new Skill(dr);
                this.Add(newSkill);
            }
        }
    }
}
