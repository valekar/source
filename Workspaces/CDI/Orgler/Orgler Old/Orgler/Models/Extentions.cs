using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace Orgler
{
    public static class Extentions
    {
        public static string GetUserName(this string strUserName)
        {
            if (strUserName.Contains("\\"))
            {
                int index = strUserName.IndexOf("\\");
                return strUserName.Substring(index + 1);
            }
             return strUserName;
        }

        public static string GetUserName(this IPrincipal ctx)
        {
            if (ctx.Identity is FormsIdentity)
            {
                return ctx.Identity.Name;
            }
            else if (ctx.Identity is WindowsIdentity)
            {
                return ctx.Identity.Name.GetUserName();
            }

            return ctx.Identity.Name;
        }
    }
}