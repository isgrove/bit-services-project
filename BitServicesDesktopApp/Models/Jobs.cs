using BitServicesDesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitServicesDesktopApp.Models
{
    public class Jobs : List<Job>
    {
        public Jobs()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "SELECT job_id, location_id, assigned_contractor_id, required_skill_name as required_skill, job_status," +
                " kilometers, description, deadline_date" +
                " FROM job";
            DataTable jobTable = helper.ExecuteSQL(sql);
            foreach (DataRow dr in jobTable.Rows)
            {
                Job newJob = new Job(dr);
                this.Add(newJob);
            }
        }
    }
}
