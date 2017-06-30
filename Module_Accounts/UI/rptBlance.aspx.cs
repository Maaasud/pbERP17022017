using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using pbERP.Class;
namespace pbERP
{
    public partial class rptBlance : System.Web.UI.Page
    {
        int ChartOfAccountID = 0;

        public string ConStrForAll = WebConfigurationManager.ConnectionStrings["DbConStr"].ConnectionString;
        private string ConnectionString = WebConfigurationManager.ConnectionStrings["DbConStr"].ConnectionString;
        private static SqlConnection con;
        private static SqlCommand cmd;
        private static SqlDataReader reader;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblHead.Text = txtHeadName.Text;
            //lblUser.Text = clsGlobalVariable.userName;
            lblDateNow.Text = DateTime.Now.ToString();
        }

        protected void btnblance_Click(object sender, EventArgs e)
        {
            try
            {
                double balance = 0;
                ChartOfAccountID = DataHelper.getID("ChartOfAccountID", "tblACC_ChartOfAccount", "ChartOfAccountName", "=", txtHeadName.Text);

                con = new SqlConnection(ConnectionString);

                con.Open();
                string FormDate = txtFromDate.Text; //DateTime.Parse(Request.Form[txtFromDate.UniqueID]);
                string ToDate = txtToDate.Text;
                //if (string.IsNullOrEmpty(txtFromDate.Text))
                //{
                //    txtFromDate.Text = DateTime.Now.ToShortDateString();
                //}
                //DateTime ToDate = DateTime.Parse(Request.Form[txtToDate.UniqueID]);

                //if (string.IsNullOrEmpty(txtToDate.Text))
                //{
                //    txtToDate.Text = DateTime.Now.ToShortDateString();
                //}


               // Total Balance ---Imam----------
                #region
                string OpenDate = "";
                string strtOP = "";
                strtOP += "select top 1 VoucherDate from tblACC_Transaction";
               
                SqlDataAdapter adaptersql = new SqlDataAdapter(strtOP, con);
                DataTable dtsql = new DataTable();
                adaptersql.Fill(dtsql);

                // Day One Minus----------------Imam----------------
                foreach (DataRow var in dtsql.Rows)
                {
                    OpenDate = var["VoucherDate"].ToString();
                }
                string changeOpenDate = DataHelper.datetest(OpenDate, OpenDate, OpenDate);
                string strminusdate = "";
                string TotalMinusDateOne = "";
                string TotalMinusDate = DataHelper.datetest(txtFromDate.Text, txtFromDate.Text, txtFromDate.Text);
                TotalMinusDateOne += "select  DATEADD(day, -1, '" + TotalMinusDate + "')";

              
                //SqlCommand cmd = new SqlCommand(TotalMinusDateOne,con);
                //cmd.CommandText = TotalMinusDateOne;
                //cmd.ExecuteNonQuery();
                SqlDataAdapter adapterMinusDate = new SqlDataAdapter(TotalMinusDateOne, con);
                DataTable dtMinusDate = new DataTable();
                adapterMinusDate.Fill(dtMinusDate);
                foreach (DataRow var in dtMinusDate.Rows)
                {
                    strminusdate = var["Column1"].ToString();
                }
                strminusdate = DataHelper.datetest(strminusdate, strminusdate, strminusdate);

                string strSQL = "";
                strSQL += " IF OBJECT_ID('tempdb..#t') IS NOT NULL DROP TABLE #t;";
                strSQL += "SELECT a.VoucherId, a.VoucherDate, c.ChartOfAccountName, a.DrAmount, a.CrAmount, 0 As Balance into #t";
                strSQL += " FROM tblACC_Transaction AS a INNER JOIN ";
                strSQL += " tblACC_Voucher AS b ON a.VoucherId = b.VoucherId INNER JOIN tblACC_ChartOfAccount AS c";
                strSQL += " ON b.ChartOfAccountId = c.ChartOfAccountID";
                strSQL += " WHERE (a.ChartOfAccountId = " + ChartOfAccountID + ") AND (a.VoucherDate BETWEEN '" + strminusdate + " ' AND '" + changeOpenDate + "')";
                strSQL += "select SUM(dramount)-SUM(cramount) As totalBalance from  #t;";
               
                #endregion
            
                SqlDataAdapter adapter = new SqlDataAdapter(strSQL, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                string s = "";
                string ss = "";
                double dr = 0;
                string drTotal = "";
                string crTotal = "";
                double cr = 0;
                string str = "";
                string date = "";
                string Total = "";
                string drCrTotals = "";

                foreach (DataRow var in dt.Rows)
                {

                    Total = var["totalBalance"].ToString();

                }
                if (string.IsNullOrEmpty(Total))
                {
                    Total = "0";
                }
                //#endregion
                // Total Debit & Total Credit Amount Caculation ---------------Imam--------------

                //con = new SqlConnection(ConnectionString);
                //con.Open();
                string FromDate = DataHelper.datetest(txtFromDate.Text, txtFromDate.Text,txtFromDate.Text);
                string ToDates = DataHelper.datetest(txtToDate.Text, txtToDate.Text, txtToDate.Text);
                #region
                string DrCrTotal = " IF OBJECT_ID('tempdb..#DrCr') IS NOT NULL DROP TABLE #DrCr;";
                DrCrTotal += " SELECT a.VoucherId, a.VoucherDate As VoucherDate, c.ChartOfAccountName, a.DrAmount, a.CrAmount, 0 As Balance into #DrCr";
                DrCrTotal += " FROM tblACC_Transaction AS a INNER JOIN  tblACC_Voucher AS b ON a.VoucherId = b.VoucherId INNER JOIN";
                DrCrTotal += " tblACC_ChartOfAccount AS c ON b.ChartOfAccountId = c.ChartOfAccountID WHERE (a.ChartOfAccountId = " + ChartOfAccountID + ")";
                DrCrTotal += " AND (a.VoucherDate BETWEEN '" + FromDate + "' AND '" + ToDates + "')";
                DrCrTotal += " IF OBJECT_ID('tempdb..#d') IS NOT NULL DROP TABLE #d;";
                DrCrTotal += " select sum(DrAmount) As DrAmountTotal,sum(CrAmount) As CrAmountTotal into #d from #DrCr";
                DrCrTotal += " select DrAmountTotal,CrAmountTotal, DrAmountTotal-CrAmountTotal As DrCrTotal from #d";
                
                DataTable dtDrCrs = new DataTable();
                SqlDataAdapter adptarDrCrs = new SqlDataAdapter(DrCrTotal, con);
                adptarDrCrs.Fill(dtDrCrs);
                foreach (DataRow var in dtDrCrs.Rows)
                {
                    drTotal = (Convert.ToDouble(var["DrAmountTotal"]).ToString());
                    crTotal = (Convert.ToDouble(var["CrAmountTotal"]).ToString());
                    drCrTotals = var["DrCrTotal"].ToString();
                }
               // drCrTotals = (Convert.ToDouble(drTotal) - Convert.ToDouble(crTotal));

                #endregion
                // Table Object &  Dr & Cr Amount Calculation----------------Imam------------
                #region
                string strDrCr = "";
                //strDrCr += " IF OBJECT_ID('tempdb..#DrCr') IS NOT NULL DROP TABLE #DrCr;";
                strDrCr += " SELECT a.VoucherId, CONVERT(VARCHAR(10), a.VoucherDate, 105) As VoucherDate , c.ChartOfAccountName, a.DrAmount, a.CrAmount, 0 As Balance ";
                strDrCr += " FROM tblACC_Transaction AS a INNER JOIN  tblACC_Voucher AS b ON a.VoucherId = b.VoucherId INNER JOIN";
                strDrCr += " tblACC_ChartOfAccount AS c ON b.ChartOfAccountId = c.ChartOfAccountID WHERE (a.ChartOfAccountId = " + ChartOfAccountID + ")";
                strDrCr += " AND (a.VoucherDate BETWEEN '" + FromDate + " '  AND '" + ToDates + " ')";
                //strDrCr += "select sum(DrAmount) As DrAmountTotal,sum(CrAmount) As CrAmountTotal from #DrCr";
                DataTable dtDrCr = new DataTable();
                SqlDataAdapter adptarDrCr = new SqlDataAdapter(strDrCr, con);
                adptarDrCr.Fill(dtDrCr);
           
                //  str += "<table cellspacing='0' rules='all' border='1' id='tbl1' style='font-size:12px;width:100%;'>";
                str += "<tr ><th>Voucher Date<th/><th>Voucher Id<th/><th>Chart Of AccountName<th/><th>DrAmount<th/><th>CrAmount<th/><th>Balance<th/><tr/>";
                str += "<tr><td><td/><td><td/><td>-<td/><td>-<td/><td>-<td><td>" + Total + "<td/><tr/>";

                foreach (DataRow var in dtDrCr.Rows)
                {
                    s = var["VoucherId"].ToString();
                    date =var["VoucherDate"].ToString();
                    ss = var["ChartOfAccountName"].ToString();
                    dr = Convert.ToDouble(var["DrAmount"].ToString());
                    cr = Convert.ToDouble(var["CrAmount"].ToString());
                    //bl=Convert.ToDouble(var["CrAmount"].ToString());
                    balance = (balance + dr) - cr;
                    str += "<tr ><td>" + date + "<td/><td >" + s + "<td/><td >" + ss + "<td/><td>" + dr + "<td/><td  >" + cr + "<td/><td>" + balance + "<td/><tr/>";
                }
                str += "<tr><td><td/><td><td/><td>Total Amount :<td/><td>" + drTotal + "<td/><td> " + crTotal + "<td><td> " + drCrTotals + "<td/><tr/>";
                Literal1.Text = str;
                #endregion

            }
            catch (Exception)
            {

                string message = "Data Not Found.";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            }
           
        }






        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<string> getHeadName(string pre)
        {
            List<string> searchResult = new List<string>();
            string query = "Select ChartOfAccountName From tblACC_ChartOfAccount Where ChartOfAccountName Like '%" + pre + "%' Order By ChartOfAccountName";
            return searchResult = DataHelper.getSearchList(query);
        }

        protected void txtPrint_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

    }

}



