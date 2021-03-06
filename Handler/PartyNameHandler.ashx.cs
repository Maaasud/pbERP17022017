﻿using pbERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace pbERP.Handler
{
    /// <summary>
    /// Summary description for PartyNameHandler
    /// </summary>
    public class PartyNameHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var term = context.Request["term"] ?? "";
            var coatype = context.Request["coatype"] ?? "";
            var query = new StringBuilder("Select AccountsRegisterName from tblACC_AccountsRegister ");
            if (!string.IsNullOrEmpty(term) || !string.IsNullOrEmpty(coatype))
            {
                query.Append(" where ");
                if (!string.IsNullOrEmpty(term))
                    query.Append(" AccountsRegisterName like '%" + term + "%' AND ");
                if (!string.IsNullOrEmpty(coatype))
                    query.Append(" RegisterTypeId = " + coatype);
            }

            var data = DataHelper.GetData(query.ToString()).Tables[0];
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