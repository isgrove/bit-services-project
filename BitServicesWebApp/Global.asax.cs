using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace BitServicesWebApp
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }
        void RegisterRoutes(RouteCollection routes)
        {
            // Global pages
            routes.MapPageRoute(
                "HomeRoute",
                "",
                "~/Pages/HomePage.aspx"
            );
            routes.MapPageRoute(
                "LoginRoute",
                "login",
                "~/Pages/LoginPage.aspx"
            );
            // Staff pages
            routes.MapPageRoute(
                "StaffJobsRoute",
                "staff/jobs",
                "~/Pages/Staff/AllJobsPage.aspx"
            );
            routes.MapPageRoute(
                "StaffAssignJobsRoute",
                "staff/jobs/assign",
                "~/Pages/Staff/AssignContractorsPage.aspx"
            );
            routes.MapPageRoute(
                "StaffVerifyJobsRoute",
                "staff/jobs/verify",
                "~/Pages/Staff/CompletedJobsPage.aspx"
            );
            // Client pages
            routes.MapPageRoute(
                "ClientJobsRoute",
                "client/jobs",
                "~/Pages/Client/JobsPage.aspx"
            );
            routes.MapPageRoute(
                "ClientNewJobRoute",
                "client/jobs/new",
                "~/Pages/Client/NewJobPage.aspx"
            );
            // Contractor pages
            routes.MapPageRoute(
                "ContractorJobsRoute",
                "contractor/jobs",
                "~/Pages/Contractor/AcceptedJobsPage.aspx"
            );
            routes.MapPageRoute(
                "ContractorAssignedJobsRoute",
                "contractor/jobs/requests",
                "~/Pages/Contractor/AssignedJobsPage.aspx"
            );
            routes.MapPageRoute(
                "ContractorRejectJobRoute",
                "contractor/jobs/requests/reject",
                "~/Pages/Contractor/RejectJobPage.aspx"
            );
        }
    }
}