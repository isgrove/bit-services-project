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
    public partial class AcceptedJobsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ContractorId"] != null)
                {
                    new ButtonHelper().UpdateButtons(Master, "Contractor", true);
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
            Contractor contractor = new Contractor()
            {
                ContractorId = Convert.ToInt32(Session["ContractorId"])
            };

            DataTable acceptedJobs = contractor.AcceptedJobs();

            gvAcceptedJobs.DataSource = acceptedJobs.DefaultView;
            gvAcceptedJobs.DataBind();

            int numberOfAssignedJobs = contractor.AssignedJobs().Rows.Count;
            Response.Write($"<script> console.log('Assigned jobs: {numberOfAssignedJobs}') </script>");
            if (numberOfAssignedJobs > 0)
            {
                lbtnPendingJobs.CssClass = lbtnPendingJobs.CssClass.Replace("d-none", "").Trim();
                lblPendingJobs.Text = numberOfAssignedJobs.ToString();
            }

            if (acceptedJobs.Rows.Count == 0)
            {
                pnlNoJobs.CssClass = pnlNoJobs.CssClass.Replace("d-none", "").Trim();
            }
        }

        protected void gvAcceptedJobs_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            Contractor currentContractor = new Contractor
            {
                ContractorId = Convert.ToInt32(Session["ContractorId"].ToString())
            };

            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvAcceptedJobs.Rows[rowIndex];

            if (e.CommandName == "Complete")
            {
                var varJobId = gvAcceptedJobs.DataKeys[rowIndex]?.Value;
                string strKilometers = ((TextBox)row.FindControl("txtKilometers")).Text.Trim();
                if (int.TryParse(strKilometers, out int kilometers) && varJobId != null)
                {
                    int jobId = (int)varJobId;
                    currentContractor.CompleteJob(jobId, kilometers);
                }
            }

            RefreshGrid();
        }

        protected void lbtnPendingJobs_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Contractor/AssignedJobsPage.aspx");
        }
    }
}