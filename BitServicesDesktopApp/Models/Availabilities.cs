using BitServicesDesktopApp.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitServicesDesktopApp.Models
{
    public class Availabilities : List<Availability>
    {
        public Availabilities()
        {
            DataTable availabilityTable;
            SQLHelper helper = new SQLHelper();
            string sql = "SELECT availability_date, contractor_id" +
                " FROM contractor_availability" +
                " WHERE availability_date >= CONVERT(char(10), GetDate(),126)";
            try
            {
                availabilityTable = helper.ExecuteSQL(sql);
            }
            catch (Exception ex)
            {
                StringBuilder errorMessage = new StringBuilder();
                errorMessage.Append("Availabilities() caused previous error.\n" +
                                    "Exception: " + ex.Message + "\n");
                new LogHelper().Log(errorMessage.ToString(), LogType.Error);
                return;
            }
            foreach (DataRow dr in availabilityTable.Rows)
            {
                Availability newAvailability = new Availability(dr);
                this.Add(newAvailability);
            }
        }
        public Availabilities(int contractorId)
        {
            DataTable availabilityTable;
            SQLHelper db = new SQLHelper();
            string sql = "SELECT availability_date, contractor_id" +
                " FROM contractor_availability" +
                " WHERE contractor_id = @ContractorId" +
                " AND availability_date >= CONVERT(char(10), GetDate(),126)";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@ContractorId", DbType.Int32);
            objParams[0].Value = contractorId;
            try
            {
                availabilityTable = db.ExecuteSQL(sql, objParams);
                foreach (DataRow dr in availabilityTable.Rows)
                {
                    Availability newAvailability = new Availability(dr);
                    this.Add(newAvailability);
                }
            }
            catch (Exception ex)
            {
                StringBuilder errorMessage = new StringBuilder();
                errorMessage.Append("Availabilities(contractorId) caused previous error:\n" +
                                    "Exception: " + ex.Message + "\n" +
                                    "SqlParameters:\n");
                foreach (SqlParameter sqlParameter in objParams)
                {
                    errorMessage.Append($"- {sqlParameter.ParameterName} {sqlParameter.DbType}: {sqlParameter.Value}\n");
                }
                new LogHelper().Log(errorMessage.ToString(), LogType.Error);
                return;
            }
        }
    }
}
