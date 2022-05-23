using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BitServicesWebApp.BLL;
using BitServicesWebApp.Helpers;

namespace BitServicesWebApp.Pages
{
    public partial class CompletedJobsPage : System.Web.UI.Page
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
            DataTable completedJobs = new Jobs().AllCompletedJobs();

            int numberOfCompletedJobs = completedJobs.Rows.Count;

            if (numberOfCompletedJobs == 0)
            {
                Response.Redirect("~/Pages/Staff/AllJobsPage.aspx");
            }

            gvCompletedJobs.DataSource = completedJobs;
            gvCompletedJobs.DataBind();
        }
        protected void gvCompletedJobs_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {

            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvCompletedJobs.Rows[rowIndex];

            if (e.CommandName == "VerifyJob")
            {
                var varJobId = gvCompletedJobs.DataKeys[rowIndex]?.Value;
                if (varJobId != null)
                {
                    int jobId = (int)varJobId;
                    Job currentJob = new Job()
                    {
                        JobId = jobId
                    };
                    currentJob.VerifyJob();
                }
            }
            RefreshGrid();
        }

        protected void lbtnBack_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Staff/AllJobsPage.aspx");
        }
    }
}