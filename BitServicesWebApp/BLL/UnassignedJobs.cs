using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BitServicesWebApp.DAL;

namespace BitServicesWebApp.BLL
{
    public class UnassignedJobs
    {
        public DataTable AllUnasignedJobs()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "usp_GetJobsToAssign";
            DataTable unassignedJobsTable = helper.ExecuteSQL(sql);
            return unassignedJobsTable;
        }
    }
}