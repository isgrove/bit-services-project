<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="BitServicesWebApp.Pages.LoginPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
          <div class="container">
       <div class="row">
           <div class="col-md-6 mx-auto">
               <div class="card">
                   <div class="card-body">
                       <div class="row">
<%--                           <div class="col">
                               <img width="150px" src="../Images/login.png" />
                           </div>--%>
                       </div>                       
                       <div class="row">
                           <div class="col text-center">
                                    <h3> Member Login</h3>
                           </div>
                       </div>
                       <div class="row">
                           <div class="col">
                             <hr />
                           </div>
                       </div>
                       <div class="row">
                           <div class="col">
                               <label> Username</label>
                               <div class="form-group">
                                   <asp:TextBox CssClass="form-control" ID="txtUserName"
                                       runat="server" placeholder="Username">
                                   </asp:TextBox>
                               </div>
                               <label> Password</label>
                               <div class="form-group">
                                   <asp:TextBox CssClass="form-control" ID="txtPassword"
                                       runat="server" placeholder="Password" 
                                       TextMode="Password">
                                   </asp:TextBox>
                               </div>
                               <div class="form-group">
                                   <asp:Button CssClass="btn btn-success btn-block btn-lg"
                                       runat="server" Text="Login" ID="btnLogin" 
                                       OnClick="btnLogin_OnClick"/>
                               </div>
                                <div class="form-group">
                                   <asp:Button CssClass="btn btn-info btn-block btn-lg"
                                       runat="server" Text="Sign Up" ID="btnSignUp" />
                               </div>
                           </div>
                       </div>

                   </div>
               </div>
           </div>
       </div>
   </div>
</asp:Content>
