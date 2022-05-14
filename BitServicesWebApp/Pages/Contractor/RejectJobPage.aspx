<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RejectJobPage.aspx.cs" Inherits="BitServicesWebApp.Pages.RejectJobPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col">
                <h3 id="title">Reject a job</h3>
            </div>
        </div>
        <div class="row d-inline p-0 m-0">
            <div class="col p-0">
                <label>Rejection Reason</label>
                <div class="form-group">
                    <asp:DropDownList CssClass="form-control" ID="ddlReason" AppendDataBoundItems="True"
                        runat="server">
                        <asp:ListItem Text="Select" Value="select" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col p-0">
                <label>Description (Max 100 chars)</label>
                <div class="form-group">
                    <asp:TextBox CssClass="form-control resize-none" ID="txtDescription" runat="server"
                        placeholder="Rejection Description" Rows="5" TextMode="MultiLine" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:Button CssClass="btn btn-primary btn-block"
                    ID="btnConfirm" runat="server" Text="Confirm Rejection" OnClick="btnConfirm_OnClick" />
            </div>
            <div class="col">
                <asp:Button CssClass="btn btn-outline btn-block"
                    ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_OnClick" />
            </div>
        </div>
    </div>
</asp:Content>
