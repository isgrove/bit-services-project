using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BitServicesWebApp.DAL;

namespace BitServicesWebApp.BLL
{
    public class ClientJobs
    {
        public DataTable AllCompletedBookings()
        {
            string sql = "SELECT cl.suburb AS [Location Suburb], j.job_status AS [Status], j.required_skill_name [Job Skill]," +
                         " j.description AS [Description], format(j.deadline_date, 'D') AS [Deadline Date]" + 
                         " FROM job j" +
                         " INNER JOIN client_location cl ON cl.location_id = j.location_id" +
                         " INNER JOIN  client c ON c.client_id = cl.client_id";
            SQLHelper helper = new SQLHelper();
            DataTable bookingsTable = helper.ExecuteSQL(sql);
            return bookingsTable;
        }
    }
}