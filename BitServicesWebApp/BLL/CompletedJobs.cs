using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BitServicesWebApp.DAL;

namespace BitServicesWebApp.BLL
{
    public class CompletedJobs
    {
        public DataTable AllCompletedJobs()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "usp_GetCompletedJobs";
            DataTable jobsTable = helper.ExecuteSQL(sql, null, true);
            return jobsTable;

        }
    }
}