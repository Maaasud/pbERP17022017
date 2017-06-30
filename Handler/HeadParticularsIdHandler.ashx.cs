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
    /// Summary description for HeadParticularsIdHandler
    /// </summary>
    public class HeadParticularsIdHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var ExpenseHead = context.Request["Particulars"] ?? "";
            if (ExpenseHead == "")
                return;

            ExpenseHeadDetails x = getExpenseHeadDetails(ExpenseHead);
        }

        public ExpenseHeadDetails getExpenseHeadDetails(string ExpenseHead)
        {
            var qry = new StringBuilder("Select * From tblGS_NonAccountExpenseHead Where ExpenseHead='"+ExpenseHead+"'");
            var data = DataHelper.GetData(qry.ToString()).Tables[0];

            ExpenseHeadDetails x = new ExpenseHeadDetails();
            if (data != null && data.Rows.Count > 0)
            {
                x = new ExpenseHeadDetails()
                {
                    ExpenseCategoryId = (int)data.Rows[0][0],
                    ExpenseHeadId = (int)data.Rows[0][1],
                    ExpenseHead = data.Rows[0][2].ToString(),
                    Remarks = data.Rows[0][3].ToString()
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