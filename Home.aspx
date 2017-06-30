<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" MasterPageFile="~/Site.master" Inherits="pbERP.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .logo {
            float: right;
            top: -180px;
            position: relative;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
    <div class="jumbotron">
        <h1>ERP</h1>
        <p>Enterprise Resource Planning</p>
        <div>
            <img class="logo" src="images/bgERP_logo.png" width="200px" height="200px" />
        </div>
    </div>
        </div>
    
</asp:Content>