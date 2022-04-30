<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompletedJobsPage.aspx.cs" Inherits="BitServicesWebApp.Pages.CompletedJobsPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                <h3> All Completed Bookings</h3>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 mx-auto">
                                <asp:GridView ID="gvCompletedBookings"
                                    CssClass="table table-striped table-bordered"
                                    runat="server" OnRowCommand="gvCompletedBookings_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Job status">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlJobStatus" runat="server">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Complete Action">
                                            <ItemTemplate>
                                                <asp:Button ID="btnComplete" runat="server" Height="40px" Width="80px"
                                                    Text="Reject" CommandName="Complete"
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
        </div>
    </div>
</asp:Content>
