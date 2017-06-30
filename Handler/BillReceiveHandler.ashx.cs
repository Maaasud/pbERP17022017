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
    /// Summary description for BillReceiveHandler
    /// </summary>
    public class BillReceiveHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            
        }

        public int getBillReceiveMaxId()
        {
            int maxId = 0;
            var BillReceiveMaxId = DataHelper.GetData("Select IsNull(Max(JobReceiveId),0)+1 From tblCnF_JobReceiveInfo").Tables[0].Rows[0][0].ToString();
            return maxId = int.Parse(BillReceiveMaxId);
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