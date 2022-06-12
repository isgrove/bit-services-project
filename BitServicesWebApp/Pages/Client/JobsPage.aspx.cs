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
    public partial class AllJobsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ClientId"] != null)
                {
                    new ButtonHelper().UpdateButtons(Master, "Client", true);

                    RefreshGrid();
                }
                else
                {
                    Response.Redirect(GetRouteUrl("LoginRoute", null));
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setButtonStyle();", true);
            }
        }
        private void RefreshGrid()
        {
            int clientId = Convert.ToInt32(Session["ClientId"]);
            FilterHelper filterHelper = new FilterHelper();

            List<String> filters = GetActiveFilters();
            DataTable filteredJobs = filterHelper.GetFilteredJobs(filters, clientId);

            gvCompletedBookings.DataSource = filteredJobs.DefaultView;
            gvCompletedBookings.DataBind();

            if (filteredJobs.Rows.Count == 0)
            {
                pnlNoJobs.CssClass = pnlNoJobs.CssClass.Replace("d-none", "").Trim();
            }
            else
            {
                pnlNoJobs.CssClass += " d-none";
            }
        }
        
        private List<string> GetActiveFilters()
        {
            CheckBox[] checkBoxes = { cbPending, cbIn_Progress, cbCompleted, cbVerified };
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
                return new List<string>() {"Pending", "In Progress", "Completed", "Verified"};
            }
            return activeFilters;
        }

        protected void lbtnApplyFilters_OnClick(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        protected void lbtnNewJob_OnClick(object sender, EventArgs e)
        {

            Response.Redirect(GetRouteUrl("ClientNewJobRoute", null));
        }
    }
}