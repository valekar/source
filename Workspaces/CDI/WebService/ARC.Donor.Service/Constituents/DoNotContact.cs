using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Constituents
{
    public class DoNotContact
    {
        public IList<Business.Constituents.DoNotContact> getDoNotContact(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.DoNotContact gd = new Data.Constituents.DoNotContact();
            var AcctLst = gd.getDoNotContact(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.DoNotContact, Business.Constituents.DoNotContact>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.DoNotContact>, IList<Business.Constituents.DoNotContact>>(AcctLst);
            return result;
        }

        public IList<Business.Constituents.AllDoNotContact> getAllDoNotContacts(int NoOfRecs, int PageNum, string Master_Id)
        {
            Data.Constituents.DoNotContact gd = new Data.Constituents.DoNotContact();
            var AcctLst = gd.getAllDoNotContacts(NoOfRecs, PageNum, Master_Id);
            Mapper.CreateMap<Data.Entities.Constituents.AllDoNotContact, Business.Constituents.AllDoNotContact>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.AllDoNotContact>, IList<Business.Constituents.AllDoNotContact>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.DoNotContactOutput> addDoNotContact(ARC.Donor.Business.Constituents.DoNotContactInput dncInput)
        {
            Mapper.CreateMap<Business.Constituents.DoNotContactInput, Data.Entities.Constituents.DoNotContactInput>();
            var Input = Mapper.Map<Business.Constituents.DoNotContactInput, Data.Entities.Constituents.DoNotContactInput>(dncInput);
            Data.Constituents.DoNotContact gd = new Data.Constituents.DoNotContact();
            var AcctLst = gd.addDoNotContact(Input);
            Mapper.CreateMap<Data.Entities.Constituents.DoNotContactOutput, Business.Constituents.DoNotContactOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.DoNotContactOutput>, IList<Business.Constituents.DoNotContactOutput>>(AcctLst);
            return result;
        }


        public IList<ARC.Donor.Business.Constituents.DoNotContactOutput> editDoNotContact(ARC.Donor.Business.Constituents.DoNotContactInput dncInput)
        {
            Mapper.CreateMap<Business.Constituents.DoNotContactInput, Data.Entities.Constituents.DoNotContactInput>();
            var Input = Mapper.Map<Business.Constituents.DoNotContactInput, Data.Entities.Constituents.DoNotContactInput>(dncInput);
            Data.Constituents.DoNotContact gd = new Data.Constituents.DoNotContact();
            var AcctLst = gd.editDoNotContact(Input);
            Mapper.CreateMap<Data.Entities.Constituents.DoNotContactOutput, Business.Constituents.DoNotContactOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.DoNotContactOutput>, IList<Business.Constituents.DoNotContactOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.DoNotContactOutput> deleteDoNotContact(ARC.Donor.Business.Constituents.DoNotContactInput dncInput)
        {
            Mapper.CreateMap<Business.Constituents.DoNotContactInput, Data.Entities.Constituents.DoNotContactInput>();
            var Input = Mapper.Map<Business.Constituents.DoNotContactInput, Data.Entities.Constituents.DoNotContactInput>(dncInput);
            Data.Constituents.DoNotContact gd = new Data.Constituents.DoNotContact();
            var AcctLst = gd.deleteDoNotContact(Input);
            Mapper.CreateMap<Data.Entities.Constituents.DoNotContactOutput, Business.Constituents.DoNotContactOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.DoNotContactOutput>, IList<Business.Constituents.DoNotContactOutput>>(AcctLst);
            return result;
        }
    }
}
