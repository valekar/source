using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stuart_V2.Models.Entities;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Stuart_V2.Models
{
    public class DropdownHelper
    {
        public static List<DropdownData> getDropdownValues(string strType)
        {
            List<Stuart_V2.Models.Entities.PickListData> listPickListData = Utility.readJSONTFromFile<List<Stuart_V2.Models.Entities.PickListData>>(System.Configuration.ConfigurationManager.AppSettings["PicklistDataPath"]);
            List<DropdownData> listDropdownData = new List<DropdownData>();
            
            if (listPickListData != null && listPickListData.Count > 0)
            {
                if (strType == "ChapterGroupName" || strType == "ChapterGroupKeyName" || strType == "ChapterGroupCodeName")
                {
                    var cc = listPickListData.Where(w => w.picklist_typ == "Group Name").ToList();
                    if (cc != null && cc.Count > 0)
                    {
                        if (strType == "ChapterGroupName")
                        {
                            cc.ForEach(f => listDropdownData.Add(new DropdownData { id = f.picklist_typ_dsc, value = f.picklist_typ_dsc }));
                        }
                        else if (strType == "ChapterGroupKeyName")
                        {
                            cc.ForEach(f => listDropdownData.Add(new DropdownData { id = f.picklist_typ_key, value = f.picklist_typ_dsc }));
                        }
                        else if (strType == "ChapterGroupCodeName")
                        {
                            cc.ForEach(f => listDropdownData.Add(new DropdownData { id = f.picklist_typ_cd, value = f.picklist_typ_dsc }));
                        }
                    }

                }
                if (strType == "ChapterGroupAssignmentMethod" || strType == "ChapterGroupAssignmentMethodReference")
                {
                    var cc = listPickListData.Where(w => w.picklist_typ == "Assignment Method").ToList();
                    if (cc != null && cc.Count > 0)
                    {
                        if (strType == "ChapterGroupAssignmentMethod")
                        {
                            cc.ForEach(f => listDropdownData.Add(new DropdownData { id = f.picklist_typ_dsc, value = f.picklist_typ_dsc }));
                        }
                        else if (strType == "ChapterGroupAssignmentMethodReference")
                        {
                            cc.ForEach(f => listDropdownData.Add(new DropdownData { id = f.picklist_typ_dsc, value = f.picklist_typ_dsc }));
                        }
                    }
                }
                else
                {
                    var cc = listPickListData.Where(w => w.picklist_typ == strType).ToList();
                    if (cc != null && cc.Count > 0)
                    {
                        cc.ForEach(f => listDropdownData.Add(new DropdownData { id = f.picklist_typ_cd, value = f.picklist_typ_dsc }));
                    }
                }
            }
            return listDropdownData;

            foreach (PickListData pickListData in listPickListData)
            {
                DropdownData dropdownData = new DropdownData();
                if (strType == "Address Type")
                {
                    if (pickListData.picklist_typ == "Address Type")
                    {
                        dropdownData.id = pickListData.picklist_typ_cd;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Email Type")
                {
                    if (pickListData.picklist_typ == "Email Type")
                    {
                        dropdownData.id = pickListData.picklist_typ_cd;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Phone Type")
                {
                    if (pickListData.picklist_typ == "Phone Type")
                    {
                        dropdownData.id = pickListData.picklist_typ_cd;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Person Name Type")
                {
                    if (pickListData.picklist_typ == "Person Name Type")
                    {
                        dropdownData.id = pickListData.picklist_typ_cd;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Organization Name Type")
                {
                    if (pickListData.picklist_typ == "Organization Name Type")
                    {
                        dropdownData.id = pickListData.picklist_typ_cd;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Characteristics Type")
                {
                    if (pickListData.picklist_typ == "Characteristics Type")
                    {
                        dropdownData.id = pickListData.picklist_typ_cd;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Contact Preference Type")
                {
                    if (pickListData.picklist_typ == "Contact Preference Type")
                    {
                        dropdownData.id = pickListData.picklist_typ_cd;
                        dropdownData.value = pickListData.picklist_typ_cd;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Contact Preference Value")
                {
                    if (pickListData.picklist_typ == "Contact Preference Value")
                    {
                        dropdownData.id = pickListData.picklist_typ_cd;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Transaction Notes")
                {
                    if (pickListData.picklist_typ == "Transaction Notes")
                    {
                        dropdownData.id = pickListData.picklist_typ_dsc;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Affiliator Group Type")
                {
                    if (pickListData.picklist_typ == "Affiliator Group Type")
                    {
                        dropdownData.id = pickListData.picklist_typ_dsc;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Transaction Type")
                {
                    if (pickListData.picklist_typ == "Transaction Type")
                    {
                        dropdownData.id = pickListData.picklist_typ_dsc;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Source System Type")
                {
                    if (pickListData.picklist_typ == "Source System Type")
                    {
                        dropdownData.id = pickListData.picklist_typ_cd;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Chapter Source System Type")
                {
                    if (pickListData.picklist_typ == "Chapter Source System Type")
                    {
                        dropdownData.id = pickListData.picklist_typ_cd;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Bose Application Source System Code")
                {
                    if (pickListData.picklist_typ == "Bose Application Source System Code")
                    {
                        dropdownData.id = pickListData.picklist_typ_key;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Reference Sources")
                {
                    if (pickListData.picklist_typ == "Reference Sources")
                    {
                        dropdownData.id = pickListData.picklist_typ_key;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Case Types")
                {
                    if (pickListData.picklist_typ == "Case Types")
                    {
                        dropdownData.id = pickListData.picklist_typ_key;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Intake Channel")
                {
                    if (pickListData.picklist_typ == "Intake Channel")
                    {
                        dropdownData.id = pickListData.picklist_typ_key;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Intake Owner Department")
                {
                    if (pickListData.picklist_typ == "Intake Owner Department")
                    {
                        dropdownData.id = pickListData.picklist_typ_key;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Preference Request Type")
                {
                    if (pickListData.picklist_typ == "Preference Request Type")
                    {
                        dropdownData.id = pickListData.picklist_typ_key;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Preference Los")
                {
                    if (pickListData.picklist_typ == "Preference Los")
                    {
                        dropdownData.id = pickListData.picklist_typ_key;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Preference Channel")
                {
                    if (pickListData.picklist_typ == "Preference Channel")
                    {
                        dropdownData.id = pickListData.picklist_typ_key;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Preference Message")
                {
                    if (pickListData.picklist_typ == "Preference Message")
                    {
                        dropdownData.id = pickListData.picklist_typ_key;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "External Assessment Code")
                {
                    if (pickListData.picklist_typ == "External Assessment Code")
                    {
                        dropdownData.id = pickListData.picklist_typ_cd;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Internal Assessment Code")
                {
                    if (pickListData.picklist_typ == "Internal Assessment Code")
                    {
                        dropdownData.id = pickListData.picklist_typ_cd;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Manual Email Assessment Code")
                {
                    if (pickListData.picklist_typ == "Manual Email Assessment Code")
                    {
                        dropdownData.id = pickListData.picklist_typ_cd;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Manual Address Assessment Code")
                {
                    if (pickListData.picklist_typ == "Manual Address Assessment Code")
                    {
                        dropdownData.id = pickListData.picklist_typ_cd;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Final Assesment Code Email")
                {
                    if (pickListData.picklist_typ == "Final Assesment Code Email")
                    {
                        dropdownData.id = pickListData.picklist_typ_cd;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Address Assessment Code")
                {
                    if (pickListData.picklist_typ == "Address Assessment Code")
                    {
                        dropdownData.id = pickListData.picklist_typ_cd;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Deliverable Locator Type")
                {
                    if (pickListData.picklist_typ == "Deliverable Locator Type")
                    {
                        dropdownData.id = pickListData.picklist_typ_cd;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Deliverability Code")
                {
                    if (pickListData.picklist_typ == "Deliverability Code")
                    {
                        dropdownData.id = pickListData.picklist_typ_cd;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }

                }
                if (strType == "Upload Type")
                {
                    if (pickListData.picklist_typ == "Upload Type")
                    {
                        dropdownData.id = pickListData.picklist_typ_cd;
                        dropdownData.value = pickListData.picklist_typ_key;
                        listDropdownData.Add(dropdownData);
                    }

                }

                if (strType == "ChapterGroupName" || strType == "ChapterGroupKeyName" || strType == "ChapterGroupCodeName")
                {
                    if (pickListData.picklist_typ == "Group Name")
                    {
                        if (strType == "ChapterGroupName")
                        {
                            dropdownData.id = pickListData.picklist_typ_dsc;
                            dropdownData.value = pickListData.picklist_typ_dsc;
                            listDropdownData.Add(dropdownData);
                        }
                        if (strType == "ChapterGroupKeyName")
                        {
                            dropdownData.id = pickListData.picklist_typ_key;
                            dropdownData.value = pickListData.picklist_typ_dsc;
                            listDropdownData.Add(dropdownData);
                        }
                        if (strType == "ChapterGroupCodeName")
                        {
                            dropdownData.id = pickListData.picklist_typ_cd;
                            dropdownData.value = pickListData.picklist_typ_dsc;
                            listDropdownData.Add(dropdownData);
                        }
                    }
                }
                //uncommenting the code as drop down values are not visible
                if (strType == "Chapter Code")
                {
                    if (pickListData.picklist_typ == "Chapter Code")
                    {
                        dropdownData.id = pickListData.picklist_typ_cd;
                        dropdownData.value = pickListData.picklist_typ_dsc;
                        listDropdownData.Add(dropdownData);
                    }
                }

                if (strType == "ChapterGroupAssignmentMethod" || strType == "ChapterGroupAssignmentMethodReference")
                {
                    if (pickListData.picklist_typ == "Assignment Method")
                    {
                        if (strType == "ChapterGroupAssignmentMethod")
                        {
                            dropdownData.id = pickListData.picklist_typ_dsc;
                            dropdownData.value = pickListData.picklist_typ_dsc;
                            listDropdownData.Add(dropdownData);
                        }
                        if (strType == "ChapterGroupAssignmentMethodReference")
                        {
                            dropdownData.id = pickListData.picklist_typ_dsc;
                            dropdownData.value = pickListData.picklist_typ_dsc;
                            listDropdownData.Add(dropdownData);
                        }
                    }
                }

            }
            return listDropdownData;
        }

        public static List<DropdownData> getGroupTypeDropdownValues()
        {
            List<Stuart_V2.Models.Entities.GroupTypeData> listGroupTypeData = Utility.readJSONTFromFile<List<Stuart_V2.Models.Entities.GroupTypeData>>(System.Configuration.ConfigurationManager.AppSettings["GroupTypeDataPath"]);
            List<Stuart_V2.Models.Entities.SubGroupTypeData> listSubGroupTypeData = Utility.readJSONTFromFile<List<Stuart_V2.Models.Entities.SubGroupTypeData>>(System.Configuration.ConfigurationManager.AppSettings["SubGroupTypeDataPath"]);

            List<DropdownData> listDropdownData = new List<DropdownData>();


            foreach (GroupTypeData groupTypeData in listGroupTypeData)
            {
                DropdownData dropdownData = new DropdownData();

                dropdownData.id = groupTypeData.GroupKey;
                dropdownData.value = groupTypeData.GroupDescription;
                listDropdownData.Add(dropdownData);
            }
            return listDropdownData;
        }

        public static List<DropdownData> getSubGroupTypeDropdownValues()
        {
            List<Stuart_V2.Models.Entities.SubGroupTypeData> listSubGroupTypeData = Utility.readJSONTFromFile<List<Stuart_V2.Models.Entities.SubGroupTypeData>>(System.Configuration.ConfigurationManager.AppSettings["SubGroupTypeDataPath"]);

            List<DropdownData> listDropdownData = new List<DropdownData>();

            foreach (SubGroupTypeData subGroupTypeData in listSubGroupTypeData)
            {
                DropdownData dropdownData = new DropdownData();

                dropdownData.id = subGroupTypeData.SubGroupKey;
                dropdownData.value = subGroupTypeData.SubGroupDescription + "|" + subGroupTypeData.GroupKeyMap;
                listDropdownData.Add(dropdownData);
            }

            return listDropdownData;
        }

    }
}