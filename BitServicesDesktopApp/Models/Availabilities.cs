using BitServicesDesktopApp.DAL;
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
            SQLHelper helper = new SQLHelper();
            string sql = "SELECT availability_date, contractor_id" +
                " FROM contractor_availability" +
                " WHERE availability_date >= CONVERT(char(10), GetDate(),126)";
            DataTable availabilityTable = helper.ExecuteSQL(sql);
            foreach (DataRow dr in availabilityTable.Rows)
            {
                Availability newAvailability = new Availability(dr);
                this.Add(newAvailability);
            }
        }
        public Availabilities(int contractorId)
        {
            SQLHelper db = new SQLHelper();
            string sql = "SELECT availability_date, contractor_id" +
                " FROM contractor_availability" +
                " WHERE contractor_id = @ContractorId" +
                " AND availability_date >= CONVERT(char(10), GetDate(),126)";
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@ContractorId", DbType.Int32);
            objParams[0].Value = contractorId;
            DataTable availabilityTable = db.ExecuteSQL(sql, objParams);
            foreach (DataRow dr in availabilityTable.Rows)
            {
                Availability newAvailability = new Availability(dr);
                this.Add(newAvailability);
            }
        }
    }
}
