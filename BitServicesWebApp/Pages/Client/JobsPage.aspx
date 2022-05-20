<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="JobsPage.aspx.cs" Inherits="BitServicesWebApp.Pages.AllJobsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-12">
                <div class="row mb-2">
                    <div class="col">
                        <h3>All your jobs</h3>
                    </div>
                    <div class="col text-right">
                        <asp:LinkButton CssClass="btn btn-primary" ID="lbtnNewJob"
                            runat="server" OnClick="lbtnNewJob_OnClick">
                                    New Job
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 mx-auto">
                        <asp:GridView ID="gvCompletedBookings" AutoGenerateColumns="false"
                            CssClass="table table-striped table-bordered"
                            runat="server">
                            <Columns>
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                <asp:BoundField DataField="Client Location" HeaderText="Location Suburb" />
                                <asp:BoundField DataField="Required Skill" HeaderText="Job Skill" />
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
