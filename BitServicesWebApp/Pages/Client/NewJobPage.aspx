<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewJobPage.aspx.cs" Inherits="BitServicesWebApp.Pages.Client.NewJobPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-12">
                <div class="row">
                    <div class="col">
                        <h3>Create a new job</h3>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <div class="row d-inline">
                            <div class="col">
                                <label>Required Skill</label>
                                <div class="form-group">
                                    <asp:DropDownList CssClass="form-control" ID="ddlRequiredSkill" AppendDataBoundItems="True"
                                                      runat="server">
                                        <asp:ListItem Text="Select" Value="select" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col">
                                <label>Deadline Date</label>
                                <div class="form-group">
                                    <asp:Calendar ID="calDeadlineDate" runat="server"></asp:Calendar>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-8">
                        <div class="row d-inline">
                            <div class="col">
                                <label>Job Location</label>
                                <div class="form-group">
                                    <asp:DropDownList CssClass="form-control" ID="ddlLocation" AppendDataBoundItems="True"
                                                      runat="server">
                                        <asp:ListItem Text="Select" Value="select"/>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col">
                                <label>Job Title (Max 64 chars)</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control resize-none" ID="txtTitle" runat="server"
                                                 placeholder="Job Title"/>
                                </div>
                            </div>
                            <div class="col">
                                <label>Job Description (Max 1000 chars)</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control resize-none h-100" ID="txtDescription" runat="server"
                                                 placeholder="Job Description" Rows="5" TextMode="MultiLine"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 mx-auto">
                        <center>
                            <asp:Button CssClass="btn btn-primary btn-block"
                                ID="btnCreateJob" runat="server" Text="Create Job" OnClick="btnCreateJob_OnClick"/>
                        </center>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
