﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BitServicesWebApp.BLL;
using BitServicesWebApp.Helpers;

namespace BitServicesWebApp.Pages.Client
{
    public partial class NewJobPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ClientId"] != null)
                {
                    ButtonHelper buttonHelper = new ButtonHelper();
                    buttonHelper.UpdateButtons(Master, "Client", true);

                    ClientLocations clientLocations = new ClientLocations();
                    DataTable locationsTable = clientLocations.AllClientLocations(Convert.ToInt32(Session["ClientId"]));

                    ddlLocation.DataSource = locationsTable;
                    ddlLocation.DataValueField = "location_id";
                    ddlLocation.DataTextField = "suburb";
                    ddlLocation.DataBind();
                    ddlLocation.SelectedIndex = 0;

                    Skills skills = new Skills();
                    DataTable skillsTable = skills.AllSkills();

                    ddlRequiredSkill.DataSource = skillsTable;
                    ddlRequiredSkill.DataValueField = "skill_name";
                    ddlRequiredSkill.DataBind();
                    ddlRequiredSkill.SelectedIndex = 0;
                }
                else
                {
                    Response.Redirect(GetRouteUrl("LoginRoute", null));
                }
            }
        }

        protected void btnCreateJob_OnClick(object sender, EventArgs e)
        {

            if (ddlLocation.SelectedIndex != 0 || ddlRequiredSkill.SelectedIndex != 0)
            {
                Job newJob = new Job
                {
                    Location = new ClientLocation(),
                    Client = new BLL.Client(),
                    AssignedContractor = new Contractor(),
                    Title = txtTitle.Text,
                    Description = txtDescription.Text,
                    DeadlineDate = calDeadlineDate.SelectedDate,
                    RequiredSkill = ddlRequiredSkill.SelectedValue,
                    JobStatus = "Pending"
                };
                newJob.Client.ClientId = Convert.ToInt32(Session["ClientId"].ToString());
                newJob.Location.LocationId = Convert.ToInt32(ddlLocation.SelectedValue);

                int returnValue = newJob.InsertJob(false);
                if (returnValue > 0)
                {
                    Response.Write("<script>alert('New job created')</script>");
                    Response.Redirect(GetRouteUrl("ClientJobsRoute", null));
                }
                else
                {
                    Response.Write("<script> alert('New job cannot be create, please try again')</script>");
                }
            }
            // TODO: Show error message if no value is selected
        }
    }
}