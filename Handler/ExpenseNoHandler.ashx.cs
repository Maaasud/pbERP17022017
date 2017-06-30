using pbERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace pbERP.Handler
{
    /// <summary>
    /// Summary description for ExpenseNoHandler
    /// </summary>
    public class ExpenseNoHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var expNo = context.Request["term"] ?? "";
            var query = "Select JobExpenseId from tblCnF_JobExpenseMaster Where JobExpenseId Like '%" + expNo + "%' Order By JobExpenseId Desc";
            var data = DataHelper.GetData(query).Tables[0];
            var expNos = new List<string>();
            foreach (DataRow item in data.Rows)
            {
                expNos.Add(item[0].ToString());
            }
            var js = new JavaScriptSerializer();
            context.Response.Write(js.Serialize(expNos));
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