using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BitServicesWebApp.DAL;

namespace BitServicesWebApp.BLL
{
    public class Skills
    {
        public DataTable AllSkills()
        {
            SQLHelper db = new SQLHelper();
            string sql = "SELECT skill_name" +
                         " FROM skill";
            DataTable skillTable = db.ExecuteSQL(sql);
            return skillTable;
        }
    }
}