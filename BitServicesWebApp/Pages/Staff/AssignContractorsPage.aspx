<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignContractorsPage.aspx.cs" Inherits="BitServicesWebApp.Pages.AssignContractorsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-12">
                <div class="row mb-2">
                    <div class="col">
                        <h3>Assign a contractor to a job</h3>
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
                        <asp:GridView ID="gvUnassignedJobs"
                            CssClass="table table-striped table-bordered" AutoGenerateColumns="false"
                            OnRowCommand="gvUnassignedJobs_OnRowCommand" OnRowDataBound="gvUnassignedJobs_OnRowDataBound" runat="server" DataKeyNames="job_id">
                            <Columns>
                                <asp:BoundField DataField="Location Suburb" HeaderText="Client Location" />
                                <asp:BoundField DataField="Job Skill" HeaderText="Job Skill" />
                                <asp:BoundField DataField="Job Title" HeaderText="Title" />
                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                <asp:BoundField DataField="Deadline Date" HeaderText="Deadline Date" />
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlContractors" AppendDataBoundItems="True"
                                                          runat="server">
                                            <asp:ListItem Text="Select Contractor" Value="select" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button ID="btnAssignContractor" runat="server" Height="40px" Width="100px"
                                            Text="Assign" CommandName="AssignContractor" CssClass="btn"
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
