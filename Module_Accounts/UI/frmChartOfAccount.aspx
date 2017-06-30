<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmChartOfAccount.aspx.cs" Inherits="pbERP.Module_Accounts.UI.frmChartOfAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Content/js/dataTables.jqueryui.css" rel="stylesheet" />
    <script src="../../Content/js/jquery-1.11.1.min.js"></script>
    <script src="../../Content/js/jquery.dataTables.min.js"></script>
    <style type="text/css">
        .paging_full_numbers span.paginate_button {
            background-color: #fff;
        }

            .paging_full_numbers span.paginate_button:hover {
                background-color: #ccc;
            }

        .paging_full_numbers span.paginate_active {
            background-color: #99B3FF;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid formDesignWrapStyle1">
        <div id="content" class="clearfix ">
            <div class="row">
                <div class="col-md-12">
                    <div class="formDesignHead" role="alert">
                        <h2 class="text-center inner_title"><i class="fa fa-user-plus"></i>Create Chart Of Account</h2>
                    </div>


                    <div>
                        <asp:Label ID="lblErrorMsg" Font-Bold="true" ForeColor="Red" Font-Italic="true" Text="" runat="server" />
                        <br />
                        <br />
                    </div>


                    <fieldset class="crateViewWrap">
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Chart Of Account Id: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlChartOfAccount" runat="server" OnSelectedIndexChanged="ddlChartOfAccount_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                <%--<asp:TextBox runat="server" id="txtChartOfAccount" onfocus="txtChartOfAccountOnFocused()" />
                                <script type="text/javascript">
                                    function txtChartOfAccountOnFocused() {
                                        var vlu = "SDJLSDJF";
                                        document.getElementById('txtChartOfAccount').value = document.getElementById('vlu').value;
                                    }
                                </script>--%>
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Account No: " runat="server" />
                            </div>

                            <div class="editor-field">
                                <asp:TextBox ID="txtChartOfAccountNo" runat="server" ReadOnly="true"/>
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Chart Of Account Name: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="txtChartOfAccountName" runat="server" />
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Account Type: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlAccountType" runat="server" OnSelectedIndexChanged="ddlAccountType_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Initial Balance: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="txtInitialBalance" runat="server" />
                            </div>
                        </div>

                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Company Name" runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlCompany" runat="server" >
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="crateViewSubmitWrap">
                            <div class="backTo">
                                <asp:LinkButton ID="lbBackToList" Text="Back To List" runat="server" class="btn btn-primary" />
                            </div>

                            <div class="crateViewSubmit">
                                <asp:Button ID="btnSave" Text="SAVE" runat="server" class="btn btn-primary" OnClick="btnSave_Click" />
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-12 info_list_tbl"  style ="margin-top:15px">
            <table class="table table-striped table-bordered " style="font-family: Serif;"
                border="1" id="example" >
                <thead>
                    <tr>
                        <th>Account ID
                        </th>
                        <th>Account No.
                        </th>
                        <th>Account Name
                        </th>
                        <th>Nature
                        </th>
                        <th>Type
                        </th>
                        <th>Level
                        </th>
                        <th>Initial Balance
                        </th>
                        <th>Company
                        </th>
                        <th>EDIT or DELETE
                        </th>
                    </tr>
                </thead>
                <tbody id="tlist" runat="server">
                </tbody>
            </table>
        </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#example').dataTable({
                "bLengthChange": true,
                "paging": true,
                "sPaginationType": "full_numbers",                    //For Different Paging  Style
                //"scrollY": 1000,                                     // For Scrolling
                "jQueryUI": true                                      //Enabling JQuery UI(User InterFace)
            });
        });
    </script>
</asp:Content>
