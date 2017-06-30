<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmTestLogin.aspx.cs" Inherits="pbERP.Module_User.UI.frmTestLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>

    <script src="../../Content/jquery-ui-1.12.1.custom/jquery-ui.min.js"></script>
    <link href="Content/jquery-ui-1.12.1.custom/jquery-ui.min.css" rel="stylesheet" />


    <link href="Content/bootstrap-3.3.7-dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-3.3.7-dist/css/bootstrap-theme.min.css" rel="stylesheet" />
    
   <script src="../../Content/bootstrap-3.3.7-dist/js/bootstrap-datepicker.min.js"></script> 
    <style type="text/css">
        .logo {
            float: right;
            top: -180px;
            position: relative;
            left:20%;
        }
        #copyright {
            float:right; 
            margin-top:20px;
            margin-right:5px;
            position:relative;
            font-size:11px;
            color: #808080;
            font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
        }
         #copyright a {
           color: #808080;
           font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
        }
        .loginscreen {
            top:50%; 
            margin-left: 25%; 
            margin-right:25%; 
            background-color:lightgray;
            padding:10px;
            border-radius:10px;
        }
        .tagline {
            font-weight:bold;
            font-size: 12px;
            color:darkgrey;
            font-family:'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
        }
        h1 a:hover {
            color:lightgray;
            text-decoration:none;
        }
        h1 a:link a:visited {
            text-decoration:none;
            color:#ffffff;
        }
        h1 a{
            color:#ffffff;
        }

       

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container" style="padding-top:20px;">
        <div class="jumbotron">
            <h1><a  href="http://moontexgroup.com/index.php">MOON TEX GROUP</a></h1>
            <p class="tagline">House of apparel-clothing & fashion accessories</p>
            <p id="copyright">Copyright &copy; 2017 <a href="http://professionbd.com/">professionbd</a> all rights reserved</p>
         <div>
            
            <img class="logo" src="../../images/bgERP_logo.png" width="150px" height="150px" />
        </div>
        </div>
        <div class="loginscreen" >
            <div style="margin:5px;padding:5px;background-color:whitesmoke;border-radius:7px;">
                <div class="form-group">
                    <asp:TextBox id="txtUserId" CssClass="form-control" placeholder="user id/email" runat="server"></asp:TextBox><br />
                    <asp:TextBox id="txtPassword" runat="server" CssClass="form-control" placeholder="password" TextMode="Password"></asp:TextBox><br />
                    <asp:TextBox id="txtCompany" CssClass="form-control" placeholder="company" runat="server" ></asp:TextBox><br />
                    <asp:TextBox id="txtBranch" CssClass="form-control" placeholder="branch" runat="server"></asp:TextBox><br />
                    <asp:LinkButton id="btnLogin" CssClass="btn btn-success" runat="server"><span class="glyphicon glyphicon-log-in"></span> &nbsp; Login </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
