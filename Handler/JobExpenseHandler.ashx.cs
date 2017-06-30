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
    /// Summary description for JobExpenseHandler
    /// </summary>
    public class JobExpenseHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public JobExpenseExtendedDetails getExpenseDetails(string JobNo)
        { 
            var qry=new StringBuilder("SELECT        b.SL, a.JobNo, c.JobYear, c.JobType, d.CompanyBranchName, b.JobExpenseId, e.ExpenseHead, ");
            qry.Append(" b.Amount,c.PartyId,f.AccountsRegisterName, a.ExpenseDate, a.ExpenseByAccountId,g.ChartOfAccountName ");
            qry.Append(" FROM tblCnF_JobExpenseMaster AS a INNER JOIN ");
            qry.Append(" tblCnF_JobExpenseDetails AS b ON a.JobExpenseId = b.JobExpenseId INNER JOIN ");
            qry.Append(" tblCnF_JobInfo AS c ON a.JobNo = c.JobNo INNER JOIN ");
            qry.Append(" tblGS_ComanyBranchInfo AS d ON c.BranchId = d.CompanyBranchID INNER JOIN ");
            qry.Append(" tblGS_NonAccountExpenseHead AS e ON b.JobExpenseHeadId = e.ExpenseHeadId INNER JOIN ");
            qry.Append(" tblACC_AccountsRegister AS f ON c.PartyId = f.AccountsRegisterId INNER JOIN ");
            qry.Append(" tblACC_ChartOfAccount As g ON a.ExpenseByAccountsId = g.ChartOfAccountID ");
            qry.Append(" WHERE (a.JobNo = '"+ JobNo +"') ");
            qry.Append(" GROUP BY b.SL, a.JobNo, c.JobYear, c.JobType, d.CompanyBranchName,b.Amount,e.ExpenseHead, b.Amount, ");
            qry.Append(" c.PartyId,f.AccountsRegisterName, a.ExpenseDate, a.ExpenseByAccountId,g.ChartOfAccountName");

            var data = DataHelper.GetData(qry.ToString()).Tables[0];
            JobExpenseExtendedDetails x = new JobExpenseExtendedDetails();
            if (data != null && data.Rows.Count > 0)
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    x = new JobExpenseExtendedDetails()
                    {
                        JobNo = data.Rows[0][0].ToString(),
                        Year = data.Rows[0][1].ToString(),
                        Type = data.Rows[0][2].ToString(),
                        Branch = data.Rows[0][3].ToString(),
                        ParticularsId = int.Parse(data.Rows[0][4].ToString()),
                        Particulars = data.Rows[0][5].ToString(),
                        ExpenseAmount = decimal.Parse(data.Rows[0][6].ToString()),
                        PartyId = int.Parse(data.Rows[0][7].ToString()),
                        PartyName = data.Rows[0][8].ToString(),
                        ExpenseDate = DateTime.Parse(data.Rows[0][9].ToString()),
                        ExpenseByAccountId = int.Parse(data.Rows[0][10].ToString()),
                        ExpenseChartOfAccount = data.Rows[0][11].ToString()

                    };
                }
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