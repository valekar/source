using ARC.Donor.Business.Constituents;
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
    [RoutePrefix("api/group")]
    public class GroupMembershipController : ApiController
    {
        private Logger log = LogManager.GetCurrentClassLogger();
        private string _msg = "";
        /// <summary>
        /// Get the details of group membership. CEM changes have been incorporated. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetGroupMembership/{id}")]
        [ResponseType(typeof(IList<GroupMembership>))]
        public IHttpActionResult getGroupMembership(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.GroupMembership arc = new ARC.Donor.Service.Constituents.GroupMembership();
                return Ok(arc.getConstituentGroupMembership(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR GroupMembershipController :: getGroupMembership : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }
        /// <summary>
        /// Get more details of group membership of a master
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetAllGroupMembership/{id}")]
        [ResponseType(typeof(IList<AllGroupMembership>))]
        public IHttpActionResult getAllGroupMembership(string id)
        {
            try
            {
                ARC.Donor.Service.Constituents.GroupMembership arc = new ARC.Donor.Service.Constituents.GroupMembership();
                return Ok(arc.getAllGroupMembership(10, 1, id));
            }
            catch (Exception ex)
            {
                _msg = "ERROR GroupMembershipController :: getAllGroupMembership : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }


        /* Create, Delete and Update API's for Group Membership*/
        [HttpPost]
        [Route("AddGroupMembership")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult addGroupMembership([FromBody] ARC.Donor.Business.Constituents.GroupMembershipInput groupMembershipInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("GroupMembership", "Add", groupMembershipInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.GroupMembership gm = new ARC.Donor.Service.Constituents.GroupMembership();
                    var searchResults = gm.addGroupMembership(groupMembershipInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR GroupMembershipController :: addGroupMembership : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("DeleteGroupMembership")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult deleteGroupMembership([FromBody] ARC.Donor.Business.Constituents.GroupMembershipInput groupMembershipInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("GroupMembership", "Delete", groupMembershipInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.GroupMembership gm = new ARC.Donor.Service.Constituents.GroupMembership();
                    var searchResults = gm.deleteGroupMembership(groupMembershipInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR GroupMembershipController :: deleteGroupMembership : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        [HttpPost]
        [Route("EditGroupMembership")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult editGroupMembership([FromBody] ARC.Donor.Business.Constituents.GroupMembershipInput groupMembershipInput)
        {
            try
            {
                Boolean boolMandatoryCheck = checkMandatoryInputs("GroupMembership", "Edit", groupMembershipInput);
                if (boolMandatoryCheck)
                {
                    ARC.Donor.Service.Constituents.GroupMembership gm = new ARC.Donor.Service.Constituents.GroupMembership();
                    var searchResults = gm.editGroupMembership(groupMembershipInput);
                    return Ok(searchResults);
                }
                else
                {
                    return Ok("Please provide the necessary inputs");
                }
            }
            catch (Exception ex)
            {
                _msg = "ERROR GroupMembershipController :: editGroupMembership : " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
                return Ok("Error");
            }
        }

        private Boolean checkMandatoryInputs(string strRequestType, string strActionType, object InputObj)
        {
            Boolean boolMandatoryCheck = true;
            //Dictionary to hold the list of columns which are mandatory while 
            Dictionary<string, Dictionary<string, List<string>>> dictMandatoryLibrary = new Dictionary<string, Dictionary<string, List<string>>>()
            {
                {"GroupMembership", new Dictionary<string, List<string>>() {
                    {"Add", new List<string>() {"i_mstr_id", "i_cnst_typ","i_grp_mbrshp_eff_strt_dt", "i_grp_mbrshp_eff_end_dt", 
                        "i_new_group_key","i_new_assgnmnt_mthd","i_new_line_of_service_cd","i_new_arc_srcsys_cd","i_user_id"}},
                    {"Edit", new List<string>() {"i_mstr_id", "i_cnst_typ","i_grp_mbrshp_eff_strt_dt", "i_grp_mbrshp_eff_end_dt", 
                        "i_new_group_key","i_new_assgnmnt_mthd","i_new_line_of_service_cd","i_new_arc_srcsys_cd","i_user_id",
                        "i_bk_group_key","i_bk_assgnmnt_mthd","i_bk_line_of_service_cd", "i_bk_arc_srcsys_cd"}},
                    {"Delete", new List<string>()  {"i_mstr_id", "i_cnst_typ", "i_new_arc_srcsys_cd","i_grp_mbrshp_eff_strt_dt", "i_grp_mbrshp_eff_end_dt", 
                        "i_user_id","i_bk_group_key","i_bk_assgnmnt_mthd","i_bk_line_of_service_cd","i_bk_arc_srcsys_cd"}}
                 }
                }
            };
            //Fetch the mandatory fields for the inputted details type and action types
            List<string> listMandatoryColumns = new List<string>();
            foreach (KeyValuePair<string, Dictionary<string, List<string>>> keyValuePair in dictMandatoryLibrary)
            {
                if (keyValuePair.Key == strRequestType)
                {
                    foreach (KeyValuePair<string, List<string>> innerkeyValuePair in keyValuePair.Value)
                    {
                        if (innerkeyValuePair.Key == strActionType)
                        {
                            listMandatoryColumns = innerkeyValuePair.Value;
                        }
                    }
                }
            }

            //Check if the mandatory columns are present in the inputted object
            foreach (string columnName in listMandatoryColumns)
            {
                //Check if the mandatory columns are null or empty
                if (InputObj.GetType().GetProperty(columnName).GetValue(InputObj) == null)
                {
                    boolMandatoryCheck = false;
                }
                else if (string.IsNullOrEmpty(InputObj.GetType().GetProperty(columnName).GetValue(InputObj).ToString()))
                {
                    boolMandatoryCheck = false;
                }
                //Check if valid numbers are provided to the not nullable fields
                else if (InputObj.GetType().GetProperty(columnName).GetValue(InputObj).ToString() == "0")
                {
                    boolMandatoryCheck = false;
                }
            }

            return boolMandatoryCheck;
        }

    }
}
