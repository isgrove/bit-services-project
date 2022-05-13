using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitServicesWebApp.DAL;

namespace BitServicesWebApp.BLL
{
    public class Job
    {
        private SQLHelper _db;
        public int JobId { get; set; }

        public string RequiredSkill { get; set; }

        public string JobStatus { get; set; }

        public string Description { get; set; }

        public int Kilometers { get; set; }

        public DateTime DeadlineDate { get; set; }

        public Client Client { get; set; }

        public ClientLocation Location { get; set; }

        public Contractor AssignedContractor { get; set; }

        public Job()
        {
            _db = new SQLHelper();
        }
        public Job(DataRow dr)
        {
            _db = new SQLHelper();
            this.JobId = Convert.ToInt32(dr["job_id"].ToString());
            this.RequiredSkill = dr["required_skill"].ToString();
            this.JobStatus = dr["job_status"].ToString();
            this.Description = dr["description"].ToString();
            this.Kilometers = Convert.ToInt32(dr["kilometers"].ToString());
            this.DeadlineDate = Convert.ToDateTime(dr["deadline_date"].ToString());
            this.Location = new ClientLocation(Convert.ToInt32(dr["location_id"].ToString()));
            this.AssignedContractor = new Contractor(Convert.ToInt32(dr["assigned_contractor_id"].ToString()));
            this.Client = new Client(Convert.ToInt32(dr["client_id"].ToString()));
        }
        public int InsertJob(bool contractorAssigned = true)
        {
            string sql;
            SqlParameter[] objParams;

            if (contractorAssigned)
            {
                sql = "INSERT INTO job (location_id, assigned_contractor_id, required_skill_name, job_status, kilometers, description, deadline_date)" +
                             " VALUES(@LocationId, @AssignedContractorId, @RequiredSkill, @JobStatus, @Kilometers, @Description, @DeadlineDate)";
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@LocationId", DbType.Int32)
                {
                    Value = this.Location.LocationId
                };
                objParams[1] = new SqlParameter("@AssignedContractorId", DbType.Int32)
                {
                    Value = this.AssignedContractor.ContractorId
                };
                objParams[2] = new SqlParameter("@RequiredSkill", DbType.String)
                {
                    Value = this.RequiredSkill
                };
                objParams[3] = new SqlParameter("@JobStatus", DbType.String)
                {
                    Value = this.JobStatus
                };
                objParams[4] = new SqlParameter("@Kilometers", DbType.Int32)
                {
                    Value = this.Kilometers
                };
                objParams[5] = new SqlParameter("@Description", DbType.String)
                {
                    Value = this.Description
                };
                objParams[6] = new SqlParameter("@DeadlineDate", DbType.Date)
                {
                    Value = this.DeadlineDate
                };
            }
            else
            {
                sql = "INSERT INTO job (location_id, required_skill_name, job_status, kilometers, description, deadline_date)" +
                             " VALUES(@LocationId, @RequiredSkill, @JobStatus, @Kilometers, @Description, @DeadlineDate)";
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@LocationId", DbType.Int32)
                {
                    Value = this.Location.LocationId
                };
                objParams[1] = new SqlParameter("@RequiredSkill", DbType.String)
                {
                    Value = this.RequiredSkill
                };
                objParams[2] = new SqlParameter("@JobStatus", DbType.String)
                {
                    Value = this.JobStatus
                };
                objParams[3] = new SqlParameter("@Kilometers", DbType.Int32)
                {
                    Value = this.Kilometers
                };
                objParams[4] = new SqlParameter("@Description", DbType.String)
                {
                    Value = this.Description
                };
                objParams[5] = new SqlParameter("@DeadlineDate", DbType.Date)
                {
                    Value = this.DeadlineDate
                };
            }
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams);
            return rowsAffected;
        }
        public int DeleteJob()
        {
            string sql = "DELETE FROM job" +
                         " WHERE job_id = @JobId";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@JobId", DbType.Int32)
            {
                Value = this.JobId
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams);
            return rowsAffected;
        }
        public int UpdateJob()
        {
            string sql = "UPDATE job" +
                         " SET location_id = @LocationId, assigned_contractor_id = @AssignedContractorId, required_skill_name = @RequiredSkill," +
                         " job_status = @JobStatus, kilometers = @Kilometers, description = @Description, deadline_date = @DeadlineDate" +
                         " WHERE job_id = @JobId";
            SqlParameter[] objParams = new SqlParameter[8];
            objParams[0] = new SqlParameter("@LocationId", DbType.Int32)
            {
                Value = this.Location.LocationId
            };
            objParams[1] = new SqlParameter("@AssignedContractorId", DbType.Int32)
            {
                Value = this.AssignedContractor.ContractorId
            };
            objParams[2] = new SqlParameter("@RequiredSkill", DbType.String)
            {
                Value = this.RequiredSkill
            };
            objParams[3] = new SqlParameter("@JobStatus", DbType.String)
            {
                Value = this.JobStatus
            };
            objParams[4] = new SqlParameter("@Kilometers", DbType.Int32)
            {
                Value = this.Kilometers
            };
            objParams[5] = new SqlParameter("@Description", DbType.String)
            {
                Value = this.Description
            };
            objParams[6] = new SqlParameter("@DeadlineDate", DbType.Date)
            {
                Value = this.DeadlineDate
            };
            objParams[7] = new SqlParameter("@JobId", DbType.Int32)
            {
                Value = this.JobId
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams);
            return rowsAffected;
        }

        public int VerifyJob()
        {
            string sql = "UPDATE job" +
                         " SET job_status = 'Verified'" +
                         " WHERE job_id = @JobId";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@JobId", DbType.Int32)
            {
                Value = this.JobId
            };
            int rowsAffected = _db.ExecuteNonQuery(sql, objParams);
            return rowsAffected;
        }
    }
}
