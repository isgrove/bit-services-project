using BitServicesDesktopApp.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitServicesDesktopApp.Models
{

    //TODO: Convert SQL into stored procedures
    public class JobStatuses : List<JobStatus>
    {
        public JobStatuses()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "SELECT status" +
                " FROM job_status";
            DataTable statusTable = helper.ExecuteSQL(sql);
            foreach (DataRow dr in statusTable.Rows)
            {
                JobStatus newStatus = new JobStatus(dr);
                this.Add(newStatus);
            }
        }
    }
}
