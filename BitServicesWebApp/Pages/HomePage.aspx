<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="BitServicesWebApp.Page.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <div class="container">
            <h1>Bit Services</h1>
            <p class="lead">Providing IT services to over 2,500 satisfied customers!</p>
            <p><asp:LinkButton CssClass="btn btn-primary" ID="lbtnLogin"
                                runat="server">
                            Login
                            </asp:LinkButton></p>
        </div>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-4">
                <img src="../Images/JBHiFiLogo.jpg" height="50" style="margin-bottom: 20px"/>
                <p class="text-justify">
                    Bit Services has made our lives so much easier with their superb service and support.
                    Their communication is always refreshing and they've always been up to our expectations
                    when it comes to quality and delivery of the projects.
                </p>
            </div>
            <div class="col-4">
                <img src="../Images/WoolworthsLogo.png" height="50" style="margin-bottom: 20px">
                <p class="text-justify">
                    We are never been disappointed with Bit Services. They solve most of the technical issues in blink of an eye. Highly technical team.
                </p>
            </div>
            <div class="col-4">
                <img src="../Images/TargetLogo.png" height="50" style="margin-bottom: 20px"/>
                <p class="text-justify">
                    Bit Services are excellent. Always so helpful and they go above and beyond to assist. They always respond very quickly and can resolve any issue.
                </p>
            </div>
        </div>
    </div>

</asp:Content>