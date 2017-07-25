using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orgler.Models.EnterpriseAccount
{
    public class GetEnterpriseOrgModel
    {
        public string ent_org_id { get; set; }
        public string ent_org_name { get; set; }
        public string ent_org_src_cd { get; set; }
        public string nk_ent_org_id { get; set; }
        public int trans_key { get; set; }
        public string created_by { get; set; }
        public string created_at { get; set; }
        public string last_modified_by { get; set; }
        public string last_modified_at { get; set; }
        public string last_modified_by_all { get; set; }
        public string last_modified_at_all { get; set; }
        public string last_reviewed_by { get; set; }
        public string last_reviewed_at { get; set; }
        public string data_stwrd_usr { get; set; }
        public string dw_srcsys_trans_ts { get; set; }
        public string row_stat_cd { get; set; }
        public string load_id { get; set; }
    }
    public class CreateEnterpriseOrgInputModel
    {

        public string user_id { get; set; }
        public string ent_org_name { get; set; }
        public string ent_org_src_cd { get; set; }
        public string nk_ent_org_id { get; set; }
        public string ent_org_dsc { get; set; }



        public CreateEnterpriseOrgInputModel()
        {
            user_id = string.Empty;
            ent_org_name = string.Empty;
            ent_org_src_cd = string.Empty;
            nk_ent_org_id = string.Empty;

        }
    }
    public class CreateEnterpriseOrgOutputModel
    {
        public string o_outputMessage { get; set; }
        public Int64 o_ent_org_id { get; set; }
    }
    public class EditEnterpriseOrgInputModel
    {
        public int ent_org_id { get; set; }
        public string ent_org_name { get; set; }
        public string ent_org_src_cd { get; set; }
        public string nk_ent_org_id { get; set; }
        public string user_id { get; set; }
        public string ent_org_dsc { get; set; }

    }
    public class EditEnterpriseOrgOutputModel
    {
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }
    }
    public class DeleteEnterpriseOrgInputModel
    {
        public int ent_org_id { get; set; }
        public string user_id { get; set; }
    }
    public class DeleteEnterpriseOrgOutputModel
    {
        public string o_outputMessage { get; set; }
        public string o_transaction_key { get; set; }
    }
}