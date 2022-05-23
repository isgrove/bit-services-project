using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BitServicesWebApp.Helpers
{
    public class ButtonHelper
    {
        public void UpdateButtons(MasterPage master, string userType, bool loggedIn)
        {
            LinkButton login = (LinkButton)master.FindControl("lbtnLogin");
            LinkButton logout = (LinkButton)master.FindControl("lbtnLogout");
            
            login.Visible = !loggedIn;
            logout.Visible = loggedIn;
        }
    }
}