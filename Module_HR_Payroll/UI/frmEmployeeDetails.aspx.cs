using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using pbERP.Class;

namespace pbERP.Module_HR_Payroll.UI
{
    public partial class frmEmployeeDetails : System.Web.UI.Page
    {
        int userId = 1;
        private static int PSIdPres = 0;
        private static int CityIdPres = 0;
        private static int PSIdPer = 0;
        private static int CityIdPer = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                formClear();
                dataloadDropDownList();
            }
        }

        private void dataloadDropDownList()
        {
            string query = "";
            query = "Select PSId, PS From tblGS_PoliceStation Order By PSId";
            DataHelper.DataLoadToDropDownList(query, ddlPresentPSId, "PS", "PSId");

            query = "Select CityId, City From tblGS_City Order By CityId";
            DataHelper.DataLoadToDropDownList(query, ddlPresentCityId, "City", "CityId");

            query = "Select PSId, PS From tblGS_PoliceStation Order By PSId";
            DataHelper.DataLoadToDropDownList(query, ddlPermanentPStId, "PS", "PSId");

            query = "Select CityId, City From tblGS_City Order By CityId";
            DataHelper.DataLoadToDropDownList(query, ddlPermanentCItyId, "City", "CityId");
        }

        private void formClear()
        {
            int MaxId = DataHelper.getTableMaxId_Sql("EmpId", "tblHRM_EmployeeDetails");
            txtEmployeeID.Text = MaxId.ToString();

            DataTable dt = DataHelper.GetData("Select Value From tblGS_GlobalOption Where OptionId=1").Tables[0];
            if (dt.Rows.Count > 0)
            {
                string EmpCode = "";
                string prefix = dt.Rows[0][0].ToString();
                DataTable dtt = DataHelper.GetData("Select Right(EmpCode,5)+1 As EmpLastNum From tblHRM_EmployeeDetails").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    int LastNum = int.Parse(dtt.Rows[0][0].ToString());

                    if (LastNum.ToString().Length == 1)
                    {
                        EmpCode = prefix + "0000" + LastNum;
                    }

                    if (LastNum.ToString().Length == 2)
                    {
                        EmpCode = prefix + "000" + LastNum;
                    }

                    if (LastNum.ToString().Length == 3)
                    {
                        EmpCode = prefix + "00" + LastNum;
                    }

                    if (LastNum.ToString().Length == 4)
                    {
                        EmpCode = prefix + "0" + LastNum;
                    }

                    if (LastNum.ToString().Length == 5)
                    {
                        EmpCode = prefix + LastNum;
                    }

                    txtEmployeeCode.Text = EmpCode;
                }
            }

            txtEmployeeName.Text = "";
            txtFathersName.Text = "";
            txtMothersName.Text = "";
            txtPresentAddress.Text = "";
            txtPermanentAddress.Text = "";
            txtMobileNo.Text = "";
            txtEmergencyContact.Text = "";
            txtDateOfBirth.Text = "";
            txtBloodGroup.Text = "";
            txtLastDegreeOfEdu.Text = "";
            BindDataTable();
            txtEmployeeName.Focus();

        }

        public void BindDataTable()
        {
            string query = "SELECT EmpId,EmpCode,EmpName,FathersName,MothersName,EmpPresentAddress FROM tblHRM_EmployeeDetails";
            SqlDataReader user = DataHelper.getReader(query);
            String UnreadText = "";
            Int32 i = 0;
            while (user.Read())
            {

                UnreadText += "<tr>";
                UnreadText += "			<td class=\"center\">" + user["EmpId"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["EmpCode"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["EmpName"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["FathersName"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["MothersName"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["EmpPresentAddress"] + "</td>";
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
            if (ErrorFound())
                return;

            DataHelper.executeNonQuerySP("Exec SPInsert 'Employee Details','" + txtEmployeeID.Text + "','" + txtEmployeeCode.Text + "','" + txtEmployeeName.Text + "','" + txtFathersName.Text + "','" + txtMothersName.Text + "','" + txtPresentAddress.Text + "','" + ddlPresentPSId.SelectedValue + "','" + ddlPresentCityId.SelectedValue + "','" + txtPermanentAddress.Text + "','" + ddlPermanentPStId.SelectedValue + "','" + ddlPermanentCItyId.SelectedValue + "','" + txtMobileNo.Text + "','" + txtEmergencyContact.Text + "','" + txtDateOfBirth.Text + "','" + ddlGender.SelectedValue + "','" + ddlReligion.SelectedValue + "','" + txtBloodGroup.Text + "','" + txtLastDegreeOfEdu.Text + "',0,'" + userId + "'");

            formClear();
        }
        private bool ErrorFound()
        {
            if (string.IsNullOrEmpty(txtEmployeeName.Text))
            {
                lblErrorMsg.Text = "Input a Employee Name!!!";
                return true;
            }

            if (string.IsNullOrEmpty(txtFathersName.Text))
            {
                lblErrorMsg.Text = "Input a Father's Name!!!";
                return true;
            }

            if (string.IsNullOrEmpty(txtMothersName.Text))
            {
                lblErrorMsg.Text = "Input a Mother's Name!!!";
                return true;
            }

            if (string.IsNullOrEmpty(txtPresentAddress.Text))
            {
                lblErrorMsg.Text = "Input a Present Address!!!";
                return true;
            }
            if (string.IsNullOrEmpty(txtPermanentAddress.Text))
            {
                lblErrorMsg.Text = "Input a Permanent Address!!!";
                return true;
            }
            if (string.IsNullOrEmpty(txtPresentAddress.Text))
            {
                lblErrorMsg.Text = "Input a Present Address!!!";
                return true;
            }

            PSIdPres = getPSIdPres(ddlPresentPSId.SelectedItem.Text);
            if (PSIdPres == 0)
            {
                lblErrorMsg.Text = "Select a Valid Police Station Name!!!";
                return true;
            }

            CityIdPres = getCityIdPres(ddlPresentCityId.SelectedItem.Text);
            if (CityIdPres == 0)
            {
                lblErrorMsg.Text = "Select a Valid City Name!!!";
                return true;
            }


            PSIdPer = getPSIdPer(ddlPermanentPStId.SelectedItem.Text);
            if (PSIdPer == 0)
            {
                lblErrorMsg.Text = "Select a Valid Police Station Name!!!";
                return true;
            }

            CityIdPer = getCityIdPer(ddlPermanentCItyId.SelectedItem.Text);
            if (CityIdPer == 0)
            {
                lblErrorMsg.Text = "Select a Valid City Name!!!";
                return true;
            }

            else
            {
                lblErrorMsg.Text = "";
                return false;

            }
        }
        private static int getPSIdPres(string PS)
        {
           PSIdPres = DataHelper.getID("PSId", "tblGS_PoliceStation", "PS", "=", PS);
            return PSIdPres;
        }


        private static int getCityIdPres(string City)
        {
            CityIdPres = DataHelper.getID("CityId", "tblGS_City", "City", "=", City);
            return CityIdPres;
        }
        private static int getPSIdPer(string PS)
         {
           PSIdPer = DataHelper.getID("PSId", "tblGS_PoliceStation", "PS", "=", PS);
            return PSIdPres;
        }

        private static int getCityIdPer(string City)
        {
            CityIdPer = DataHelper.getID("CityId", "tblGS_City", "City", "=", City);
            return CityIdPres;
        }
       
    }
}