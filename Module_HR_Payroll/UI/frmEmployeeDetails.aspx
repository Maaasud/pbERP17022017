<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmEmployeeDetails.aspx.cs" Inherits="pbERP.Module_HR_Payroll.UI.frmEmployeeDetails" %>
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
                        <h2 class="text-center inner_title"><i class="fa fa-user-plus"></i>Create Employee Details</h2>
                    </div>
                    <div>
                        <asp:Label ID="lblErrorMsg" Font-Bold="true" ForeColor="Red" Font-Italic="true" Text="" runat="server" />
                        <br />
                        <br />
                    </div>

                    <fieldset class="crateViewWrap">
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Employee ID: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="txtEmployeeID" ReadOnly="true" runat="server" />
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Employee Code: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="txtEmployeeCode" runat="server" ReadOnly="true"/>
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Employee Name: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="txtEmployeeName" runat="server" />
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Father's Name: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="txtFathersName" runat="server" />
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Mother's Name: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="txtMothersName" runat="server" />
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Present Address: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="txtPresentAddress" runat="server" />
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Present Police Stassion ID: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlPresentPSId" runat="server">
                                    
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Present City Id: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlPresentCityId" runat="server">
                                    
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Permanent Address: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="txtPermanentAddress" runat="server" />
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Permanent Police Stassion Id: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlPermanentPStId" runat="server">
                                    
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Permanent CIty Id: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlPermanentCItyId" runat="server">
                                    
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Mobile No: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="txtMobileNo" runat="server" />
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
                                <asp:Label Text="Date Of Birth: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="txtDateOfBirth" runat="server" />   
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Gender: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlGender" runat="server">
                                    <asp:ListItem Text="Male" />
                                    <asp:ListItem Text="Female" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Religion: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:DropDownList ID="ddlReligion" runat="server">
                                    <asp:ListItem Text="Muslim" />
                                    <asp:ListItem Text="Others" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Blood Group: " runat="server">
                                    
                                </asp:Label>
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="txtBloodGroup" runat="server" />
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Last Degree Of Education: " runat="server" />
                            </div>
                            <div class="editor-field">
                                <asp:TextBox ID="txtLastDegreeOfEdu" runat="server" />
                            </div>
                        </div>
                        <div class="crateViewFieldBox">
                            <div class="editor-label">
                                <asp:Label Text="Is Active: " runat="server" />
                            </div>
                            <div class="editor-field">
                                
                                <asp:CheckBox ID="chkDiscontinue" Text="Discontinue This Employee" runat="server" Checked="true" />
                            </div>
                        </div>
                        
                        <div class="crateViewSubmitWrap">
                            <div class="backTo">
                                <asp:LinkButton ID="lbBacktoList" Text="Back To List" runat="server" class="btn btn-danger"/>
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
<div class="col-md-12 info_list_tbl"  style ="margin-top:15px">
            <table class="table table-striped table-bordered " style="font-family: Serif;"
                border="1" id="example" >
                <thead>
                    <tr>
                        <th>ID
                        </th>
                        <th>Code
                        </th>
                        <th>Name
                        </th>
                        <th>Father's Name
                        </th>
                        <th>Mother's Name
                        </th>
                        <th>Present Address
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
