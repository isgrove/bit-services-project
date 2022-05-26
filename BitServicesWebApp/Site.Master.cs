using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BitServicesWebApp
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbtnLogin_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(GetRouteUrl("LoginRoute", null));
        }

        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect(GetRouteUrl("LoginRoute", null));
        }

        protected void OnClick(object sender, EventArgs e)
        {
            Response.Redirect(GetRouteUrl("HomeRoute", null));
        }
    }
}