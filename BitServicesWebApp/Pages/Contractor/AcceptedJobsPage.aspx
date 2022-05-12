<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AcceptedJobsPage.aspx.cs" Inherits="BitServicesWebApp.Pages.AcceptedJobsPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-12">
                <div class="row mb-2">
                    <div class="col">
                        <h3>All your accepted jobs</h3>
                    </div>
                    <div class="col text-right">
                        <asp:LinkButton CssClass="btn btn-outline d-none" ID="lbtnPendingJobs"
                                        runat="server" OnClick="lbtnPendingJobs_OnClick">
                            Pending Jobs
                            <asp:Label runat="server" ID="lblPendingJobs" CssClass="new"></asp:Label>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 mx-auto">
                        <asp:GridView ID="gvAcceptedJobs"
                                      CssClass="table table-striped table-bordered" AutoGenerateColumns="false"
                                      OnRowCommand="gvAcceptedJobs_OnRowCommand" runat="server" DataKeyNames="job_id">
                            <Columns>
                                <asp:BoundField DataField="Location Suburb" HeaderText="Location Suburb"/>
                                <asp:BoundField DataField="Status" HeaderText="Status"/>
                                <asp:BoundField DataField="Job Skill" HeaderText="Job Skill"/>
                                <asp:BoundField DataField="Description" HeaderText="Description"/>
                                <asp:BoundField DataField="Deadline Date" HeaderText="	Deadline Date"/>
                                <asp:TemplateField HeaderText="Kilometers">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtKilometers" runat="server" Height="40px" Width="80px"
                                                     Text="0" CommandName="Kilometers" type="number"
                                                     CommandArgument="<%#Container.DataItemIndex %>"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button ID="btnComplete" runat="server" Height="40px" Width="100px"
                                                    Text="Complete" CommandName="Complete" CssClass="btn"
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
