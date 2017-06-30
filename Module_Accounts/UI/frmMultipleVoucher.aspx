<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmMultipleVoucher.aspx.cs" Inherits="pbERP.Module_Buyer_Shipment.UI.frmMultipleVoucher" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <%-- Auto Complieate Box --%>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <%-- Auto Complieate Box --%>

    <%-- Date Time Picker --%>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
    rel="stylesheet" type="text/css" />
    <%-- Date Time Picker --%>
    <style>
        .element {
    z-index: 1;
    top: 501px;
    left: 301px;
    display: none;
    width: 300px;
}
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" >
    
    <div class="col-md-10 pull-right" id="mainContentBox" style="background-color:white; width:85%">

                <h1>Voucher Entry</h1>


         <div>
                        <asp:Label ID="lblErrorMsg" Font-Bold="true" ForeColor="Red" Font-Italic="true" Text="" runat="server" />
                        <br />
                        <br />
                    </div>
            <div class="form-class">

                <div class="row form-class">
                </div>
                <div class="row form-class" style="padding-bottom:5px">
                    <div class="col-md-2">Voucher No</div>
                    <div class="col-md-4 " >
                        <asp:TextBox runat="server" id="txtVoucherNo"  ReadOnly="true" class="form-control input-sm" BackColor="White" disabled="disabled"/>
                    </div>
                    <div class="col-md-2"></div>
                    <div class="col-md-2">
                        <asp:Button Text="EDIT" runat="server" ID="btnEdit" class="form-control btn-primary input-sm" />                       
                    </div>

                    <div class="col-md-2">
                    </div>

                </div>


              
                <div class="row">
                    <div class="col-md-2">Voucher Type:</div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="ddlVoucherType" class="form-control input-sm"  AutoPostBack="True"  OnSelectedIndexChanged="ddlVoucherType_SelectedIndexChanged">
                            <asp:ListItem Text="PAYMENT" />
                            <asp:ListItem Text="RECEIVE" />
                            <asp:ListItem Text="JOURNAL" />
                            <asp:ListItem Text="CONTRA" />
                        </asp:DropDownList>
                    </div>

                    
              
                    <div class="col-md-2">Voucher Date:</div>
                    <div class="col-md-2">
                       <asp:TextBox runat="server" ID="txtVoucherDate" class="form-control input-sm" />                       
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox runat="server" ID="txtTransYear" class="form-control input-sm" ReadOnly="true"/>
                    </div>
                </div>
                <br />
                
                <div class="text-center">
                </div>
            <div>
                        <asp:Label Text="Job Ref No." runat="server" />
                        <asp:TextBox ID="txtJobRefNo" runat="server" />
                    </div>
        
                <div class="table table-responsive table-hover" ">
                    <div><br />
                        <table class="Gridview text-center" cellspacing="1" cellpadding="3" rules="all" border="1" id="ContentPlaceHolder1_gvDetails" style="background-color: White; border-style: None; width: 100%;">
                            <tbody>
                                 <asp:Literal ID="ltlTable" runat="server"></asp:Literal>
                                <tr>
                                    <td>1</td>
                                    <td>
                                        <asp:TextBox  runat="server" ID="txtDebited" class="form-control input-sm ui-autocomplete-input" style=" font-family: Calibri; width: 98%; "/>
                                        
                                    </td>
                                    <td>
                                       <asp:TextBox runat="server" ID="txtCredited" class="form-control input-sm ui-autocomplete-input" style="font-family: Calibri; width: 98%;"/>
                                    </td>
                                    <td>

                                        <%--<asp:TextBox ID="txtOffice" runat="server" class="form-control input-sm" />--%>


                                        <asp:DropDownList runat="server" ID="ddlOffice" class="form-control input-sm" style="font-family: Calibri; width: 98%;">
                                           
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlTransactionMode" AutoPostBack="true" class="form-control input-sm" Style="font-family: Calibri; width: 98%;" OnSelectedIndexChanged="ddlTransactionMode_SelectedIndexChanged">
                                            <asp:ListItem Text="CASH" />
                                            <asp:ListItem Text="CASH CHEQUE" />
                                            <asp:ListItem Text="A/C PAYEE CHEQUE" />
                                            <asp:ListItem Text="ONLINE TRANSFER" />
                                            <asp:ListItem Text="PAY ORDER" />
                                            <asp:ListItem Text="ATM" />
                                            <asp:ListItem Text="D.D." />
                                            <asp:ListItem Text="T.T." />
                                            <asp:ListItem Text="OTHERS" />
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                       <asp:TextBox runat="server" ID="txtChequeNo"  class="form-control input-sm " style="font-family: Calibri; width: 200%;" />
                                    </td>
                                    <td>
                                        <strong>
                                           <asp:TextBox runat="server" ID="txtChequeNDate"  class="form-control input-sm " style="font-family: Calibri; width: 200%;"/>
                                        </strong>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtRemarks" class="form-control input-sm ui-autocomplete-input" style="font-family: Calibri; width: 98%;"/>
                                    </td>
                                    <td>
                                         <asp:TextBox runat="server" ID="txtAmount" class="form-control input-sm ui-autocomplete-input" style="font-family: Calibri; width: 98%;" AutoPostBack="true" OnTextChanged="txtAmount_TextChanged"/>                                      
                                    </td>
                                    <td>
                                        <%--<asp:Button ID="btnSave"  runat="server"  title="Save &amp; Continue" OnClick="btnSave_Click" />--%>
                                        <%--<input type="image" id="bt" style="height:25px;width:25px;" runat="server"  src="../../images/AddNewitem.png" value="imagebtn" class="submit"  />--%>
                                        <asp:ImageButton ID="imgbtnAdd" runat="server" title="Save &amp; Continue" src="../../Images/AddNewitem.png" style="height: 25px; width: 25px;" OnClick="imgbtnAdd_Click" />
                                       <%-- <asp:ImageButton ID="imgbtnAdd" runat="server" title="Save &amp; Continue" src="../../Images/AddNewitem.png" style="height: 25px; width: 25px;" />--%>
                                        <asp:ImageButton ID="imgbtnAdd1" runat="server" title="Complete" src="../../Images/checkmark.png" style="height: 25px; width: 25px;"/>
                                        <asp:ImageButton ID="imgbtnAddPrint" runat="server" title="Save &amp; Print" src="../../Images/print.png" style="height: 25px; width: 25px;"/>                                     
                                    
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
           
            
                <div class="row form-class" style="padding-bottom:5px">
                    <div class="col-md-2">In Words :</div>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtInWords" ReadOnly="true" class="form-control input-sm ui-autocomplete-input" style="font-family: Calibri; width: 98%;"/>                                      
                    </div>
                    <div class="col-md-3"></div>
                </div>

            
                <div class="row form-class">
                    <div class="col-md-2">In Words (Total) :</div>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtInWordsTotal" ReadOnly="true" class="form-control input-sm ui-autocomplete-input" style="font-family: Calibri; width: 98%;"/>                                      
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtTotalAmount" ReadOnly="true" class="form-control input-sm ui-autocomplete-input" style="font-family: Calibri; width: 98%;"/>                                      
                    </div>
                </div>
            
            </div>
        </div>

     <script type="text/javascript">
         $(function () {
             $("[id$=txtVoucherDate]").datepicker({
                 dateFormat: "dd/mm/yy",
                 changeMonth: true,
                 changeYear: true
             });
         });

     </script>
    <script language="javascript" type="text/javascript">
        $(function () {
         
            $('#<%=txtDebited.ClientID%>').autocomplete({
             minLength: 1,
             source: function (request, response) {
                 $.ajax({
                     url: "frmMultipleVoucher.aspx/getDebited",
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
           $('#<%=txtCredited.ClientID%>').autocomplete({
                minLength: 1,
                source: function (request, response) {
                    $.ajax({
                        url: "frmMultipleVoucher.aspx/getCredited",
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
            
<script type="text/javascript">
    $( function() {
        $( ".datepicker" ).datepicker();
    });
</script>
<script type="text/javascript">
    $(function () {
        $("[id$=txtChequeNDate]").datepicker({
            
            changeMonth: true,
            changeYear: true
        });
        dateFormat: 'dd/mm/yy',
        });
</script>

    <script language="javascript" type="text/javascript">
        $(function () {
         
            $('#<%=txtJobRefNo.ClientID%>').autocomplete({
                minLength: 1,
                source: function (request, response) {
                    $.ajax({
                        url: "frmMultipleVoucher.aspx/getJobRefNo",
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
</asp:Content>


