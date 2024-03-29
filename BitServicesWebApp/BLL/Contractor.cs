﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using BitServicesWebApp.Helpers;

namespace BitServicesWebApp.BLL
{
    public class Contractor
    {
        private SQLHelper _db;
        public int ContractorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string Street { get; set; }

        public string Suburb { get; set; }

        public string Postcode { get; set; }

        public string State { get; set; }

        public string LicenceNumber { get; set; }

        public string VehicleRegistration { get; set; }

        public bool Active { get; set; }

        public Contractor()
        {
            _db = new SQLHelper();
        }
        public Contractor(DataRow dr)
        {
            _db = new SQLHelper();
            this.ContractorId = Convert.ToInt32(dr["contractor_id"].ToString());
            this.FirstName = dr["first_name"].ToString();
            this.LastName = dr["last_name"].ToString();
            this.Email = dr["email"].ToString();
            this.Phone = dr["phone"].ToString();
            this.Street = dr["street"].ToString();
            this.Suburb = dr["suburb"].ToString();
            this.Postcode = dr["postcode"].ToString();
            this.State = dr["state"].ToString();
            this.LicenceNumber = dr["licence_number"].ToString();
            this.VehicleRegistration = dr["vehicle_registration"].ToString();
            this.Active = Convert.ToBoolean(dr["active"]);
        }

        public Contractor(int contractorId)
        {
            _db = new SQLHelper();
            string sql = "SELECT contractor_id, first_name, last_name, email, phone, street, suburb, postcode, state" +
                         " suburb, postcode, state, licence_number, vehicle_registration, active" +
                         " FROM contractor" +
                         " WHERE contractor_id = @ContractorId";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@ContractorId", DbType.Int32);
            objParams[0].Value = contractorId;

            DataRow dr = _db.ExecuteSQL(sql, objParams).Rows[0];

            this.ContractorId = Convert.ToInt32(dr["contractor_id"].ToString());
            this.FirstName = dr["first_name"].ToString();
            this.LastName = dr["last_name"].ToString();
            this.Email = dr["email"].ToString();
            this.Phone = dr["phone"].ToString();
            this.Street = dr["street"].ToString();
            this.Suburb = dr["suburb"].ToString();
            this.Postcode = dr["postcode"].ToString();
            this.State = dr["state"].ToString();
            this.LicenceNumber = dr["licence_number"].ToString();
            this.VehicleRegistration = dr["vehicle_registration"].ToString();
            this.Active = Convert.ToBoolean(dr["active"]);
        }

        public int CompleteJob(int jobId, int kilometers)
        {
            string sql = "UPDATE Job" +
                         " SET job_status = 'Completed', kilometers = @Kilometers " +
                         " WHERE job_id = @JobId";

            SqlParameter[] objParams = new SqlParameter[2];
            objParams[0] = new SqlParameter("@Kilometers", DbType.Int32)
            {
                Value = kilometers
            };
            objParams[1] = new SqlParameter("@JobId", DbType.Int32)
            {
                Value = jobId
            };
            int returnValue = _db.ExecuteNonQuery(sql, objParams);
            return returnValue;
        }
        public int AcceptJob(int jobId, DateTime completionDate)
        {
            string sql = "usp_AcceptJob";

            SqlParameter[] objParams = new SqlParameter[3];
            objParams[0] = new SqlParameter("@ContractorId", DbType.Int32)
            {
                Value = this.ContractorId
            };
            objParams[1] = new SqlParameter("@JobId", DbType.Int32)
            {
                Value = jobId
            };
            objParams[2] = new SqlParameter("@CompletionDate", DbType.Date)
            {
                Value = completionDate
            };
            int returnValue = _db.ExecuteNonQuery(sql, objParams, true);
            return returnValue;
        }
        public int RejectJob(int jobId, string reason)
        {
            string sql = "usp_RejectJob";

            SqlParameter[] objParams = new SqlParameter[3];
            objParams[0] = new SqlParameter("@ContractorId", DbType.Int32)
            {
                Value = this.ContractorId
            };
            objParams[1] = new SqlParameter("@JobId", DbType.Int32)
            {
                Value = jobId
            };
            objParams[2] = new SqlParameter("@Reason", DbType.String)
            {
                Value = reason
            };
            int returnValue = _db.ExecuteNonQuery(sql, objParams, true);
            return returnValue;
        }
        public DataTable AcceptedJobs()
        {
            string sql = "usp_GetInProgressJobs";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@ContractorId", DbType.String)
            {
                Value = this.ContractorId
            };
            DataTable jobsTable = _db.ExecuteSQL(sql, objParams, true);
            return jobsTable;
        }
        public DataTable AssignedJobs()
        {
            string sql = "usp_GetPendingJobs";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@ContractorId", DbType.String)
            {
                Value = this.ContractorId
            };
            DataTable jobsTable = _db.ExecuteSQL(sql, objParams, true);
            return jobsTable;
        }
        public DataTable FilteredJobs(DataTable jobStatuses)
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

        // Can be optimized by not using list.remove, however, due to how small list is this should have a minimal impact on performance
        public DataTable[] AllJobs(List<string> activeFilters)
        {
            Jobs jobs = new Jobs();

            bool getRejectedJobs = activeFilters.Contains("Rejected");
            bool getInProgressJobs = activeFilters.Contains("In Progress");

            DataTable[] resultTables = new DataTable[3];
            DataTable allJobs = new DataTable();
            DataTable rejectedJobs = new DataTable();
            DataTable inProgressJobs = new DataTable();

            if (getRejectedJobs)
            {
                rejectedJobs = this.RejectedJobs();
                activeFilters.Remove("Rejected");
            }

            if (getInProgressJobs)
            {
                inProgressJobs = this.AcceptedJobs();
                activeFilters.Remove("In Progress");
            }

            // Pass filters as DT for stored proc params
            if (activeFilters.Count > 0)
            {
                DataTable jobStatuses = new DataTable();
                jobStatuses.Columns.Add("job_status", typeof(string));

                foreach (string jobStatus in activeFilters)
                {
                    jobStatuses.Rows.Add(jobStatus);
                }
                allJobs = this.FilteredJobs(jobStatuses);
            }

            resultTables[0] = allJobs;
            resultTables[1] = rejectedJobs;
            resultTables[2] = inProgressJobs;

            return resultTables;
        }
        public DataTable RejectedJobs()
        {
            string sql = "usp_GetAllRejectedJobs";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@ContractorId", DbType.Int32)
            {
                Value = this.ContractorId
            };
            DataTable jobsTable = _db.ExecuteSQL(sql, objParams, true);
            return jobsTable;
        }
        public DataTable GetAvailabilityForJob(DateTime deadlineDate)
        {
            string sql = "usp_GetAvailabilityForJob";
            SqlParameter[] objParams = new SqlParameter[2];
            objParams[0] = new SqlParameter("@ContractorId", DbType.String)
            {
                Value = this.ContractorId   
            };
            objParams[1] = new SqlParameter("@DeadlineDate", DbType.DateTime)
            {
                Value = deadlineDate
            };
            DataTable availabilitiesTable = _db.ExecuteSQL(sql, objParams, true);
            return availabilitiesTable;
        }
    }
}
