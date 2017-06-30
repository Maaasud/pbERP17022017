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
    /// Summary description for JobNoHandler
    /// </summary>
    public class JobNoHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var jobNo = context.Request["term"] ?? "";
            var query = "Select JobId from tblCnF_JobInfo Where JobId Like '%" + jobNo + "%'";
            var data = DataHelper.GetData(query).Tables[0];
            var jobNos = new List<string>();
            foreach (DataRow item in data.Rows)
            {
                jobNos.Add(item[0].ToString());
            }
            var js = new JavaScriptSerializer();
            context.Response.Write(js.Serialize(jobNos));
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