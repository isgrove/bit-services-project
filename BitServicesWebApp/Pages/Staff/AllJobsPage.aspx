<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllJobsPage.aspx.cs" Inherits="BitServicesWebApp.Pages.Staff.AllJobsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-12">
                <div class="row mb-2">
                    <div class="col">
                        <h3>All jobs</h3>
                    </div>
                    <%-- TODO: add filter to filter jobs via the status (in progress, pending, completed, verified, canceled) and rejected jobs. Rejected jobs will be shown in a seperate GridView --%> 
                    <div class="col text-right">
                        <asp:LinkButton CssClass="btn btn-outline d-none" ID="lbtnAssignContractors"
                            runat="server" OnClick="lbtnAssignContractors_OnClick">
                            Assign Contractors
                            <asp:Label runat="server" ID="lblAssignContractors" CssClass="new"></asp:Label>
                        </asp:LinkButton>
                        <asp:LinkButton CssClass="btn btn-outline ml-4 d-none" ID="lbtnVerifyJobs"
                            runat="server" OnClick="lbtnVerifyJobs_OnClick">
                            Verify Jobs
                            <asp:Label runat="server" ID="lblVerifyJobs" CssClass="new"></asp:Label>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 mx-auto">
                        <asp:Panel runat="server" CssClass="text-center mt-5 d-none" ID="pnlNoJobs">
                            <i class="fa-solid fa-building-circle-exclamation h1 mb-3"></i>
                            <p class="">There are currently no jobs in the system.</p>
                        </asp:Panel>
                        <asp:GridView ID="gvAllJobs"
                            CssClass="table table-striped table-bordered" AutoGenerateColumns="false"
                            runat="server" DataKeyNames="job_id">
                            <Columns>
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                <asp:BoundField DataField="Client Location" HeaderText="Client Location" />
                                <asp:BoundField DataField="Contractor Name" HeaderText="Contractor Name" />
                                <asp:BoundField DataField="Required Skill" HeaderText="Required Skill" />
                                <asp:BoundField DataField="Title" HeaderText="Title" />
                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                <asp:BoundField DataField="Deadline Date" HeaderText="Deadline Date" />
                                <asp:BoundField DataField="Completion Date" HeaderText="Completion Date" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
