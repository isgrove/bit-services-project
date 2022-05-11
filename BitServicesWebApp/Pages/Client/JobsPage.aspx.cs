using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BitServicesDesktopApp.Models;
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
                    Response.Redirect("LoginPage.aspx");
                }
            }
        }
        private void RefreshGrid()
        {
            ClientJobs clientJobs = new ClientJobs();
            gvCompletedBookings.DataSource = clientJobs.AllCompletedBookings().DefaultView;
            gvCompletedBookings.DataBind();
        }
    }
}