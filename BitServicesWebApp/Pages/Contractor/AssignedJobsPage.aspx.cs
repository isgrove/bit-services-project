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

    public partial class AssignedJobsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ContractorId"] != null)
                {
                    new ButtonHelper().UpdateButtons(Master, "Contractor", true);
                    RefreshGrid();

                    Contractor currentContractor = new Contractor()
                    {
                        ContractorId = Convert.ToInt32(Session["ContractorId"])
                    };
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

            DataTable assignedJobs = contractor.AssignedJobs();

            int numberOfAssignedJobs = assignedJobs.Rows.Count;

            if (numberOfAssignedJobs == 0)
            {
                Response.Redirect("~/Pages/Contractor/AcceptedJobsPage.aspx");
            }

            gvAssignedJobs.DataSource = assignedJobs.DefaultView;
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

            var varJobId = gvAssignedJobs.DataKeys[rowIndex]?.Value;

            if (varJobId != null)
            {
                int jobId = (int)varJobId;

                if (e.CommandName == "Accept")
                {
                    DropDownList ddlCompletionDate = ((DropDownList)row.FindControl("ddlCompletionDate"));
                    if (ddlCompletionDate.SelectedIndex != 0)
                    {
                        DateTime completionDate = Convert.ToDateTime(ddlCompletionDate.SelectedValue);
                        currentContractor.AcceptJob(jobId, completionDate);
                    }
                }

                if (e.CommandName == "Reject")
                {
                    Session.Add("JobId", jobId);
                    Response.Redirect("~/Pages/Contractor/RejectJobPage.aspx");
                }

                RefreshGrid();
            }
        }

        protected void lbtnBack_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Contractor/AcceptedJobsPage.aspx");
        }

        protected void gvAssignedJobs_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            Contractor currentContractor = new Contractor
            {
                ContractorId = Convert.ToInt32(Session["ContractorId"].ToString())
            };

            DateTime jobDeadlineDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Deadline Date"));
            DropDownList drop = (DropDownList)e.Row.FindControl("ddlCompletionDate");
            if (drop != null)
            {
                DataTable availabilitiesTable = currentContractor.GetAvailabilityForJob(jobDeadlineDate.Date);
                drop.DataSource = availabilitiesTable;
                drop.DataValueField = "Date";
                drop.DataTextField = "Formatted Date";
                drop.DataBind();
                drop.SelectedIndex = 0;

                Response.Write("<script>console.log('Dropdown found')</script>");
                Response.Write($"<script>console.log('Deadline date: {jobDeadlineDate}')</script>");
                Response.Write($"<script>console.log('Deadline type: {jobDeadlineDate.GetType()}')</script>");

                foreach (DataRow x in availabilitiesTable.Rows)
                {
                    Response.Write($"<script>console.log('Avb Date: {x["Date"]}')</script>");
                }
            }

        }
    }
}