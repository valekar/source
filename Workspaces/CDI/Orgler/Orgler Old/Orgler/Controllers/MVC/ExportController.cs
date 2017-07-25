

using Orgler.Models.Entities.AccountMonitoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Orgler.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Web.UI;
namespace Orgler.Controllers.MVC
{
    public class ExportController : BaseController 
    {

         
        JavaScriptSerializer serializer;

        public ExportController()
        {

            serializer = new JavaScriptSerializer();

        }     
   

    }
}