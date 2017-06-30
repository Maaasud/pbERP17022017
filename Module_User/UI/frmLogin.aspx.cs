using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Web.Script.Services;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using pbERP.Class;

namespace pbERP.Module_User.UI
{
    public partial class frmLogin : System.Web.UI.Page
    {
        //string conStr = WebConfigurationManager.ConnectionStrings["DbConStr"].ConnectionString;
        string strSql = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            strSql = "Select GroupName,GroupDialouge From tblGS_GroupOfCompanyInfo";
            DataTable dt = DataHelper.GetData(strSql).Tables[0];

            lblCompanyGroupName.Text = dt.Rows[0]["GroupName"].ToString();
            lblGroupDialouge.Text = dt.Rows[0]["GroupDialouge"].ToString();

            //CompanyInfoLoad(null,null);
        }

        protected void CompanyInfoLoad(object sender, EventArgs e)
        {
            strSql = "Select CompanyName,CompanyDialouge,Address,CompanyPhone,CompanyFax From tblGS_CompanyInfo";
            DataTable dt = DataHelper.GetData(strSql).Tables[0];
            if (dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lblCompanyName.Text = dt.Rows[dt.Rows.Count-1 - i]["CompanyName"].ToString();
                    lblCompanyDialouge.Text = dt.Rows[i]["CompanyDialouge"].ToString();
                    lblCompanyAddress.Text = dt.Rows[i]["Address"].ToString();
                    lblCompanyPhone.Text = dt.Rows[i]["CompanyPhone"].ToString();
                    lblCompanyFax.Text = dt.Rows[i]["CompanyFax"].ToString();

                    if (dt.Rows.Count > 1)
                    {
                        i = i + 1;
                        lblCompanyName2.Text = dt.Rows[dt.Rows.Count - 1 - (dt.Rows.Count - i)]["CompanyName"].ToString();
                        lblCompanyDialouge2.Text = dt.Rows[i]["CompanyDialouge"].ToString();
                        lblCompanyAddress2.Text = dt.Rows[i]["Address"].ToString();
                        lblCompanyPhone2.Text = dt.Rows[i]["CompanyPhone"].ToString();
                        lblCompanyFax2.Text = dt.Rows[i]["CompanyFax"].ToString();
                    }

                    if (dt.Rows.Count > 2)
                    {
                        i = i + 1;
                        lblCompanyName3.Text = dt.Rows[dt.Rows.Count - 1 - (dt.Rows.Count - i)]["CompanyName"].ToString();
                        lblCompanyDialouge3.Text = dt.Rows[i]["CompanyDialouge"].ToString();
                        lblCompanyAddress3.Text = dt.Rows[i]["Address"].ToString();
                        lblCompanyPhone3.Text = dt.Rows[i]["CompanyPhone"].ToString();
                        lblCompanyFax3.Text = dt.Rows[i]["CompanyFax"].ToString();
                    }
                    
                    if (dt.Rows.Count > 3)
                    {
                        i = i + 1;
                        lblCompanyName3.Text = dt.Rows[dt.Rows.Count - 1 - (dt.Rows.Count - i)]["CompanyName"].ToString();
                        lblCompanyDialouge3.Text = dt.Rows[i]["CompanyDialouge"].ToString();
                        lblCompanyAddress3.Text = dt.Rows[i]["Address"].ToString();
                        lblCompanyPhone3.Text = dt.Rows[i]["CompanyPhone"].ToString();
                        lblCompanyFax3.Text = dt.Rows[i]["CompanyFax"].ToString();
                    }
                }
            }
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string LogId = string.Format("{0}", Request.Form["txtLoginId"]);
            string Password = string.Format("{0}", Request.Form["txtPass"]);

            strSql = "Select * From tblUSR_UserInfo Where UserLoginName='"+ LogId +"' and UserPassword='"+ Password +"'";
            DataTable dt = DataHelper.GetData(strSql).Tables[0];
           
            if (txtBranchName.Text!="" && txtCompanyName.Text!="" )
            {
                if (dt.Rows.Count > 0)
                {
                    Session["userName"] = LogId.ToString();
                    Session["RunninguserId"] = DataHelper.getID("UserID", "tblUSR_UserInfo", "UserLoginName", "=", LogId.ToString());
                    Session["RunningCompanyName"] = txtCompanyName.Text;
                    Session["RunningCompanyId"] = DataHelper.getID("CompanyId", "tblGS_CompanyInfo", "CompanyName", "=", txtCompanyName.Text.ToString());
                    Session["RunningCompanyBranchName"] = txtBranchName.Text;
                    Session["RunningCompanyBranchId"] = DataHelper.getID("CompanyBranchID", "tblGS_ComanyBranchInfo", "CompanyBranchName", "=", txtBranchName.Text.ToString());
                    Response.Redirect("~/Home.aspx");
                }
                else
                {
                    //Set a Error ID Message
                    Response.Redirect("~/Module_User/UI/frmLogin.aspx");
                }
            }
            else
            {
                //Set a Error ID Message
               
                if (txtCompanyName.Text == "")
                {
                    Literal1.Text = "Please fill company name";
                }
                if (txtBranchName.Text == "")
                {
                    Literal2.Text = "Please fill branch name";
                }
                Response.Redirect("~/Module_User/UI/frmLogin.aspx");
            }
           
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<string> GetCompanyName(string pre)
        {
            List<string> searchResult = new List<string>();
            string query = "Select CompanyName From tblGS_CompanyInfo Where CompanyName Like '%" + pre + "%' Order By CompanyName";
            return searchResult = DataHelper.getSearchList(query);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<string> GetBranchName(string pre)
        {
            List<string> searchResult = new List<string>();
            string query = "Select CompanyBranchName From tblGS_ComanyBranchInfo Where CompanyBranchName Like '%" + pre + "%' Order By CompanyBranchName";
            return searchResult = DataHelper.getSearchList(query);
        }

        protected void txtCompanyName_TextChanged(object sender, EventArgs e)
        {
            if (txtCompanyName.Text == "")
            {
                Literal1.Text = "Please fill company name";
            }
        }

        protected void txtBranchName_TextChanged(object sender, EventArgs e)
        {
            if (txtCompanyName.Text == "")
            {
                Literal1.Text = "Please fill company name";
            }
        }


        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public static List<string> GetCompanyName(string pre)
        //{

        //    List<string> allCompanyName = new List<string>();
        //    using (Model.pbERP_MainEntities_Server dc = new Model.pbERP_MainEntities_Server())
        //    {
        //        allCompanyName = (from a in dc.tblGS_CompanyInfo
        //                          where a.CompanyName.StartsWith(pre)
        //                          select a.CompanyName).ToList();
        //    }
        //    return allCompanyName;
        //}
    }
}