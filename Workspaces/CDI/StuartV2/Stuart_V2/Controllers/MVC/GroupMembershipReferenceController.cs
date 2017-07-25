using Newtonsoft.Json;
using Stuart_V2.Exceptions;
using Stuart_V2.Models;
using Stuart_V2.Models.Entities;
using Stuart_V2.Models.Entities.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Stuart_V2.Controllers.MVC
{
    [RouteArea("GroupMembershipReferenceNative")]
    [RoutePrefix("")]
    [Authorize]
    public class GroupMembershipReferenceNativeController:BaseController
    {      

        [HandleException]
        [TabLevelSecurity("upload_tb_access", "RW")]
        public async Task<JsonResult> getGroupMembershipReferenceData()
        {
            string url = BaseURL + "api/Upload/getGroupMembershipReferenceData/";
            string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [TabLevelSecurity("upload_tb_access", "RW")]
        public async Task<JsonResult> postAddNewGrpMbrshpRefRecordParams(GroupMembershipReferenceInsertData addRefDataRec)
        {
            string url = BaseURL + "api/Upload/postNewGroupMembershipReferenceRecord/";
            string res = await Models.Resource.PostResourceAsync(url, Token, addRefDataRec, ClientID);
            new Task(async () => await writeSerializedJsonDataOutput(res)).Start();
            return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [TabLevelSecurity("upload_tb_access", "RW")]
        public async Task<JsonResult> postDeleteGrpMbrshpRefRecordParam (GroupMembershipDeleteReferenceParam gm)
        {
            string url = BaseURL + "api/Upload/postDeleteMembershipReferenceRecord/";
            string res = await Models.Resource.PostResourceAsync(url, Token, gm, ClientID);
            new Task(async () => await deleteSerializedJsonDataOutput(res)).Start();
            return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [TabLevelSecurity("upload_tb_access", "RW")]
        public async Task<JsonResult> postEditGrpMbrshpRefRecordParam(GroupMembershipEditReferenceParam gm)
        {
            string url = BaseURL + "api/Upload/postEditMembershipReferenceRecord/";
            string res = await Models.Resource.PostResourceAsync(url, Token, gm, ClientID);
            new Task(async () => await writeSerializedJsonDataOutput(res)).Start();
            return handleTrivialHttpRequests(res);
        }


        public async Task writeSerializedJsonDataOutput(string res)
        {
            await Task.Run(() =>
            {
                var serializer = new JavaScriptSerializer();
                GroupMembershipReferenceDataWriteOutput gmwo = JsonConvert.DeserializeObject<GroupMembershipReferenceDataWriteOutput>(res);
                bool flag = true;
                if (gmwo.transOutput.ToUpper() == "SUCCESS" && (gmwo.groupCode != null || gmwo.groupName != null))
                {
                    try
                    {
                        string json = System.Configuration.ConfigurationManager.AppSettings["PicklistDataPath"];
                        List<PicklistDataHelper> deserializedPicklistData = JsonConvert.DeserializeObject<List<PicklistDataHelper>>(System.IO.File.ReadAllText(json));
                        foreach (var item in deserializedPicklistData)
                        {
                            if (item.picklist_typ_key == gmwo.groupKey.ToString())
                            {
                                item.picklist_typ_cd = gmwo.groupCode != null ? gmwo.groupCode : string.Empty;
                                item.picklist_typ_dsc = gmwo.groupName != null ? gmwo.groupName : string.Empty;
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            PicklistDataHelper newPickListData = new PicklistDataHelper("Group Name");
                            newPickListData.picklist_typ_cd = gmwo.groupCode;
                            newPickListData.picklist_typ_key = gmwo.groupKey.ToString();
                            newPickListData.picklist_typ_dsc = gmwo.groupName;
                            newPickListData.ref_order = null;
                            deserializedPicklistData.Add(newPickListData);
                        }
                        string jsonWrite = JsonConvert.SerializeObject(deserializedPicklistData.ToArray(), Formatting.Indented);
                        //write string to file
                        System.IO.File.WriteAllText(@"" + System.Configuration.ConfigurationManager.AppSettings["PicklistDataPath"], jsonWrite);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine("Error occured while writing to PickListData");
                    }
                }
            });
        }

        public async Task deleteSerializedJsonDataOutput(string res)
        {
            await Task.Run(() => {
                var serializer = new JavaScriptSerializer();
                GroupMembershipReferenceDataWriteOutput gmwo = JsonConvert.DeserializeObject<GroupMembershipReferenceDataWriteOutput>(res);
                if (gmwo.transOutput.ToUpper() == "SUCCESS" && string.IsNullOrEmpty(gmwo.groupCode) && string.IsNullOrEmpty(gmwo.groupName))
                {
                    try
                    {
                        string json = System.Configuration.ConfigurationManager.AppSettings["PicklistDataPath"];
                        List<PicklistDataHelper> deserializedPicklistData = JsonConvert.DeserializeObject<List<PicklistDataHelper>>(System.IO.File.ReadAllText(json));

                        var item = deserializedPicklistData.SingleOrDefault(x => x.picklist_typ_key == gmwo.groupKey.ToString());
                        if (item != null)
                            deserializedPicklistData.Remove(item);

                        string jsonWrite = JsonConvert.SerializeObject(deserializedPicklistData.ToArray(), Formatting.Indented);

                        //write string to file
                        System.IO.File.WriteAllText(@"" + System.Configuration.ConfigurationManager.AppSettings["PicklistDataPath"], jsonWrite);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine("Error occured while writing to PickListData");
                    }
                }
            });
           
        }

      
    }
}