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
using System.Globalization;

namespace pbERP.Module_Buyer_Shipment.UI
{
    public partial class frmMultipleVoucher : System.Web.UI.Page
    {


        public string ConStrForAll = WebConfigurationManager.ConnectionStrings["DbConStr"].ConnectionString;
        private string ConnectionString = WebConfigurationManager.ConnectionStrings["DbConStr"].ConnectionString;
        private static SqlConnection con;
        private static SqlCommand cmd;
        private static SqlDataReader reader;

       // int userId = 1;
        int paymentTo = 0;
        int paymentFrom = 0;
      //  int companyBranchId = 0;
        int maxCAID = 0;
       string Narration = "";
    string VoucherType = "";
       // int BranchId = 0;
        int ProjectId = 1;

        protected void Page_Load(object sender, EventArgs e)
        {

            
            ltlTable.Text = "<tr class='text-center' align='center' style='background-color: #D9EDF7; font-size: 13px; height: 30px;'>  <th align='center' scope='col' style='width: 2%;'>SL</th><th align='center' scope='col' style='width: 18%;'><span id='ContentPlaceHolder1_gvDetails_lblDebit'>Payment To</span></th><th align='center' scope='col' style='width: 18%;'><span id='ContentPlaceHolder1_gvDetails_lblCredit'>Payment From</span></th><th align='center' scope='col' style='width: 10%;'>Office</th><th align='center' scope='col' style='width: 6%;'>Transaction Mode</th><th align='center' scope='col' style='width: 5%;'>Cheque No</th><th align='center' scope='col' style='width: 6%;'>Cheque Date</th><th align='center' scope='col' style='width: 10%;'>Remarks</th><th align='center' scope='col' style='width: 10%;'>Amount</th><th align='center' scope='col' style='width: 10%;'></th><tr/>";

            if (!IsPostBack)
            {
                formClear();
                dataloadDropDownList();

            }

            txtChequeNo.ReadOnly = true;
            txtChequeNDate.ReadOnly = true;
        }

        private void dataloadDropDownList()
        {
            string query = "";
            ////Reazul cannot completed
            //query = "Select CompanyBranchID, CompanyBranchName From tblGS_ComanyBranchInfo where CompanyId='" + clsGlobalVariable.RunningCompanyId + "'";
            //DataHelper.DataLoadToDropDownList(query, ddlOffice, "CompanyBranchName", "CompanyBranchID");


            query = "Select CompanyBranchID,CompanyBranchName From tblGS_ComanyBranchInfo Where CompanyId='" + clsGlobalVariable.RunningCompanyId + "'";
            DataHelper.DataLoadToDropDownList(query, ddlOffice, "CompanyBranchName", "CompanyBranchID");
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<string> getDebited(string pre)
        {
            List<string> searchResult = new List<string>();
            string query = "Select ChartOfAccountName From tblACC_ChartOfAccount Where ChartOfAccountTypeId ='2' And ChartOfAccountName Like '%" + pre + "%'";
            return searchResult = DataHelper.getSearchList(query);
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<string> getCredited(string pre)
        {
            List<string> searchResult = new List<string>();
            string query = "Select ChartOfAccountName From tblACC_ChartOfAccount Where ChartOfAccountTypeId ='2' And ChartOfAccountName Like '%" + pre + "%'";
            return searchResult = DataHelper.getSearchList(query);
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<string> getJobRefNo(string pre)
        {
            List<string> searchResult = new List<string>();
            string query = "Select SHP_JobNumber From tblSHP_SampleRequestFromBuyer Where SHP_JobNumber Like '%" + pre + "%'";
            return searchResult = DataHelper.getSearchList(query);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {  

        }

        private void formClear()
        {
            int MaxId = DataHelper.getTableMaxId_Sql("VoucherId", "tblACC_Voucher");
            txtVoucherNo.Text = MaxId.ToString();
            
        }

        private Boolean ErrorFound()
        {
            //if (txtJobRefNo.Text!=null)
            //{
            //    string query = "Select SHP_JobNumber From tblSHP_SampleRequestFromBuyer Where SHP_JobNumber='" + txtJobRefNo.Text + "'";
            //    DataTable dt = DataHelper.GetData(query).Tables[0];
            //    if (dt.Rows.Count < 1)
            //    {
            //        lblErrorMsg.Text = "Not a valid Job Ref No.";
            //        return true;
            //    }
            //    else
            //        lblErrorMsg.Text = "";
            //}


            if (string.IsNullOrEmpty(txtDebited.Text))
            {
                lblErrorMsg.Text = "Select Payment To/Receive From";
                return true;
            }
            else
                lblErrorMsg.Text = "";

            if (string.IsNullOrEmpty(txtCredited.Text))
            {
                lblErrorMsg.Text = "Select Payment To/Receive From";
                return true;
            }
            else
                lblErrorMsg.Text = "";

            if (string.IsNullOrEmpty(txtAmount.Text))
            {
                lblErrorMsg.Text = "Input Amount";
                return true;
            }
            else
                lblErrorMsg.Text = "";

            return false;
        }

        protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
        {
            if (ErrorFound())
            {
                return;
            }

           string ymd=DataHelper.datetest(txtVoucherDate.Text, txtVoucherDate.Text, txtVoucherDate.Text);
           Response.Write(ymd);
            //con = new SqlConnection(ConnectionString);
            //con.Open();
            //string st = "";
            //st += " select Convert(datetime,'"+txtVoucherDate.Text+"',105)";
            //DataTable dtDrCrs = new DataTable();
            //SqlDataAdapter adptarDrCrs = new SqlDataAdapter(st, con);
            //adptarDrCrs.Fill(dtDrCrs);


            paymentTo = DataHelper.getID("ChartOfAccountID", "tblACC_ChartOfAccount", "ChartOfAccountName", "=", txtDebited.Text);
            paymentFrom = DataHelper.getID("ChartOfAccountID", "tblACC_ChartOfAccount", "ChartOfAccountName", "=", txtCredited.Text);

            int VoucherType = 0;
            List<string> query = new List<string>();
            int counter = 0;
            int saveResult = 0;

            CreateNarration();

            CreateVoucherType();
            // Accounts Voucher Entry
            #region
                query.Add("Exec SPInsert 'Accounts Voucher','" + txtVoucherNo.Text + "','" + Narration + "','" + clsGlobalVariable.RunninguserId + "','" + clsGlobalVariable.RunninguserId + "','1','3','" + VoucherType + "','"+txtJobRefNo.Text+"','"+ ProjectId +"','"+ clsGlobalVariable.RunningCompanyId +"','" + clsGlobalVariable.RunningCompanyBranchId + "','" + clsGlobalVariable.RunninguserId + "','"+paymentTo+"'");
                counter += 1;
                // Dr Cr Entry  Convert(datetime,'" + txtVoucherDate.Text + " ',105)
                query.Add("Exec SPInsert 'Accounts Transaction','" + txtVoucherNo.Text + "','" + paymentTo + "','" 
                    + ymd + "','" + txtAmount.Text + "',0,'" + clsGlobalVariable.RunningCompanyId + "','" + clsGlobalVariable.RunningCompanyBranchId + "','" + clsGlobalVariable.RunninguserId + "'");
               
                counter += 1;

                query.Add("Exec SPInsert 'Accounts Transaction','" + txtVoucherNo.Text + "','" + paymentFrom + "','"
                    + ymd+ " ',0,'" + txtAmount.Text + "','" + clsGlobalVariable.RunningCompanyId + "','" + clsGlobalVariable.RunningCompanyBranchId + "','" + clsGlobalVariable.RunninguserId + "'");
                counter += 1;
            #endregion
          
            // Query Execute Code Or Insert Value ---------Imam-------
            #region
            if (query.Count > 0)
            {
                saveResult = DataHelper.executeNonQueryTransaction(query, counter);
            }

            if (saveResult == 1)
            {
                lblErrorMsg.Text = "Data Saved Successfully!!!";

            }
            else 
                lblErrorMsg.Text = "Error found," + " Data not saved." + " Please try again!!!";
            #endregion
            formClear();
    
        }

        private void CreateVoucherType()
        {
            if (ddlTransactionMode.Text=="CASH")
            {
                VoucherType = "CASH" + " " + ddlVoucherType.Text;
            }

            if (ddlTransactionMode.Text == "CASH CHEQUE")
            {
                VoucherType = "CASH PAYMENT" + " " + ddlVoucherType.Text;
            }

            if (ddlTransactionMode.Text == "A/C PAYEE CHEQUE")
            {
                VoucherType = "A/C PAYEE CHEQUE" + " " + ddlVoucherType.Text;
            }

            if (ddlTransactionMode.Text == "ONLINE TRANSFER")
            {
                VoucherType = "ONLINE TRANSFER" + " " + ddlVoucherType.Text;
            }

            if (ddlTransactionMode.Text == "PAY ORDER")
            {
                VoucherType = "PAY ORDER" + " " + ddlVoucherType.Text;
            }

            if (ddlTransactionMode.Text == "D.D.")
            {
                VoucherType = "D.D." + " " + ddlVoucherType.Text;
            }

            if (ddlTransactionMode.Text ==  "T.T.")
            {
                VoucherType = "T.T." + " " + ddlVoucherType.Text;
            }

            if (ddlTransactionMode.Text == "OTHERS")
            {
                VoucherType = "OTHERS" + " " + ddlVoucherType.Text;
            }
        }

        private void CreateNarration()
        {
            if (ddlVoucherType.Text == "PAYMENT")
            {
                if (ddlTransactionMode.Text == "CASH")
                {
                    Narration = "Being the amount paid to " + txtDebited.Text + " From " + txtCredited.Text + " by " + ddlTransactionMode.Text;       
                }
                else if (ddlTransactionMode.Text=="CASH CHEQUE"  || ddlTransactionMode.Text=="A/C PAYEE CHEQUE")
                {
                    Narration = "Being the amount paid to " + txtDebited.Text + " From " + txtCredited.Text + " by " + ddlTransactionMode.Text + " Cheque No. " + txtChequeNo.Text + " Cheque Date: " + txtChequeNDate.Text;
                }
                else
                {
                    Narration = "Being the amount paid to " + txtDebited.Text + " From " + txtCredited.Text + " by " + ddlTransactionMode.Text;
                }
            }
               
            else if (ddlVoucherType.Text=="RECEIVE")
            {
                if (ddlTransactionMode.Text == "CASH")
                {
                    Narration = "Being the amount receive from " + txtCredited.Text + " To " + txtDebited.Text + " by " + ddlTransactionMode.Text;
                }
                else if (ddlTransactionMode.Text == "CASH CHEQUE" || ddlTransactionMode.Text == "A/C PAYEE CHEQUE")
                {
                    Narration = "Being the amount receive from " + txtCredited.Text + " To " + txtDebited.Text + " by " + ddlTransactionMode.Text + " Cheque No. " + txtChequeNo.Text + " Cheque Date: " + txtChequeNDate.Text;
                }
                else
                {
                    Narration = "Being the amount receive from " + txtCredited.Text + " To " + txtDebited.Text + " by " + ddlTransactionMode.Text;
                }
            }
                
            else if (ddlVoucherType.Text == "JOURNAL")
            {
                if (ddlTransactionMode.Text == "CASH")
                {
                    Narration = "Being the amount receive from " + txtCredited.Text + " To " + txtDebited.Text + " by " + ddlTransactionMode.Text;
                }
                else if (ddlTransactionMode.Text == "CASH CHEQUE" || ddlTransactionMode.Text == "A/C PAYEE CHEQUE")
                {
                    Narration = "Being the amount receive from " + txtCredited.Text + " To " + txtDebited.Text + " by " + ddlTransactionMode.Text + " Cheque No. " + txtChequeNo.Text + " Cheque Date: " + txtChequeNDate.Text;
                }
                else
                {
                    Narration = "Being the amount receive from " + txtCredited.Text + " by " + ddlTransactionMode.Text;
                }
            }
        }
        
        protected void ddlVoucherType_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (ddlVoucherType.SelectedIndex==0)
            {
                ltlTable.Text = "<tr class='text-center' align='center' style='background-color: #D9EDF7; font-size: 13px; height: 30px; text-align:center'>  <th align='center' scope='col' style='width: 2%;'>SL</th><th align='center' scope='col' style='width: 18%;'><span id='ContentPlaceHolder1_gvDetails_lblDebit'>Payment To</span></th><th align='center' scope='col' style='width: 18%;'><span id='ContentPlaceHolder1_gvDetails_lblCredit'>Payment From</span></th><th align='center' scope='col' style='width: 10%;'>Office</th><th align='center' scope='col' style='width: 6%;'>Transaction Mode</th><th align='center' scope='col' style='width: 10%;'>Cheque No</th><th align='center' scope='col' style='width: 10%;'>Cheque Date</th><th align='center' scope='col' style='width: 10%;'>Remarks</th><th align='center' scope='col' style='width: 10%;'>Amount</th><th align='center' scope='col' style='width: 10%;'></th><tr/>";
              

            }
              else if(ddlVoucherType.SelectedIndex==1)
            {
                ltlTable.Text = "<tr class='text-center' align='center' style='background-color: #D9EDF7; font-size: 13px; height: 30px;'>  <th align='center' scope='col' style='width: 2%;'>SL</th><th align='center' scope='col' style='width: 18%;'><span id='ContentPlaceHolder1_gvDetails_lblDebit'>Received To</span></th><th align='center' scope='col' style='width: 18%;'><span id='ContentPlaceHolder1_gvDetails_lblCredit'>Received From</span></th><th align='center' scope='col' style='width: 10%;'>Office</th><th align='center' scope='col' style='width: 6%;'>Transaction Mode</th><th align='center' scope='col' style='width: 5%;'>Cheque No</th><th align='center' scope='col' style='width: 6%;'>Cheque Date</th><th align='center' scope='col' style='width: 10%;'>Remarks</th><th align='center' scope='col' style='width: 10%;'>Amount</th><th align='center' scope='col' style='width: 10%;'></th><tr/>";
            }

            else if (ddlVoucherType.SelectedIndex == 2)
            {
                ltlTable.Text = "<tr class='text-center' align='center' style='background-color: #D9EDF7; font-size: 13px; height: 30px;'>  <th align='center' scope='col' style='width: 2%;'>SL</th><th align='center' scope='col' style='width: 18%;'><span id='ContentPlaceHolder1_gvDetails_lblDebit'>Debited To</span></th><th align='center' scope='col' style='width: 18%;'><span id='ContentPlaceHolder1_gvDetails_lblCredit'>Credited To</span></th><th align='center' scope='col' style='width: 10%;'>Office</th><th align='center' scope='col' style='width: 6%;'>Transaction Mode</th><th align='center' scope='col' style='width: 5%;'>Cheque No</th><th align='center' scope='col' style='width: 6%;'>Cheque Date</th><th align='center' scope='col' style='width: 10%;'>Remarks</th><th align='center' scope='col' style='width: 10%;'>Amount</th><th align='center' scope='col' style='width: 10%;'></th><tr/>";
            }

            else if (ddlVoucherType.SelectedIndex == 3)
            {
                ltlTable.Text = "<tr class='text-center' align='center' style='background-color: #D9EDF7; font-size: 13px; height: 30px;'>  <th align='center' scope='col' style='width: 2%;'>SL</th><th align='center' scope='col' style='width: 18%;'><span id='ContentPlaceHolder1_gvDetails_lblDebit'>Payment To</span></th><th align='center' scope='col' style='width: 18%;'><span id='ContentPlaceHolder1_gvDetails_lblCredit'>Payment From</span></th><th align='center' scope='col' style='width: 10%;'>Office</th><th align='center' scope='col' style='width: 6%;'>Transaction Mode</th><th align='center' scope='col' style='width: 5%;'>Cheque No</th><th align='center' scope='col' style='width: 6%;'>Cheque Date</th><th align='center' scope='col' style='width: 10%;'>Remarks</th><th align='center' scope='col' style='width: 10%;'>Amount</th><th align='center' scope='col' style='width: 10%;'></th><tr/>";
            }

        }

        protected void ddlTransactionMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTransactionMode.SelectedValue == "CASH")
            {
                txtChequeNo.ReadOnly = true;
                txtChequeNDate.ReadOnly = true;
              

            }
            else if (ddlTransactionMode.SelectedValue == "CASH CHEQUE")
            {
                txtChequeNo.ReadOnly = false;
                txtChequeNDate.ReadOnly = false;
                txtChequeNo.Focus();

            }
            else if (ddlTransactionMode.SelectedValue == "A/C PAYEE CHEQUE")
            {
                txtChequeNo.ReadOnly = false;
                txtChequeNDate.ReadOnly = false;
                txtChequeNo.Focus();
                
            }
            else if (ddlTransactionMode.SelectedValue == "ONLINE TRANSFER")
            {
                txtChequeNo.ReadOnly = true;
                txtChequeNDate.ReadOnly = true;
              //txtChequeNDate.
            }
            else if (ddlTransactionMode.SelectedValue == "PAY ORDER")
            {
                txtChequeNo.ReadOnly = true;
                txtChequeNDate.ReadOnly = true;
            }
            else if (ddlTransactionMode.SelectedValue == "ATM")
            {
                txtChequeNo.ReadOnly = true;
                txtChequeNDate.ReadOnly = true;
            }
            else if (ddlTransactionMode.SelectedValue == "D.D.")
            {
                txtChequeNo.ReadOnly = true;
                txtChequeNDate.ReadOnly = true;
            }
            else if (ddlTransactionMode.SelectedValue == "T.T.")
            {
                txtChequeNo.ReadOnly = true;
                txtChequeNDate.ReadOnly = true;
            }
            else if (ddlTransactionMode.SelectedValue == "OTHERS")
            {
                txtChequeNo.ReadOnly = true;
                txtChequeNDate.ReadOnly = true;
            }
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
           txtInWords.Text= ConvertClass.NumberToWords(Convert.ToInt32(txtAmount.Text));
        }
    }

   
}
