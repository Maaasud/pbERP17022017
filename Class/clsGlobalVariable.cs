using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace pbERP.Class
{
    public class clsGlobalVariable
    {
        public static int RunningCompanyId = 1;
        public static string RunningCompanyName = "";
        public static int RunningCompanyBranchId = 1;
        public static string RunningCompanyBranchName = "";
        public static int RunninguserId = 1;
        public static string userName = "";

        internal static bool AuthorizeUser(HttpSessionState session)
        {
            if (session["RunninguserId"] != null && session["userName"] != null)
                return true;
            return false;

        }
    }
}