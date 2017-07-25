using ARC.Donor.Service;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace DonorWebservice.Controllers
{
    [Authorize]
    [RoutePrefix("api/Admin")]
    [ApiExplorerSettings(IgnoreApi=true)]
    public class AdminController : ApiController
    {
        private Logger log = LogManager.GetCurrentClassLogger();
        private string _msg = "";
        [HttpPost]
        [Route("GetTabLevelSecurity")]
        public IHttpActionResult GetTabLevelSecurity(ARC.Donor.Business.Admin.AdminTabSecurityInput adminInput)
        {
            try
            {
                ARC.Donor.Service.Admin.AdminServices adminServices = new ARC.Donor.Service.Admin.AdminServices();
                adminServices.InsertQueryLogEvent += adminServices_InsertQueryLog;
                var results = adminServices.getTabLevelSecurity(adminInput);
                return Ok(results);
            }
            catch(Exception ex)
            {
                _msg = "ERROR AdminController :: GetTabLevelSecurity : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("InsertTabLevelSecurity")]
        public IHttpActionResult InsertTabLevelSecurity(ARC.Donor.Business.Admin.AdminPostInput adminInput)
        {
            try
            {
                ARC.Donor.Service.Admin.AdminServices adminServices = new ARC.Donor.Service.Admin.AdminServices();
                adminServices.InsertQueryLogEvent += adminServices_InsertQueryLog;
                var results = adminServices.insertTabLevelSecurity(adminInput);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _msg = "ERROR AdminController :: InsertTabLevelSecurity : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
           
        }

        [HttpPost]
        [Route("EditTabLevelSecurity")]
        public IHttpActionResult EditTabLevelSecurity(ARC.Donor.Business.Admin.AdminPostInput adminInput)
        {
            try
            {
                ARC.Donor.Service.Admin.AdminServices adminServices = new ARC.Donor.Service.Admin.AdminServices();
                adminServices.InsertQueryLogEvent += adminServices_InsertQueryLog;
                var results = adminServices.editTabLevelSecurity(adminInput);
                return Ok(results);
            }
           

             catch (Exception ex)
            {
                _msg = "ERROR AdminController :: EditTabLevelSecurity : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }


        [HttpPost]
        [Route("DeleteTabLevelSecurity")]
        public IHttpActionResult DeleteTabLevelSecurity(ARC.Donor.Business.Admin.AdminPostInput adminInput)
        {
            try
            {
                ARC.Donor.Service.Admin.AdminServices adminServices = new ARC.Donor.Service.Admin.AdminServices();
                adminServices.InsertQueryLogEvent += adminServices_InsertQueryLog;
                var results = adminServices.deleteTabLevelSecurity(adminInput);
                return Ok(results);
            }
           
             catch (Exception ex)
            {
                _msg = "ERROR AdminController :: DeleteTabLevelSecurity : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }



        private void adminServices_InsertQueryLog(object sender, QueryLogEventArgs e)
        {
            var qry = new ClientValidation.QueryTimeLogger();
            qry.UserName = e.UserName;
            qry.Query = e.Query;
            qry.StartTime = e.StartTime;
            qry.EndTime = e.EndTime;
            qry.Action = e.Action;
            Models.QueryLogger.InsertQuery(qry);
        }
    }
}
