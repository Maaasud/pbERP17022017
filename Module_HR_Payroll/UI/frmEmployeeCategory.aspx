<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmEmployeeCategory.aspx.cs" Inherits="pbERP.Module_HR_Payroll.UI.frmEmployeeCategory" %>
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
    <div class="container-fluid">
        <div id="content" class="clearfix ">
            <div class="row">
                <div class="col-md-12" style="background-color: #fbfbd5">
                    <div class="alert alert-info" role="alert">
                        <h2 class="text-center inner_title"><i class="fa fa-user-plus"></i>Create Employee Category</h2>
                    </div>
                    <div>
                        <asp:Label ID="lblErrorMsg" Font-Bold="true" ForeColor="Red" Font-Italic="true" Text="" runat="server" />
                        <br />
                        <br />
                    </div>

                    <fieldset class="crateViewWrap">
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Employee Category ID: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="txtEmpCategoryID" runat="server">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Employee Category: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="txtEmployeeCategory" runat="server" />
                            </div>

                        </div>
                        <div class="crateViewSubmitWrap">
                            <div class="backTo">
                                <asp:LinkButton ID="lbBackToList" Text="Back To List" runat="server" class="btn btn-primary" />
                            </div>

                            <div class="crateViewSubmit">
                                <asp:Button ID="btnSave" Text="Save" runat="server" class="btn btn-primary" OnClick="btnSave_Click" />
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
                        <th>ID
                        </th>
                        <th>Category
                        </th>
                        <th>Edit Or Delete
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
