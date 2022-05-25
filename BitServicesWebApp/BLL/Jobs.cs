using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BitServicesWebApp.Helpers;

namespace BitServicesWebApp.BLL
{
    public class Jobs
    {
        private SQLHelper _db;

        public Jobs()
        {
            _db = new SQLHelper();
        }
        public DataTable AllCompletedJobs()
        {
            string sql = "usp_GetAllJobs";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@JobStatus", DbType.String)
            {
                Value = "Completed"
            };
            DataTable jobsTable = _db.ExecuteSQL(sql, objParams, true);
            return jobsTable;

        }
        public DataTable AllUnasignedJobs()
        {
            string sql = "usp_GetJobsToAssign";
            DataTable unassignedJobsTable = _db.ExecuteSQL(sql);
            return unassignedJobsTable;
        }

        public DataTable AllJobs()
        {
            string sql = "usp_GetAllJobs";
            DataTable jobsTable = _db.ExecuteSQL(sql, null, true);
            return jobsTable;
        }

        public DataTable AllJobs(DataTable jobStatuses)
        {
            string sql = "usp_GetAllJobsFromStatuses";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@JobStatuses", SqlDbType.Structured)
            {
                Value = jobStatuses
            };
            DataTable jobsTable = _db.ExecuteSQL(sql, objParams, true);
            return jobsTable;
        }

        public DataTable AllRejectedJobs()
        {
            string sql = "usp_GetAllRejectedJobs";
            DataTable jobsTable = _db.ExecuteSQL(sql, null, true);
            return jobsTable;
        }

        public DataTable AllRejectionReasons()
        {
            string sql = "usp_GetAllRejectionReasons";
            DataTable reasonsTable = _db.ExecuteSQL(sql, null, true);
            return reasonsTable;
        }
    }
}