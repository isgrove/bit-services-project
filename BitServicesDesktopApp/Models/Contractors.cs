using BitServicesDesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BitServicesApp.Models
{
    public class Contractors : List<Contractor>
    {
        public Contractors()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "SELECT contractor_id, first_name, last_name, email, phone, street, suburb, postcode, state" +
                " suburb, postcode, state, licence_number, vehicle_registration, active" +
                " FROM contractor";
            DataTable contractorsTable = helper.ExecuteSQL(sql);
            foreach (DataRow dr in contractorsTable.Rows)
            {
                Contractor newContractor = new Contractor(dr);
                this.Add(newContractor);
            }
        }
    }
}
