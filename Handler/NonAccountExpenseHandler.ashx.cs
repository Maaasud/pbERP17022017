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
    /// Summary description for NonAccountExpenseHandler
    /// </summary>
    public class NonAccountExpenseHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var term = context.Request["term"] ?? "";
            var query = "Select ExpenseHead from tblGS_NonAccountExpenseHead where ExpenseHead like '%" + term + "%' Order By ExpenseHead";
            var data  = DataHelper.GetData(query).Tables[0];

            var accounts = new List<String>();
            foreach (DataRow item in data.Rows)
            {
                accounts.Add(item[0].ToString());
            }
            var js = new JavaScriptSerializer();
            context.Response.Write(js.Serialize(accounts));
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