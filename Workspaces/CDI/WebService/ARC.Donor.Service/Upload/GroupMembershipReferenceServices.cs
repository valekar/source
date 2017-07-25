using ARC.Donor.Business.Upload;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ARC.Donor.Service.Upload
{
    public class GroupMembershipReferenceServices
    {
        public IList<Business.Upload.GroupMembershipReference> GetGroupMembershipReferenceData()
        {
            Data.Upload.GroupMembershipReferenceDetails grd = new Data.Upload.GroupMembershipReferenceDetails();

            var reference_data = grd.getGroupMembershipReferenceData();

            Mapper.CreateMap<Data.Entities.Upload.GroupMembershipReference, Business.Upload.GroupMembershipReference>();

            var result = Mapper.Map<IList<Data.Entities.Upload.GroupMembershipReference>, IList<Business.Upload.GroupMembershipReference>>(reference_data);
            
            return result;
        }

        public Business.Upload.GroupMembershipReferenceDataWriteOutput postNewGroupMembershipReferenceRecord(GroupMembershipReferenceInsertData postGrpMbrshpRefRecord)
        {
            Mapper.CreateMap<ARC.Donor.Business.Upload.GroupMembershipReferenceInsertData, Data.Entities.Upload.GroupMembershipReferenceInsertData>();
            var input = Mapper.Map<ARC.Donor.Business.Upload.GroupMembershipReferenceInsertData, ARC.Donor.Data.Entities.Upload.GroupMembershipReferenceInsertData>(postGrpMbrshpRefRecord);
            
            Data.Upload.GroupMembershipReferenceDetails grd = new Data.Upload.GroupMembershipReferenceDetails();

            var postGroupMembereshipReferenceRecord = grd.postNewGroupMembershipReferenceRecord(input);


            Data.Entities.Upload.GroupMembershipReferenceDataWriteOutput insertOutput = new Data.Entities.Upload.GroupMembershipReferenceDataWriteOutput();
            var destProperties = insertOutput.GetType().GetProperties();

            foreach (var sourceProperty in postGroupMembereshipReferenceRecord.GetType().GetProperties())
            {
                foreach (var destProperty in destProperties)
                {
                    if (destProperty.Name == sourceProperty.Name &&
                destProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                    {
                        destProperty.SetValue(insertOutput, sourceProperty.GetValue(
                            postGroupMembereshipReferenceRecord, new object[] { }), new object[] { });

                        break;
                    }
                }
            }

            insertOutput.groupCode = postGrpMbrshpRefRecord.groupCode != null ? postGrpMbrshpRefRecord.groupCode : string.Empty;
            insertOutput.groupName = postGrpMbrshpRefRecord.groupName != null ? postGrpMbrshpRefRecord.groupName : string.Empty;


            Mapper.CreateMap<Data.Entities.Upload.GroupMembershipReferenceDataWriteOutput, ARC.Donor.Business.Upload.GroupMembershipReferenceDataWriteOutput>();
            var result = Mapper.Map<Data.Entities.Upload.GroupMembershipReferenceDataWriteOutput, ARC.Donor.Business.Upload.GroupMembershipReferenceDataWriteOutput>(insertOutput);
            //writeSerializedJsonDataOutput(result);
            return result;
        }

        public Business.Upload.GroupMembershipReferenceDataWriteOutput postEditGroupMembershipReferenceRecord(GroupMembershipEditReferenceParam groupMembershipEditReferenceParam)
        {
            Mapper.CreateMap<ARC.Donor.Business.Upload.GroupMembershipEditReferenceParam, Data.Entities.Upload.GroupMembershipEditReferenceParam>();
            var input = Mapper.Map<ARC.Donor.Business.Upload.GroupMembershipEditReferenceParam, ARC.Donor.Data.Entities.Upload.GroupMembershipEditReferenceParam>(groupMembershipEditReferenceParam);

            Data.Upload.GroupMembershipReferenceDetails grd = new Data.Upload.GroupMembershipReferenceDetails();

            var postGroupMembereshipReferenceRecord = grd.postEditGroupMembershipReferenceRecord(input);

            Data.Entities.Upload.GroupMembershipReferenceDataWriteOutput editOutput = new Data.Entities.Upload.GroupMembershipReferenceDataWriteOutput();
            var destProperties = editOutput.GetType().GetProperties();

            foreach (var sourceProperty in postGroupMembereshipReferenceRecord.GetType().GetProperties())
            {
                foreach (var destProperty in destProperties)
                {
                    if (destProperty.Name == sourceProperty.Name &&
                destProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                    {
                        destProperty.SetValue(editOutput, sourceProperty.GetValue(
                            postGroupMembereshipReferenceRecord, new object[] { }), new object[] { });

                        break;
                    }
                }
            }

            editOutput.groupCode = groupMembershipEditReferenceParam.groupCode != null ? groupMembershipEditReferenceParam.groupCode : string.Empty;
            editOutput.groupName = groupMembershipEditReferenceParam.groupName != null ? groupMembershipEditReferenceParam.groupName : string.Empty;

            Mapper.CreateMap<Data.Entities.Upload.GroupMembershipReferenceDataWriteOutput, ARC.Donor.Business.Upload.GroupMembershipReferenceDataWriteOutput>();

            var result = Mapper.Map<Data.Entities.Upload.GroupMembershipReferenceDataWriteOutput, ARC.Donor.Business.Upload.GroupMembershipReferenceDataWriteOutput>(editOutput);

            //writeSerializedJsonDataOutput(result);
            return result;

        }

        public Business.Upload.GroupMembershipReferenceDataWriteOutput postDeleteGroupMembershipReferenceRecord(GroupMembershipDeleteReferenceParam groupMembershipDeleteReferenceParam)
        {
            Mapper.CreateMap<ARC.Donor.Business.Upload.GroupMembershipDeleteReferenceParam, Data.Entities.Upload.GroupMembershipDeleteReferenceParam>();
            var input = Mapper.Map<ARC.Donor.Business.Upload.GroupMembershipDeleteReferenceParam, ARC.Donor.Data.Entities.Upload.GroupMembershipDeleteReferenceParam>(groupMembershipDeleteReferenceParam);

            Data.Upload.GroupMembershipReferenceDetails grd = new Data.Upload.GroupMembershipReferenceDetails();

            var postGroupMembereshipReferenceRecord = grd.postDeleteGroupMembershipReferenceRecord(input);

            Data.Entities.Upload.GroupMembershipReferenceDataWriteOutput deleteOutput = new Data.Entities.Upload.GroupMembershipReferenceDataWriteOutput();
            var destProperties = deleteOutput.GetType().GetProperties();

            foreach (var sourceProperty in postGroupMembereshipReferenceRecord.GetType().GetProperties())
            {
                foreach (var destProperty in destProperties)
                {
                    if (destProperty.Name == sourceProperty.Name &&
                destProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                    {
                        destProperty.SetValue(deleteOutput, sourceProperty.GetValue(
                            postGroupMembereshipReferenceRecord, new object[] { }), new object[] { });

                        break;
                    }
                }
            }

            deleteOutput.groupCode = string.Empty;
            deleteOutput.groupName = string.Empty;

            Mapper.CreateMap<Data.Entities.Upload.GroupMembershipReferenceDataWriteOutput, ARC.Donor.Business.Upload.GroupMembershipReferenceDataWriteOutput>();

            var result = Mapper.Map<Data.Entities.Upload.GroupMembershipReferenceDataWriteOutput, ARC.Donor.Business.Upload.GroupMembershipReferenceDataWriteOutput>(deleteOutput);
            //deleteSerializedJsonDataOutput(result);
            return result;

        }

        public void writeSerializedJsonDataOutput(Business.Upload.GroupMembershipReferenceDataWriteOutput gmwo)
        {
            var serializer = new JavaScriptSerializer();

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
                    System.IO.File.WriteAllText(@""+System.Configuration.ConfigurationManager.AppSettings["PicklistDataPath"], jsonWrite);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Error occured while writing to PickListData");
                }
            }
        }

        public void deleteSerializedJsonDataOutput(Business.Upload.GroupMembershipReferenceDataWriteOutput gmwo)
        {
            var serializer = new JavaScriptSerializer();
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
        }


        

    }
}
