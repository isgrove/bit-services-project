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
            string sql = "SELECT c.client_name + ' ' + l.suburb AS [Client Location], co.first_name + ' ' + co.last_name AS [Contractor Name]," +
                         " j.description AS[Description], format(j.deadline_date, 'D') AS[Deadline Date], 'To Implement' AS[Completed Date], j.job_id" +
                         " FROM job j " +
                         " INNER JOIN client_location l ON l.location_id = j.location_id" +
                         " INNER JOIN client c ON c.client_id = l.client_id" +
                         " INNER JOIN contractor co ON co.contractor_id = j.assigned_contractor_id" +
                         " WHERE j.job_status = 'Completed'";
            DataTable jobsTable = helper.ExecuteSQL(sql);
            return jobsTable;

        }
    }
}