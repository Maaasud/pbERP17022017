﻿using System;
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
    public partial class frmEmployeeDivision : System.Web.UI.Page
    {
        int userId = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                formClear();
            }
        }

        private void formClear()
        {
            int MaxId = DataHelper.getTableMaxId_Sql("EmpDivID", "tblHRM_EmployeeDivision");
            txtDivisionID.Text = MaxId.ToString();
            BindDataTable();
            txtDivisionName.Text = "";
            txtDivisionName.Focus();
        }


        public void BindDataTable()
        {
            string query = "SELECT EmpDivID,DivisionName FROM tblHRM_EmployeeDivision";
            SqlDataReader user = DataHelper.getReader(query);

            String UnreadText = "";
            Int32 i = 0;
            while (user.Read())
            {

                UnreadText += "<tr>";
                UnreadText += "			<td class=\"center\">" + user["EmpDivID"] + "</td>";
                UnreadText += "			<td class=\"center\">" + user["DivisionName"] + "</td>";
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

            DataHelper.executeNonQuerySP("Exec SPInsert 'Employee Division','" + txtDivisionID.Text + "','" + txtDivisionName.Text + "','"  + userId + "'");

            formClear();
        }

        private bool ErrorFound()
        {
            if (string.IsNullOrEmpty(txtDivisionName.Text))
            {
                lblErrorMsg.Text = "Input a Division Name!!!";
                return true;
            }

            else
            {
                lblErrorMsg.Text = "";
                return false;

            }
        }
    }
}