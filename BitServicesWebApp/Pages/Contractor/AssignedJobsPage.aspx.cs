using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BitServicesWebApp.BLL;

namespace BitServicesWebApp.Pages
{
    public partial class AssignedJobsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ContractorId"] != null)
                {
                    new ButtonManager().UpdateButtons(Master, "Contractor", true);
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

            int numberOfAssignedJobs = contractor.AssignedJobs().Rows.Count;

            if (numberOfAssignedJobs == 0)
            {
                Response.Redirect("~/Pages/Contractor/AcceptedJobsPage.aspx");
            }

            gvAssignedJobs.DataSource = contractor.AssignedJobs().DefaultView;
            gvAssignedJobs.DataBind();
        }

        protected void gvAssignedJobs_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            Contractor currentContractor = new Contractor
            {
                ContractorId = Convert.ToInt32(Session["ContractorId"].ToString())
            };

            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvAssignedJobs.Rows[rowIndex];

            if (e.CommandName == "Accept")
            {
                var varJobId = gvAssignedJobs.DataKeys[rowIndex]?.Value;
                if (varJobId != null)
                {
                    int jobId = (int)varJobId;
                    currentContractor.AcceptJob(jobId);
                }
            }
            if (e.CommandName == "Reject")
            {
                //TODO Implement Job Rejection
            }

            RefreshGrid();
        }

        protected void lbtnBack_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Contractor/AcceptedJobsPage.aspx");
        }
    }
}