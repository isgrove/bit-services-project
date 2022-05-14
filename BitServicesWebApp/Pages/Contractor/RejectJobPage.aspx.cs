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
    public partial class RejectJobPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ContractorId"] != null)
                {
                    if (Session["JobId"] != null)
                    {
                        ButtonManager buttonManager = new ButtonManager();
                        buttonManager.UpdateButtons(Master, "Contractor", true);

                        DataTable rejectionReasonsTable = new Jobs().AllRejectionReasons();

                        ddlReason.DataSource = rejectionReasonsTable;
                        ddlReason.DataValueField = "reason";
                        ddlReason.DataTextField = "reason";
                        ddlReason.DataBind();
                        ddlReason.SelectedIndex = 0;
                    }
                    else
                    {
                        Response.Redirect("~/Pages/Contractor/AssignedJobsPage.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Pages/LoginPage.aspx");
                }
            }
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Contractor/AssignedJobsPage.aspx");
        }

        protected void btnConfirm_OnClick(object sender, EventArgs e)
        {
            if (ddlReason.SelectedIndex != 0)
            {
                Contractor currentContractor = new Contractor()
                {
                    ContractorId = Convert.ToInt32(Session["ContractorId"])
                };

                int jobId = Convert.ToInt32(Session["JobId"].ToString());
                string reason = ddlReason.SelectedValue;
                string description = txtDescription.Text;

                int returnValue = currentContractor.RejectJob(jobId, reason, description);
                if (returnValue > 0)
                {
                    Session.Remove("JobId");
                    Response.Write("<script>alert('You have successfully reject the job')</script>");
                    Response.Redirect("~/Pages/Contractor/AssignedJobsPage.aspx");
                }
                else
                {
                    Response.Write("<script> alert('Job could not be reject, please try again')</script>");
                }
            }
        }
    }
}