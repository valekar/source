
using ARC.Donor.Data.Entities.Locator;
using ARC.Donor.Data.Entities.LocatorDomain;
using ARC.Donor.Data.Entities.LocatorAddress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teradata.Client.Provider;

namespace ARC.Donor.Data.SQL
{
    public class LocatorSQL
    {
        #region Locator Email
        public static string getLocatorEmailSearchSQL(ListlocatorEmailInputSearchModel listlocatorSearchModel)
        {
            String strUnionQuery = "";

            #region Locator Email
            foreach (LocatorEmailInputSearchModel dict in listlocatorSearchModel.LocatorEmailInputSearchModel)
            {
                String strQuery = " select cnst_email_addr, email_key, int_assessmnt_cd, ext_hygiene_result, code_category from dw_stuart_vws.bz_locator_email";
                if (dict.LocEmailId != null && !dict.LocEmailId.Equals(""))
                {

                    if (dict.ExactMatch == true)
                    {
                        strQuery += " WHERE cnst_email_addr = '" + dict.LocEmailId + "'";
                    }
                    else
                    {
                        strQuery += " WHERE cnst_email_addr LIKE '%" + dict.LocEmailId + "%'";
                    }

                }

                if (dict.LocEmailKey != null && !dict.LocEmailKey.Equals(""))
                {

                    if (strQuery.Contains("WHERE"))
                    {
                        strQuery += " AND email_key = '" + dict.LocEmailKey + "'";
                    }
                    else
                    {
                        strQuery += " WHERE email_key = '" + dict.LocEmailKey + "'";
                    }

                }
                if (dict.IntAssessCode != null && !dict.IntAssessCode.Equals(""))
                {
                    if (strQuery.Contains("WHERE"))
                    {
                        strQuery += " AND int_assessmnt_cd = '" + dict.IntAssessCode + "'";
                    }
                    else
                    {
                        strQuery += " WHERE int_assessmnt_cd = '" + dict.IntAssessCode + "'";
                    }
                }

                if (dict.ExtAssessCode != null)
                {
                    if (strQuery.Contains("WHERE"))
                    {
                        strQuery += " AND assessmnt_cd = '" + dict.ExtAssessCode + "'";
                    }
                    else
                    {
                        strQuery += " WHERE assessmnt_cd = '" + dict.ExtAssessCode + "'";
                    }
                }
                if (strUnionQuery == "")
                {
                    strUnionQuery = strQuery;
                }
                else
                {
                    strUnionQuery += " UNION " + strQuery;
                }
                if (dict.Type == "Details")
                {
                    strUnionQuery = "select* from dw_stuart_vws.bz_locator_email as A inner join (SELECT * FROM dw_stuart_vws.strx_locator_email_mstr WHERE email_key = " + dict.LocEmailKey.ToString() + " QUALIFY ROW_NUMBER() OVER(PARTITION BY constituent_id ORDER BY srcsys_trans_ts DESC) <= 1)  as b on a.email_key = b.email_key WHERE a.email_key = " + dict.LocEmailKey.ToString() + "";

                }

            }
            strUnionQuery = "select top 100 * from (" + strUnionQuery + ")A";
            #endregion

            #region Email Detailed View
            //foreach (LocatorEmailInputSearchModel dict in listlocatorSearchModel.LocatorEmailInputSearchModel)
            //{

           
            //    if (dict.Type == "Details")
            //    {
             //      strUnionQuery = "select* from dw_stuart_vws.bz_locator_email as A inner join (SELECT * FROM dw_stuart_vws.strx_locator_email_mstr WHERE email_key = " + dict.LocEmailKey.ToString() + " QUALIFY ROW_NUMBER() OVER(PARTITION BY constituent_id ORDER BY srcsys_trans_ts DESC) <= 1)  as b on a.email_key = b.email_key WHERE a.email_key = " + dict.LocEmailKey.ToString() + "";
                    
            //    }
            //}
            #endregion

            return strUnionQuery;
        }
        public static string getLocatorDeatilsByIDSQL(LocatorEmailInputSearchModel locatorSearchModel)
        {
            String strUnionQuery = "";

            if (locatorSearchModel.LocEmailId != null && !locatorSearchModel.LocEmailId.Equals(""))
            {
                strUnionQuery = "SELECT  top 100 * FROM dw_stuart_vws.bz_locator_email where cnst_email_addr LIKE '%" + locatorSearchModel.LocEmailId + "%'";

            }
            else
            {
                
                strUnionQuery = "select Distinct * from dw_stuart_vws.bz_locator_email where email_key = " + locatorSearchModel.LocEmailKey.ToString() + "";
            }

            return strUnionQuery;
        }

        public static string getLocatorConstDeatilsByIDSQL(LocatorEmailInputSearchModel locatorSearchModel)
        {
            String strUnionQuery = "";

            if (locatorSearchModel.LocEmailId != null && !locatorSearchModel.LocEmailId.Equals(""))
            {
                strUnionQuery = "SELECT  top 100 * FROM dw_stuart_vws.bz_locator_email where cnst_email_addr LIKE '%" + locatorSearchModel.LocEmailId + "%'";

            }
            else
            {

                strUnionQuery = "SELECT * FROM dw_stuart_vws.strx_locator_email_mstr WHERE email_key = " + locatorSearchModel.LocEmailKey.ToString() + " QUALIFY ROW_NUMBER() OVER(PARTITION BY constituent_id ORDER BY srcsys_trans_ts DESC) <= 1";
            }

            return strUnionQuery;
        }

        public static string getBridgeCount(string strResultEmailCSV)
        {
            string strMasterCountQuery = " select * from dw_stuart_vws.strx_cnst_email_cnt where cnst_email_addr in (" + "'" + strResultEmailCSV + "'" + ")";
            return strMasterCountQuery;
        }
        public static CreateLocatorEmailInput getWriteLocatorEmailParameters(ARC.Donor.Data.Entities.Locator.CreateLocatorEmailInput LocatorInput,out string strSPQuery, out List<object> parameters)
        {
            
            CreateLocatorEmailInput LocatorHelper = new CreateLocatorEmailInput();
            
            //if (!string.IsNullOrEmpty(LocatorInput.IntAssessCode.ToString()))            
            //    ConstHelper.IntAssessCode = LocatorInput.IntAssessCode;           
            LocatorHelper.IntAssessCode = "";

            if (!string.IsNullOrEmpty(LocatorInput.ExtAssessCode))
                LocatorHelper.ExtAssessCode = LocatorInput.ExtAssessCode;

            if (!string.IsNullOrEmpty(LocatorInput.LocEmailKey.ToString()))
                LocatorHelper.LocEmailKey = LocatorInput.LocEmailKey; 

            if (!string.IsNullOrEmpty(LocatorInput.LoggedInUser.ToString()))
                LocatorHelper.LoggedInUser = LocatorInput.LoggedInUser;

            int intNumberOfInputParameters = 4;
            List<string> listOutputParameters = new List<string> { "o_outputMessage" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_locator_email", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();

            ParamObjects.Add(SPHelper.createTdParameter("i_int_assessmnt_cd", LocatorHelper.IntAssessCode, "IN", TdType.VarChar, 30));
            ParamObjects.Add(SPHelper.createTdParameter("i_ext_assessmnt_cd", LocatorHelper.ExtAssessCode, "IN", TdType.VarChar, 30));
            ParamObjects.Add(SPHelper.createTdParameter("i_email_key", LocatorHelper.LocEmailKey, "IN", TdType.BigInt,250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", LocatorHelper.LoggedInUser, "IN", TdType.VarChar, 50));
            parameters = ParamObjects;

            return LocatorHelper;
        }


        #endregion

        #region Locator Domain
        public static string getEmailDomain_CorrectionDataSQL(ListLocatorDomainInputSearchModel locatorDomainSearchModel)
        {
            String strUnionQuery = "";

            string strQuery = "select  * from arc_mdm_vws.bzfc_email_domain_mapping";
            foreach (LocatorDomainInputSearchModel locatorDomain in locatorDomainSearchModel.LocatorDomainInputSearchModel)
            {
                if (locatorDomain.LocValidDomain != null)
                {
                    strQuery += " WHERE ( poss_domain_corrctn LIKE '%" + locatorDomain.LocValidDomain + "%')";
                }

                if (locatorDomain.LocInvalidDomain != null)
                {
                    if (strQuery.Contains("WHERE"))
                    {
                        strQuery += " AND domain_part LIKE '%" + locatorDomain.LocInvalidDomain + "%'";
                    }
                    else
                    {
                        strQuery += " WHERE domain_part LIKE '%" + locatorDomain.LocInvalidDomain + "%'";
                    }
                }
                if (locatorDomain.LocStatus != null)
                {
                    if (strQuery.Contains("WHERE"))
                    {
                        if (locatorDomain.LocStatus.ToString() != "All")
                        {
                            strQuery += " AND sts LIKE '%" + locatorDomain.LocStatus + "%'";
                        }
                    }
                    else
                    {
                        if (locatorDomain.LocStatus.ToString() != "All")
                        {
                            strQuery += " WHERE sts LIKE '%" + locatorDomain.LocStatus + "%'";
                        }

                    }
                    // if(dict["LocStatus"].ToString() == "All")
                    //{
                    //  strQuery = "select  * from arc_mdm_vws.bzfc_email_domain_mapping";
                    //}
                }

                //making sure that we get only 50 counts per select query of the Row
                if (strQuery.Contains("WHERE"))
                {
                    strQuery += " AND cnt>50 ";
                }
                else
                {
                    strQuery += " WHERE cnt>50 ";
                }
                // check if its empty
                if (strUnionQuery == "")
                {
                    strUnionQuery = strQuery;
                }
                else
                {
                    strUnionQuery += " UNION " + strQuery;
                }

            }
            strUnionQuery = "SELECT * FROM (" + strUnionQuery + ") A order by cnt desc;"; // select * FROM (select  * from arc_mdm_vws.bzfc_email_domain_mapping UNION select  * from arc_mdm_vws.bzfc_email_domain_mapping) A order by cnt desc;



            return strUnionQuery;
        }


        public static LocatorDomainInputSearchModel updateEmailDomainStatus(ListLocatorDomainInputSearchModel LocatorInput, out string strSPQuery, out List<object> parameters)
        {
            var email_domain_map_key = "";
            var status = "";
            var LoggedInUser = "";
            Dictionary<string, string> dictdomaindata = new Dictionary<string, string>();
            foreach (LocatorDomainInputSearchModel data in LocatorInput.LocatorDomainInputSearchModel)
            {
                email_domain_map_key += data.email_domain_map_key + ",";
                status = data.status;
                LoggedInUser = data.LoggedInUser;
            }
            string emailkey = email_domain_map_key.TrimEnd(',');
            //dictdomaindata.Add("email_domain_map_key", email_domain_map_key.TrimEnd(','));
            //dictdomaindata.Add("status", status);
            //dictdomaindata.Add("LoggedInUser", LoggedInUser);

            Dictionary<string, string> dictResults = new Dictionary<string, string>();
            LocatorDomainInputSearchModel LocatorHelper = new LocatorDomainInputSearchModel();


            if (emailkey != null)
            {
                LocatorHelper.email_domain_map_key = emailkey;
            }
            if (status != null)
            {
                LocatorHelper.status = status;
            }
            if (LoggedInUser != null)
            {
                LocatorHelper.LoggedInUser = LoggedInUser;
            }


            int intNumberOfInputParameters = 3;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "o_transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_locator_email_domain", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();

            ParamObjects.Add(SPHelper.createTdParameter("i_email_domain_map_keys", LocatorHelper.email_domain_map_key.ToString(), "IN", TdType.VarChar, 30));
            ParamObjects.Add(SPHelper.createTdParameter("i_sts", LocatorHelper.status, "IN", TdType.VarChar, 30));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", LocatorHelper.LoggedInUser, "IN", TdType.VarChar, 50));
            parameters = ParamObjects;

            return LocatorHelper;
        }



        #endregion

        #region Locator Address
        public static string getLocatorAddressDataSQL(ListLocatorAddressInputSearchModel listlocatorSearchModel)
        {
            String strUnionQuery = "";

          
            foreach (LocatorAddressInputSearchModel dict in listlocatorSearchModel.LocatorAddressInputSearchModel)
            {
                String strQuery = "SELECT  * FROM dw_stuart_vws.strx_locator_addr_srch";
                if (dict.LocAddressLine!=null)
                {
                    strQuery += " WHERE ( line1_addr LIKE '%" + dict.LocAddressLine + "%' OR line2_addr LIKE '%" + dict.LocAddressLine + "%' )";
                }
                if (dict.LocState!=null)
                {
                    if (strQuery.Contains("WHERE"))
                    {
                        strQuery += " AND state = '" + dict.LocState + "'";
                    }
                    else
                    {
                        strQuery += " WHERE state = '" + dict.LocState+ "'";
                    }
                }
                if (dict.LocCity!=null)
                {
                    if (strQuery.Contains("WHERE"))
                    {
                        strQuery += " AND city LIKE '%" + dict.LocCity + "%'";
                    }
                    else
                    {
                        strQuery += " WHERE city LIKE '%" + dict.LocCity + "%'";
                    }
                }
                if (dict.LocZip!=null)
                {
                    if (strQuery.Contains("WHERE"))
                    {
                        strQuery += " AND zip_5 = '" + dict.LocZip + "'";
                    }
                    else
                    {
                        strQuery += " WHERE zip_5 = '" + dict.LocZip+ "'";
                    }
                }
               
                if (dict.LocAddrKey!=null)
                {
                    if (strQuery.Contains("WHERE"))
                    {
                        strQuery += " AND locator_addr_key = '" + dict.LocAddrKey+ "'";
                    }
                    else
                    {
                        strQuery += " WHERE locator_addr_key = '" + dict.LocAddrKey+ "'";
                    }
                }

                /* if (dict.LocMailabilityFrom !=null)
                 {
                     if (strQuery.Contains("WHERE"))
                     {
                         strQuery += " AND mailability_score >= " + int.Parse(dict.LocMailabilityFrom);
                     }
                     else
                     {
                         strQuery += " WHERE mailability_score >= " + int.Parse(dict.LocMailabilityFrom);
                     }
                 }
                 if (dictLocMailabilityTo!=null)
                 {
                     if (strQuery.Contains("WHERE"))
                     {
                         strQuery += " AND mailability_score <= " + int.Parse(dict.LocMailabilityTo"]);
                     }
                     else
                     {
                         strQuery += " WHERE mailability_score <= " + int.Parse(dict.LocMailabilityTo"]);
                     }
                 }*/
                if (dict.LocDelType!=null)
                {
                    if (strQuery.Contains("WHERE"))
                    { 
                        strQuery += " AND deliv_loc_type = '" + dict.LocDelType + "'";
                    }
                    else
                    {
                        strQuery += " WHERE deliv_loc_type = '" + dict.LocDelType+ "'";
                    }
                }
                if (dict.LocDelCode!=null)
                {
                    if (strQuery.Contains("WHERE"))
                    {
                        strQuery += " AND dpv_cd = '" + dict.LocDelCode + "'";
                    }
                    else
                    {
                        strQuery += " WHERE dpv_cd = '" + dict.LocDelCode + "'";
                    }
                }
                if (dict.LocAssessCode!=null)
                {
                    if (strQuery.Contains("WHERE"))
                    {
                        strQuery += " AND assessmnt_cd = '" + dict.LocAssessCode+ "'";
                    }
                    else
                    {
                        strQuery += " WHERE assessmnt_cd = '" + dict.LocAssessCode+ "'";
                    }
                }
                if (strUnionQuery == "")
                {
                    strUnionQuery = strQuery;
                }
                else
                {
                    strUnionQuery += " UNION " + strQuery;
                }
            }

            strUnionQuery = "SELECT TOP 100 * FROM (" + strUnionQuery + ") A;";


            return strUnionQuery;
        }
        public static string getLocatorAddressDataSQLById(LocatorAddressInputSearchModel locatorAddressSearchModel)
        {
            string strQuery = "SELECT * FROM dw_stuart_vws.bz_locator_addr WHERE locator_addr_key =" + locatorAddressSearchModel.LocAddrKey +"";
            return strQuery;
        }
        public static string getLocatorAddressConstituentsDataSQLById(LocatorAddressInputSearchModel locatoraddressSearchModel)
        {
            string strQuery = "SELECT * FROM dw_stuart_vws.strx_locator_addr_mstr  WHERE locator_addr_key =" + locatoraddressSearchModel.LocAddrKey + "";
            
            return strQuery;
        }
        public static string getLocatorAddressAssessmentDataSQLById(LocatorAddressInputSearchModel locatorAddressSearchModel)
        {
            string strQuery = "SELECT * FROM  dw_stuart_vws.bz_assessmnt_addr_int  WHERE locator_addr_key =" + locatorAddressSearchModel.LocAddrKey + "";
            return strQuery;
        }
        public static string getLocatorAddress(LocatorAddressInputSearchModel locatorDomainSearchModel)
        {
            string strQuery = "SELECT * FROM	dw_stuart_vws.bz_assessmnt_addr_int WHERE	locator_addr_key =" + locatorDomainSearchModel.LocAddrKey + "";

            return strQuery;
        }
        public static CreateLocatorAddressInput getWriteLocatorAddressParameters(ARC.Donor.Data.Entities.LocatorAddress.CreateLocatorAddressInput LocatorInput, out string strSPQuery, out List<object> parameters)
        {
            
            CreateLocatorAddressInput LocatorHelper = new CreateLocatorAddressInput();


            //if (!string.IsNullOrEmpty(LocatorInput.IntAssessCode.ToString()))            
            //    ConstHelper.IntAssessCode = LocatorInput.IntAssessCode;           
           //     LocatorHelper.IntAssessCode = "";

            if (!string.IsNullOrEmpty(LocatorInput.LocAssessCode))
                LocatorHelper.LocAssessCode = LocatorInput.LocAssessCode;

            if (!string.IsNullOrEmpty(LocatorInput.LocAddrKey.ToString()))
                LocatorHelper.LocAddrKey = LocatorInput.LocAddrKey;

            if (!string.IsNullOrEmpty(LocatorInput.LoggedInUser.ToString()))
                LocatorHelper.LoggedInUser = LocatorInput.LoggedInUser;

            int intNumberOfInputParameters = 3;
            List<string> listOutputParameters = new List<string> { "o_outputMessage", "o_transaction_key" };

            strSPQuery = SPHelper.createSPQuery("dw_stuart_macs.strx_locator_addr_upd", intNumberOfInputParameters, listOutputParameters);

            var ParamObjects = new List<object>();

            ParamObjects.Add(SPHelper.createTdParameter("i_assessmnt_cd", LocatorHelper.LocAssessCode, "IN", TdType.VarChar, 30));
            ParamObjects.Add(SPHelper.createTdParameter("i_locator_addr_key", LocatorHelper.LocAddrKey, "IN",TdType.BigInt, 250));
            ParamObjects.Add(SPHelper.createTdParameter("i_usr_nm", LocatorHelper.LoggedInUser, "IN", TdType.VarChar, 50));
            //ParamObjects.Add(SPHelper.createTdParameter("o_outputMessage", LocatorHelper.o_outputMessage, "OUT", TdType.VarChar, 1200));

            

            parameters = ParamObjects;

            return LocatorHelper;
        }

        #endregion

    }
}