using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using BitServicesWebApp.BLL;

namespace BitServicesWebApp.Helpers
{
    public class FilterHelper
    {

        // based on filters return filtered jobs.
        // rejected jobs will be returned in a different DataTable
        public DataTable[] GetFilteredJobs(List<string> activeFilters)
        {
            Jobs jobs = new Jobs();

            bool getRejectedJobs = activeFilters.Contains("Rejected");

            DataTable[] resultTables = new DataTable[2];
            DataTable allJobs = new DataTable();
            DataTable rejectedJobs = new DataTable();

            // pass filters as DT for stored proc params
            if (!(getRejectedJobs && activeFilters.Count == 1))
            {
                DataTable jobStatuses = new DataTable();
                jobStatuses.Columns.Add("job_status", typeof(string));

                foreach (string jobStatus in activeFilters)
                {
                    jobStatuses.Rows.Add(jobStatus);
                }
                allJobs = jobs.AllJobs(jobStatuses);
            }

            if (getRejectedJobs)
            {
                rejectedJobs = jobs.AllRejectedJobs();
            }

            resultTables[0] = allJobs;
            resultTables[1] = rejectedJobs;

            return resultTables;
        }
        public DataTable GetFilteredJobs(List<string> activeFilters, int clientId)
        {
            Client client = new Client()
            {
                ClientId = clientId
            };

            DataTable allJobs;
            DataTable jobStatuses = new DataTable();

            jobStatuses.Columns.Add("job_status", typeof(string));

            foreach (string jobStatus in activeFilters)
            {
                jobStatuses.Rows.Add(jobStatus);
            }
            allJobs = client.AllJobs(jobStatuses);

            return allJobs;
        }
    }
}