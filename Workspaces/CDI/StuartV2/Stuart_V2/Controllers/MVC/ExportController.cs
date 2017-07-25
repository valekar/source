
using Stuart_V2.Data.Entities.Constituents;
using Stuart_V2.Models.Entities.Case;
using Stuart_V2.Models.Entities.Constituents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Stuart_V2.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Web.UI;
using Stuart_V2.Models.Entities.Cem;
namespace Stuart_V2.Controllers.MVC
{
    [RouteArea("Export")]
    [RoutePrefix("")]
    public class ExportController : BaseController 
    {

         
        JavaScriptSerializer serializer;

        public ExportController()
        {

            serializer = new JavaScriptSerializer();

        }


        [Route("constituentDetailsExport/{id}")]
        public async Task<FileContentResult> constituentDetailsExport(string ids)
        {
            string id = "1234";
            string url = BaseURL + "api/Constituent/GetConstituentPersonName/" + id;
            string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            List<Name> listNames = serializer.Deserialize<List<Name>>(res);

            url = BaseURL + "api/Constituent/GetConstituentAddress/" + id;
            res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            List<Address> listAddresses = serializer.Deserialize<List<Address>>(res);

            url = BaseURL + "api/Constituent/GetConstituentPhone/" + id;
            res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            List<Phone> listPhones = serializer.Deserialize<List<Phone>>(res);

            url = BaseURL + "api/Constituent/GetConstituentEmail/" + id;
            res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            List<Email> listEmails = serializer.Deserialize<List<Email>>(res);

            url = BaseURL + "api/Constituent/GetARCBest/" + id;
            res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            List<ARCBest> listArcBest = serializer.Deserialize<List<ARCBest>>(res);

            url = BaseURL + "api/Constituent/getconstituentbirth/" + id;
            res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            List<Birth> listBirth = serializer.Deserialize<List<Birth>>(res);

            url = BaseURL + "api/Constituent/getconstituentdeath/" + id;
            res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            List<Death> listDeath = serializer.Deserialize<List<Death>>(res);

            url = BaseURL + "api/Constituent/getconstituentcharacteristics/" + id;
            res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            List<Characteristics> listCharacteristics = serializer.Deserialize<List<Characteristics>>(res);

            url = BaseURL + "api/Constituent/getconstituentcontactpreference/" + id;
            res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            List<ContactPreference> listcontactPreference = serializer.Deserialize<List<ContactPreference>>(res);

            url = BaseURL + "api/Constituent/getconstituentexternalbridge/" + id;
            res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            List<ExternalBridge> listextBridge = serializer.Deserialize<List<ExternalBridge>>(res);

            url = BaseURL + "api/Constituent/getconstituentinternalbridge/" + id;
            res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            List<InternalBridge> listintBridge = serializer.Deserialize<List<InternalBridge>>(res);

            url = BaseURL + "api/Constituent/getconstituentgroupmembership/" + id;
            res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            List<GroupMembership> listgroupMembership = serializer.Deserialize<List<GroupMembership>>(res);


            int intExcelCurrentPointerValue = 2;
            string strExcelSectionHeaderCurrentPointerValue = "A1";
            string strExcelBlockCurrentPointerValue = "A2";
            using (ExcelPackage package = new ExcelPackage())
            {
                int wsRow = 1;
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Constituent Details");
                worksheet.Cells["A1"].Value = "Person Name";
                worksheet.Cells["A2"].LoadFromCollection(listNames, true);

                intExcelCurrentPointerValue += listNames.Count + 1;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Addresses";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listAddresses, true);

                intExcelCurrentPointerValue += listAddresses.Count + 1;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Email";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listEmails, true);

                intExcelCurrentPointerValue += listEmails.Count + 1;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Phone";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listPhones, true);

                intExcelCurrentPointerValue += listPhones.Count + 1;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Email";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listEmails, true);

                intExcelCurrentPointerValue += listEmails.Count + 1;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Arc Best";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listArcBest, true);

                intExcelCurrentPointerValue += listArcBest.Count + 1;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Birth";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listBirth, true);

                intExcelCurrentPointerValue += listBirth.Count + 1;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Death";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listDeath, true);

                intExcelCurrentPointerValue += listDeath.Count + 1;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Characteristics";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listCharacteristics, true);

                intExcelCurrentPointerValue += listCharacteristics.Count + 1;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Contact Preference";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listcontactPreference, true);

                intExcelCurrentPointerValue += listcontactPreference.Count + 1;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "External Bridge";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listextBridge, true);

                intExcelCurrentPointerValue += listextBridge.Count + 1;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Internal Bridge";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listintBridge, true);

                intExcelCurrentPointerValue += listintBridge.Count + 1;
                strExcelSectionHeaderCurrentPointerValue = "A" + intExcelCurrentPointerValue.ToString();
                strExcelBlockCurrentPointerValue = "A" + (intExcelCurrentPointerValue + 1).ToString();

                worksheet.Cells[strExcelSectionHeaderCurrentPointerValue].Value = "Group Memberships";
                worksheet.Cells[strExcelBlockCurrentPointerValue].LoadFromCollection(listgroupMembership, true);

                return new FileContentResult(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
   

    }
}