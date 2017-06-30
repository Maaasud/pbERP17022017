using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pbERP.Class;

namespace pbERP.Module_Accounts.UI
{
    public partial class frmAccountsRegister : System.Web.UI.Page
    {
        int PSID = 0;
        int CityId = 0;
        int CompanyId = 0;
        int RegisterTypeId = 0;
        int maxCAID = 0;
        int maxCACId = 0;
        int maxCaLinkCac = 0;
        int userID = 1;
        DataTable dt = new DataTable();
        int ChartOfAccountControllId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtRegisterId.Text = DataHelper.getTableMaxId_Sql("AccountsRegisterId", "tblACC_AccountsRegister").ToString();
                dataloadDropDownList();

                BindDataTable();
            }
        }

        private void dataloadDropDownList()
        {
            string query = "";
            query = "Select CompanyId, CompanyName From tblGS_CompanyInfo Order By CompanyName";
            DataHelper.DataLoadToDropDownList(query, ddlCompany, "CompanyName", "CompanyId");

            query = "Select PSId,PS From tblGS_PoliceStation Order By PS";
            DataHelper.DataLoadToDropDownList(query, ddlPoliceStation, "PS", "PSID");

            query = "Select CityId,City From tblGS_City Order By City";
            DataHelper.DataLoadToDropDownList(query, ddlCity, "City", "CityId");

            query = "Select RegisterTypeId,RegisterType From tblACC_RegisterType Order By RegisterType";
            DataHelper.DataLoadToDropDownList(query, ddlRegisterType, "RegisterType", "RegisterTypeId");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> query = new List<string>();
            int counter = 0;
            int saveResult = 0;

            if (ErrorFound())
                return;
            
            PSID = DataHelper.getID("PSID", "tblGS_PoliceStation","PS","=",ddlPoliceStation.SelectedItem.Text);
            CityId = DataHelper.getID("CityId", "tblGS_City", "City", "=", ddlCity.SelectedItem.Text);
            CompanyId = DataHelper.getID("CompanyId", "tblGS_CompanyInfo", "CompanyName", "=", ddlCompany.SelectedItem.Text);
            RegisterTypeId = DataHelper.getID("RegisterTypeId", "tblACC_RegisterType", "RegisterType", "=", ddlRegisterType.SelectedItem.Text);
            maxCAID = DataHelper.getTableMaxId_Sql("ChartOfAccountId", "tblACC_ChartOfAccount");
            maxCaLinkCac = DataHelper.getTableMaxId_Sql("CALinkCAControlID", "tblACC_CA_Link_CA_Control");
            userID = 1;
            

            if (ddlRegisterType.SelectedItem.Text == "N/A")
            {
                DataHelper.executeNonQuerySP("Exec SPInsert 'Accounts Register', '" + txtRegisterId.Text + "','" + txtRegisterName.Text + "','" + txtRegisterAddress.Text + "','" + PSID + "','" + CityId + "','" + txtContactNumber.Text + "','" + txtEmergencyContact.Text + "','" + txtAccountsRegisterEmail.Text + "','" + RegisterTypeId + "',0,'" + CompanyId + "',0,'" + userID + "'");
            }

            if (ddlRegisterType.SelectedItem.Text == "Customer")
            {
                dt = DataHelper.GetData("Select ChartOfAccountID From tblACC_QueryLinkAccount Where QueryLinkAccountID=1").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ChartOfAccountControllId = int.Parse(dt.Rows[0][0].ToString());
                }

                string ChartOfAccountNo = DataHelper.MakeChartOfAccountNo(ChartOfAccountControllId, 2);

                query.Add("Exec SPInsert 'Chart Of Account','" + maxCAID + "','" + ChartOfAccountNo + "','" + txtRegisterName.Text + "',1,2,4,0,'" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortDateString() + "','" + CompanyId + "','" + userID + "'");

                counter += 1;

                query.Add("Exec SPInsert 'CA LINK CAC','" + maxCaLinkCac + "','" + maxCAID + "','" + ChartOfAccountControllId + "','" + CompanyId + "','" + userID + "'");

                counter += 1;

                query.Add("Exec SPInsert 'Accounts Register', '" + txtRegisterId.Text + "','" + txtRegisterName.Text + "','" + txtRegisterAddress.Text + "','" + PSID + "','" + CityId + "','" + txtContactNumber.Text + "','" + txtEmergencyContact.Text + "','" + txtAccountsRegisterEmail.Text + "','" + RegisterTypeId + "',0,'" + CompanyId + "','" + maxCAID + "','" + userID + "'");
                counter += 1;
            }


            if (ddlRegisterType.SelectedItem.Text == "Supplier")
            {
                dt = DataHelper.GetData("Select ChartOfAccountID From tblACC_QueryLinkAccount Where QueryLinkAccountID=2").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ChartOfAccountControllId = int.Parse(dt.Rows[0][0].ToString());
                }

                string ChartOfAccountNo = DataHelper.MakeChartOfAccountNo(ChartOfAccountControllId, 2);

                query.Add("Exec SPInsert 'Chart Of Account','" + maxCAID + "','" + ChartOfAccountNo + "','" + txtRegisterName.Text + "',2,2,4,0,'" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortDateString() + "','" + CompanyId + "','" + userID + "'");

                counter += 1;

                query.Add("Exec SPInsert 'CA LINK CAC','" + maxCaLinkCac + "','" + maxCAID + "','" + ChartOfAccountControllId + "','" + CompanyId + "','" + userID + "'");

                counter += 1;

                query.Add("Exec SPInsert 'Accounts Register', '" + txtRegisterId.Text + "','" + txtRegisterName.Text + "','" + txtRegisterAddress.Text + "','" + PSID + "','" + CityId + "','" + txtContactNumber.Text + "','" + txtEmergencyContact.Text + "','" + txtAccountsRegisterEmail.Text + "','" + RegisterTypeId + "',0,'" + CompanyId + "','" + maxCAID + "','" + userID + "'");
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

        private void clearForm()
        {
            txtRegisterId.Text = DataHelper.getTableMaxId_Sql("AccountsRegisterId", "tblACC_AccountsRegister").ToString();
            txtRegisterName.Text = "";
            txtRegisterAddress.Text = "";
            txtEmergencyContact.Text = "";
            txtContactNumber.Text = "";
            txtAccountsRegisterEmail.Text = "";
            PSID = 0;
            CityId = 0;
            CompanyId = 0;
            RegisterTypeId = 0;
            maxCAID = 0;
            maxCACId = 0;
            maxCaLinkCac = 0;
            txtAccountsRegisterEmail.Text = "";
            BindDataTable();
            txtRegisterName.Focus();
        }

        public void BindDataTable()
        {
            string query = "SELECT ac.AccountsRegisterId, ac.AccountsRegisterName, ac.Address, ac.ContactNumber1, ac.AccountsRegisterEmail, rt.RegisterType, ci.CompanyName FROM   tblACC_AccountsRegister AS ac INNER JOIN tblACC_RegisterType AS rt ON ac.RegisterTypeId = rt.RegisterTypeId INNER JOIN tblGS_CompanyInfo AS ci ON ac.CompayId = ci.CompanyID";
            SqlDataReader user = DataHelper.getReader(query);

            String UnreadText = "";
            Int32 i = 0;
            while (user.Read())
            {

                UnreadText += "<tr>";
                UnreadText += "			<td class=\"center\">" + user["AccountsRegisterId"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["AccountsRegisterName"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["Address"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["ContactNumber1"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["AccountsRegisterEmail"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["RegisterType"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["CompanyName"] + "</td>";
                UnreadText += "			<td class=\"center\">";
                UnreadText += "				<a class=\"btn btn-info\" href=\"frmAccountsRegister.aspx?ID=" + user[0] + "\">";
                UnreadText += "					<i class=\"icon-edit icon-white\"></i>  ";
                UnreadText += "					Edit                                    ";
                UnreadText += "				</a>";
                //UnreadText += "				<a class=\"btn btn-info\" href=\"Delete.aspx?ID=" + user[0] + "\">";
                //UnreadText += "					<i class=\"icon-edit icon-white\"></i>  ";
                //UnreadText += "					Delete                                    ";
                //UnreadText += "				</a>";
                UnreadText += "			</td>";
                UnreadText += "		</tr>";
                tlist.InnerHtml = UnreadText;
                i++;
            }
        }

        private bool ErrorFound()
        {
            if (string.IsNullOrEmpty(txtRegisterName.Text))
            {
                lblErrorMsg.Text = "Input Register Name!!!";
                return true;
            }
            else
                lblErrorMsg.Text = "";


            return false;
        }
    }
}