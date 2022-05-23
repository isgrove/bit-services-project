using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BitServicesWebApp.BLL;
using BitServicesWebApp.Helpers;

namespace BitServicesWebApp.Pages.Staff
{
    public partial class AllJobsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["StaffId"] != null)
                {
                    new ButtonHelper().UpdateButtons(Master, "Staff", true);
                    RefreshGrid();
                }
                else
                {
                    Response.Redirect("~/Pages/LoginPage.aspx");
                }
            }
        }

        private void RefreshGrid()
        {
            Jobs jobs = new Jobs();

            DataTable allJobs = jobs.AllJobs();

            gvAllJobs.DataSource = allJobs.DefaultView;
            gvAllJobs.DataBind();

            if (allJobs.Rows.Count == 0)
            {
                pnlNoJobs.CssClass = pnlNoJobs.CssClass.Replace("d-none", "").Trim();
            }

            int numberOfUnassignedJobs = jobs.AllUnasignedJobs().Rows.Count;
            if (numberOfUnassignedJobs > 0)
            {
                lbtnAssignContractors.CssClass = lbtnAssignContractors.CssClass.Replace("d-none", "").Trim();
                lblAssignContractors.Text = numberOfUnassignedJobs.ToString();
            }

            int numberOfCompletedJobs = jobs.AllCompletedJobs().Rows.Count;
            if (numberOfCompletedJobs > 0)
            {
                lbtnVerifyJobs.CssClass = lbtnVerifyJobs.CssClass.Replace("d-none", "").Trim();
                lblVerifyJobs.Text = numberOfCompletedJobs.ToString();
            }
        }

        protected void lbtnAssignContractors_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Staff/AssignContractorsPage.aspx");
        }

        protected void lbtnVerifyJobs_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Staff/CompletedJobsPage.aspx");
        }
    }
}