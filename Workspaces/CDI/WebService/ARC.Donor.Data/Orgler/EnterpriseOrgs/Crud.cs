using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ARC.Donor.Data;

namespace ARC.Donor.Data.Orgler.EnterpriseOrgs
{
    public class Crud
    {
        public IList<Entities.Orgler.EnterpriseOrgs.GetEnterpriseOrgModel> getEnterpriseOrg(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository("TDOrglerEF");
            var EntOrgLst = rep.ExecuteSqlQuery<Entities.Orgler.EnterpriseOrgs.GetEnterpriseOrgModel>(SQL.Orgler.EnterpriseOrgs.Crud.getEnterpriseOrgSQL(NoOfRecs, PageNum, id)).ToList();
            return EntOrgLst;
        }
        public IList<Entities.Orgler.EnterpriseOrgs.CreateEnterpriseOrgOutputModel> createEntOrg(ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.CreateEnterpriseOrgInputModel CreateEntOrgInput)
        {
            Repository rep = new Repository("TDOrglerEF");
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Orgler.EnterpriseOrgs.Crud.getCreateEnterpriseOrgParameters(CreateEntOrgInput, out strSPQuery, out listParam);
            var EntOrgLst = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.CreateEnterpriseOrgOutputModel>(strSPQuery, listParam).ToList();
            return EntOrgLst;
        }
        public IList<Entities.Orgler.EnterpriseOrgs.EditEnterpriseOrgOutputModel> updateEntOrg(ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.EditEnterpriseOrgInputModel EditEntOrgInput)
        {
            Repository rep = new Repository("TDOrglerEF");
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Orgler.EnterpriseOrgs.Crud.getUpdateEnterpriseOrgParameters(EditEntOrgInput, out strSPQuery, out listParam);
            var EntOrgLst = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.EditEnterpriseOrgOutputModel>(strSPQuery, listParam).ToList();
            return EntOrgLst;
        }
        public IList<Entities.Orgler.EnterpriseOrgs.DeleteEnterpriseOrgOutputModel> deleteEntOrg(ARC.Donor.Data.Entities.Orgler.EnterpriseOrgs.DeleteEnterpriseOrgInputModel DeleteEntOrgInput)
        {
            Repository rep = new Repository("TDOrglerEF");
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Orgler.EnterpriseOrgs.Crud.getDeleteEnterpriseOrgParameters(DeleteEntOrgInput, out strSPQuery, out listParam);
            var EntOrgLst = rep.ExecuteStoredProcedure<Entities.Orgler.EnterpriseOrgs.DeleteEnterpriseOrgOutputModel>(strSPQuery, listParam).ToList();
            return EntOrgLst;
        }
    }
}
           