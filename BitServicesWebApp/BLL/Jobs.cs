using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BitServicesWebApp.DAL;

namespace BitServicesWebApp.BLL
{
    public class Jobs
    {
        public DataTable AllCompletedJobs()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "usp_GetAllJobs";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@JobStatus", DbType.String)
            {
                Value = "Completed"
            };
            DataTable jobsTable = helper.ExecuteSQL(sql, objParams, true);
            return jobsTable;

        }
        public DataTable AllUnasignedJobs()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "usp_GetJobsToAssign";
            DataTable unassignedJobsTable = helper.ExecuteSQL(sql);
            return unassignedJobsTable;
        }

        public DataTable AllJobs()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "usp_GetAllJobs";
            DataTable jobsTable = helper.ExecuteSQL(sql, null, true);
            return jobsTable;

        }

        public DataTable AllRejectionReasons()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "usp_GetAllRejectionReasons";
            DataTable reasonsTable = helper.ExecuteSQL(sql, null, true);
            return reasonsTable;
        }
    }
}