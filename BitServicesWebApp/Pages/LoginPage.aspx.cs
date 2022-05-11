using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BitServicesWebApp.BLL;

namespace BitServicesWebApp.Pages
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_OnClick(object sender, EventArgs e)
        {
            User user = new User()
            {
                Email = txtUserName.Text,
                Password = txtPassword.Text
            };
            int id = user.CheckClient();
            if (id > 0)
            {
                Session.Add("ClientId", id);
                Response.Redirect("HomePage.aspx");
            }
            else
            {
                id = user.CheckContractor();
                if (id > 0)
                {
                    Session.Add("ContractorId", id);
                    Response.Redirect("HomePage.aspx");
                }
                else
                {
                    id = user.CheckStaff();
                    if (id > 0)
                    {
                        Session.Add("StaffId", id);
                        Response.Redirect("HomePage.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('Username or password incorrect')</script>");
                    }
                }
            }
        }
    }
}