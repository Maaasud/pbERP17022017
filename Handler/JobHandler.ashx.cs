using pbERP.Class;
using pbERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace pbERP.Handler
{
    /// <summary>
    /// Summary description for JobHandler
    /// </summary>
    public class JobHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var jobNo = context.Request["jobNo"] ?? "";
            if (jobNo == "")
                return;
            JobInfoDetails x = GetJobDetails(jobNo);
            var js = new JavaScriptSerializer();
            context.Response.Write(js.Serialize(x));
        }

        public JobInfoDetails GetJobDetails(string jobNo)
        {
            if (jobNo.Contains("|"))
            {
                var qry = new StringBuilder("Select JobNo From tblCnF_JobInfo Where JobId='"+jobNo+"'");
                jobNo = DataHelper.GetData(qry.ToString()).Tables[0].Rows[0][0].ToString();
            }

            var query = new StringBuilder("Select top 1 a.JobNo,a.JobYear,a.JobType,b.CompanyBranchName,c.AccountsRegisterName, a.PartyId,a.JobDate,0 As JobBillAmount,d.CompanyName From ");
            query.Append(" tblCnF_JobInfo As a Inner Join tblGS_ComanyBranchInfo As b On a.BranchId = b.CompanyBranchID ");
            query.Append(" Inner Join tblACC_AccountsRegister As c On a.PartyId = c.AccountsRegisterId ");
            query.Append(" Inner Join tblGS_CompanyInfo d On a.CompanyId=d.CompanyID ");
            query.Append(" where a.JobNo = '" + jobNo + "' ");

            var data = DataHelper.GetData(query.ToString()).Tables[0];
            JobInfoDetails x = new JobInfoDetails();
            if (data != null && data.Rows.Count > 0)
            {
                x = new JobInfoDetails()
                {
                    JobNo = data.Rows[0][0].ToString(),
                    Year = data.Rows[0][1].ToString(),
                    Type = data.Rows[0][2].ToString(),
                    Branch = data.Rows[0][3].ToString(),
                    PartyName = data.Rows[0][4].ToString(),
                    PartyId = data.Rows[0][5].ToString(),
                    JobDate =DateTime.Parse(data.Rows[0][6].ToString()),
                    JobBillAmount=double.Parse(data.Rows[0][7].ToString()),
                    CompanyName = data.Rows[0][8].ToString()
                };
            }

            return x;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}