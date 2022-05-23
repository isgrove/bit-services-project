using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BitServicesWebApp.BLL;

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
                    new ButtonManager().UpdateButtons(Master, "Client", true);

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
            int clientId = Convert.ToInt32(Session["ClientId"]);
            BLL.Client currentClient = new BLL.Client()
            {
                ClientId = clientId
            };

            DataTable allJobs = currentClient.AllJobs();

            gvCompletedBookings.DataSource = allJobs.DefaultView;
            gvCompletedBookings.DataBind();

            if (allJobs.Rows.Count == 0)
            {
                pnlNoJobs.CssClass = pnlNoJobs.CssClass.Replace("d-none", "").Trim();
            }
        }

        protected void lbtnNewJob_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Client/NewJobPage.aspx");
        }
    }
}