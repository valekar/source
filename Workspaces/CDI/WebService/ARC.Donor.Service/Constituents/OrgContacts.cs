using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Constituents
{
  public  class OrgContacts
    {
        public IList<Business.Constituents.OrgContacts> getOrgContacts(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.OrgContacts gd = new Data.Constituents.OrgContacts();
            var ContactsLst = gd.getOrgContacts(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.OrgContacts, Business.Constituents.OrgContacts>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.OrgContacts>, IList<Business.Constituents.OrgContacts>>(ContactsLst);
            return result;
        }
    }
}
