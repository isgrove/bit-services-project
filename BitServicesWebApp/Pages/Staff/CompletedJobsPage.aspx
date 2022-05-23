<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompletedJobsPage.aspx.cs" Inherits="BitServicesWebApp.Pages.CompletedJobsPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="container">
        <div class="row">
            <div class="col-12">
                <div class="row mb-2">
                    <div class="col">
                        <h3>Jobs to be verified</h3>
                    </div>
                    <div class="col text-right">
                        <asp:LinkButton CssClass="btn btn-outline" ID="lbtnBack"
                                                      runat="server" OnClick="lbtnBack_OnClick">
                            Go Back
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 mx-auto">
                        <asp:GridView ID="gvCompletedJobs"
                                      CssClass="table table-striped table-bordered" AutoGenerateColumns="false"
                                      OnRowCommand="gvCompletedJobs_OnRowCommand" runat="server" DataKeyNames="job_id">
                            <Columns>
                                <asp:BoundField DataField="Client Location" HeaderText="Client Location"/>
                                <asp:BoundField DataField="Contractor Name" HeaderText="Contractor Name"/>
                                <asp:BoundField DataField="Description" HeaderText="Description"/>
                                <asp:BoundField DataField="Deadline Date" HeaderText="Deadline Date"/>
                                <asp:BoundField DataField="Completion Date" HeaderText="Completion Date"/>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button ID="btnVerifyJob" runat="server" Height="40px" Width="100px"
                                                    Text="Verify Job" CommandName="VerifyJob" CssClass="btn"
                                                    CommandArgument="<%#Container.DataItemIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
