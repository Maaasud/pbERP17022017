using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pbERP.Class;

using System.Data;
using System.Data.SqlClient;

namespace pbERP.Module_Accounts.UI
{
    public partial class frmChartOfAccount : System.Web.UI.Page
    {
        private static int ChartOfAccountId = 0;
        private static string ChartOfAccountNo = "";
        private static int ChartOfAccountTypeId = 0;
        private static int ChartOfAccountNatureId = 0;
        private static int CompanyId = 0;
        private static int treeLevel = 0;
        private static int userID = 1;
        private static int maxCAID = 0;
        private static int maxCACID = 0;
        private static int maxCaLinkCac = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                clearForm();
            }
        }

        private void dataLoadDropDownList()
        {
            string queryForCOA = "Select ChartOfAccountControlId, ChartOfAccountName From ViewACC_ChartOfAccountControlList Where  ChartOfAccountControlId<>0";
            DataHelper.DataLoadToDropDownList(queryForCOA, ddlChartOfAccount, "ChartOfAccountName", "ChartOfAccountControlId");

            string queryForCOAType = "Select ChartOfAccountTypeId, ChartOfAccountType From tblACC_ChartOfAccountType";
            DataHelper.DataLoadToDropDownList(queryForCOAType, ddlAccountType, "ChartOfAccountType", "ChartOfAccountTypeId");

            string queryForCompany = "Select CompanyId, CompanyName From tblGS_CompanyInfo";
            DataHelper.DataLoadToDropDownList(queryForCompany, ddlCompany, "CompanyName", "CompanyId");
        }

        public void BindDataTable()
        {
            string query = "Exec Get_ChartOfAccountListForChartOfAccountPage";
            SqlDataReader user = DataHelper.getReader(query);

            String UnreadText = "";
            Int32 i = 0;
            while (user.Read())
            {

                UnreadText += "<tr>";
                UnreadText += "			<td class=\"center\">" + user["ChartOfAccountID"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["ChartOfAccountNo"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["ChartOfAccountName"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["ChartOfAccountNature"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["ChartOfAccountType"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["TreeLevel"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["InitialBalance"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["CompanyName"] + "</td>";

                UnreadText += "			<td class=\"center\">";
                UnreadText += "				<a class=\"btn btn-info\" href=\"Edit.aspx?ID=" + user[0] + "\">";
                UnreadText += "					<i class=\"icon-edit icon-white\"></i>  ";
                UnreadText += "					Edit                                    ";
                UnreadText += "				</a>";
                UnreadText += "				<a class=\"btn btn-info\" href=\"Delete.aspx?ID=" + user[0] + "\">";
                UnreadText += "					<i class=\"icon-edit icon-white\"></i>  ";
                UnreadText += "					Delete                                    ";
                UnreadText += "				</a>";
                UnreadText += "			</td>";
                UnreadText += "		</tr>";
                tlist.InnerHtml = UnreadText;
                i++;
            }
        }
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (ErrorFound())
                    return;
                DataTable dt = new DataTable();


                ChartOfAccountId = DataHelper.getID("ChartOfAccountId", "tblACC_ChartOfAccount", "ChartOfAccountName", "=", ddlChartOfAccount.SelectedItem.Text);

                ChartOfAccountTypeId = DataHelper.getID("ChartOfAccountTypeId", "tblACC_ChartOfAccountType", "ChartOfAccountType", "=", ddlAccountType.SelectedItem.Text);

                CompanyId = DataHelper.getID("CompanyID", "tblGS_CompanyInfo", "CompanyName", "=", ddlCompany.SelectedItem.Text);



                maxCAID = DataHelper.getTableMaxId_Sql("ChartOfAccountId", "tblACC_ChartOfAccount");
                maxCACID = DataHelper.getTableMaxId_Sql("ChartOfAccountControlId", "tblACC_ChartOfAccountControl");
                maxCaLinkCac = DataHelper.getTableMaxId_Sql("CALinkCAControlID", "tblACC_CA_Link_CA_Control");

                if (txtChartOfAccountNo.Text.Substring(0, 2) == "01" || txtChartOfAccountNo.Text.Substring(0, 2) == "04")
                {
                    ChartOfAccountNatureId = 1;
                }

                if (txtChartOfAccountNo.Text.Substring(0, 2) == "02" || txtChartOfAccountNo.Text.Substring(0, 2) == "03")
                {
                    ChartOfAccountNatureId = 2;
                }

                List<string> query = new List<string>();
                int counter = 0;
                int saveResult = 0;

                if (ChartOfAccountTypeId == 1)
                {
                    query.Add("Exec SPInsert 'Chart Of Account','" + maxCAID + "','" + txtChartOfAccountNo.Text + "','" + txtChartOfAccountName.Text + "','" + ChartOfAccountNatureId + "','" + ChartOfAccountTypeId + "','" + treeLevel + "','" + txtInitialBalance.Text + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortDateString() + "','" + CompanyId + "','" + userID + "'");

                    counter += 1;

                    query.Add("Exec SPInsert 'Chart Of Account Control','" + maxCACID + "','" + maxCAID + "','" + ChartOfAccountId + "','" + CompanyId + "','" + userID + "'");

                    counter += 1;
                }

                if (ChartOfAccountTypeId == 2)
                {
                    query.Add("Exec SPInsert 'Chart Of Account','" + maxCAID + "','" + txtChartOfAccountNo.Text + "','" + txtChartOfAccountName.Text + "','" + ChartOfAccountNatureId + "','" + ChartOfAccountTypeId + "','" + treeLevel + "','" + txtInitialBalance.Text + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortDateString() + "','" + CompanyId + "','" + userID + "'");

                    counter += 1;

                    query.Add("Exec SPInsert 'CA LINK CAC','" + maxCaLinkCac + "','" + maxCAID + "','" + ChartOfAccountId + "','" + CompanyId + "','" + userID + "'");

                    counter += 1;
                }


                if (query.Count > 0)
                {
                    saveResult = DataHelper.executeNonQueryTransaction(query, counter);
                }
                if (saveResult == 1)
                {
                    lblErrorMsg.Text = "Data Saved Successfully!!!";
                    clearForm();
                }
                else
                    lblErrorMsg.Text = "Error found," + " Data not saved." + " Please try again!!!";
            }
        }

        private void clearForm()
        {
            ChartOfAccountId = 0;
            ChartOfAccountNo = "";
            ChartOfAccountTypeId = 0;
            ChartOfAccountNatureId = 0;
            CompanyId = 0;
            treeLevel = 0;
            userID = 1;
            maxCAID = 0;
            maxCACID = 0;
            maxCaLinkCac = 0;
            txtChartOfAccountName.Text = "";
            txtChartOfAccountNo.Text = "";
            txtInitialBalance.Text = "";
            dataLoadDropDownList();
            BindDataTable();
            txtChartOfAccountName.Focus();
        }

        private bool ErrorFound()
        {
            if (string.IsNullOrEmpty(txtChartOfAccountName.Text))
            {
                lblErrorMsg.Text = "Input Chart Of Account Name!!!";
                return true;
            }
            else
                lblErrorMsg.Text = "";

            DataTable dt = DataHelper.GetData("Select TreeLevel+1 From tblACC_ChartOfAccount Where ChartOfAccountId='" + ChartOfAccountId + "'").Tables[0];
            if (dt.Rows.Count > 0)
                treeLevel = int.Parse(dt.Rows[0][0].ToString());
            if (treeLevel < 1)
            {
                lblErrorMsg.Text = "Chart Of Account Tree Level Not Define!!!";
                return true;
            }
            else
                lblErrorMsg.Text = "";

            if (txtChartOfAccountNo.Text.Substring(3,2)=="00")
            {
                lblErrorMsg.Text = "Select Chart Of Account Type!!!";
                return true;
            }
            else
                lblErrorMsg.Text = "";


            return false;
        }

        protected void ddlAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ChartOfAccountId = DataHelper.getID("ChartOfAccountId", "tblACC_ChartOfAccount", "ChartOfAccountName", "=", ddlChartOfAccount.SelectedItem.Text);

                ChartOfAccountTypeId = DataHelper.getID("ChartOfAccountTypeId", "tblACC_ChartOfAccountType", "ChartOfAccountType", "=", ddlAccountType.SelectedItem.Text);

                txtChartOfAccountNo.Text = DataHelper.MakeChartOfAccountNo(ChartOfAccountId, ChartOfAccountTypeId);
            }
        }

        protected void ddlChartOfAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ChartOfAccountId = DataHelper.getID("ChartOfAccountId", "tblACC_ChartOfAccount", "ChartOfAccountName", "=", ddlChartOfAccount.SelectedItem.Text);

                ChartOfAccountTypeId = DataHelper.getID("ChartOfAccountTypeId", "tblACC_ChartOfAccountType", "ChartOfAccountType", "=", ddlAccountType.SelectedItem.Text);

                txtChartOfAccountNo.Text = DataHelper.MakeChartOfAccountNo(ChartOfAccountId, ChartOfAccountTypeId);
            }
        }
    }
}