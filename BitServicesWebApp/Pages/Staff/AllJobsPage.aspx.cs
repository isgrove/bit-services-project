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
                    RefreshButtons();
                }
                else
                {
                    Response.Redirect(GetRouteUrl("LoginRoute", null));
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setButtonStyle();", true);
                RefreshButtons();
            }
        }

        private void RefreshGrid()
        {
            FilterHelper filterHelper = new FilterHelper();
            Jobs jobs = new Jobs();
            
            List<String> filters = GetActiveFilters();
            DataTable[] filteredJobs = filterHelper.GetFilteredJobs(filters);

            DataTable allJobs = filteredJobs[0];
            gvAllJobs.DataSource = allJobs.DefaultView;
            gvAllJobs.DataBind();

            if (allJobs.Rows.Count == 0)
            {
                pnlNoJobs.CssClass = pnlNoJobs.CssClass.Replace("d-none", "").Trim();
            }
            else
            {
                pnlNoJobs.CssClass += " d-done";
            }

            DataTable rejectedJobs = filteredJobs[1];
            gvRejectedJobs.DataSource = rejectedJobs.DefaultView;
            gvRejectedJobs.DataBind();

            if (rejectedJobs.Rows.Count == 0)
            {
                pnlNoRejectedJobs.CssClass = pnlNoRejectedJobs.CssClass.Replace("d-none", "").Trim();
            }
            else
            {
                pnlNoRejectedJobs.CssClass += " d-none";
            }
        }

        private void RefreshButtons()
        {
            Jobs jobs = new Jobs();
            int numberOfUnassignedJobs = jobs.AllUnasignedJobs().Rows.Count;
            if (numberOfUnassignedJobs > 0)
            {
                lbtnAssignContractors.CssClass = lbtnAssignContractors.CssClass.Replace("d-none", "").Trim();

                txtAssignContractors.Text = "Assign Contractors ";
                lblAssignContractors.Text = numberOfUnassignedJobs.ToString();
            }
            else if (!lbtnAssignContractors.CssClass.Contains("d-none"))
            {
                lbtnAssignContractors.CssClass += " d-none";
            }

            int numberOfCompletedJobs = jobs.AllCompletedJobs().Rows.Count;
            if (numberOfCompletedJobs > 0)
            {
                lbtnVerifyJobs.CssClass = lbtnVerifyJobs.CssClass.Replace("d-none", "").Trim();
                txtVerifyJobs.Text = "Verify Jobs ";
                lblVerifyJobs.Text = numberOfCompletedJobs.ToString();
            }
            else if (!lbtnVerifyJobs.CssClass.Contains("d-none"))
            {
                lbtnVerifyJobs.CssClass += " d-none";
            }
        }

        private List<string> GetActiveFilters()
        {
            CheckBox[] checkBoxes = { cbPending, cbIn_Progress, cbCompleted, cbVerified, cbRejected, cbCanceled };
            List<string> activeFilters = new List<string>();

            foreach (CheckBox checkBox in checkBoxes)
            {
                if (checkBox.Checked)
                {
                    string filterName = checkBox.ID.Replace("cb", "").Replace("_", " ");
                    activeFilters.Add(filterName);
                }
            }
            // If not filters are "active" we want to activate all filters
            if (activeFilters.Count == 0)
            {
                return new List<string>() { "Pending", "In Progress", "Completed", "Verified", "Rejected", "Canceled" };
            }
            return activeFilters;
        }

        protected void lbtnApplyFilters_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        protected void lbtnAssignContractors_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(GetRouteUrl("StaffAssignJobsRoute", null));
        }

        protected void lbtnVerifyJobs_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(GetRouteUrl("StaffVerifyJobsRoute", null));
        }

        protected void lbtnFilter_OnClick(object sender, EventArgs e)
        {

        }
    }
}