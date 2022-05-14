﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BitServicesWebApp.BLL;

namespace BitServicesWebApp.Pages
{
    public partial class AssignContractorsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["StaffId"] != null)
                {
                    new ButtonManager().UpdateButtons(Master, "Staff", true);
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
            DataTable assignedJobs = new Jobs().AllUnasignedJobs();

            int numberOfAssignedJobs = assignedJobs.Rows.Count;

            if (numberOfAssignedJobs == 0)
            {
                Response.Redirect("~/Pages/Staff/AllJobsPage.aspx");
            }

            gvUnassignedJobs.DataSource = assignedJobs.DefaultView;
            gvUnassignedJobs.DataBind();
        }

        protected void gvUnassignedJobs_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvUnassignedJobs.Rows[rowIndex];

            if (e.CommandName == "AssignContractor")
            {
                var varJobId = gvUnassignedJobs.DataKeys[rowIndex]?.Value;
                DropDownList ddlContractors = ((DropDownList)row.FindControl("ddlContractors"));
                if (varJobId != null && ddlContractors.SelectedIndex != 0)
                {
                    int jobId = (int)varJobId;
                    int staffId = Convert.ToInt32(Session["StaffId"].ToString());
                    int contractorId = Convert.ToInt32(ddlContractors.SelectedValue);

                    Job currentJob = new Job()
                    {
                        JobId = jobId
                    };
                    currentJob.AssignContractor(contractorId, staffId);
                }
            }

            RefreshGrid();
        }


        protected void gvUnassignedJobs_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItemIndex >= 0)
            {
                Job currentJob = new Job
                {
                    JobId = Convert.ToInt32(gvUnassignedJobs.DataKeys[e.Row.DataItemIndex]?.Value)
                };

                DropDownList drop = (DropDownList)e.Row.FindControl("ddlContractors");
                if (drop != null)
                {
                    DataTable contractorsTable = currentJob.GetContractorsForJob();
                    drop.DataSource = contractorsTable;
                    drop.DataValueField = "contractor_id";
                    drop.DataTextField = "Contractor Name";
                    drop.DataBind();
                    drop.SelectedIndex = 0;
                }
            }
        }

        protected void lbtnBack_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Staff/AllJobsPage.aspx");
        }
    }
}