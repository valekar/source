using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Constituents
{
    public class ConstituentMaster
    {
        public IList<Business.Constituents.ConstituentMaster> getConstituentMasterDetails(int NoOfRecs, int PageNum, string Master_Id)
        {
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentMaster, Business.Constituents.ConstituentMaster>();
            Data.Constituents.ConstituentMaster gd = new Data.Constituents.ConstituentMaster();
            var AcctLst = gd.getConstituentMasterDetails(NoOfRecs, PageNum, Master_Id);
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentMaster>, IList<Business.Constituents.ConstituentMaster>>(AcctLst);
            return result;
        }
    }
}
