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
    public partial class frmEmployeeLinkShift : System.Web.UI.Page
    {
        int userId = 1;
        private static int EmpId = 0;
        private static int ShftId = 0;
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
            query = "Select EmpId, EmpName From tblHRM_EmployeeDetails Order By EmpId";
            DataHelper.DataLoadToDropDownList(query, ddlEmpId, "EmpName", "EmpId");

            query = "Select EmpShiftId, EmpShift From tblHRM_EmployeeShift Order By EmpShiftId";
            DataHelper.DataLoadToDropDownList(query, ddlShiftId, "EmpShift", "EmpShiftId");
        }


        public void BindDataTable()
        {
            string query = "SELECT els.EmpLinkShiftId, ed.EmpName, es.EmpShift, els.ShiftDate, els.EndShift FROM tblHRM_EmployeeLinkShift els INNER JOIN  tblHRM_EmployeeDetails ed ON els.EmpId = ed.EmpId INNER JOIN  tblHRM_EmployeeShift es ON els.ShiftId = es.EmpShiftId";
            SqlDataReader user = DataHelper.getReader(query);

            String UnreadText = "";
            Int32 i = 0;
            while (user.Read())
            {

                UnreadText += "<tr>";
                UnreadText += "			<td class=\"center\">" + user["EmpLinkShiftId"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["EmpName"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["EmpShift"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["ShiftDate"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["EndShift"] + "</td>";
               
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

        private void formClear()
        {
            int MaxId = DataHelper.getTableMaxId_Sql("EmpLinkShiftId", "tblHRM_EmployeeLinkShift");
            txtEmpLinkShiftId.Text = MaxId.ToString();
            EmpId = 0;
            txtShiftDate.Text = "";
            txtEndShift.Text = "";
            BindDataTable();
            ddlEmpId.Focus();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ErrorFound())
                return;

            EmpId = DataHelper.getID("EmpId", "tblHRM_EmployeeDetails", "EmpName", "=", ddlEmpId.SelectedItem.Text);

            ShftId = DataHelper.getID("EmpShiftId", "tblHRM_EmployeeShift", "EmpShift", "=", ddlShiftId.SelectedItem.Text);
            

            DataHelper.executeNonQuerySP("Exec SPInsert 'Employee Link Shift','" + txtEmpLinkShiftId.Text + "','" + EmpId + "','" + ShftId + "','" + txtShiftDate.Text + "','" + txtEndShift.Text + "','" + userId + "'");

            formClear();

            
        }
        private bool ErrorFound()
        {

            EmpId = getEmpId(ddlEmpId.SelectedItem.Text);
            if (EmpId == 0)
            {
                lblErrorMsg.Text = "Select a Valid Employee Id!!!";
                return true;
            }

            ShftId = getShftId(ddlShiftId.SelectedItem.Text);
            if (ShftId == 0)
            {
                lblErrorMsg.Text = "Select a Valid Shift Id!!!";
                return true;
            }

            if (string.IsNullOrEmpty(txtShiftDate.Text))
            {
                lblErrorMsg.Text = "Input a Shift Date!!!";
                return true;
            }

            if (string.IsNullOrEmpty(txtEndShift.Text))
            {
                lblErrorMsg.Text = "Input an End Shift Date!!!";
                return true;
            }

            else
            {
                lblErrorMsg.Text = "";
                return false;

            }
        }
        private static int getEmpId(string Employee)
        {
            EmpId = DataHelper.getID("EmpId", "tblHRM_EMployeeDetails", "EmpName", "=", Employee);
            return EmpId;
        }


        private static int getShftId(string Shift)
        {
            ShftId = DataHelper.getID("EmpShiftId", "tblHRM_EmployeeShift", "EmpShift", "=", Shift);
            return ShftId;
        }
    }
}