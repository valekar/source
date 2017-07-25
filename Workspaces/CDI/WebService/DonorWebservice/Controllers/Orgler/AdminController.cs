using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NLog;
using ARC.Donor.Service.Orgler.Admin;


namespace DonorWebservice.Controllers.Orgler
{
 
    [Authorize]
    [RoutePrefix("api/adminservice")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AdminServiceController : ApiController
    {
        private Logger log = LogManager.GetCurrentClassLogger();       
        private string _msg = "";
        /// <summary>
        /// This method displays all UserProfileDetails/// </summary>
        /// <param name="adminInput"></param>
        /// <returns></returns>
        [Route("getuserprofiledetails")]       
        [HttpPost]        
        public IHttpActionResult GetUserProfileDetails(ARC.Donor.Business.Orgler.Admin.AdminTabSecurityInput adminInput)
        {
            try
            {
                log.Info("Get User Profile API");
                UserProfile userprofile = new UserProfile();
                var results = userprofile.getUserProfile(adminInput);
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("User Profile API Error - " + ex.Message);
                return Ok("Error");
            }
        }
        [HttpPost]
        [Route("insertuserprofile")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult InsertUserProfile(ARC.Donor.Business.Orgler.Admin.UserProfile userProfileInput)
        {
            ARC.Donor.Service.Orgler.Admin.UserProfile userProfileServices = new ARC.Donor.Service.Orgler.Admin.UserProfile();
            try {


                var results = userProfileServices.insertUserProfile(userProfileInput);
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("Insert User Profile API Error - " + ex.Message);
                return Ok("Error");
            }
        }
        /* API methods to add tab level security */
        [AllowAnonymous]
        [HttpPost]
        [Route("addUserTabLevelSecurity")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult PostAddTabLevelSecurity([FromBody] ARC.Donor.Business.Orgler.Admin.UserTabLevelSecurity UserTabLevelSecurity)
        {
            try
            {
                ARC.Donor.Service.Orgler.Admin.UserTabLevelSecurity c = new ARC.Donor.Service.Orgler.Admin.UserTabLevelSecurity();
                c.addUserTabLevelSecurity(UserTabLevelSecurity);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return Ok("Error");
            }
        }


        [HttpPost]
        [Route("edituserprofile")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult EditUserProfile(ARC.Donor.Business.Orgler.Admin.AdminPostInput adminInput)
        {
            ARC.Donor.Service.Orgler.Admin.UserProfile userProfileServices = new ARC.Donor.Service.Orgler.Admin.UserProfile();
            // adminServices.InsertQueryLogEvent += adminServices_InsertQueryLog;
            try { 
            var results = userProfileServices.editUserProfile(adminInput);
            return Ok(results);
        }
              catch (Exception ex)
            {
                log.Info("Edit User Profile API Error - " + ex.Message);
                return Ok("Error");
            }
        }
        [HttpPost]
        [Route("deleteuserprofile")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult DeleteUserProfile(ARC.Donor.Business.Orgler.Admin.AdminPostInput adminInput)
        {
            ARC.Donor.Service.Orgler.Admin.UserProfile userProfileServices = new ARC.Donor.Service.Orgler.Admin.UserProfile();
            try
            {
                var results = userProfileServices.deleteUserProfile(adminInput);
                return Ok(results);
            }
            catch (Exception ex)
            {
                log.Info("Delete User Profile API Error - " + ex.Message);
                return Ok("Error");
            }
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
        public IHttpActionResult PostInsertLoginHistory([FromBody] ARC.Donor.Business.Orgler.Admin.LoginHistoryInput LoginHistoryInput)
        {
            try
            {
                ARC.Donor.Service.Orgler.Admin.UserProfile c = new ARC.Donor.Service.Orgler.Admin.UserProfile();
                c.insertLoginHistory(LoginHistoryInput);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                _msg = "ERROR AdminServiceController :: PostInsertLoginHistory : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
    }
}