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
            Contractor contractor = new Contractor()
            {
                ContractorId = Convert.ToInt32(Session["ContractorId"])
            };

            List<String> filters = GetActiveFilters();
            DataTable[] filteredJobs = contractor.AllJobs(filters);

            DataTable allJobs = filteredJobs[0];
            gvAllJobs.DataSource = allJobs.DefaultView;
            gvAllJobs.DataBind();

            if (allJobs.Rows.Count == 0)
            {
                pnlNoJobs.CssClass = pnlNoJobs.CssClass.Replace("d-none", "").Trim();
            }
            else
            {
                pnlNoJobs.CssClass += " d-none";
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

            DataTable inProgressJobs = filteredJobs[2];
            gvInProgressJobs.DataSource = inProgressJobs.DefaultView;
            gvInProgressJobs.DataBind();

            if (inProgressJobs.Rows.Count == 0)
            {
                pnlNoInProgressJobs.CssClass = pnlNoInProgressJobs.CssClass.Replace("d-none", "").Trim();
            }
            else
            {
                pnlNoInProgressJobs.CssClass += " d-none";
            }
        }

        private void RefreshButtons()
        {
            Contractor contractor = new Contractor()
            {
                ContractorId = Convert.ToInt32(Session["ContractorId"])
            };
            int numberOfAssignedJobs = contractor.AssignedJobs().Rows.Count;
            if (numberOfAssignedJobs > 0)
            {
                lbtnPendingJobs.CssClass = lbtnPendingJobs.CssClass.Replace("d-none", "").Trim();

                txtPendingJobs.Text = "Pending Jobs ";
                lblPendingJobs.Text = numberOfAssignedJobs.ToString();
            }
            else if (!lbtnPendingJobs.CssClass.Contains("d-none"))
            {
                lbtnPendingJobs.CssClass += " d-none";
            }
        }

        private List<string> GetActiveFilters()
        {
            CheckBox[] checkBoxes = { cbPending, cbIn_Progress, cbCompleted, cbVerified, cbRejected };
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
                return new List<string>() { "Pending", "In Progress", "Completed", "Verified", "Rejected" };
            }
            return activeFilters;
        }

        protected void gvInProgressJobs_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            Contractor currentContractor = new Contractor
            {
                ContractorId = Convert.ToInt32(Session["ContractorId"].ToString())
            };

            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvInProgressJobs.Rows[rowIndex];

            if (e.CommandName == "Complete")
            {
                var varJobId = gvInProgressJobs.DataKeys[rowIndex]?.Value;
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
            Response.Redirect(GetRouteUrl("ContractorAssignedJobsRoute", null));
        }

        protected void lbtnApplyFilters_OnClick(object sender, EventArgs e)
        {
            RefreshGrid();
        }
    }
}