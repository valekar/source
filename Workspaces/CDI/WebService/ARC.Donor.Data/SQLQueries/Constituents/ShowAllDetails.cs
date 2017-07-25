using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.SQL.Constituents
{
    public class ShowAllDetails
    {
        /* Method to get the frame the query to select all the columns from the respective details table for the inputted master id
         * Input Parameters : ShowDetailsInput object
         * Output Parameter : Output the query
         */
        public static string getAllDetailsSQL(ARC.Donor.Data.Entities.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            string strViewName = string.Empty;
            string strQuery = "select * from ";
            if (ShowDetailsInput.DetailsType.ToLower() == "cdi")
            {
                strViewName = getCDITableName(ShowDetailsInput.DetailsName);
                strQuery += " arc_mdm_vws." + strViewName;
                strQuery += " where cnst_mstr_id = \'{0}\' ";

                if (strViewName.ToLower() == "mstr_metric")
                {
                    strQuery = " SELECT * FROM dw_stuart_vws.strx_mstr_metric strx_mstr_metric WHERE cnst_mstr_id = \'{0}\' ORDER BY load_id ; ";
                }
            }
            else if (ShowDetailsInput.DetailsType.ToLower() == "fsa")
            {
                strViewName = getFSATableName(ShowDetailsInput.DetailsName);
                strQuery += " ddcoe_vws." + strViewName;
                strQuery += " where cnst_fsa_key = \'{0}\' ";
                
                if (ShowDetailsInput.DetailsName.ToLower() == "fsarelationship")
                {
                    strQuery = " select * from  ddcoe_vws.bzal_cnst_fsa_rlshp where (superior_cnst_key, subord_cnst_key) in ( select superior_cnst_key,subord_cnst_key  from dw_stuart_vws.strx_cnst_dtl_fsa_rlshp where (superior_cnst_mstr_id = \'{0}\' OR subord_cnst_mstr_id = \'{0}\' )); ";
                }
            }
            return String.Format(strQuery, ShowDetailsInput.ConstituentId);
        }

        /* Method to get the view name for the respective CDI details section
         * Input Parameters : Master detail type
         * Output Parameter : View name
         */
        public static string getCDITableName(string strDetail)
        {
            string strViewName = string.Empty;
            switch (strDetail.ToLower())
            {
                case "address": strViewName = "bzal_cnst_addr"; break;
                case "phone": strViewName = "bzal_cnst_phn_typ2"; break;
                case "email": strViewName = "bzal_cnst_email_typ2"; break;
                case "externalbridge": strViewName = "bzal_cnst_mstr_external_brid"; break;
                case "internalbridge": strViewName = "bzal_cnst_mstr_bridge"; break;
                case "namein": strViewName = "bzal_cnst_prsn_nm"; break;
                case "nameor": strViewName = "bzal_cnst_org_nm"; break;
                case "contactpreference": strViewName = "bzal_cnst_cntct_prefc"; break;
                case "characteristics": strViewName = "bzal_cnst_chrctrstc"; break;
                case "birth": strViewName = "bzal_cnst_birth"; break;
                case "death": strViewName = "bzal_cnst_death"; break;
                case "metric": strViewName = "mstr_metric"; break;
            }
            return strViewName;
        }

        /* Method to get the view name for the respective FSA details section
         * Input Parameters : Master detail type
         * Output Parameter : View name
         */
        public static string getFSATableName(string strDetail)
        {
            string strViewName = string.Empty;
            switch (strDetail.ToLower())
            {
                case "address": strViewName = "bzal_cnst_fsa_addr"; break;
                case "phone": strViewName = "bzal_cnst_fsa_phn"; break;
                case "email": strViewName = "bzal_cnst_fsa_email"; break;
                case "name": strViewName = "bzal_cnst_lifecycle"; break;
                case "fsarelationship": strViewName = "bzal_cnst_fsa_rlshp"; break;
            }
            return strViewName;
        }
    }

}
