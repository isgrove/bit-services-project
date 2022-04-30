using BitServicesDesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BitServicesApp.Models
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
    }
}
