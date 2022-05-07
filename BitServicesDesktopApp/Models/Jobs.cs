using BitServicesDesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BitServicesDesktopApp.Models
{
    public class Jobs : List<Job>
    {
        public Jobs()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "SELECT j.job_id, j.location_id, j.assigned_contractor_id, j.required_skill_name as required_skill, j.job_status," +
                " j.kilometers, j.description, j.deadline_date, cl.client_id" +
                " FROM job j, client_location cl" +
                " WHERE j.location_id = cl.location_id";
            DataTable jobTable = helper.ExecuteSQL(sql);
            foreach (DataRow dr in jobTable.Rows)
            {
                Job newJob = new Job(dr);
                this.Add(newJob);
            }
        }
        public Jobs(string jobStatus)
        {
            SQLHelper helper = new SQLHelper();
            string sql = "SELECT j.job_id, j.location_id, j.assigned_contractor_id, j.required_skill_name as required_skill, j.job_status," +
                         " j.kilometers, j.description, j.deadline_date, cl.client_id" +
                         " FROM job j, client_location cl" +
                         " WHERE j.location_id = cl.location_id" +
                         " AND j.job_status = @JobStatus";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@Jobstatus", DbType.String)
            {
                Value = jobStatus
            };
            DataTable jobTable = helper.ExecuteSQL(sql, objParams);
            foreach (DataRow dr in jobTable.Rows)
            {
                Job newJob = new Job(dr);
                this.Add(newJob);
            }
        }
    }
}
