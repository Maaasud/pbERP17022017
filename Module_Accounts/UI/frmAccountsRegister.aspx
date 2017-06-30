<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAccountsRegister.aspx.cs" Inherits="pbERP.Module_Accounts.UI.frmAccountsRegister" %>
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
                <div class="col-md-12" style="background-color: #fbfbd5; top: -60px; left: 2px;">
                    <div class="alert alert-info" role="alert">
                        <h2 class="text-center inner_title"><i class="fa fa-user-plus"></i>Create Accounts Register</h2>
                    </div>

                    <div>
                        <asp:Label ID="lblErrorMsg" Font-Bold="true" ForeColor="Red" Font-Italic="true" Text="" runat="server" />
                        <br />
                        <br />
                    </div>


                    <fieldset class="crateViewWrap">
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Register Id: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="txtRegisterId" ReadOnly="true" runat="server" />
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Register Name: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="txtRegisterName" runat="server" />
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Address: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="txtRegisterAddress" runat="server" />
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Police Station: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlPoliceStation" runat="server">
                                    
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="City: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlCity" runat="server">
                                    
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Contact Number: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="txtContactNumber" runat="server" />
                            </div>
                        </div>

                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Emergency Contact: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="txtEmergencyContact" runat="server" />
                            </div>
                        </div>

                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Email" runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="txtAccountsRegisterEmail" runat="server" />
                            </div>
                        </div>

                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Register Type: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlRegisterType" runat="server">
                                    
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            
                            <div class="editor-field">
                                <asp:CheckBox Text="Is Discontinue?" runat="server" Checked="false"/>
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Company Name" runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlCompany" runat="server">
                                    
                                </asp:DropDownList>
                            </div>
                        </div>
                        
                        <div class="crateViewSubmitWrap">
                            <div class="backTo">
                                <asp:LinkButton ID="lbBackToList" Text="Back To List" runat="server" class="btn btn-danger"/>
                            </div>

                            <div class="crateViewSubmit">
                                <asp:Button ID="btnSave" Text="SAVE" runat="server" class="btn btn-success" OnClick="btnSave_Click"/>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>

    <div>
        <table class="table table-striped table-bordered " style="font-family: Serif;"
            border="1" id="example">
            <thead>
                <tr>
                    <th>ID
                    </th>
                    <th>Name
                    </th>
                    <th>Address
                    </th>
                    <th>Contact Number
                    </th>
                    <th>Email
                    </th>
                    <th>Type
                    </th>
                    <th>Company
                    </th>
                    <th>Edit
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
