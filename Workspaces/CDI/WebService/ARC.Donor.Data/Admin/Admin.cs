using ARC.Donor.Data.Entities;
using ARC.Donor.Data.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Admin
{
    public class Admin : Entities.QueryLogger
    {
        public IList<ARC.Donor.Data.Entities.Admin.Admin> getTabSecurityDetails(int NoOfRecs, int PageNum)
        {
            Repository rep = new Repository();
            string qry = SQL.Admin.Admin.getAdminTabSecuritySQL(NoOfRecs, PageNum);
            this._Query = qry;
            this._StartTime = DateTime.Now;
            var AcctLst = rep.ExecuteSqlQuery<Entities.Admin.Admin>(qry).ToList();
            this._EndTime = DateTime.Now;
            return AcctLst;
        }

        public ARC.Donor.Data.Entities.Admin.AdminTransOutput insertTabLevelSecurity(ARC.Donor.Data.Entities.Admin.Admin adminInput)
        {
            Repository rep = new Repository();
            AdminTransOutput adminOutput = new AdminTransOutput();
            string transOutput = "Failed while updating approve";

            try
            {
                CrudOperationOutput crudOutput;
                crudOutput = SQL.Admin.Admin.tabLevelSecurityProcParams(adminInput, "Insert");
                this._Query = crudOutput.strSPQuery;
                this._StartTime = DateTime.Now;
                var output = rep.ExecuteStoredProcedure<AdminTransOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
                this._EndTime = DateTime.Now;
                transOutput = output[0] != null ? output[0].o_transOutput : transOutput;
                adminOutput.o_transOutput = transOutput;
                return adminOutput;

            }
            catch (Exception ex)
            {
                adminOutput.o_transOutput = transOutput;
                return adminOutput;
            }

        }


        public ARC.Donor.Data.Entities.Admin.AdminTransOutput editTabLevelSecurity(ARC.Donor.Data.Entities.Admin.Admin adminInput)
        {
            Repository rep = new Repository();
            AdminTransOutput adminOutput = new AdminTransOutput();
            string transOutput = "Failed while updating approve";

            try
            {
                CrudOperationOutput crudOutput;
                crudOutput = SQL.Admin.Admin.tabLevelSecurityProcParams(adminInput, "Update");
                this._Query = crudOutput.strSPQuery;
                this._StartTime = DateTime.Now;
                var output = rep.ExecuteStoredProcedure<AdminTransOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
                this._EndTime = DateTime.Now;
                transOutput = output[0] != null ? output[0].o_transOutput : transOutput;
                adminOutput.o_transOutput = transOutput;
                return adminOutput;
               
            }
            catch (Exception ex)
            {
                adminOutput.o_transOutput = transOutput;
                return adminOutput;
            }
          
        }


        public ARC.Donor.Data.Entities.Admin.AdminTransOutput deleteTabLevelSecurity(ARC.Donor.Data.Entities.Admin.Admin adminInput)
        {
            Repository rep = new Repository();
            AdminTransOutput adminOutput = new AdminTransOutput();
            string transOutput = "Failed while removing approve";

            try
            {
                CrudOperationOutput crudOutput;
                crudOutput = SQL.Admin.Admin.tabLevelSecurityProcParams(adminInput, "Delete");
                this._Query = crudOutput.strSPQuery;
                this._StartTime = DateTime.Now;
                var output = rep.ExecuteStoredProcedure<AdminTransOutput>(crudOutput.strSPQuery, crudOutput.parameters).ToList();
                this._EndTime = DateTime.Now;
                transOutput = output[0] != null ? output[0].o_transOutput : transOutput;
                adminOutput.o_transOutput = transOutput;
                return adminOutput;

            }
            catch (Exception ex)
            {
                adminOutput.o_transOutput = transOutput;
                return adminOutput;
            }

        }

    }
}
