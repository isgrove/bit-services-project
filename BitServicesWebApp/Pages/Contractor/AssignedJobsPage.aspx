<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignedJobsPage.aspx.cs" Inherits="BitServicesWebApp.Pages.AssignedJobsPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-12">
                <div class="row mb-2">
                    <div class="col">
                        <h3>All your assigned jobs</h3>
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
                        <asp:GridView ID="gvAssignedJobs"
                                      CssClass="table table-striped table-bordered" AutoGenerateColumns="false"
                                      OnRowCommand="gvAssignedJobs_OnRowCommand" OnRowDataBound="gvAssignedJobs_OnRowDataBound" runat="server" DataKeyNames="job_id">
                            <Columns>
                                <asp:BoundField DataField="Location Suburb" HeaderText="Location Suburb"/>
                                <asp:BoundField DataField="Status" HeaderText="Status"/>
                                <asp:BoundField DataField="Job Skill" HeaderText="Job Skill"/>
                                <asp:BoundField DataField="Description" HeaderText="Description"/>
                                <asp:BoundField DataField="Deadline Date" HeaderText="	Deadline Date"/>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlCompletionDate" AppendDataBoundItems="True"
                                                          runat="server">
                                            <asp:ListItem Text="Select Date" Value="select" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button ID="btnAccept" runat="server" Height="40px" Width="120px"
                                                    Text="Accept Job" CommandName="Accept" CssClass="btn"
                                                    CommandArgument="<%#Container.DataItemIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button ID="btnReject" runat="server" Height="40px" Width="120px"
                                                    Text="Reject Job" CommandName="Reject" CssClass="btn"
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
