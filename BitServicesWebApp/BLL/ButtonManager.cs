using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BitServicesWebApp.BLL
{
    // TODO: Rename to ButtonManager to ButtonHelper
    // TODO: Rename DAL to Helper
    // TODO: Move ButtonHelper into Helper directory
    public class ButtonManager
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