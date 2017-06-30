<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="pbERP.Module_User.UI.frmLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
        <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
        <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
        <link href="../../Content/CSS/bootstrap.css" rel="stylesheet" />
        <link href="../../Content/CSS/style.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server" >
            <div class="U_Form_Upper_Panel top_fixed">
                <div>
                    <h1 class="CompanyNameTitle">
                        <asp:Label runat="server" ID="lblCompanyGroupName" OnLoad="Page_Load"></asp:Label></h1>
                </div>
                <div class="site_tag_left"></div>
                <div class="site_tag_right">
                    <h3>
                        <asp:Label runat="server" ID="lblGroupDialouge" OnLoad="Page_Load"></asp:Label></h3>
                </div>
            </div>
            
            <div class="U_Form_Middle_Pannel">
                <div class="loginWrap">
                    <h2>Sign in to your account</h2>
        
                    <div class="LoginInputArea">
                        <input runat="server" id="txtLoginId" type="text" placeholder="User Name/Email" />
                    </div>
                    <div class="LoginInputArea">
                        <input runat="server" id="txtPass" type="password" placeholder="Password" />
                    </div>
                    <div class="LoginInputArea">
                         <asp:TextBox ID="txtCompanyName" runat="server"  placeholder="Select Company" AutoPostBack="true" Font-Size="12px" Text="Moon Tex" OnTextChanged="txtCompanyName_TextChanged" /><asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    </div>

                    <div class="LoginInputArea">
                         <asp:TextBox ID="txtBranchName" runat="server"   placeholder="Select Branch"  Font-Size="12px" AutoPostBack="true" Text="Dhaka" OnTextChanged="txtBranchName_TextChanged" /><asp:Literal ID="Literal2" runat="server"></asp:Literal>
                    </div>

                    <div class="LoginInputArea timeBx">
                       Your log in time: 09:00 AM to.08:00 PM
                    </div>
                    <div class="LoginInputArea submitBttn">
                        <%--<asp:Button runat="server" id="btnOK" type="submit" value="Sign In" />--%>
                        <asp:Button ID="btnSubmit" Text="Sign In" runat="server" OnClick="btnSubmit_Click"/>
                    </div>
                </div>
            </div>
            <asp:LinkButton runat="server" id="SomeLinkButton" href="http://moontexgroup.com/index.php" CssClass="btn btn-primary btn-sm">Go To Our Website</asp:LinkButton>

            <div class="U_Form_Lower_Pannel clearfix">
                <div class="col-md-4 col-sm-4 col-xs-12">
                <div class="footerCmpName">
                    <h3><asp:Label runat="server" ID="lblCompanyName" OnLoad="CompanyInfoLoad"></asp:Label></h3>
                </div>

                <div class="logoTag">
                    <asp:Label runat="server" Id="lblCompanyDialouge"></asp:Label>
                </div>
                <address>
                   <div> <asp:Label runat="server" Id="lblCompanyAddress"></asp:Label></div>
                    <div><asp:Label runat="server" Id="lblCompanyPhone"></asp:Label></div>
                    <div><asp:Label runat="server" Id="lblCompanyFax"></asp:Label></div>
                </address>
                </div>
            
                <div class="col-md-4 col-sm-4 col-xs-12">
                <div class="footerCmpName">
                    <h3><asp:Label runat="server" ID="lblCompanyName2"></asp:Label></h3>
                </div>

                <div class="logoTag">
                    <asp:Label runat="server" Id="lblCompanyDialouge2"></asp:Label>
                </div>
                <address>
                   <div> <asp:Label runat="server" Id="lblCompanyAddress2"></asp:Label></div>
                    <div><asp:Label runat="server" Id="lblCompanyPhone2"></asp:Label></div>
                    <div><asp:Label runat="server" Id="lblCompanyFax2"></asp:Label></div>
                </address>
                </div>
            
                <div class="col-md-4 col-sm-4 col-xs-12">
                <div class="footerCmpName">
                    <h3><asp:Label runat="server" ID="lblCompanyName3"></asp:Label></h3>
                </div>

                <div class="logoTag">
                    <asp:Label runat="server" Id="lblCompanyDialouge3"></asp:Label>
                </div>
                <address>
                   <div> <asp:Label runat="server" Id="lblCompanyAddress3"></asp:Label></div>
                    <div><asp:Label runat="server" Id="lblCompanyPhone3"></asp:Label></div>
                    <div><asp:Label runat="server" Id="lblCompanyFax3"></asp:Label></div>
                </address>
                </div>
            </div>

        </form>
    </body>
</html>

<script language="javascript" type="text/javascript">
    $(function () {
        $('#<%=txtCompanyName.ClientID%>').autocomplete({
                 minLength: 1,
                 source: function (request, response) {
                     $.ajax({
                         url: "frmLogin.aspx/GetCompanyName",
                         data: "{ 'pre':'" + request.term + "'}",
                         dataType: "json",
                         type: "POST",
                         contentType: "application/json; charset=utf-8",
                         success: function (data) {
                             response($.map(data.d, function (item) {
                                 return { value: item }
                             }))
                         },
                         error: function (XMLHttpRequest, textStatus, errorThrown) {
                             alert(textStatus);
                         }
                     });
                 }
             });
         });
</script>

<script language="javascript" type="text/javascript">
    $(function () {
        $('#<%=txtBranchName.ClientID%>').autocomplete({
            minLength: 1,
            source: function (request, response) {
                $.ajax({
                    url: "frmLogin.aspx/GetBranchName",
                    data: "{ 'pre':'" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return { value: item }
                        }))
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus);
                    }
                });
            }
        });
    });
</script>
