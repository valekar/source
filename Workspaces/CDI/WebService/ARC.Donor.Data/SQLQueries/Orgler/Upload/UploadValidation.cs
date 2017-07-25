using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities;

namespace ARC.Donor.Data.SQLQueries.Orgler.Upload
{
    public class UploadValidation
    {
        //Validate Enterprise Ids
        public static CrudOperationOutput getValidEnterpriseIdsSQL(List<string> ltInput)
        {
            CrudOperationOutput output = new CrudOperationOutput();

            string strEnterpriseList = string.Empty;
            strEnterpriseList = string.Join(",", ltInput).ToString();
            
            string strQuery = string.Empty;
            strQuery = "SELECT DISTINCT ent_org_id FROM arc_mdm_vws.bz_cdim_enterprise_orgs ";

            if (!string.IsNullOrEmpty(strEnterpriseList))
            {
                strQuery += " WHERE ent_org_id IN (" + strEnterpriseList + ");";
            }
            else
            {
                strQuery += " WHERE 1=0;";
            }

            output.strSPQuery = strQuery;
            return output;
        }

        //Validate Master Ids
        public static CrudOperationOutput getValidMasterIdsSQL(List<string> ltInput)
        {
            CrudOperationOutput output = new CrudOperationOutput();

            string strMasterList = string.Empty;
            strMasterList = string.Join(",", ltInput).ToString();

            string strQuery = string.Empty;
            strQuery = "SELECT DISTINCT cnst_mstr_id FROM arc_mdm_vws.bz_cnst_mstr ";

            if (!string.IsNullOrEmpty(strMasterList))
            {
                strQuery += " WHERE cnst_mstr_id IN (" + strMasterList + ") AND cnst_typ_cd = 'OR';";
            }
            else
            {
                strQuery += " WHERE 1=0;";
            }

            output.strSPQuery = strQuery;
            return output;
        }

        //Validate Naics Codes
        public static CrudOperationOutput getValidNaicsCodeSQL(List<string> ltInput)
        {
            CrudOperationOutput output = new CrudOperationOutput();

            string strNaicsCodeList = string.Empty;
            foreach (string s in ltInput)
            {
                if (string.IsNullOrEmpty(strNaicsCodeList))
                    strNaicsCodeList += "'" + s + "'";
                else
                    strNaicsCodeList += ", '" + s + "'";
            }
            string strQuery = string.Empty;
            strQuery = "SELECT DISTINCT naics_cd FROM arc_orgler_vws.orgler_naics_ref_cd ";

            if (!string.IsNullOrEmpty(strNaicsCodeList))
            {
                strQuery += " WHERE naics_cd IN (" + strNaicsCodeList + ");";
            }
            else
            {
                strQuery += " WHERE 1=0;";
            }

            output.strSPQuery = strQuery;
            return output;
        }

        //Validate Characteristic Types
        public static CrudOperationOutput getValidCharacteristicTypeSQL(List<string> ltInput)
        {
            CrudOperationOutput output = new CrudOperationOutput();

            string strCharacteristicTypeList = string.Empty;
            foreach(string s in ltInput)
            {
                if (string.IsNullOrEmpty(strCharacteristicTypeList))
                    strCharacteristicTypeList += "'" + s + "'";
                else
                    strCharacteristicTypeList += ", '" + s + "'";
            }

            string strQuery = string.Empty;
            strQuery = "SELECT DISTINCT cnst_chrctrstc_typ_cd FROM arc_mdm_vws.bz_cnst_chrctrstc_typ WHERE COALESCE(cnst_chrctrstc_typ_cnfdntl_ind,0) = 0 ";

            if (!string.IsNullOrEmpty(strCharacteristicTypeList))
            {
                strQuery += " AND cnst_chrctrstc_typ_cd IN (" + strCharacteristicTypeList + ");";
            }
            else
            {
                strQuery += " AND 1=0;";
            }

            output.strSPQuery = strQuery;
            return output;
        }

        //Validate Enterprise Name
        public static CrudOperationOutput getValidEnterpriseNamesSQL(List<string> ltInput)
        {
            CrudOperationOutput output = new CrudOperationOutput();

            string strEnterpriseList = string.Empty;
            foreach(string strNm in ltInput)
            {
                strEnterpriseList = string.IsNullOrEmpty(strEnterpriseList) ? "'" + strNm.Trim() + "'" :
                                    strEnterpriseList + ", '" + strNm.Trim() + "'";
            }
            
            string strQuery = string.Empty;
            strQuery = "SELECT DISTINCT ent_org_name FROM arc_mdm_vws.bz_cdim_enterprise_orgs ";

            if (!string.IsNullOrEmpty(strEnterpriseList))
            {
                strQuery += " WHERE ent_org_name IN (" + strEnterpriseList + ");";
            }
            else
            {
                strQuery += " WHERE 1=0;";
            }

            output.strSPQuery = strQuery;
            return output;
        }

        //Validate Tags
        public static CrudOperationOutput getValidTagsSQL(List<string> ltInput)
        {
            CrudOperationOutput output = new CrudOperationOutput();

            string strTagsList = string.Empty;
            foreach(string strTag in ltInput)
            {
                strTagsList = string.IsNullOrEmpty(strTagsList) ? "'" + strTag.Trim() + "'" :
                                    strTagsList + ", '" + strTag.Trim() + "'";
            }
            
            string strQuery = string.Empty;
            strQuery = "SELECT DISTINCT tag FROM arc_orgler_vws.tags ";

            if (!string.IsNullOrEmpty(strTagsList))
            {
                strQuery += " WHERE tag IN (" + strTagsList + ");";
            }
            else
            {
                strQuery += " WHERE 1=0;";
            }

            output.strSPQuery = strQuery;
            return output;
        }
    }
}
