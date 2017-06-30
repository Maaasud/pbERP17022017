<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rptBlance.aspx.cs" Inherits="pbERP.rptBlance" %>

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

    <%-- Data Grid View --%>
    <link href="../../Content/js/dataTables.jqueryui.css" rel="stylesheet" />
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
    <%-- Data Grid View --%>



    <style>
         
        .MyCssClass thead {
            display: table-header-group;
         
        }

        #main {
            width: 920px;
            /* to centre page on screen*/
            margin-left: auto;
            margin-right: auto;
        }

        @font-face {
            font-family: "Dodgv2";
            src: url("../../../MenuCssJs/fonts/Dodgv2.ttf")format("truetype");
            font-weight: normal;
            font-style: normal;
        }

        #btnPrint {
            font-weight: 700;
        }

        .GrandGrandTotalRowStyle {
            border: solid 1px Gray;
            color: #000000;
            font-weight: bold;
            font-size: 16px;
            font-family: Calibri;
            text-align: right;
            height: 30px;
        }

        .style1 {
            text-align: center;
            width: 922px;
            font-family: Calibri;
        }

        .style2 {
            font-size: 16px;
        }

        .style12 {
            width: 109px;
        }

        .style14 {
            text-align: center;
            width: 922px;
            font-family: Calibri;
            font-size: 20px;
            font-weight: 700;
        }
    </style>

   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <center>
        <table style="width:80%">
            <tr>
                <td>
                    <p>Date: <input type="text" id="datepicker" / ></p>

                </td>
            </tr>
        <tr><td><img src="../../images/LogoMoontex.bmp" height ="50" width = "50"/></td><td></td></tr>
        <tr><td>Sector-1, Road-5, House-7</td><td></td><td><asp:Label ID="lblDateNow" runat="server" ></asp:Label></td><td><asp:Label ID="lblUser" runat="server" ></asp:Label></td></tr>       
        <tr><td><asp:Label ID="lblHead" runat="server" ></asp:Label></td><td></td></tr>
        <%--<tr><td><asp:Label ID="lblUser" runat="server" >--%>
       
        <tr><td></td><td>Received From</td><td>Received To</td></tr>
    </table>

    </center>
        

    





    <%--Header Table--%>
    <%--Work Tomm--%>
  
    <table style="width:80%">
        
        <tr>
            <td>Head Name :</td>
            <td>
                <asp:TextBox ID="txtHeadName" runat="server" required="required" class="form-control input-sm" ></asp:TextBox>
            </td>
            <td>From Date :</td>
            <td>
                <asp:TextBox ID="txtFromDate" runat="server" required="required" class="form-control input-sm"></asp:TextBox>
            </td>
            <td>To Date :</td>
            <td>
                <asp:TextBox ID="txtToDate" runat="server" required="required" class="form-control input-sm"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td></td>
             <br />


               <td style="padding-bottom:5px"><br />
                   <asp:Button ID="btnblance" runat="server" Text="Blance Preview" OnClick="btnblance_Click" class="form-control input-sm btn-primary" />
               
               </td>
             <td></td> <td></td> 
                <td><input type="button" value="Print"   onclick="fnPrint('tbl1')" class="form-control input-sm btn-primary" /></td>
              
        </tr>
    </table>
      


    <div  >
    <table  cellpadding="0"  border='1' id='tbl1'  style='font-size:12px; font-family:Calibri; width:960px;' >
         <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </table>
  
      </div> 

      

  
  <%--  <input type="button" value="Print"  tabindex="1"  onclick="ClosePrint('tbl1')"/>--%>

  <script>
        function fnPrint(id) {
            var tableToPrint = document.getElementById(id);
            newWin = window.open("rptBlance.aspx");
            newWin.document.write(tableToPrint.outerHTML);
            newWin.print();
            newWin.close();
        }
</script>
   <%-- <script type="text/javascript">
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            //            print.display = false;

            window.print();
        }
    </script>--%>

     <script language="javascript" type="text/javascript">
        $(function () {
            $('#<%=txtHeadName.ClientID%>').autocomplete({
                minLength: 1,
                source: function (request, response) {
                    $.ajax({
                        url: "rptBlance.aspx/getHeadName",
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
        $(function () {
            $("[id$=txtFromDate]").datepicker({
                dateFormat: "dd/mm/yy",
               changeMonth: true,
               changeYear: true,
               yearRange: "-100:+10"
            });
        });


       $(function () {
            $("[id$=txtToDate]").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "-100:+10"
            });
        })
</script>
     <script>
         $(function() {
             $( "#datepicker" ).datepicker();
         });
         </script>
</asp:Content>
