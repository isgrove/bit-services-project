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
            string sql = "usp_GetAllJobs";
            DataTable jobTable = helper.ExecuteSQL(sql, null, true);
            foreach (DataRow dr in jobTable.Rows)
            {
                Job newJob = new Job(dr);
                this.Add(newJob);
            }
        }
        public Jobs(string jobStatus)
        {
            DataTable jobTable;
            if (jobStatus == "Rejected")
            {
                SQLHelper helper = new SQLHelper();
                string sql = "usp_GetAllRejectedJobs";
                jobTable = helper.ExecuteSQL(sql, null, true);
            }
            else
            {
                SQLHelper helper = new SQLHelper();
                string sql = "usp_GetAllJobs";
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@JobStatus", DbType.String)
                {
                    Value = jobStatus
                };
                jobTable = helper.ExecuteSQL(sql, objParams, true);
            }

            foreach (DataRow dr in jobTable.Rows)
            {
                Job newJob = new Job(dr);
                this.Add(newJob);
            }
        }
    }
}
