using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BitServicesWebApp.BLL
{
    public class ButtonManager
    {
        public void UpdateButtons(MasterPage master, string userType, bool loggedIn)
        {
            LinkButton signUp = (LinkButton)master.FindControl("lbtnSignUp");
            LinkButton login = (LinkButton)master.FindControl("lbtnLogin");
            LinkButton logout = (LinkButton)master.FindControl("lbtnLogout");

            signUp.Visible = !loggedIn;
            login.Visible = !loggedIn;
            logout.Visible = loggedIn;
        }
    }
}