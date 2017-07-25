using ARC.Donor.Business;
using ARC.Donor.Business.Admin;
using ARC.Donor.Service;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Http.Description;

namespace DonorWebservice.Controllers
{
    
    [RoutePrefix("api/login")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class LoginController : ApiController
    {
        private Logger log = LogManager.GetCurrentClassLogger();
        private string _msg = "";
        LoginController()
        {

        }
       
        /// <summary>
        /// Insert into Login History
        /// </summary>
        /// <param name="LoginHistoryInput"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("insertloginhistory")]
         [ResponseType(typeof(string))]
        public IHttpActionResult PostInsertLoginHistory([FromBody] ARC.Donor.Business.Login.LoginHistoryInput LoginHistoryInput)
        {
            try
            {
                ARC.Donor.Service.Login.LogUserHistory c = new ARC.Donor.Service.Login.LogUserHistory();
                c.insertLoginHistory(LoginHistoryInput);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                _msg = "ERROR LoginController :: PostInsertLoginHistory : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /* API methods to add tab level security */
        /// <summary>
        /// Add user tab level into user profile. Used if the user logs in for the first time
        /// </summary>
        /// <param name="UserTabLevelSecurity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("addusertablevelsecurity")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PostAddTabLevelSecurity([FromBody] ARC.Donor.Business.Login.UserTabLevelSecurity UserTabLevelSecurity)
        {
            try
            {
                ARC.Donor.Service.Login.UserTabLevelSecurity c = new ARC.Donor.Service.Login.UserTabLevelSecurity();
                c.addUserTabLevelSecurity(UserTabLevelSecurity);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                _msg = "ERROR LoginController :: addusertablevelsecurity : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        /// <summary>
        /// Used for editing of user if there are any changes in the role of user while logging in
        /// </summary>
        /// <param name="adminInput"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("EditTabLevelSecurity")]
        [ResponseType(typeof(AdminTransOutput))]
        public IHttpActionResult EditTabLevelSecurity(ARC.Donor.Business.Admin.AdminPostInput adminInput)
        {
            try
            {
                ARC.Donor.Service.Admin.AdminServices adminServices = new ARC.Donor.Service.Admin.AdminServices();
                adminServices.InsertQueryLogEvent += adminServices_InsertQueryLog;
                var results = adminServices.editLoginTabLevelSecurity(adminInput);
                return Ok(results);
            }
           
             catch (Exception ex)
            {
                _msg = "ERROR LoginController :: EditTabLevelSecurity : " + ex.Message;
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
